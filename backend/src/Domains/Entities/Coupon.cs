using System;
using Domains.Base;
using Domains.Enums;

namespace Domains.Entities
{
    public class Coupon : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        public DiscountType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        
        public decimal? MinOrderValue { get; set; }
        public decimal? MaxDiscountValue { get; set; } // useful for percentage coupons
        
        public int MaxUsage { get; set; }
        public int CurrentUsage { get; set; }
        
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        // Optional: restriction to a specific restaurant
        public Guid? RestaurantId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }

        public bool IsFirstOrderOnly { get; set; } = false;
    }
}
