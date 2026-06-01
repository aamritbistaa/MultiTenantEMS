using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Infrastructure.Identity;
using MultiTenantEMS.Infrastructure.Persistence.MasterDb;
using System.Text;

namespace MultiTenantEMS.API.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityAndAuth(this IServiceCollection services, IConfiguration configuration)
        {
            // Identity setup
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<MasterDbContext>()
                .AddDefaultTokenProviders();

            // JWT setup
            var secret = configuration["Jwt:Secret"];
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = issuer,
                        ValidAudience = audience,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    };
                });

            // Authorization policies
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminOrEmployee", policy =>
                    policy.RequireRole(Roles.Admin, Roles.Employee));

                opt.AddPolicy("SuperAdmin", policy =>
                    policy.RequireRole(Roles.SuperAdmin));

                opt.AddPolicy("Admin", policy =>
                    policy.RequireRole(Roles.Admin));

                opt.AddPolicy("Employee", policy =>
                    policy.RequireRole(Roles.Employee));
            });

            return services;
        }
    }
}
