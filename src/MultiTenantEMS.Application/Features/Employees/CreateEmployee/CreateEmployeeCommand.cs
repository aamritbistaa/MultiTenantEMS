using MultiTenantEMS.Application.Abstractions.Messaging;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantEMS.Application.Features.Employees.CreateEmployee
{
    public class CreateEmployeeCommand : ICommand<string>
    {
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
