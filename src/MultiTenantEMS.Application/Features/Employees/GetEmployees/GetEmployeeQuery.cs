using MultiTenantEMS.Application.Abstractions.Messaging;

namespace MultiTenantEMS.Application.Features.Employees.GetEmployees
{
    public class GetEmployeeQuery : IQuery<GetEmployeeQueryResponseDto>
    {
        public int Skip { get; set; }
        public int Take { get; set; } = 10;
    }
}
