using MediatR;
using Domains.Entities;
using Repositories.Interfaces;
using Data.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Commands.SetMainAddress
{
    public class SetMainAddressHandler : IRequestHandler<SetMainAddressCommand, bool>
    {
        private readonly IBaseRepository<Address> _addressRepository;
        private readonly IUnitOfWork _uow;

        public SetMainAddressHandler(IBaseRepository<Address> addressRepository, IUnitOfWork uow)
        {
            _addressRepository = addressRepository;
            _uow = uow;
        }

        public async Task<bool> Handle(SetMainAddressCommand request, CancellationToken cancellationToken)
        {
            var userAddresses = (await _addressRepository.FindAsync(a => a.UserId == request.UserId)).ToList();
            
            var targetAddress = userAddresses.FirstOrDefault(a => a.Id == request.AddressId);
            if (targetAddress == null) return false;

            foreach (var addr in userAddresses.Where(a => a.IsMain))
            {
                addr.IsMain = false;
                await _addressRepository.UpdateAsync(addr);
            }

            targetAddress.IsMain = true;
            await _addressRepository.UpdateAsync(targetAddress);

            return await _uow.CommitAsync();
        }
    }
}
