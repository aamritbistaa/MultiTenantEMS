using MultiTenantEMS.Application.Abstractions.Messaging;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantEMS.Application.Features.Tenants.DeleteTenant
{
    public class DeleteTenantCommand : ICommand
    {
        [Required(ErrorMessage = "Tenant ID is required.")]
        [MaxLength(4, ErrorMessage = "Tenant ID cannot exceed 4 characters.")]
        public string TenantId { get; set; }
    }
}
