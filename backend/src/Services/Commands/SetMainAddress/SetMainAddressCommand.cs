using MediatR;
using System;

namespace Services.Commands.SetMainAddress
{
    public class SetMainAddressCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }

        public SetMainAddressCommand(Guid userId, Guid addressId)
        {
            UserId = userId;
            AddressId = addressId;
        }
    }
}
