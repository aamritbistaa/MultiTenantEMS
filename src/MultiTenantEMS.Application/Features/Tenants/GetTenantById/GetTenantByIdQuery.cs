using MultiTenantEMS.Application.Abstractions.Messaging;

namespace MultiTenantEMS.Application.Features.Tenants.GetTenantById
{
    public class GetTenantByIdQuery : IQuery<GetTenantByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
