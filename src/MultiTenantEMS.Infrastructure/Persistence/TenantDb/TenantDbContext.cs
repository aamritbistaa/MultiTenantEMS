using Microsoft.EntityFrameworkCore;
using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Infrastructure.Persistence.TenantDb
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options)
            : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}