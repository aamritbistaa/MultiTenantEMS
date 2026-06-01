using MultiTenantEMS.Application.Abstractions.Messaging;

namespace MultiTenantEMS.Application.Features.Employees.CreateEmployee
{
    public class CreateEmployeeCommand : ICommand<string>
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
