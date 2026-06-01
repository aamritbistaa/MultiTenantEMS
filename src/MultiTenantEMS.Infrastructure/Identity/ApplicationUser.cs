using Microsoft.AspNetCore.Identity;

namespace MultiTenantEMS.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        // null = SuperAdmin
        public string? TenantId { get; set; }
    }
}
