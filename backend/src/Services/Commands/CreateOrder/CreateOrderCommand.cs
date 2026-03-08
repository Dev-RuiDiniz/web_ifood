using MediatR;
using System;
using System.Collections.Generic;

namespace Services.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid?>
    {
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }
        public string? CouponCode { get; set; }
    }
}
