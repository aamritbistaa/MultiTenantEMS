using Microsoft.AspNetCore.Identity;
using MultiTenantEMS.Application.Abstractions.Authentication;

namespace MultiTenantEMS.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> CreateUserAsync( string email, string password, string role, Guid? tenantId)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = email,
                TenantId = tenantId,
                EmailConfirmed = true,
                Role = role
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return (false, result.Errors.Select(x => x.Description));
            }

            await _userManager.AddToRoleAsync(user, role);

            return (true, Enumerable.Empty<string>());
        }
        public async Task<AuthenticatedUser?> AuthenticateAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            var valid = await _userManager.CheckPasswordAsync(user, password);

            if (!valid)
            {
                return null;
            }

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            return new AuthenticatedUser()
            {
                UserId = user.Id,
                Email = email,
                TenantId = user.TenantId,
                Role = role
            };
        }
        public async Task<(bool Succeeded, IEnumerable<string> Errors)> ChangePassword(string email, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return (false, new[] { "User not found." });
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (result.Succeeded)
            {
                return (true, Enumerable.Empty<string>());
            }

            var errors = result.Errors.Select(e => e.Description);
            return (false, errors);
        }
    }
}
