using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Abstractions.Services;
using MultiTenantEMS.Application.Common;

namespace MultiTenantEMS.Application.Features.Employees.GetEmployeeById
{
    internal class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponseDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeeByIdQueryHandler(ICurrentUserService currentUserService, IEmployeeRepository employeeRepository)
        {
            _currentUserService = currentUserService;
            _employeeRepository = employeeRepository;
        }

        public async Task<Result<GetEmployeeByIdResponseDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _currentUserService.GetCurrentTenant();

            var employee = await _employeeRepository.GetEmployeeById(tenant.ConnectionString, request.Id);
            var response = new GetEmployeeByIdResponseDto()
            {
                Id = employee.Id,
                FullName = employee.FullName,
                EmailAddress = employee.EmailAddress
            };

            return Result<GetEmployeeByIdResponseDto>.Success(response);
        }
    }
}
                    