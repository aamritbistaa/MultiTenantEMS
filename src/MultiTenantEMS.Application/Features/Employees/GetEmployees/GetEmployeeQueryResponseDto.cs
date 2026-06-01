using MultiTenantEMS.Application.Features.Employees.GetEmployeeById;

namespace MultiTenantEMS.Application.Features.Employees.GetEmployees
{
    public class GetEmployeeQueryResponseDto
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Count { get; set; }
        public List<GetEmployeeByIdResponseDto> Data { get; set; } = new();
    }
}
