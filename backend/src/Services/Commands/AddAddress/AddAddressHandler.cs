using MediatR;
using Domains.Entities;
using Repositories.Interfaces;
using Data.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Commands.AddAddress
{
    public class AddAddressHandler : IRequestHandler<AddAddressCommand, Guid?>
    {
        private readonly IBaseRepository<Address> _addressRepository;
        private readonly IUnitOfWork _uow;

        public AddAddressHandler(IBaseRepository<Address> addressRepository, IUnitOfWork uow)
        {
            _addressRepository = addressRepository;
            _uow = uow;
        }

        public async Task<Guid?> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            var userAddresses = await _addressRepository.FindAsync(a => a.UserId == request.UserId);
            
            if (userAddresses.Count() >= 10)
            {
                throw new Exception("Limite de 10 endereços atingido.");
            }

            if (request.IsMain)
            {
                foreach (var addr in userAddresses.Where(a => a.IsMain))
                {
                    addr.IsMain = false;
                    await _addressRepository.UpdateAsync(addr);
                }
            }
            else if (!userAddresses.Any())
            {
                request.IsMain = true; // First address is always main
            }

            var address = new Address
            {
                UserId = request.UserId,
                Street = request.Street,
                Number = request.Number,
                Neighborhood = request.Neighborhood,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                IsMain = request.IsMain
            };

            await _addressRepository.AddAsync(address);
            await _uow.CommitAsync();

            return address.Id;
        }
    }
}
