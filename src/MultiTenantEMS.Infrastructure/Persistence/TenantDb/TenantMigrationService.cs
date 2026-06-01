using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MultiTenantEMS.Application.Abstractions.Services;
using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Infrastructure.Persistence.TenantDb
{
    internal class TenantMigrationService : ITenantMigrationService
    {
        private readonly ILogger<TenantMigrationService> _logger;

        public TenantMigrationService(ILogger<TenantMigrationService> logger)
        {
            _logger = logger;
        }

        public async Task MigrateAsync(string connectionString)
        {
            var options = new DbContextOptionsBuilder<TenantDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            await using var context = new TenantDbContext(options);

            await context.Database.MigrateAsync();
        }

        public async Task MigrateAllAsync(IEnumerable<Tenant> tenants)
        {
            foreach (var tenant in tenants)
            {
                try
                {
                    await MigrateAsync(tenant.DbConnStr);
                    _logger.LogInformation("Migrated tenant {TenantId}", tenant.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Migration failed for {TenantId}", tenant.Id);
                }
            }
        }
    }
}
