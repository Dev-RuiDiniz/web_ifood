using System;
using Domains.Base;

namespace Domains.Entities
{
    public class DeliveryFeeConfig : BaseEntity
    {
        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; } = null!;
        
        public double MinDistanceKm { get; set; }
        public double MaxDistanceKm { get; set; }
        public decimal Fee { get; set; }
        
        public decimal? FreeShippingThreshold { get; set; } // Order value for free shipping
        public bool IsActive { get; set; } = true;
    }
}
