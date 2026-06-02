using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantEMS.Application.Abstractions.Authentication;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Abstractions.Services;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Infrastructure.Identity;
using MultiTenantEMS.Infrastructure.Persistence.MasterDb;
using MultiTenantEMS.Infrastructure.Persistence.MasterDb.Repositories;
using MultiTenantEMS.Infrastructure.Persistence.TenantDb;
using MultiTenantEMS.Infrastructure.Persistence.TenantDb.Repository;

namespace MultiTenantEMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Helper.DatabaseConnectionString;
            services.AddDbContext<MasterDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITenantMigrationService, TenantMigrationService>();
            services.AddScoped<ITenantDatabaseManager, TenantDatabaseManager>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddScoped<ITenantDbContextFactory, TenantDbContextFactory>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}
