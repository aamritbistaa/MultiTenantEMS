using Microsoft.EntityFrameworkCore;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Infrastructure.Persistence.TenantDb.Repository
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly ITenantDbContextFactory _factory;
        public EmployeeRepository(ITenantDbContextFactory factory)
        {
            _factory = factory;
        }
        public async Task<Employee?> GetEmployeeById(string connectionString, Guid id)
        {
            await using var tenantDbContext = _factory.Create(connectionString);
            return await tenantDbContext.Employees.FindAsync(id);
        }
        public async Task UpdateEmployee(string connectionString, Employee employee)
        {
            await using var tenantDbContext = _factory.Create(connectionString);
            tenantDbContext.Employees.Update(employee);
            await tenantDbContext.SaveChangesAsync();
        }
        public async Task<Guid> AddEmployee(string connectionString, Employee employee)
        {
            await using var tenantDbContext = _factory.Create(connectionString);
            tenantDbContext.Employees.Add(employee);
            await tenantDbContext.SaveChangesAsync();
            return employee.Id;
        }
        public async Task DeleteEmployee(string connectionString, Employee employee)
        {
            await using var tenantDbContext = _factory.Create(connectionString);
            tenantDbContext.Employees.Remove(employee);
            await tenantDbContext.SaveChangesAsync();
        }
        public async Task<List<Employee>> GetEmployees(string connectionString, int skip, int take)
        {
            await using var tenantDbContext = _factory.Create(connectionString);
            return await tenantDbContext.Employees.Where(x => !x.IsDeleted).Skip(skip).Take(take).ToListAsync();
        }
        public async Task<int> CountEmployees(string connectionString)
        {
            await using var tenantDbContext = _factory.Create(connectionString);
            return await tenantDbContext.Employees.CountAsync();
        }
    }
}
