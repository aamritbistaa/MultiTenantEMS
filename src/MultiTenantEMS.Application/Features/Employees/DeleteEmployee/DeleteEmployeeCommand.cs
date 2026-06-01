using MultiTenantEMS.Application.Abstractions.Messaging;

namespace MultiTenantEMS.Application.Features.Employees.DeleteEmployee
{
    public class DeleteEmployeeCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
