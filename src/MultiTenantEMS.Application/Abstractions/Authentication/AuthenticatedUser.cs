using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEMS.Application.Abstractions.Authentication
{
    public class AuthenticatedUser
    {
        public string UserId { get; set; }
        public string? TenantId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
