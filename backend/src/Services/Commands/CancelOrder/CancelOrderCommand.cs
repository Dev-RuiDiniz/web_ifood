using MediatR;
using System;

namespace Services.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
        public Guid RequesterId { get; set; } // Can be User or RestaurantOwner
        public string Reason { get; set; } = string.Empty;
    }
}
