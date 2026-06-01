using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Application.Abstractions.Persistence
{
    public interface ITenantRepository
    {
        Task<Guid> AddTenantAsync(Tenant tenant, CancellationToken cancellationToken = default);
        Task UpdateTenantAsync(Tenant tenant, CancellationToken cancellationToken = default);
        Task<Tenant?> GetTenantByIdAsync(Guid id);
        Task<bool> IsTenantExistAsync(string tenantId, CancellationToken cancellationToken = default);
        Task<bool> IsTenantEmailExistAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> IsTenantIdExistsExceptAsync(string tenantId, Guid id, CancellationToken cancellationToken = default);
        Task<Tenant?> GetTenantByTenantIdAsync(string tenantId, CancellationToken cancellationToken = default);
        Task<List<Tenant>> GetAllTenantsAsync(int skip, int take, CancellationToken cancellationToken = default);
        Task<int> CountAllTenantsAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
