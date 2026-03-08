using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Services.Payments;
using Repositories.Interfaces;
using Domains.Entities;
using Services.Commands.CancelOrder;
using Services.Commands.CreateOrder;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPaymentProvider _paymentProvider;
        private readonly IBaseRepository<Order> _orderRepository;

        public OrderController(IMediator mediator, IPaymentProvider paymentProvider, IBaseRepository<Order> orderRepository)
        {
            _mediator = mediator;
            _paymentProvider = paymentProvider;
            _orderRepository = orderRepository;
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userId!);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null || order.UserId != GetUserId()) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var command = new CreateOrderCommand
            {
                UserId = GetUserId(),
                AddressId = request.AddressId,
                CouponCode = request.CouponCode
            };

            var orderId = await _mediator.Send(command);
            
            if (orderId == null)
            {
                return BadRequest(new { message = "Não foi possível criar o pedido." });
            }

            return Ok(new { orderId });
        }

        [HttpPost("{id}/pay/pix")]
        public async Task<IActionResult> PayWithPix(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null || order.UserId != GetUserId()) return NotFound();

            if (order.Status != "Created" && order.Status != "PendingPayment")
            {
                return BadRequest(new { message = "Este pedido não está aguardando pagamento." });
            }

            var pixResponse = await _paymentProvider.GeneratePixAsync(order);
            return Ok(pixResponse);
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id, [FromBody] CancelRequest request)
        {
            var command = new CancelOrderCommand
            {
                OrderId = id,
                RequesterId = GetUserId(),
                Reason = request.Reason
            };

            var success = await _mediator.Send(command);
            if (!success) return BadRequest(new { message = "Não foi possível cancelar o pedido no estágio atual." });
            
            return NoContent();
        }

        public class CancelRequest
        {
            public string Reason { get; set; } = string.Empty;
        }

        public class CreateOrderRequest
        {
            public Guid AddressId { get; set; }
            public string? CouponCode { get; set; }
        }
    }
}
