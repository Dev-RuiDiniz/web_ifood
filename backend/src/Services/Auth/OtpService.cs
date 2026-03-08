using System;
using System.Linq;
using System.Threading.Tasks;
using Domains.Entities;
using Repositories.Interfaces;
using Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace Services.Auth
{
    public class OtpService : IOtpService
    {
        private readonly IBaseRepository<OtpCode> _otpRepository;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<OtpService> _logger;

        public OtpService(IBaseRepository<OtpCode> otpRepository, IUnitOfWork uow, ILogger<OtpService> logger)
        {
            _otpRepository = otpRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<string> GenerateAndSendOtpAsync(User user, string type)
        {
            var random = new Random();
            var code = random.Next(100000, 999999).ToString();

            var otp = new OtpCode
            {
                UserId = user.Id,
                Code = code,
                Type = type,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false
            };

            await _otpRepository.AddAsync(otp);
            await _uow.CommitAsync();

            // Mock sending email/SMS
            _logger.LogInformation("Enviando código OTP {Code} para o usuário {Email}", code, user.Email);
            
            return code;
        }

        public async Task<bool> ValidateOtpAsync(Guid userId, string code, string type)
        {
            var otps = await _otpRepository.FindAsync(o => 
                o.UserId == userId && 
                o.Code == code && 
                o.Type == type && 
                !o.IsUsed && 
                o.ExpiresAt > DateTime.UtcNow);

            var otp = otps.OrderByDescending(o => o.CreatedAt).FirstOrDefault();

            if (otp == null) return false;

            otp.IsUsed = true;
            await _otpRepository.UpdateAsync(otp);
            await _uow.CommitAsync();

            return true;
        }
    }
}
