using Domains.Entities;
using Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Promotions
{
    public interface ICouponValidator
    {
        Task<(bool IsValid, string Message, decimal DiscountAmount)> ValidateAsync(string code, Guid userId, decimal orderAmount, Guid restaurantId);
    }

    public class CouponValidator : ICouponValidator
    {
        private readonly IBaseRepository<Coupon> _couponRepository;
        private readonly IBaseRepository<Order> _orderRepository;

        public CouponValidator(IBaseRepository<Coupon> couponRepository, IBaseRepository<Order> orderRepository)
        {
            _couponRepository = couponRepository;
            _orderRepository = orderRepository;
        }

        public async Task<(bool IsValid, string Message, decimal DiscountAmount)> ValidateAsync(string code, Guid userId, decimal orderAmount, Guid restaurantId)
        {
            var coupon = (await _couponRepository.FindAsync(c => c.Code == code && c.IsActive)).FirstOrDefault();

            if (coupon == null) return (false, "Cupom inválido ou inativo.", 0);

            if (DateTime.UtcNow < coupon.StartDate || DateTime.UtcNow > coupon.EndDate)
                return (false, "Cupom fora do prazo de validade.", 0);

            if (coupon.CurrentUsage >= coupon.MaxUsage)
                return (false, "Cupom esgotado.", 0);

            if (coupon.MinOrderValue.HasValue && orderAmount < coupon.MinOrderValue.Value)
                return (false, $"O valor mínimo para este cupom é {coupon.MinOrderValue:C}.", 0);

            if (coupon.RestaurantId.HasValue && coupon.RestaurantId.Value != restaurantId)
                return (false, "Este cupom não é válido para este restaurante.", 0);

            if (coupon.IsFirstOrderOnly)
            {
                var userOrders = await _orderRepository.FindAsync(o => o.UserId == userId);
                if (userOrders.Any())
                    return (false, "Este cupom é exclusivo para a primeira compra.", 0);
            }

            decimal discount = 0;
            if (coupon.DiscountType == Domains.Enums.DiscountType.Percentage)
            {
                discount = orderAmount * (coupon.DiscountValue / 100m);
                if (coupon.MaxDiscountValue.HasValue && discount > coupon.MaxDiscountValue.Value)
                    discount = coupon.MaxDiscountValue.Value;
            }
            else
            {
                discount = coupon.DiscountValue;
            }

            // Discount cannot be greater than order amount
            if (discount > orderAmount) discount = orderAmount;

            return (true, "Cupom aplicado!", discount);
        }
    }
}
