using Domains.Enums;
using System;
using System.Collections.Generic;

namespace Services.Ordering
{
    public interface IOrderStateService
    {
        bool CanTransitionTo(OrderStatus current, OrderStatus next);
    }

    public class OrderStateService : IOrderStateService
    {
        private readonly Dictionary<OrderStatus, List<OrderStatus>> _allowedTransitions = new()
        {
            { OrderStatus.Created, new List<OrderStatus> { OrderStatus.PendingPayment, OrderStatus.Cancelled } },
            { OrderStatus.PendingPayment, new List<OrderStatus> { OrderStatus.Confirmed, OrderStatus.Cancelled } },
            { OrderStatus.Confirmed, new List<OrderStatus> { OrderStatus.Preparing, OrderStatus.Cancelled } },
            { OrderStatus.Preparing, new List<OrderStatus> { OrderStatus.Ready, OrderStatus.Cancelled } },
            { OrderStatus.Ready, new List<OrderStatus> { OrderStatus.InTransit, OrderStatus.Cancelled } },
            { OrderStatus.InTransit, new List<OrderStatus> { OrderStatus.Delivered, OrderStatus.Cancelled } },
            { OrderStatus.Delivered, new List<OrderStatus>() },
            { OrderStatus.Cancelled, new List<OrderStatus>() }
        };

        public bool CanTransitionTo(OrderStatus current, OrderStatus next)
        {
            if (!_allowedTransitions.ContainsKey(current)) return false;
            return _allowedTransitions[current].Contains(next);
        }
    }
}
