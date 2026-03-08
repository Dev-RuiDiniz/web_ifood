using Dtos.Cart;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Cart
{
    public class CartService : ICartService
    {
        private readonly IDatabase _redis;
        private const string CartKeyPrefix = "cart:";

        public CartService(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }

        public async Task<CartDto?> GetCartAsync(Guid userId)
        {
            var data = await _redis.StringGetAsync($"{CartKeyPrefix}{userId}");
            if (data.IsNullOrEmpty) return null;

            return JsonSerializer.Deserialize<CartDto>(data!);
        }

        public async Task<CartDto> AddItemAsync(Guid userId, CartItemDto item, Guid restaurantId, string restaurantName)
        {
            var cart = await GetCartAsync(userId);

            if (cart == null || cart.RestaurantId != restaurantId)
            {
                // Reset cart if it's from a different restaurant
                cart = new CartDto
                {
                    UserId = userId,
                    RestaurantId = restaurantId,
                    RestaurantName = restaurantName,
                    Items = new List<CartItemDto>()
                };
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                existingItem.Observation = item.Observation;
            }
            else
            {
                cart.Items.Add(item);
            }

            cart.TotalAmount = cart.Items.Sum(i => i.SubTotal);

            await SaveCartAsync(cart);
            return cart;
        }

        public async Task<CartDto> RemoveItemAsync(Guid userId, Guid productId)
        {
            var cart = await GetCartAsync(userId);
            if (cart == null) return new CartDto { UserId = userId };

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Items.Remove(item);
                cart.TotalAmount = cart.Items.Sum(i => i.SubTotal);
                await SaveCartAsync(cart);
            }

            return cart;
        }

        public async Task<bool> ClearCartAsync(Guid userId)
        {
            return await _redis.KeyDeleteAsync($"{CartKeyPrefix}{userId}");
        }

        private async Task SaveCartAsync(CartDto cart)
        {
            var data = JsonSerializer.Serialize(cart);
            await _redis.StringSetAsync($"{CartKeyPrefix}{cart.UserId}", data, TimeSpan.FromDays(7));
        }
    }
}
