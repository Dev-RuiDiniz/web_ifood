using System;
using System.Threading.Tasks;
using Domains.Entities;
using Repositories.Interfaces;
using Data.Interfaces;

namespace Services.Privacy
{
    public interface IPrivacyService
    {
        Task<bool> AnonymizeUserAsync(Guid userId);
        Task<string> ExportUserDataAsync(Guid userId);
    }

    public class PrivacyService : IPrivacyService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IUnitOfWork _uow;

        public PrivacyService(IBaseRepository<User> userRepository, IUnitOfWork uow)
        {
            _userRepository = userRepository;
            _uow = uow;
        }

        public async Task<bool> AnonymizeUserAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            user.Name = "ANONYMOUS_" + Guid.NewGuid().ToString().Substring(0, 8);
            user.Email = "deleted_" + Guid.NewGuid().ToString().Substring(0, 8) + "@example.com";
            user.Document = "00000000000";
            user.Phone = "00000000000";
            user.IsActive = false;
            user.IsDeleted = true;

            await _userRepository.UpdateAsync(user);
            return await _uow.CommitAsync();
        }

        public async Task<string> ExportUserDataAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return string.Empty;

            var data = new
            {
                user.Id,
                user.Name,
                user.Email,
                user.Document,
                user.Phone,
                user.Role,
                user.CreatedAt,
                Consents = user.TermConsents
            };

            return System.Text.Json.JsonSerializer.Serialize(data, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
        }
    }
}
