using Microsoft.EntityFrameworkCore;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Infrastructure.Persistence.MasterDb.Repositories
{
    internal class TenantRepository : ITenantRepository
    {
        private readonly MasterDbContext _context;

        public TenantRepository(MasterDbContext context, CancellationToken cancellationToken = default)
        {
            _context = context;
        }

        public async Task<Guid> AddTenantAsync(Tenant tenant, CancellationToken cancellationToken = default)
        {
            await _context.Tenants.AddAsync(tenant);
            return tenant.Id;
        }

        public async Task<Tenant?> GetTenantByIdAsync(Guid id)
        {
            return await _context.Tenants.FindAsync(id);
        }

        public async Task<bool> IsTenantExistAsync(string tenantId, CancellationToken cancellationToken = default)
        {
            return await _context.Tenants.AnyAsync(t => t.TenantId == tenantId, cancellationToken);
        }

        public async Task<bool> IsTenantEmailExistAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Tenants.AnyAsync(t => t.EmailAddress == email, cancellationToken);
        }

        public async Task<bool> IsTenantIdExistsExceptAsync(string tenantId, Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Tenants.AnyAsync(t => t.TenantId == tenantId && t.Id != id, cancellationToken);
        }

        public async Task UpdateTenantAsync(Tenant tenant, CancellationToken cancellationToken = default)
        {
            _context.Tenants.Update(tenant);
        }
        public async Task<Tenant?> GetTenantByTenantIdAsync(string tenantId, CancellationToken cancellationToken = default)
        {
            return await _context.Tenants.AsNoTracking().FirstOrDefaultAsync(t => t.TenantId == tenantId, cancellationToken);
        }
        public async Task<List<Tenant>> GetAllTenantsAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return await _context.Tenants.AsNoTracking().Where(x => !x.IsDeleted).Skip(skip).Take(take).ToListAsync(cancellationToken);
        }
        public async Task<int> CountAllTenantsAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return await _context.Tenants.AsNoTracking().CountAsync(x => !x.IsDeleted);
        }
    }
}
