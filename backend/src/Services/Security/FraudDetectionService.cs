using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Services.Security
{
    public interface IFraudDetectionService
    {
        Task<bool> IsCardSuspiciousAsync(string cardFingerprint, Guid userId);
    }

    public class FraudDetectionService : IFraudDetectionService
    {
        private readonly IDatabase _redis;

        public FraudDetectionService(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }

        public async Task<bool> IsCardSuspiciousAsync(string cardFingerprint, Guid userId)
        {
            var key = $"fraud:card:{cardFingerprint}";
            
            // Set of UserIds that used this card in the last 10 mins
            await _redis.SetAddAsync(key, userId.ToString());
            await _redis.KeyExpireAsync(key, TimeSpan.FromMinutes(10));

            var userCount = await _redis.SetLengthAsync(key);

            if (userCount >= 5)
            {
                // Serious fraud signal: many accounts using 1 card
                return true; 
            }

            return false;
        }
    }
}
