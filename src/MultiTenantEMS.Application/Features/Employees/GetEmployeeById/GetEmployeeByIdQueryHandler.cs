using Microsoft.Extensions.Logging;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Common;

namespace MultiTenantEMS.Application.Features.Employees.GetEmployeeById
{
    internal class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponseDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<GetEmployeeByIdQueryHandler> _logger;
        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, ILogger<GetEmployeeByIdQueryHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<Result<GetEmployeeByIdResponseDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeById(request.Id);
                var response = new GetEmployeeByIdResponseDto()
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    EmailAddress = employee.EmailAddress
                };

                return Result<GetEmployeeByIdResponseDto>.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while processing GetEmployeeByIdQuery for Id: {Id}", request.Id);
                return Result<GetEmployeeByIdResponseDto>.Failure("An unexpected error occurred while retrieving the employee details. Please try again later.", ApiResponseCode.InternalServerError);
            }
        }
    }
}
