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
        private readonly ILogger<AuthController> _logger;
        public AuthController(ISender sender, ICurrentUserService currentUserService, ILogger<AuthController> logger)
        {
            _sender = sender;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<Result<LoginResponse>> Login(LoginCommand command)
        {
            _logger.LogInformation("Login attempt for email: {EmailAddress}", command.EmailAddress);
            var result = await _sender.Send(command);
            return result;
        }

        [Authorize]
        [HttpPut("password")]
        public async Task<Result> UpdatePassword(UpdatePasswordCommand command)
        {
            var userId = _currentUserService.GetCurrentUserId();
            _logger.LogInformation("Password update attempt for user ID: {UserId}", userId);
            command.UserId = userId;
            var result = await _sender.Send(command);
            return result;
        }
    }
}
