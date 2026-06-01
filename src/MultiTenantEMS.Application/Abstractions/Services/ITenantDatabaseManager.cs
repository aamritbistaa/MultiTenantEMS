
namespace MultiTenantEMS.Application.Abstractions.Services
{
    public interface ITenantDatabaseManager
    {
        Task<bool> DatabaseExistsAsync(string tenantId);
        Task<string> CreateDatabaseAsync(string tenantId);
        Task DropDatabaseAsync(string tenantId);
    }
}
