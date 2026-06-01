using Microsoft.Extensions.Logging;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Abstractions.Services;
using MultiTenantEMS.Application.Common;

namespace MultiTenantEMS.Application.Features.Employees.UpdateEmployee
{
    internal class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<UpdateEmployeeCommandHandler> _logger;
        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, ICurrentUserService currentUserService, ILogger<UpdateEmployeeCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeById(request.Id);
                if (employee == null)
                {
                    return Result.Failure("Employee not found");
                }
                employee.FullName = request.FullName;
                await _employeeRepository.UpdateEmployee(employee);
                _logger.LogInformation("Employee with id {EmployeeId} updated successfully", employee.Id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while updating employee with id {EmployeeId}", request.Id);
                return Result.Failure("An error occurred while updating the employee", ApiResponseCode.InternalServerError);
            }
        }
    }
}
