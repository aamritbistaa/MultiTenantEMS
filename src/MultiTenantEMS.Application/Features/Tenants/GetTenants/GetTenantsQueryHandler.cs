using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Application.Features.Tenants.GetTenantById;

namespace MultiTenantEMS.Application.Features.Tenants.GetTenants
{
    internal class GetTenantsQueryHandler(ITenantRepository tenantRepository) : IQueryHandler<GetTenantsQuery, GetTenantResponse>
    {
        public async Task<Result<GetTenantResponse>> Handle(GetTenantsQuery request, CancellationToken cancellationToken)
        {
            var countTask = tenantRepository.CountAllTenantsAsync(request.Skip, request.Take);
            var dataTask = tenantRepository.GetAllTenantsAsync(request.Skip, request.Take);

            await Task.WhenAll(countTask, dataTask);

            var count = await countTask;
            var data = await dataTask;

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

            return Result<GetTenantResponse>.Success(response);
        }
    }
}
