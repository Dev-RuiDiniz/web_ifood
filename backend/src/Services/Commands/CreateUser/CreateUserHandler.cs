using MediatR;
using Domains.Entities;
using Repositories.Interfaces;
using Data.Interfaces;
using Services.Auth;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IUnitOfWork _uow;
        private readonly IHashService _hashService;

        public CreateUserHandler(IBaseRepository<User> userRepository, IUnitOfWork uow, IHashService hashService)
        {
            _userRepository = userRepository;
            _uow = uow;
            _hashService = hashService;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = _hashService.Hash(request.Password),
                Document = request.Document,
                Phone = request.Phone,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            await _uow.CommitAsync();

            return user.Id;
        }
    }
}
