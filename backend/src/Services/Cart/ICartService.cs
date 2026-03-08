using Dtos.Cart;
using System;
using System.Threading.Tasks;

namespace Services.Cart
{
    public interface ICartService
    {
        Task<CartDto?> GetCartAsync(Guid userId);
        Task<CartDto> AddItemAsync(Guid userId, CartItemDto item, Guid restaurantId, string restaurantName);
        Task<CartDto> RemoveItemAsync(Guid userId, Guid productId);
        Task<bool> ClearCartAsync(Guid userId);
    }
}
