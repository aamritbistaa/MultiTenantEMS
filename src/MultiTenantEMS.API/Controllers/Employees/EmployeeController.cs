using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Application.Features.Employees.CreateEmployee;
using MultiTenantEMS.Application.Features.Employees.DeleteEmployee;
using MultiTenantEMS.Application.Features.Employees.GetEmployeeById;
using MultiTenantEMS.Application.Features.Employees.GetEmployees;
using MultiTenantEMS.Application.Features.Employees.UpdateEmployee;

namespace MultiTenantEMS.API.Controllers.Employees
{
    [Route("api/v1/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly ISender _sender;
        public EmployeeController(ILogger<EmployeeController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<Result<string>> CreateEmployee(CreateEmployeeCommand request)
        {
            _logger.LogInformation("Create employee initiated with name: {Name}, email: {Email}", request.FullName, request.EmailAddress);
            var result = await _sender.Send(request);
            return result;
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<Result> UpdateEmployee([FromRoute] Guid id, [FromBody] string fullName)
        {
            _logger.LogInformation("Update employee initiated with name: {Name}", fullName);
            var result = await _sender.Send(new UpdateEmployeeCommand()
            {
                Id = id,
                FullName = fullName
            });
            return result;
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<Result> DeleteEmployee([FromRoute] Guid id)
        {
            _logger.LogInformation("delete employee initiated with id: {Id}", id);
            var result = await _sender.Send(new DeleteEmployeeCommand()
            {
                Id = id
            });
            return result;
        }
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<Result<GetEmployeeQueryResponseDto>> GetEmployee([FromQuery] GetEmployeeQuery request)
        {
            _logger.LogInformation("Get all employees initiated");
            return await _sender.Send(request);
        }
        [HttpGet("{id:Guid}")]
        //[Authorize(Policy = "AdminOrEmployee")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Employee}")]
        public async Task<Result<GetEmployeeByIdResponseDto>> GetEmployeeById(Guid id)
        {
            _logger.LogInformation("Get employee {Id} initiated", id);
            return await _sender.Send(new GetEmployeeByIdQuery{ Id = id });
        }
    }
}
