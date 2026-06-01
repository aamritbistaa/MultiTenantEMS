using Microsoft.Extensions.Logging;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Common;

namespace MultiTenantEMS.Application.Features.Employees.DeleteEmployee
{
    internal class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<DeleteEmployeeCommandHandler> _logger;
        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, ILogger<DeleteEmployeeCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeById(request.Id);
                if (employee == null)
                {
                    return Result.Failure("Employee not found.");
                }
                employee.IsDeleted = true;
                await _employeeRepository.UpdateEmployee(employee);
                _logger.LogInformation("Employee with ID {EmployeeId} deleted.", request.Id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while deleting employee with ID {EmployeeId}.", request.Id);
                return Result.Failure("An error occurred while deleting the employee.", ApiResponseCode.InternalServerError);
            }
        }
    }
}
