using MultiTenantEMS.Application.Features.Tenants.GetTenantById;

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
