using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Application.Features.Tenants.CreateTenant;
using MultiTenantEMS.Application.Features.Tenants.DeleteTenant;
using MultiTenantEMS.Application.Features.Tenants.GetTenantById;
using MultiTenantEMS.Application.Features.Tenants.GetTenants;
using MultiTenantEMS.Application.Features.Tenants.UpdateTenant;

namespace MultiTenantEMS.API.Controllers.Tenants
{
    [Route("api/v1/tenant")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<TenantController> _logger;
        public TenantController(ISender sender, ILogger<TenantController> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Policy = "SuperAdmin")]
        public async Task<Result<GetTenantByIdResponse>> GetTenantByTenantId(Guid id)
        {
            _logger.LogInformation("Get tenant {Id} initiated", id);
            return await _sender.Send(new GetTenantByIdQuery() { Id = id });
        }

        [HttpGet]
        [Authorize(Policy = "SuperAdmin")]
        public async Task<Result<GetTenantResponse>> GetTenant([FromQuery] GetTenantsQuery request)
        {
            _logger.LogInformation("Get all tenants initiated");
            return await _sender.Send(request);
        }

        [HttpPost]
        [Authorize(Policy = "SuperAdmin")]
        public async Task<Result<string>> CreateTenant(CreateTenantCommand request)
        {
            _logger.LogInformation("Create tenant initiated with name: {Name}, email: {Email}, tenantId: {TenantId}", request.Name, request.EmailAddress, request.TenantId);
            var result = await _sender.Send(request);
            return result;
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Policy = "SuperAdmin")]
        public async Task<Result> UpdateTenant([FromRoute]Guid id, [FromBody] UpdateTenantCommandRequestDto request)
        {
            _logger.LogInformation("Update tenant initiated id: {Id}", id);
            var command = new UpdateTenantCommand()
            {
                Id = id,
                EmailAddress = request.EmailAddress,
                Name = request.Name,
                TenantId = request.TenantId
            };
            return await _sender.Send(command);
        }

        [HttpDelete("{tenantId}")]
        [Authorize(Policy = "SuperAdmin")]
        public async Task<Result> DeleteTenant(string tenantId)
        {
            _logger.LogInformation("Delete tenant initiated tenantId: {TenantId}", tenantId);
            return await _sender.Send(new DeleteTenantCommand { TenantId = tenantId });
        }
    }
}
