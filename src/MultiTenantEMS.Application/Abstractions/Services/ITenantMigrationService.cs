using MultiTenantEMS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEMS.Application.Abstractions.Services
{
    public interface ITenantMigrationService
    {
        Task MigrateAsync(string connectionString);
        Task MigrateAllAsync(IEnumerable<Tenant> tenants);
    }
}
