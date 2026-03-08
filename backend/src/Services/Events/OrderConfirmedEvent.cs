using MediatR;
using System;

namespace Services.Events
{
    public class OrderConfirmedEvent : INotification
    {
        public Guid OrderId { get; set; }
        public Guid RestaurantId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
    }
}
