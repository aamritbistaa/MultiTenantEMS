using Microsoft.Extensions.Logging;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Application.Features.Employees.GetEmployeeById;

namespace MultiTenantEMS.Application.Features.Employees.GetEmployees
{
    internal class GetEmployeeQueryHandler : IQueryHandler<GetEmployeeQuery, GetEmployeeQueryResponseDto>
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<GetEmployeeQueryHandler> _logger;
        public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository, ILogger<GetEmployeeQueryHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<Result<GetEmployeeQueryResponseDto>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allEmployees = await _employeeRepository.GetEmployees(request.Skip, request.Take);
                var count = await _employeeRepository.CountEmployees();
                
                var response = new GetEmployeeQueryResponseDto
                {
                    Skip = request.Skip,
                    Take = request.Take,
                    Count = count,
                    Data = allEmployees.Select(e => new GetEmployeeByIdResponseDto
                    {
                        Id = e.Id,
                        FullName = e.FullName,
                        EmailAddress = e.EmailAddress,
                    }).ToList()
                };
                _logger.LogInformation("Successfully retrieved employees. Skip: {Skip}, Take: {Take}, Count: {Count}", request.Skip, request.Take, count);
                return Result<GetEmployeeQueryResponseDto>.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while retrieving employees. Skip: {Skip}, Take: {Take}", request.Skip, request.Take);
                return Result<GetEmployeeQueryResponseDto>.Failure("An error occurred while retrieving employees. Please try again later.", ApiResponseCode.InternalServerError);
            }
        }
    }
}
