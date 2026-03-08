using MediatR;
using Domains.Entities;
using Repositories.Interfaces;
using Data.Interfaces;
using Services.Finance;
using Domains.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;
        private readonly Services.Ordering.IOrderStateService _stateService;
        private readonly Services.RealTime.INotificationService _notificationService;
        private readonly IBalanceService _balanceService;
        private readonly IBaseRepository<Coupon> _couponRepository;

        public UpdateOrderStatusHandler(
            IBaseRepository<Order> orderRepository, 
            IUnitOfWork uow, 
            IMediator mediator,
            Services.Ordering.IOrderStateService stateService,
            Services.RealTime.INotificationService notificationService,
            IBalanceService balanceService,
            IBaseRepository<Coupon> couponRepository)
        {
            _orderRepository = orderRepository;
            _uow = uow;
            _mediator = mediator;
            _stateService = stateService;
            _notificationService = notificationService;
            _balanceService = balanceService;
            _couponRepository = couponRepository;
        }

        public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null) return false;

            if (order.Status == request.NewStatus) return true;

            // Validate transition
            if (!Enum.TryParse<Domains.Enums.OrderStatus>(order.Status, out var currentStatus) ||
                !Enum.TryParse<Domains.Enums.OrderStatus>(request.NewStatus, out var nextStatus))
            {
                return false;
            }

            if (!_stateService.CanTransitionTo(currentStatus, nextStatus))
            {
                return false;
            }

            order.Status = request.NewStatus;
            
            await _orderRepository.UpdateAsync(order);
            var success = await _uow.CommitAsync();

            if (success)
            {
                // Notify User real-time
                await _notificationService.NotifyUserAsync(
                    order.UserId.ToString(), 
                    $"O status do seu pedido mudou para: {request.NewStatus}", 
                    new { orderId = order.Id, status = request.NewStatus });

                if (request.NewStatus == OrderStatus.Confirmed.ToString())
                {
                    if (order.CouponId.HasValue)
                    {
                        var coupon = await _couponRepository.GetByIdAsync(order.CouponId.Value);
                        if (coupon != null)
                        {
                            coupon.CurrentUsage++;
                            await _couponRepository.UpdateAsync(coupon);
                        }
                    }

                    await _mediator.Publish(new Services.Events.OrderConfirmedEvent
                    {
                        OrderId = order.Id,
                        RestaurantId = order.RestaurantId,
                        OrderNumber = order.OrderNumber
                    });
                }
                else if (request.NewStatus == OrderStatus.Delivered.ToString())
                {
                    await _balanceService.ProcessOrderFinancialsAsync(order);
                    await _uow.CommitAsync();
                }
            }

            return success;
        }
    }
}
