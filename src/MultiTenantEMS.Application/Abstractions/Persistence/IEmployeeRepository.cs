using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Application.Abstractions.Persistence
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetEmployeeById(string connectionString, Guid id);
        Task UpdateEmployee(string connectionString, Employee employee);
        Task<Guid> AddEmployee(string connectionString, Employee employee);
        Task<List<Employee>> GetEmployees(string connectionString, int skip, int take);
        Task DeleteEmployee(string connectionString, Employee employee);
        Task<int> CountEmployees(string connectionString);
    }
}
