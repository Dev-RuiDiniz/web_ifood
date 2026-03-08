using MediatR;
using Domains.Entities;
using Domains.Enums;
using Repositories.Interfaces;
using Data.Interfaces;
using Services.Ordering;
using Services.Payments;
using Services.RealTime;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Commands.CancelOrder
{
    public class CancelOrderHandler : IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IUnitOfWork _uow;
        private readonly IOrderStateService _stateService;
        private readonly IRefundService _refundService;
        private readonly INotificationService _notificationService;

        public CancelOrderHandler(
            IBaseRepository<Order> orderRepository, 
            IUnitOfWork uow, 
            IOrderStateService stateService, 
            IRefundService refundService, 
            INotificationService notificationService)
        {
            _orderRepository = orderRepository;
            _uow = uow;
            _stateService = stateService;
            _refundService = refundService;
            _notificationService = notificationService;
        }

        public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null) return false;

            // Check if transition to Cancelled is possible
            if (!Enum.TryParse<OrderStatus>(order.Status, out var currentStatus)) return false;
            
            if (!_stateService.CanTransitionTo(currentStatus, OrderStatus.Cancelled)) return false;

            // Business logic for refund (if order was confirmed or after)
            bool needsRefund = currentStatus != OrderStatus.Created && currentStatus != OrderStatus.PendingPayment;

            if (needsRefund)
            {
                var refundResult = await _refundService.RefundOrderAsync(order);
                if (!refundResult)
                {
                    // Fail if refund failed (could also log and retry, but for now we block)
                    return false;
                }
            }

            order.Status = OrderStatus.Cancelled.ToString();
            order.Observation = (order.Observation ?? "") + $" [CANCELAMENTO: {request.Reason}]";
            
            await _orderRepository.UpdateAsync(order);
            var success = await _uow.CommitAsync();

            if (success)
            {
                // Notify both parties
                await _notificationService.NotifyUserAsync(order.UserId.ToString(), 
                    $"Pedido {order.OrderNumber} cancelado. Motivo: {request.Reason}", new { orderId = order.Id });
                
                await _notificationService.NotifyRestaurantAsync(order.RestaurantId.ToString(), 
                    $"Pedido {order.OrderNumber} cancelado pelo cliente/sistema.", new { orderId = order.Id });
            }

            return success;
        }
    }
}
