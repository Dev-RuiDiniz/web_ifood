using System;
using System.Collections.Generic;
using Domains.Base;

namespace Domains.Entities
{
    public class Restaurant : BaseEntity
    {
        public string CommercialName { get; set; } = string.Empty;
        public string LegalName { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public string? BannerUrl { get; set; }
        
        public string Status { get; set; } = "Pending"; // Pending, Active, Paused, Rejected
        
        public bool IsOpen { get; set; } = true;

        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
        public decimal CommissionPercentage { get; set; } = 15.0m;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        public Guid OwnerId { get; set; }
        public virtual User Owner { get; set; } = null!;

        public Guid AddressId { get; set; }
        public virtual Address Address { get; set; } = null!;
        
        public DateTime? ApprovedAt { get; set; }
    }
}
