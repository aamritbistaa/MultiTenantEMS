using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Application.Abstractions.Persistence
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetEmployeeById(Guid id);
        Task UpdateEmployee(Employee employee);
        Task<Guid> AddEmployee(Employee employee);
        Task<List<Employee>> GetEmployees(int skip, int take);
        Task DeleteEmployee(Employee employee);
        Task<int> CountEmployees();
    }
}
