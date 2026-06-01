
namespace MultiTenantEMS.Application.Abstractions.Authentication
{
    public interface IJwtProvider
    {
        string GenerateToken(string userId, string email, string role, string? tenantId);
    }
}
