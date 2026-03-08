using MediatR;
using AutoMapper;
using Domains.Entities;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Queries.ListUserAddresses
{
    public class ListUserAddressesHandler : IRequestHandler<ListUserAddressesQuery, IEnumerable<AddressDto>>
    {
        private readonly IBaseRepository<Address> _addressRepository;
        private readonly IMapper _mapper;

        public ListUserAddressesHandler(IBaseRepository<Address> addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressDto>> Handle(ListUserAddressesQuery request, CancellationToken cancellationToken)
        {
            var addresses = await _addressRepository.FindAsync(a => a.UserId == request.UserId);
            return _mapper.Map<IEnumerable<AddressDto>>(addresses);
        }
    }
}
