using MediatR;
using Domains.Entities;
using Repositories.Interfaces;
using Data.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Commands.UpdateProfile
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand, bool>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IUnitOfWork _uow;

        public UpdateProfileHandler(IBaseRepository<User> userRepository, IUnitOfWork uow)
        {
            _userRepository = userRepository;
            _uow = uow;
        }

        public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null) return false;

            user.Name = request.Name;
            user.Phone = request.Phone;

            await _userRepository.UpdateAsync(user);
            return await _uow.CommitAsync();
        }
    }
}
