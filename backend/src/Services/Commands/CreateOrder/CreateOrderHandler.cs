using MediatR;
using Domains.Entities;
using Repositories.Interfaces;
using Data.Interfaces;
using Services.Cart;
using Services.Delivery;
using Services.Promotions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid?>
    {
        private readonly ICartService _cartService;
        private readonly IDeliveryCalculator _deliveryCalculator;
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<Restaurant> _restaurantRepository;
        private readonly IBaseRepository<Coupon> _couponRepository;
        private readonly ICouponValidator _couponValidator;
        private readonly IUnitOfWork _uow;

        public CreateOrderHandler(
            ICartService cartService, 
            IDeliveryCalculator deliveryCalculator, 
            IBaseRepository<Order> orderRepository,
            IBaseRepository<Restaurant> restaurantRepository,
            IBaseRepository<Coupon> couponRepository,
            ICouponValidator couponValidator,
            IUnitOfWork uow)
        {
            _cartService = cartService;
            _deliveryCalculator = deliveryCalculator;
            _orderRepository = orderRepository;
            _restaurantRepository = restaurantRepository;
            _couponRepository = couponRepository;
            _couponValidator = couponValidator;
            _uow = uow;
        }

        public async Task<Guid?> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetCartAsync(request.UserId);
            if (cart == null || !cart.Items.Any()) return null;

            var restaurant = await _restaurantRepository.GetByIdAsync(cart.RestaurantId);
            if (restaurant == null || !restaurant.IsOpen) return null;

            var deliveryFee = await _deliveryCalculator.CalculateFeeAsync(cart.RestaurantId, request.AddressId, cart.TotalAmount);

            decimal discountAmount = 0;
            Guid? couponId = null;

            if (!string.IsNullOrEmpty(request.CouponCode))
            {
                var validationResult = await _couponValidator.ValidateAsync(request.CouponCode, request.UserId, cart.TotalAmount, cart.RestaurantId);
                if (validationResult.IsValid)
                {
                    discountAmount = validationResult.DiscountAmount;
                    var coupon = (await _couponRepository.FindAsync(c => c.Code == request.CouponCode)).FirstOrDefault();
                    couponId = coupon?.Id;
                }
            }

            var order = new Order
            {
                UserId = request.UserId,
                RestaurantId = cart.RestaurantId,
                DeliveryAddressId = request.AddressId,
                SubTotal = cart.TotalAmount,
                DeliveryFee = deliveryFee,
                DiscountAmount = discountAmount,
                CouponId = couponId,
                TotalAmount = cart.TotalAmount + deliveryFee - discountAmount,
                Status = "Created",
                OrderNumber = GenerateOrderNumber(),
                Items = cart.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Observation = i.Observation
                }).ToList()
            };

            await _orderRepository.AddAsync(order);
            
            if (await _uow.CommitAsync())
            {
                await _cartService.ClearCartAsync(request.UserId);
                return order.Id;
            }

            return null;
        }

        private string GenerateOrderNumber()
        {
            return "#" + new Random().Next(1000, 9999).ToString();
        }
    }
}
