using Domains.Entities;
using Domains.Enums;
using Repositories.Interfaces;
using Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace Services.Finance
{
    public interface IBalanceService
    {
        Task ProcessOrderFinancialsAsync(Order order);
        Task ProcessRefundFinancialsAsync(Order order);
    }

    public class BalanceService : IBalanceService
    {
        private readonly IBaseRepository<Statement> _statementRepository;
        private readonly IBaseRepository<Restaurant> _restaurantRepository;
        private readonly IBaseRepository<Courier> _courierRepository;

        public BalanceService(
            IBaseRepository<Statement> statementRepository,
            IBaseRepository<Restaurant> restaurantRepository,
            IBaseRepository<Courier> courierRepository)
        {
            _statementRepository = statementRepository;
            _restaurantRepository = restaurantRepository;
            _courierRepository = courierRepository;
        }

        public async Task ProcessOrderFinancialsAsync(Order order)
        {
            // 1. Credit to Restaurant (Items Total)
            var itemsTotal = order.TotalAmount - order.DeliveryFee;
            await _statementRepository.AddAsync(new Statement
            {
                OrderId = order.Id,
                WalletId = order.Restaurant.OwnerId,
                Amount = itemsTotal,
                Type = StatementType.OrderCredit.ToString(),
                Description = $"Crédito de venda de produtos do pedido #{order.OrderNumber}"
            });

            // 2. Commission Debit (from Restaurant)
            var restaurant = await _restaurantRepository.GetByIdAsync(order.RestaurantId);
            if (restaurant != null)
            {
                var commissionRate = restaurant.CommissionPercentage / 100m;
                var commissionAmount = itemsTotal * commissionRate;
                
                await _statementRepository.AddAsync(new Statement
                {
                    OrderId = order.Id,
                    WalletId = order.Restaurant.OwnerId,
                    Amount = -commissionAmount, // Debit
                    Type = StatementType.CommissionDebit.ToString(),
                    Description = $"Comissão da plataforma ({restaurant.CommissionPercentage}%) do pedido #{order.OrderNumber}"
                });
            }

            // 3. Credit to Courier (Delivery Fee)
            if (order.CourierId.HasValue)
            {
                var courier = await _courierRepository.GetByIdAsync(order.CourierId.Value);
                if (courier != null)
                {
                    await _statementRepository.AddAsync(new Statement
                    {
                        OrderId = order.Id,
                        WalletId = courier.UserId,
                        Amount = order.DeliveryFee,
                        Type = StatementType.DeliveryFeeCredit.ToString(),
                        Description = $"Taxa de entrega do pedido #{order.OrderNumber}"
                    });
                }
            }
        }

        public async Task ProcessRefundFinancialsAsync(Order order)
        {
            // This would create reversal entries if needed, or simply record the Payout subtraction
            // For now, let's keep it simple as the Sprint focuses on Payouts calculation
        }
    }
}
