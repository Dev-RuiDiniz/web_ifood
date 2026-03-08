using System;
using System.Threading.Tasks;
using Domains.Entities;
using Repositories.Interfaces;
using Data.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Services.Audit
{
    public interface IAuditService
    {
        Task LogActionAsync(Guid? userId, string action, string details);
    }

    public class AuditService : IAuditService
    {
        private readonly IBaseRepository<AuditLog> _auditRepository;
        private readonly IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditService(IBaseRepository<AuditLog> auditRepository, IUnitOfWork uow, IHttpContextAccessor httpContextAccessor)
        {
            _auditRepository = auditRepository;
            _uow = uow;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LogActionAsync(Guid? userId, string action, string details)
        {
            var context = _httpContextAccessor.HttpContext;
            var ipAddress = context?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
            var userAgent = context?.Request?.Headers["User-Agent"].ToString() ?? "Unknown";

            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                IpAddress = ipAddress,
                UserAgent = userAgent,
                Details = details
            };

            await _auditRepository.AddAsync(log);
            await _uow.CommitAsync();
        }
    }
}
