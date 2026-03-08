using MediatR;
using System;

namespace Services.Commands.AddAddress
{
    public class AddAddressCommand : IRequest<Guid?>
    {
        public Guid UserId { get; set; }
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
