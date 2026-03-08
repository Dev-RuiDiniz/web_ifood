using Api.Hubs;
using Microsoft.AspNetCore.SignalR;
using Services.RealTime;
using System.Threading.Tasks;

namespace Api.Services
{
    public class SignalRNotificationService : INotificationService
    {
        private readonly IHubContext<OrderHub> _hubContext;

        public SignalRNotificationService(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyUserAsync(string userId, string message, object data)
        {
            await _hubContext.Clients.Group($"User_{userId}").SendAsync("ReceiveNotification", message, data);
        }

        public async Task NotifyRestaurantAsync(string restaurantId, string message, object data)
        {
            await _hubContext.Clients.Group($"Restaurant_{restaurantId}").SendAsync("ReceiveOrderUpdate", message, data);
        }
    }
}
