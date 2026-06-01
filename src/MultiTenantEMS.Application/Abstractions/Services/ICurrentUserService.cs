
namespace MultiTenantEMS.Application.Abstractions.Services
{
    public interface ICurrentUserService
    {
        Task<CurrentUser?> GetCurrentTenant();
        Guid GetCurrentUserId();
        Task<string> GetConnectionString();
    }
}
