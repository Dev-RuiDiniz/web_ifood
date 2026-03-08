using System.Threading.Tasks;

namespace Services.RealTime
{
    public interface INotificationService
    {
        Task NotifyUserAsync(string userId, string message, object data);
        Task NotifyRestaurantAsync(string restaurantId, string message, object data);
    }
}
