using Microsoft.Extensions.Logging;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Application.Features.Tenants.DeleteTenant
{
    internal class DeleteTenantCommandHandler : ICommandHandler<DeleteTenantCommand>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteTenantCommandHandler> _logger;
        public DeleteTenantCommandHandler(ITenantRepository tenantRepository, IUnitOfWork unitOfWork, ILogger<DeleteTenantCommandHandler> logger)
        {
            _tenantRepository = tenantRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Tenant? tenant = await _tenantRepository.GetTenantByTenantIdAsync(request.TenantId);
                if (tenant is null)
                {
                    return Result.Failure("Tenant not found");
                }
                tenant.IsDeleted = true;
                await _tenantRepository.UpdateTenantAsync(tenant);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Tenant with id {TenantId} deleted", request.TenantId);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while deleting tenant with id {TenantId}", request.TenantId);
                return Result.Failure("An error occurred while deleting tenant", ApiResponseCode.InternalServerError);
            }
        }

    }
}
