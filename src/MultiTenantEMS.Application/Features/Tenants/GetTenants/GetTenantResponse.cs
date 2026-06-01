using MultiTenantEMS.Application.Features.Tenants.GetTenantById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEMS.Application.Features.Tenants.GetTenants
{
    public class GetTenantResponse
    {
        public int Count { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public List<GetTenantByIdResponse> Data { get; set; } = new();
    }
}
