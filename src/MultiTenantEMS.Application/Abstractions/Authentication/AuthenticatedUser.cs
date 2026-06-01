
namespace MultiTenantEMS.Application.Abstractions.Authentication
{
    public class AuthenticatedUser
    {
        public string UserId { get; set; }
        public Guid? TenantId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
