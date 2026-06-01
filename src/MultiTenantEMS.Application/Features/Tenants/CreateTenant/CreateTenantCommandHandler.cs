using MediatR;
using Microsoft.Extensions.Logging;
using MultiTenantEMS.Application.Abstractions.Authentication;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Abstractions.Services;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Application.Features.Tenants.CreateTenant
{
    internal class CreateTenantCommandHandler : ICommandHandler<CreateTenantCommand, string>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantDatabaseManager _tenantDatabaseManager;
        private readonly ITenantMigrationService _tenantMigrationService;
        private readonly ILogger<CreateTenantCommandHandler> _logger;
        public CreateTenantCommandHandler(ITenantRepository tenantRepository, IIdentityService identityService, IUnitOfWork unitOfWork, ITenantDatabaseManager tenantDatabaseManager, ITenantMigrationService tenantMigrationService, ILogger<CreateTenantCommandHandler> logger)
        {
            _tenantRepository = tenantRepository;
            _identityService = identityService;
            _unitOfWork = unitOfWork;
            _tenantDatabaseManager = tenantDatabaseManager;
            _tenantMigrationService = tenantMigrationService;
            _logger = logger;
        }
        async Task<Result<string>> IRequestHandler<CreateTenantCommand, Result<string>>.Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool isTenantExist = await _tenantRepository.IsTenantExistAsync(request.TenantId);
                if (isTenantExist)
                {
                    return Result<string>.Failure($"Tenant with id {request.TenantId} already exists.");
                }

                bool isTenantEmailExist = await _tenantRepository.IsTenantEmailExistAsync(request.EmailAddress);
                if (isTenantEmailExist)
                {
                    return Result<string>.Failure($"Tenant with email {request.EmailAddress} already exists.");
                }

                var connectionString = await _tenantDatabaseManager.CreateDatabaseAsync(request.TenantId);

                await _tenantMigrationService.MigrateAsync(connectionString);

                var tenant = new Tenant()
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    EmailAddress = request.EmailAddress,
                    TenantId = request.TenantId,
                    DbConnStr = connectionString,
                };


                var tenantId = await _tenantRepository.AddTenantAsync(tenant);

                // Create Admin User
                var password = request.Password;
                var result = await _identityService.CreateUserAsync(request.EmailAddress, password, Roles.Admin, request.TenantId);

                if (!result.Succeeded)
                {
                    return Result<string>.Failure(string.Join(", ", result.Errors));
                }

                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Tenant {TenantId} created successfully with admin user {EmailAddress}.", tenantId, request.EmailAddress);
                return Result<string>.Success($"Tenant {tenantId} created successfully.");
            }
            catch (Exception ex)
            {
                if(await _tenantDatabaseManager.DatabaseExistsAsync(request.TenantId))
                {
                    await _tenantDatabaseManager.DropDatabaseAsync(request.TenantId);
                }
                _logger.LogCritical(ex, "An error occurred while creating tenant {TenantId}.", request.TenantId);
                return Result<string>.Failure(ex.Message, ApiResponseCode.InternalServerError);
            }
        }
    }
}
