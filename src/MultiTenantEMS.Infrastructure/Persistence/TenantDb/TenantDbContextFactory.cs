using Microsoft.EntityFrameworkCore;

namespace MultiTenantEMS.Infrastructure.Persistence.TenantDb
{
    internal class TenantDbContextFactory : ITenantDbContextFactory
    {
        public TenantDbContext Create(string connectionString)
        {
            var options = new DbContextOptionsBuilder<TenantDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            return new TenantDbContext(options);
        }
    }
}
