using Microsoft.AspNetCore.Identity;
using MultiTenantEMS.Application.Common;

namespace MultiTenantEMS.Infrastructure.Identity
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            string[] roles =
            {
                Roles.SuperAdmin,
                Roles.Admin,
                Roles.Employee
            };

            // Create Roles
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create SuperAdmin
            var email = "assessment@yopmail.com";

            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, "Tester@123");

                await userManager.AddToRoleAsync(
                    user,
                    Roles.SuperAdmin);
            }
        }
    }
}
