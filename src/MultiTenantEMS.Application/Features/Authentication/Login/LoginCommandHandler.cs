using Microsoft.Extensions.Logging;
using MultiTenantEMS.Application.Abstractions.Authentication;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Common;

namespace MultiTenantEMS.Application.Features.Authentication.Login
{
    internal class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtProvider _jwtProvider;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(IIdentityService identityService, IJwtProvider jwtProvider, ILogger<LoginCommandHandler> logger)
        {
            _identityService = identityService;
            _jwtProvider = jwtProvider;
            _logger = logger;
        }
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _identityService.AuthenticateAsync(request.EmailAddress, request.Password);

                if (user is null)
                {
                    _logger.LogWarning("Failed login attempt for user {Email}", request.EmailAddress);
                    return Result<LoginResponse>.Failure("Invalid email or password");
                }

                string token = _jwtProvider.GenerateToken(user.UserId, user.Email, user.Role, user.TenantId);
                var response = new LoginResponse()
                {
                    Token = token,
                };

                _logger.LogInformation("User logged in successfully: {Email}", request.EmailAddress);
                return Result<LoginResponse>.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while processing the login request for user {Email}", request.EmailAddress);
                return Result<LoginResponse>.Failure("An error occurred while processing the login request", ApiResponseCode.InternalServerError);
            }
        }
    }
}
