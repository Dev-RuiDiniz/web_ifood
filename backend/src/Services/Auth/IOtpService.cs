using System;
using System.Threading.Tasks;
using Domains.Entities;

namespace Services.Auth
{
    public interface IOtpService
    {
        Task<string> GenerateAndSendOtpAsync(User user, string type);
        Task<bool> ValidateOtpAsync(Guid userId, string code, string type);
    }
}
