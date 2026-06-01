using Microsoft.Extensions.Logging;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Domain.Entity;

namespace MultiTenantEMS.Application.Features.Tenants.GetTenantById
{
    internal class GetTenantByIdQueryHandler : IQueryHandler<GetTenantByIdQuery, GetTenantByIdResponse>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly ILogger<GetTenantByIdQueryHandler> _logger;
        public GetTenantByIdQueryHandler(ITenantRepository tenantRepository, ILogger<GetTenantByIdQueryHandler> logger)
        {
            _tenantRepository = tenantRepository;
            _logger = logger;
        }

        private GetTenantByIdResponse MapTenantEntityToResponseDto(Tenant tenant)
        {
            return new GetTenantByIdResponse
            {
                Id = tenant.Id,
                Name = tenant.Name,
                EmailAddress = tenant.EmailAddress,
                TenantId = tenant.TenantId
            };
        }
        public async Task<Result<GetTenantByIdResponse>> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tenant = await _tenantRepository.GetTenantByIdAsync(request.Id);
                if (tenant == null)
                {
                    return Result<GetTenantByIdResponse>.Failure("Tenant not found.");
                }
                var responseDto = this.MapTenantEntityToResponseDto(tenant);
                _logger.LogInformation("Successfully retrieved tenant with ID {TenantId}", request.Id);
                return Result<GetTenantByIdResponse>.Success(responseDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while retrieving tenant with ID {TenantId}", request.Id);
                return Result<GetTenantByIdResponse>.Failure("An error occurred while processing your request. Please try again later.", ApiResponseCode.InternalServerError);
            }
        }
    }
}