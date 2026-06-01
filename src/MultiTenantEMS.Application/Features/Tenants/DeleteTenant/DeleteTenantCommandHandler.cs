using MediatR;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Application.Features.Tenants.DeleteTenant
{
    internal class DeleteTenantCommandHandler(ITenantRepository tenantRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteTenantCommand>
    {
        async Task<Result> IRequestHandler<DeleteTenantCommand, Result>.Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            Tenant? tenant = await tenantRepository.GetTenantByTenantIdAsync(request.TenantId);
            if(tenant is null)
            {
                return Result.Failure("Tenant not found");
            }
            tenant.IsDeleted = true;
            await tenantRepository.UpdateTenantAsync(tenant);
            await unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}
