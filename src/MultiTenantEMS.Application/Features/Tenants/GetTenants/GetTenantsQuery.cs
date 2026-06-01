using MultiTenantEMS.Application.Abstractions.Messaging;

namespace MultiTenantEMS.Application.Features.Tenants.GetTenants
{
    public class GetTenantsQuery : IQuery<GetTenantResponse>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
