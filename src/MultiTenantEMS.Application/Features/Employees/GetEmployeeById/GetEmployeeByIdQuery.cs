using MultiTenantEMS.Application.Abstractions.Messaging;

namespace MultiTenantEMS.Application.Features.Employees.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IQuery<GetEmployeeByIdResponseDto>
    {
        public Guid Id { get; set; }
    }
}
