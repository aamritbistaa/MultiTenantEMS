using MediatR;
using MultiTenantEMS.Application.Abstractions.Authentication;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Abstractions.Services;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Application.Features.Tenants.CreateTenant
{
    internal class CreateTenantCommandHandler(ITenantRepository tenantRepository, IIdentityService identityService, IUnitOfWork unitOfWork, ITenantDatabaseManager tenantDatabaseManager, ITenantMigrationService tenantMigrationService) : ICommandHandler<CreateTenantCommand, string>
    {
        async Task<Result<string>> IRequestHandler<CreateTenantCommand, Result<string>>.Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool isTenantExist = await tenantRepository.IsTenantExistAsync(request.TenantId);
                if (isTenantExist)
                {
                    return Result<string>.Failure($"Tenant with id {request.TenantId} already exists.");
                }

                bool isTenantEmailExist = await tenantRepository.IsTenantEmailExistAsync(request.EmailAddress);
                if (isTenantEmailExist)
                {
                    return Result<string>.Failure($"Tenant with email {request.EmailAddress} already exists.");
                }

                var connectionString = await tenantDatabaseManager.CreateDatabaseAsync(request.TenantId);

                await tenantMigrationService.MigrateAsync(connectionString);

                var tenant = new Tenant()
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    EmailAddress = request.EmailAddress,
                    TenantId = request.TenantId,
                    DbConnStr = connectionString,
                };


                var tenantId = await tenantRepository.AddTenantAsync(tenant);

                // Create Admin User
                var password = request.Password;
                var result = await identityService.CreateUserAsync(request.EmailAddress, password, Roles.Admin, request.TenantId);

                if (!result.Succeeded)
                {
                    return Result<string>.Failure(string.Join(", ", result.Errors));
                }

                await unitOfWork.SaveChangesAsync();

                return Result<string>.Success($"Tenant {tenantId} created successfully.");
            }
            catch (Exception ex)
            {
                if(await tenantDatabaseManager.DatabaseExistsAsync(request.TenantId))
                {
                    await tenantDatabaseManager.DropDatabaseAsync(request.TenantId);
                }
                return Result<string>.Failure(ex.Message, ApiResponseCode.InternalServerError);
            }
        }
    }
}
