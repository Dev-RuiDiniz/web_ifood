using MediatR;
using System;

namespace Services.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
        public string NewStatus { get; set; } = string.Empty;
    }
}
