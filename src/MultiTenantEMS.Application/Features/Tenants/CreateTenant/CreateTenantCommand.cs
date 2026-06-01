using MultiTenantEMS.Application.Abstractions.Messaging;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantEMS.Application.Features.Tenants.CreateTenant
{
    public class CreateTenantCommand : ICommand<string>
    {
        [Required(ErrorMessage ="Tenant name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Tenant ID is required.")]
        [MaxLength(4, ErrorMessage = "Tenant ID cannot exceed 4 characters.")]
        public string TenantId { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }
    }
}
