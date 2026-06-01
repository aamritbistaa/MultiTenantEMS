using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Domain.Entity;
using MultiTenantEMS.Infrastructure.Configurations;
using MultiTenantEMS.Infrastructure.Identity;

namespace MultiTenantEMS.Infrastructure.Persistence.MasterDb
{
    public class MasterDbContext : IdentityDbContext<ApplicationUser>
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options)
            : base(options)
        {
        }
        public DbSet<Tenant> Tenants { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TenantConfiguration());

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(x => x.TenantId)
                .HasMaxLength(4);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
