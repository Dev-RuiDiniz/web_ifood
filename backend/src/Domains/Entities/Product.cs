using System;
using Domains.Base;

namespace Domains.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsAvailable { get; set; } = true;

        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; } = null!;
        
        public string? Category { get; set; } // Burger, Drink, Dessert
    }
}
