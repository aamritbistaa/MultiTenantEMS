using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MultiTenantEMS.Application.Abstractions.Services;
using MultiTenantEMS.Infrastructure.Persistence.MasterDb;
using System.Security.Claims;

namespace MultiTenantEMS.Infrastructure.Identity
{
    internal class CurrentUserService(IHttpContextAccessor httpContextAccessor, MasterDbContext masterDbContext) : ICurrentUserService
    {
        public async Task<CurrentUser?> GetCurrentTenant()
        {
            var tenantId = httpContextAccessor.HttpContext?.User?
                .FindFirstValue("tenantId");

            if (tenantId is null)
            {
                return null;
            }
            var tenant = await masterDbContext.Tenants
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TenantId == tenantId);
            return new CurrentUser()
            {
                TenantId = tenant?.TenantId,
                ConnectionString = tenant?.DbConnStr
            };
        }
        public Guid GetCurrentUserId()
        {
            var userId = httpContextAccessor.HttpContext?.User?
                .FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return Guid.Empty;
            }
            return Guid.Parse(userId);
        }
    }
}
