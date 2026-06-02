using MediatR;
using Microsoft.Extensions.Logging;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Application.Features.Tenants.UpdateTenant
{
    internal class UpdateTenantCommandHandler : ICommandHandler<UpdateTenantCommand>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateTenantCommandHandler> _logger;
        public UpdateTenantCommandHandler(ITenantRepository tenantRepository, IUnitOfWork unitOfWork, ILogger<UpdateTenantCommandHandler> logger)
        {
            _tenantRepository = tenantRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        async Task<Result> IRequestHandler<UpdateTenantCommand, Result>.Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Tenant? tenant = await _tenantRepository.GetTenantByIdAsync(request.Id);
                if (tenant is null)
                {
                    return Result.Failure($"Tenant with id {request.Id} not found.");
                }

                bool isTenantExist = await _tenantRepository.IsTenantIdExistsExceptAsync(request.TenantId, request.Id);
                if (isTenantExist)
                {
                    return Result.Failure($"Tenant with id {request.TenantId} already exists.");
                }

                tenant.Name = request.Name;
                tenant.TenantId = request.TenantId;
                await _tenantRepository.UpdateTenantAsync(tenant);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Tenant with id {TenantId} updated successfully.", tenant.Id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while updating tenant with id {TenantId}.", request.Id);
                return Result.Failure("An error occurred while updating tenant. Please try again later.", ApiResponseCode.InternalServerError);
            }
        }
    }
}
