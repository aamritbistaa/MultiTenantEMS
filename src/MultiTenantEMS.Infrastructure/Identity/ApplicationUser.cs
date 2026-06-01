using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEMS.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        // null = SuperAdmin
        public string? TenantId { get; set; }
    }
}
