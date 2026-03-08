using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domains.Entities;
using Repositories.Interfaces;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [ApiController]
    [Route("api/admin/restaurants")]
    public class RestaurantAdminController : ControllerBase
    {
        private readonly IBaseRepository<Restaurant> _restaurantRepository;
        private readonly IUnitOfWork _uow;

        public RestaurantAdminController(IBaseRepository<Restaurant> restaurantRepository, IUnitOfWork uow)
        {
            _restaurantRepository = restaurantRepository;
            _uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null) return NotFound();
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantRequest request)
        {
            var existing = await _restaurantRepository.FindAsync(r => r.CNPJ == request.CNPJ);
            if (System.Linq.Enumerable.Any(existing))
            {
                return BadRequest(new { message = "CNPJ já cadastrado." });
            }

            var restaurant = new Restaurant
            {
                CommercialName = request.CommercialName,
                LegalName = request.LegalName,
                CNPJ = request.CNPJ,
                Description = request.Description,
                Phone = request.Phone,
                OwnerId = request.OwnerId,
                AddressId = request.AddressId,
                Status = "Pending"
            };

            await _restaurantRepository.AddAsync(restaurant);
            await _uow.CommitAsync();

            return CreatedAtAction(nameof(GetById), new { id = restaurant.Id }, restaurant);
        }

        [HttpPatch("{id}/approve")]
        public async Task<IActionResult> Approve(Guid id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null) return NotFound();

            restaurant.Status = "Active";
            restaurant.ApprovedAt = DateTime.UtcNow;

            await _restaurantRepository.UpdateAsync(restaurant);
            await _uow.CommitAsync();

            return NoContent();
        }

        public class CreateRestaurantRequest
        {
            public string CommercialName { get; set; } = string.Empty;
            public string LegalName { get; set; } = string.Empty;
            public string CNPJ { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public Guid OwnerId { get; set; }
            public Guid AddressId { get; set; }
        }
    }
}
