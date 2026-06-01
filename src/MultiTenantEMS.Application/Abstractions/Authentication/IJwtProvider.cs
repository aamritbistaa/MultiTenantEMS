using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEMS.Application.Abstractions.Authentication
{
    public interface IJwtProvider
    {
        string GenerateToken(string userId, string email, string role, string? tenantId);
    }
}
