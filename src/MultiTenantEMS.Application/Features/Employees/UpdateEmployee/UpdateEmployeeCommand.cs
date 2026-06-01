using MultiTenantEMS.Application.Abstractions.Messaging;

namespace MultiTenantEMS.Application.Features.Employees.UpdateEmployee
{
    public class UpdateEmployeeCommand : ICommand
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
    }
}
