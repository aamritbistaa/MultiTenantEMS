using MediatR;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Application.Features.Tenants.UpdateTenant
{
    internal class UpdateTenantCommandHandler(ITenantRepository tenantRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateTenantCommand>
    {
        async Task<Result> IRequestHandler<UpdateTenantCommand, Result>.Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
        {
            Tenant? tenant = await tenantRepository.GetTenantByIdAsync(request.Id);
            if(tenant is null)
            {
                return Result.Failure($"Tenant with id {request.Id} not found.");
            }

            bool isTenantExist = await tenantRepository.IsTenantIdExistsExceptAsync(request.TenantId, request.Id);
            if (isTenantExist)
            {
                return Result.Failure($"Tenant with id {request.TenantId} already exists.");
            }

            tenant.Name = request.Name;
            tenant.EmailAddress = request.EmailAddress;
            tenant.TenantId = request.TenantId;
            await tenantRepository.UpdateTenantAsync(tenant);
            await unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}
