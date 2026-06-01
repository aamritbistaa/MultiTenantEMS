using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MultiTenantEMS.Infrastructure.Persistence.TenantDb
{
    public class TenantDbContextDesignFactory : IDesignTimeDbContextFactory<TenantDbContext>
    {
        //Used for Migrations, not for runtime
        public TenantDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();

            //optionsBuilder.UseNpgsql("Host=postgres;Database=tenant_migration_db;Username=postgres;Password=postgres");
            optionsBuilder.UseNpgsql();

            return new TenantDbContext(optionsBuilder.Options);
        }
    }
}
