using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domains.Entities;
using Repositories.Interfaces;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize(Policy = "StoreOwnerOnly")]
    [ApiController]
    [Route("api/store/menu")]
    public class StoreMenuController : ControllerBase
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<Restaurant> _restaurantRepository;
        private readonly IUnitOfWork _uow;

        public StoreMenuController(
            IBaseRepository<Product> productRepository, 
            IBaseRepository<Restaurant> restaurantRepository, 
            IUnitOfWork uow)
        {
            _productRepository = productRepository;
            _restaurantRepository = restaurantRepository;
            _uow = uow;
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userId!);
        }

        private async Task<Restaurant?> GetUserRestaurantAsync()
        {
            var userId = GetUserId();
            var restaurants = await _restaurantRepository.FindAsync(r => r.OwnerId == userId);
            return restaurants.FirstOrDefault();
        }

        [HttpGet("{restaurantId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMenu(Guid restaurantId)
        {
            var products = await _productRepository.FindAsync(p => p.RestaurantId == restaurantId && p.IsActive);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductRequest request)
        {
            var restaurant = await GetUserRestaurantAsync();
            if (restaurant == null) return Forbidden();

            if (request.DiscountPrice.HasValue && request.DiscountPrice > request.Price)
            {
                return BadRequest(new { message = "Preço com desconto não pode ser maior que o preço original." });
            }

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                DiscountPrice = request.DiscountPrice,
                Category = request.Category,
                ImageUrl = request.ImageUrl,
                RestaurantId = restaurant.Id
            };

            await _productRepository.AddAsync(product);
            await _uow.CommitAsync();

            return CreatedAtAction(nameof(GetMenu), new { restaurantId = restaurant.Id }, product);
        }

        [HttpPatch("{id}/availability")]
        public async Task<IActionResult> ToggleAvailability(Guid id)
        {
            var restaurant = await GetUserRestaurantAsync();
            if (restaurant == null) return Forbidden();

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null || product.RestaurantId != restaurant.Id) return NotFound();

            product.IsAvailable = !product.IsAvailable;
            await _productRepository.UpdateAsync(product);
            await _uow.CommitAsync();

            return Ok(new { isAvailable = product.IsAvailable });
        }

        private IActionResult Forbidden() => StatusCode(Constants.FORBIDDEN);

        public class CreateProductRequest
        {
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public decimal? DiscountPrice { get; set; }
            public string? Category { get; set; }
            public string? ImageUrl { get; set; }
        }

        private static class Constants
        {
            public const int FORBIDDEN = 403;
        }
    }
}
