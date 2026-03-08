using MediatR;
using AutoMapper;
using Domains.Entities;
using Repositories.Interfaces;
using Dtos.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Queries.GetUserDetails
{
    public class GetUserDetailsHandler : IRequestHandler<GetUserDetailsQuery, UserDetailDto?>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUserDetailsHandler(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDetailDto?> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            
            if (user == null) return null;

            return _mapper.Map<UserDetailDto>(user);
        }
    }
}
