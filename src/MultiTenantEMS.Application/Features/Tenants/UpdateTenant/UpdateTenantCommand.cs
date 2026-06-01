using MultiTenantEMS.Application.Abstractions.Messaging;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantEMS.Application.Features.Tenants.UpdateTenant
{
    public class UpdateTenantCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string TenantId { get; set; }
    }
}
