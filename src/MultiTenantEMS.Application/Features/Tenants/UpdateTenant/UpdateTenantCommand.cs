using MultiTenantEMS.Application.Abstractions.Messaging;

namespace MultiTenantEMS.Application.Features.Tenants.UpdateTenant
{
    public class UpdateTenantCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TenantId { get; set; }
    }
}
