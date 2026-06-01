
namespace MultiTenantEMS.Application.Abstractions.Authentication
{
    public interface IIdentityService
    {
        Task<(bool Succeeded, IEnumerable<string> Errors)> CreateUserAsync(string email, string password, string role, string? tenantId);
        Task<AuthenticatedUser?> AuthenticateAsync(string email, string password);
        Task<(bool Succeeded, IEnumerable<string> Errors)> ChangePassword(string email, string oldPassword, string newPassword);
    }
}
