using System;
using Domains.Base;

namespace Domains.Entities
{
    public class Courier : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        
        public string VehicleType { get; set; } = string.Empty; // Moto, Carro, Bike
        public string LicenseNumber { get; set; } = string.Empty;
        public string VehiclePlate { get; set; } = string.Empty;
        
        public string Status { get; set; } = "AwaitingApproval"; // AwaitingApproval, Active, Banned, Offline
        
        public string? DocumentUrl { get; set; }
        public DateTime? ApprovedAt { get; set; }
        
        public double CurrentLatitude { get; set; }
        public double CurrentLongitude { get; set; }
    }
}
