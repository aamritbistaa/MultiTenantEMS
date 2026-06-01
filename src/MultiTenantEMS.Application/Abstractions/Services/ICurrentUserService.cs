using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEMS.Application.Abstractions.Services
{
    public interface ICurrentUserService
    {
        Task<CurrentUser?> GetCurrentTenant();
        Guid GetCurrentUserId();
    }
}
