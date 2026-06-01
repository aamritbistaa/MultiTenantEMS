using Microsoft.Extensions.Logging;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Application.Features.Tenants.GetTenantById;

namespace MultiTenantEMS.Application.Features.Tenants.GetTenants
{
    internal class GetTenantsQueryHandler : IQueryHandler<GetTenantsQuery, GetTenantResponse>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly ILogger<GetTenantsQueryHandler> _logger;
        public GetTenantsQueryHandler(ITenantRepository tenantRepository, ILogger<GetTenantsQueryHandler> logger)
        {
            _tenantRepository = tenantRepository;
            _logger = logger;
        }

        public async Task<Result<GetTenantResponse>> Handle(GetTenantsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var count = await _tenantRepository.CountAllTenantsAsync(request.Skip, request.Take);
                var data = await _tenantRepository.GetAllTenantsAsync(request.Skip, request.Take);

                var response = new GetTenantResponse
                {
                    Count = count,
                    Skip = request.Skip,
                    Take = request.Take,
                    Data = data.Select(tenant => new GetTenantByIdResponse
                    {
                        Id = tenant.Id,
                        Name = tenant.Name,
                        EmailAddress = tenant.EmailAddress,
                        TenantId = tenant.TenantId,
                    }).ToList()
                };
                _logger.LogInformation("Retrieved {Count} tenants with Skip={Skip} and Take={Take}", count, request.Skip, request.Take);
                return Result<GetTenantResponse>.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while retrieving tenants with Skip={Skip} and Take={Take}", request.Skip, request.Take);
                return Result<GetTenantResponse>.Failure("An error occurred while retrieving tenants. Please try again later.", ApiResponseCode.InternalServerError);
            }
        }
    }
}
