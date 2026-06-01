using Microsoft.AspNetCore.Identity;
using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        // null = SuperAdmin
        public Guid? TenantId { get; set; }
        public string Role { get; set; }
    }
}
