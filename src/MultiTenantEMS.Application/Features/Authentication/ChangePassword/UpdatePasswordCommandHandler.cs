using Microsoft.Extensions.Logging;
using MultiTenantEMS.Application.Abstractions.Authentication;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Common;
namespace MultiTenantEMS.Application.Features.Authentication.ChangePassword
{
    internal class UpdatePasswordCommandHandler : ICommandHandler<UpdatePasswordCommand>
    {
        private readonly ILogger<UpdatePasswordCommandHandler> _logger;
        private readonly IIdentityService _identityService;
        public UpdatePasswordCommandHandler(ILogger<UpdatePasswordCommandHandler> logger, IIdentityService identityService)
        {
            _logger = logger;
            _identityService = identityService;
        }

        public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _identityService.ChangePassword(request.EmailAddress, request.CurrentPassword, request.NewPassword);

                if (!result.Succeeded)
                {
                    return Result.Failure(string.Join(", ", result.Errors));
                }

                _logger.LogInformation("Password changed successfully for user {Email}", request.EmailAddress);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while changing password for user {Email}", request.EmailAddress);
                return Result.Failure("An error occurred while changing the password.", ApiResponseCode.InternalServerError);
            }
        }
    }
}
