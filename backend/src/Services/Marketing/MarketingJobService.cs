using Domains.Entities;
using Repositories.Interfaces;
using Services.RealTime;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Marketing
{
    public interface IMarketingJobService
    {
        Task ProcessInactiveUsersCampaignAsync();
    }

    public class MarketingJobService : IMarketingJobService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly INotificationService _notificationService;

        public MarketingJobService(
            IBaseRepository<User> userRepository, 
            IBaseRepository<Order> orderRepository, 
            INotificationService notificationService)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _notificationService = notificationService;
        }

        public async Task ProcessInactiveUsersCampaignAsync()
        {
            var sevenDaysAgo = DateTime.UtcNow.AddDays(-7);

            // Find users whose last order was more than 7 days ago
            // For a real production app, we would use a more optimized SQL query
            var allUsers = await _userRepository.GetAllAsync();
            var allOrders = await _orderRepository.GetAllAsync();

            foreach (var user in allUsers)
            {
                var lastOrder = allOrders.Where(o => o.UserId == user.Id)
                                         .OrderByDescending(o => o.CreatedAt)
                                         .FirstOrDefault();

                if (lastOrder != null && lastOrder.CreatedAt < sevenDaysAgo)
                {
                    await _notificationService.NotifyUserAsync(
                        user.Id.ToString(), 
                        "Saudades de você!", 
                        new { coupon = "VOLTA20", message = "Use o cupom VOLTA20 para 20% de desconto no seu próximo pedido!" }
                    );
                }
            }
        }
    }
}
