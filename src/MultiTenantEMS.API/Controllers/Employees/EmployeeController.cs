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
        /// <summary>
        /// Get all employees with pagination
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<Result<GetEmployeeQueryResponseDto>> GetEmployee([FromQuery] GetEmployeeQuery request)
        {
            _logger.LogInformation("Get all employees initiated");
            return await _sender.Send(request);
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:Guid}")]
        [Authorize(Policy = "AdminOrEmployee")]
        public async Task<Result<GetEmployeeByIdResponseDto>> GetEmployeeById(Guid id)
        {
            _logger.LogInformation("Get employee {Id} initiated", id);
            return await _sender.Send(new GetEmployeeByIdQuery { Id = id });
        }

        /// <summary>
        /// Create a new employee
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<Result<string>> CreateEmployee(CreateEmployeeCommand request)
        {
            _logger.LogInformation("Create employee initiated with name: {Name}, email: {Email}", request.FullName, request.EmailAddress);
            var result = await _sender.Send(request);
            return result;
        }

        /// <summary>
        /// Update an existing employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id:Guid}")]
        [Authorize(Policy = "Admin")]
        public async Task<Result> UpdateEmployee([FromRoute] Guid id, [FromBody] UpdateEmployeeCommandRequestDto request)
        {
            _logger.LogInformation("Update employee initiated with name: {Name}", request.FullName);
            var result = await _sender.Send(new UpdateEmployeeCommand()
            {
                Id = id,
                FullName = request.FullName
            });
            return result;
        }

        /// <summary>
        /// Delete employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "Admin")]
        public async Task<Result> DeleteEmployee([FromRoute] Guid id)
        {
            _logger.LogInformation("Delete employee initiated with id: {Id}", id);
            var result = await _sender.Send(new DeleteEmployeeCommand()
            {
                Id = id
            });
            return result;
        }
    }
}
