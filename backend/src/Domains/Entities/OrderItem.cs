using System;
using Domains.Base;

namespace Domains.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
        
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        
        public string ProductName { get; set; } = string.Empty; // Snapshot name
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // Snapshot price
        public decimal SubTotal => UnitPrice * Quantity;
        
        public string? Observation { get; set; }
    }
}
