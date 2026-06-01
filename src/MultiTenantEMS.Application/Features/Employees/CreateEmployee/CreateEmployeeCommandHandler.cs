using Microsoft.Extensions.Logging;
using MultiTenantEMS.Application.Abstractions.Authentication;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Abstractions.Services;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Application.Features.Employees.CreateEmployee
{
    internal class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, string>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateEmployeeCommandHandler> _logger;
        public CreateEmployeeCommandHandler(ICurrentUserService currentUserService, IEmployeeRepository employeeRepository, IIdentityService identityService, IUnitOfWork unitOfWork, ILogger<CreateEmployeeCommandHandler> logger)
        {
            _currentUserService = currentUserService;
            _employeeRepository = employeeRepository;
            _identityService = identityService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result<string>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _currentUserService.GetCurrentTenant();

            var employee = new Employee
            {
                FullName = request.FullName,
                EmailAddress = request.EmailAddress
            };

            var employeeId = await _employeeRepository.AddEmployee(employee);
            try
            {
                var identityResult = await _identityService.CreateUserAsync(employee.EmailAddress, request.Password, Roles.Employee, tenant.TenantId);
                await _unitOfWork.SaveChangesAsync();
                if (!identityResult.Succeeded)
                {
                    await _employeeRepository.DeleteEmployee(employee);

                    return Result<string>.Failure(string.Join(", ", identityResult.Errors));
                }
                _logger.LogInformation("Employee created successfully with email: {Email}", employee.EmailAddress);
                return Result<string>.Success($"Employee: {employeeId} added successfully.", ApiResponseCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while creating employee with email: {Email}", employee.EmailAddress);
                await _employeeRepository.DeleteEmployee(employee);
                return Result<string>.Failure("An error occurred while creating the employee.", ApiResponseCode.InternalServerError);
            }
        }
    }
}
