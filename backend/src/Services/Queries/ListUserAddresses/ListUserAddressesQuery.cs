using MediatR;
using Dtos.Users;
using System;
using System.Collections.Generic;

namespace Services.Queries.ListUserAddresses
{
    public class ListUserAddressesQuery : IRequest<IEnumerable<AddressDto>>
    {
        public Guid UserId { get; set; }

        public ListUserAddressesQuery(Guid userId)
        {
            UserId = userId;
        }
    }

    public class AddressDto
    {
        public Guid Id { get; set; }
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public bool IsMain { get; set; }
    }
}
