using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantEMS.Application.Abstractions.Services;
using MultiTenantEMS.Application.Common;
using MultiTenantEMS.Application.Features.Authentication.ChangePassword;
using MultiTenantEMS.Application.Features.Authentication.Login;

namespace MultiTenantEMS.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ICurrentUserService _currentUserService;
        public AuthController(ISender sender, ICurrentUserService currentUserService)
        {
            _sender = sender;
            _currentUserService = currentUserService;
        }

        [HttpPost("login")]
        //[AllowAnonymous]
        public async Task<Result<LoginResponse>> Login(LoginCommand command)
        {
            var result = await _sender.Send(command);
            return result;
        }

        [Authorize]
        [HttpPut("password")]
        public async Task<Result> UpdatePassword(UpdatePasswordCommand command)
        {
            command.UserId = _currentUserService.GetCurrentUserId();
            var result = await _sender.Send(command);
            return result;
        }
    }
}
