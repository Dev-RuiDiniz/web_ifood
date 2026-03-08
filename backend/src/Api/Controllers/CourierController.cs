using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domains.Entities;
using Repositories.Interfaces;
using Data.Interfaces;
using Services.RealTime;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CourierController : ControllerBase
    {
        private readonly IBaseRepository<Courier> _courierRepository;
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IUnitOfWork _uow;
        private readonly ICourierGeoService _geoService;
        private readonly INotificationService _notificationService;

        public CourierController(
            IBaseRepository<Courier> courierRepository, 
            IBaseRepository<Order> orderRepository, 
            IUnitOfWork uow, 
            ICourierGeoService geoService,
            INotificationService notificationService)
        {
            _courierRepository = courierRepository;
            _orderRepository = orderRepository;
            _uow = uow;
            _geoService = geoService;
            _notificationService = notificationService;
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userId!);
        }

        [HttpPost("onboarding")]
        public async Task<IActionResult> Register([FromBody] CourierRegisterRequest request)
        {
            var existing = await _courierRepository.FindAsync(c => c.UserId == GetUserId());
            if (existing.Any()) return BadRequest(new { message = "Você já possui uma conta de entregador ou solicitação em andamento." });

            var courier = new Courier
            {
                UserId = GetUserId(),
                VehicleType = request.VehicleType,
                LicenseNumber = request.LicenseNumber,
                VehiclePlate = request.VehiclePlate,
                DocumentUrl = request.DocumentUrl,
                Status = "AwaitingApproval"
            };

            await _courierRepository.AddAsync(courier);
            await _uow.CommitAsync();

            return Ok(new { message = "Solicitação enviada com sucesso. Aguarde a aprovação administrativa." });
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPatch("{id}/approve")]
        public async Task<IActionResult> Approve(Guid id)
        {
            var courier = await _courierRepository.GetByIdAsync(id);
            if (courier == null) return NotFound();

            courier.Status = "Active";
            courier.ApprovedAt = DateTime.UtcNow;

            await _courierRepository.UpdateAsync(courier);
            await _uow.CommitAsync();

            return Ok(new { message = "Entregador aprovado com sucesso." });
        }

        [HttpPost("{orderId}/accept")]
        public async Task<IActionResult> AcceptOrder(Guid orderId)
        {
            var courier = (await _courierRepository.FindAsync(c => c.UserId == GetUserId())).FirstOrDefault();
            if (courier == null || courier.Status != "Active") return Forbid();

            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null || order.Status != "Ready") return BadRequest(new { message = "Pedido não disponível para entrega." });

            order.CourierId = courier.Id;
            order.Status = "InTransit";

            await _orderRepository.UpdateAsync(order);
            await _uow.CommitAsync();

            return Ok(new { message = "Corrida aceita! Vá até o restaurante buscar o pedido." });
        }

        [HttpPut("location")]
        public async Task<IActionResult> UpdateLocation([FromBody] LocationUpdateRequest request)
        {
            var userId = GetUserId();
            var courier = (await _courierRepository.FindAsync(c => c.UserId == userId)).FirstOrDefault();
            if (courier == null || courier.Status != "Active") return Forbid();

            courier.CurrentLatitude = request.Latitude;
            courier.CurrentLongitude = request.Longitude;
            await _courierRepository.UpdateAsync(courier);
            await _uow.CommitAsync();

            await _geoService.UpdateLocationAsync(courier.Id, request.Latitude, request.Longitude);

            var activeOrders = await _orderRepository.FindAsync(o => o.CourierId == courier.Id && o.Status == "InTransit");
            foreach (var order in activeOrders)
            {
                await _notificationService.NotifyUserAsync(
                    order.UserId.ToString(), 
                    "UpdateLocation", 
                    new { orderId = order.Id, lat = request.Latitude, lng = request.Longitude }
                );
            }

            return NoContent();
        }

        public class CourierRegisterRequest
        {
            public string VehicleType { get; set; } = string.Empty;
            public string LicenseNumber { get; set; } = string.Empty;
            public string VehiclePlate { get; set; } = string.Empty;
            public string? DocumentUrl { get; set; }
        }

        public class LocationUpdateRequest
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }
}
