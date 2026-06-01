using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Abstractions.Persistence;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MultiTenantEMS.Application.Features.Tenants.GetTenantById
{
    internal class GetTenantByIdQueryHandler(ITenantRepository tenantRepository) : IQueryHandler<GetTenantByIdQuery, GetTenantByIdResponse>
    {
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
            var tenant = await tenantRepository.GetTenantByIdAsync(request.Id);
            if (tenant == null)
            {
                return Result<GetTenantByIdResponse>.Failure("Tenant not found.");
            }
            var responseDto = this.MapTenantEntityToResponseDto(tenant);
            return Result<GetTenantByIdResponse>.Success(responseDto);
        }
    }
}