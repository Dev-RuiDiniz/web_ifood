using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Logistics
{
    public interface IDispatchService
    {
        Task<Guid?> FindBestCourierAsync(Order order);
        Task AssignOrderAsync(Guid orderId, Guid courierId);
    }

    public class NearbyDispatchService : IDispatchService
    {
        private readonly RealTime.ICourierGeoService _geoService;
        private readonly RealTime.INotificationService _notificationService;
        private readonly Repositories.Interfaces.IBaseRepository<Order> _orderRepository;

        public NearbyDispatchService(
            RealTime.ICourierGeoService geoService, 
            RealTime.INotificationService notificationService,
            Repositories.Interfaces.IBaseRepository<Order> orderRepository)
        {
            _geoService = geoService;
            _notificationService = notificationService;
            _orderRepository = orderRepository;
        }

        public async Task<Guid?> FindBestCourierAsync(Order order)
        {
            // Initial radius: 2km
            var couriers = await _geoService.GetNearbyCouriersAsync(
                order.Restaurant.Address.Latitude ?? 0, 
                order.Restaurant.Address.Longitude ?? 0, 
                2.0
            );

            // In a real scenario, we would filter those who are 'Active' and 'Online' in SQL
            // For now, we return the first one found in Redis
            var bestCourierId = couriers.FirstOrDefault();
            
            if (bestCourierId != Guid.Empty)
            {
                // Send invite via SignalR
                await _notificationService.NotifyUserAsync(
                    bestCourierId.ToString(), 
                    "Você tem uma nova oferta de entrega!", 
                    new { orderId = order.Id, restaurantName = order.Restaurant.CommercialName }
                );
                return bestCourierId;
            }

            return null;
        }

        public async Task AssignOrderAsync(Guid orderId, Guid courierId)
        {
            // Implementation to link Courier to Order and change status to InTransit
        }
    }
}
