using System;
using System.Collections.Generic;
using Domains.Base;

namespace Domains.Entities
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        
        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; } = null!;
        
        public decimal SubTotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TotalAmount { get; set; }
        
        public string Status { get; set; } = "Created"; // Created, PendingPayment, Confirmed, Preparing, InTransit, Delivered, Cancelled
        
        public Guid DeliveryAddressId { get; set; }
        public virtual Address DeliveryAddress { get; set; } = null!;

        public Guid? CourierId { get; set; }
        public virtual Courier? Courier { get; set; }

        public Guid? CouponId { get; set; }
        public virtual Coupon? Coupon { get; set; }
        public decimal DiscountAmount { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        
        public string? Observation { get; set; }
    }
}
