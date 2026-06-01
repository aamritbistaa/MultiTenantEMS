
namespace MultiTenantEMS.Application.Features.Employees.GetEmployeeById
{
    public class GetEmployeeByIdResponseDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
    }
}
