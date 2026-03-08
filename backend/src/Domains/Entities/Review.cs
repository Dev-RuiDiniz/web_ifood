using System;
using Domains.Base;

namespace Domains.Entities
{
    public class Review : BaseEntity
    {
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
        
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        
        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; } = null!;

        public int Stars { get; set; } // 1 to 5
        public string? Comment { get; set; }
    }
}
