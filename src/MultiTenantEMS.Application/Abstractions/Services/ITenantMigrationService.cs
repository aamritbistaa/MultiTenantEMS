
namespace MultiTenantEMS.Application.Abstractions.Services
{
    public interface ITenantMigrationService
    {
        Task MigrateAsync(string connectionString);
    }
}
