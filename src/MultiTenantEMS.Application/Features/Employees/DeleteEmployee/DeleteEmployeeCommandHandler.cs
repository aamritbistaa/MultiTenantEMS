using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Abstractions.Services;
using MultiTenantEMS.Application.Common;

namespace MultiTenantEMS.Application.Features.Employees.DeleteEmployee
{
    internal class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, ICurrentUserService currentUserService)
        {
            _employeeRepository = employeeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _currentUserService.GetCurrentTenant();

            var employee = await _employeeRepository.GetEmployeeById(tenant.ConnectionString, request.Id);
            if(employee == null)
            {
                return Result.Failure("Employee not found.");
            }
            employee.IsDeleted = true;
            await _employeeRepository.UpdateEmployee(tenant.ConnectionString, employee);
            return Result.Success();
        }
    }
}
