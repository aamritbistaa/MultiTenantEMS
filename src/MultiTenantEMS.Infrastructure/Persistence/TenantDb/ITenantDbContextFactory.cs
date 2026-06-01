namespace MultiTenantEMS.Infrastructure.Persistence.TenantDb
{
    public interface ITenantDbContextFactory
    {
        TenantDbContext Create(string connectionString);
    }
}
