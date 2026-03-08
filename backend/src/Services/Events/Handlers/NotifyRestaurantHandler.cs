using MediatR;
using Services.RealTime;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Events.Handlers
{
    public class NotifyRestaurantHandler : INotificationHandler<OrderConfirmedEvent>
    {
        private readonly INotificationService _notificationService;

        public NotifyRestaurantHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(OrderConfirmedEvent notification, CancellationToken cancellationToken)
        {
            await _notificationService.NotifyRestaurantAsync(
                notification.RestaurantId.ToString(), 
                $"Novo pedido recebido: {notification.OrderNumber}", 
                new { orderId = notification.OrderId, orderNumber = notification.OrderNumber }
            );
        }
    }
}
