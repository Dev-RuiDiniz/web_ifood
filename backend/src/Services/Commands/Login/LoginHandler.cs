using MediatR;
using Domains.Entities;
using Repositories.Interfaces;
using Data.Interfaces;
using Services.Auth;
using Dtos.Auth;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponseDto?>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<RefreshToken> _refreshTokenRepository;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _uow;
        private readonly Services.Audit.IAuditService _auditService;

        public LoginHandler(
            IBaseRepository<User> userRepository, 
            IBaseRepository<RefreshToken> refreshTokenRepository,
            IHashService hashService, 
            ITokenService tokenService,
            IUnitOfWork uow,
            Services.Audit.IAuditService auditService)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _hashService = hashService;
            _tokenService = tokenService;
            _uow = uow;
            _auditService = auditService;
        }

        public async Task<LoginResponseDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = (await _userRepository.FindAsync(u => u.Email == request.Email)).FirstOrDefault();

            if (user == null || !_hashService.Verify(request.Password, user.PasswordHash))
            {
                return null;
            }

            var token = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepository.AddAsync(refreshTokenEntity);
            await _uow.CommitAsync();

            await _auditService.LogActionAsync(user.Id, "Login", "Usuário realizou login com sucesso.");

            return new LoginResponseDto
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }
    }
}
