using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Infrastructure.Configurations
{
    internal class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(t=>t.Id);
            builder
                .Property(t => t.Name)
                .IsRequired();
            builder
                .Property(t => t.EmailAddress)
                .IsRequired();

            builder
                .Property(t => t.TenantId)
                .HasMaxLength(4)
                .IsRequired();
            builder
                .HasIndex(t => t.TenantId)
                .IsUnique();

            builder
                .Property(t => t.DbConnStr)
                .IsRequired();
        }
    }
}
