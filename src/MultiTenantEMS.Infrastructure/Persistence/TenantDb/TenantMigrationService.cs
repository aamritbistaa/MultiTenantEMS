using Microsoft.EntityFrameworkCore;
using MultiTenantEMS.Application.Abstractions.Services;

namespace MultiTenantEMS.Infrastructure.Persistence.TenantDb
{
    internal class TenantMigrationService : ITenantMigrationService
    {
        public async Task MigrateAsync(string connectionString)
        {
            var options = new DbContextOptionsBuilder<TenantDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            await using var context = new TenantDbContext(options);

            await context.Database.MigrateAsync();
        }
    }
}
