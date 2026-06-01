using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Features.Employees.GetEmployees;

namespace MultiTenantEMS.Application.Features.Employees.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IQuery<GetEmployeeByIdResponseDto>
    {
        public Guid Id { get; set; }
    }
}
