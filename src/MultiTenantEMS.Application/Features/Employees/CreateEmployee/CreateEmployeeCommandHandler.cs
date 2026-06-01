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
        public CreateEmployeeCommandHandler(ICurrentUserService currentUserService, IEmployeeRepository employeeRepository, IIdentityService identityService, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _employeeRepository = employeeRepository;
            _identityService = identityService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _currentUserService.GetCurrentTenant();

            var employee = new Employee
            {
                FullName = request.FullName,
                EmailAddress = request.EmailAddress
            };

            var employeeId = await _employeeRepository.AddEmployee(tenant.ConnectionString, employee);
            try
            {
                var identityResult = await _identityService.CreateUserAsync(employee.EmailAddress, request.Password, Roles.Employee, tenant.TenantId);
                await _unitOfWork.SaveChangesAsync();
                if (!identityResult.Succeeded)
                {
                    await _employeeRepository.DeleteEmployee(tenant.ConnectionString, employee);

                    return Result<string>.Failure(string.Join(", ", identityResult.Errors));
                }

                return Result<string>.Success($"Employee: {employeeId} added successfully.");
            }
            catch(Exception ex)
            {
                await _employeeRepository.DeleteEmployee(tenant.ConnectionString, employee);
                throw;
            }
        }
    }
}
