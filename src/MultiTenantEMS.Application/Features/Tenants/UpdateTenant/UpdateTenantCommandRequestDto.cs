using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEMS.Application.Features.Tenants.UpdateTenant
{
    public class UpdateTenantCommandRequestDto
    {
        [Required(ErrorMessage = "Tenant name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Tenant ID is required.")]
        [MaxLength(4, ErrorMessage = "Tenant ID cannot exceed 4 characters.")]
        public string TenantId { get; set; }
    }
}
