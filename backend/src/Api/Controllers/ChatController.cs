using Domains.Entities;
using Repositories.Interfaces;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Api.Hubs;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IBaseRepository<ChatMessage> _chatRepository;
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IUnitOfWork _uow;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(
            IBaseRepository<ChatMessage> chatRepository, 
            IBaseRepository<Order> orderRepository,
            IUnitOfWork uow,
            IHubContext<ChatHub> hubContext)
        {
            _chatRepository = chatRepository;
            _orderRepository = orderRepository;
            _uow = uow;
            _hubContext = hubContext;
        }

        [HttpGet("{orderId}/history")]
        public async Task<IActionResult> GetHistory(Guid orderId)
        {
            var userId = GetUserId();
            var order = await _orderRepository.GetByIdAsync(orderId);
            
            if (order == null) return NotFound();
            // Verify if user is part of the order (Customer, Restaurant Owner or Courier)
            if (order.UserId != userId && order.Restaurant.OwnerId != userId) // Simplified check
                return Forbid();

            var messages = await _chatRepository.FindAsync(m => m.OrderId == orderId);
            return Ok(messages.OrderBy(m => m.SentAt));
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] SendMessageRequest request)
        {
            var userId = GetUserId();
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null) return NotFound();

            var message = new ChatMessage
            {
                OrderId = request.OrderId,
                SenderId = userId,
                Content = request.Content,
                SentAt = DateTime.UtcNow
            };

            await _chatRepository.AddAsync(message);
            await _uow.CommitAsync();

            // Broadcast to SignalR
            await _hubContext.Clients.Group($"Chat_{request.OrderId}")
                .SendAsync("ReceiveMessage", userId.ToString(), request.Content);

            return Ok(message);
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userId!);
        }

        public class SendMessageRequest
        {
            public Guid OrderId { get; set; }
            public string Content { get; set; } = string.Empty;
        }
    }
}
