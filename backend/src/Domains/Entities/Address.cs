using System;
using Domains.Base;

namespace Domains.Entities
{
    public class Address : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsMain { get; set; }
    }
}
