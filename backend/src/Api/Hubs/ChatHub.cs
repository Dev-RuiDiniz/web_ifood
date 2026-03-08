using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task JoinOrderChat(string orderId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Chat_{orderId}");
        }

        public async Task SendMessage(string orderId, string message)
        {
            // The actual persistence is handled via a Controller/Service call 
            // but the Hub can broadcast it instantly.
            await Clients.Group($"Chat_{orderId}").SendAsync("ReceiveMessage", Context.UserIdentifier, message);
        }
    }
}
