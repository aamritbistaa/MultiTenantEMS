using Microsoft.EntityFrameworkCore;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Abstractions.Services;
using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Infrastructure.Persistence.TenantDb.Repository
{
    internal class EmployeeRepository : IEmployeeRepository, IAsyncDisposable
    {
        private readonly ITenantDbContextFactory _factory;
        private TenantDbContext? _context;
        private readonly ICurrentUserService _currentUserService;
        public EmployeeRepository(ITenantDbContextFactory factory, ICurrentUserService currentUserService)
        {
            _factory = factory;
            _currentUserService = currentUserService;
        }
        private async Task<TenantDbContext> GetContextAsync()
        {
            if (_context is null)
            {
                var connectionString = await _currentUserService.GetConnectionString();
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new Exception("Could not resolve tenant connection string from the current user session.");
                }
                _context = _factory.Create(connectionString);
            }
            return _context;
        }
        public async Task<Employee?> GetEmployeeById(Guid id)
        {
            var context = await GetContextAsync();
            return await context.Employees.FindAsync(id);
        }
        public async Task UpdateEmployee(Employee employee)
        {
            var context = await GetContextAsync();
            context.Employees.Update(employee);
            await context.SaveChangesAsync();
        }
        public async Task<Guid> AddEmployee(Employee employee)
        {
            var context = await GetContextAsync();
            context.Employees.Add(employee);
            await context.SaveChangesAsync();
            return employee.Id;
        }
        public async Task DeleteEmployee(Employee employee)
        {
            var context = await GetContextAsync();
            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
        }
        public async Task<List<Employee>> GetEmployees(int skip, int take)
        {
            var context = await GetContextAsync();
            return await context.Employees.AsNoTracking().Where(x => !x.IsDeleted).Skip(skip).Take(take).ToListAsync();
        }
        public async Task<int> CountEmployees()
        {
            var context = await GetContextAsync();
            return await context.Employees.CountAsync(x => !x.IsDeleted);
        }
        public async ValueTask DisposeAsync()
        {
            if (_context is not null)
            {
                await _context.DisposeAsync();
            }
        }
    }
}
