using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Abstractions.Services;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Application.Features.Employees.GetEmployeeById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEMS.Application.Features.Employees.GetEmployees
{
    internal class GetEmployeeQueryHandler : IQueryHandler<GetEmployeeQuery, GetEmployeeQueryResponseDto>
    {

        private readonly ICurrentUserService _currentUserService;
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeeQueryHandler(ICurrentUserService currentUserService, IEmployeeRepository employeeRepository)
        {
            _currentUserService = currentUserService;
            _employeeRepository = employeeRepository;
        }

        public async Task<Result<GetEmployeeQueryResponseDto>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _currentUserService.GetCurrentTenant();

            var employeeTask = _employeeRepository.GetEmployees(tenant.ConnectionString, request.Skip, request.Take);
            var countTask = _employeeRepository.CountEmployees(tenant.ConnectionString);

            await Task.WhenAll(employeeTask, countTask);

            var allEmployees = await employeeTask;
            var count = await countTask;

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

            return Result<GetEmployeeQueryResponseDto>.Success(response);
        }
    }
}
