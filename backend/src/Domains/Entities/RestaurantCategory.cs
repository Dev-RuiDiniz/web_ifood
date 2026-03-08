using System;
using Domains.Base;

namespace Domains.Entities
{
    public class RestaurantCategory
    {
        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; } = null!;
        
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        
        public DateTime AssociatedAt { get; set; } = DateTime.UtcNow;
    }
}
