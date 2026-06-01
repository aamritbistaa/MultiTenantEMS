using MultiTenantEMS.Application.Abstractions.Authentication;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEMS.Application.Features.Authentication.Login
{
    internal class LoginCommandHandler(IIdentityService identityService, IJwtProvider jwtProvider) : ICommandHandler<LoginCommand, LoginResponse>
    {
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await identityService.AuthenticateAsync(request.EmailAddress, request.Password);

            if (user is null)
            {
                return Result<LoginResponse>.Failure(
                    "Invalid email or password");
            }

            string token = jwtProvider.GenerateToken(user.UserId, user.Email, user.Role, user.TenantId);
            var response = new LoginResponse()
            {
                Token = token,
            };

            return Result<LoginResponse>.Success(response);
        }
    }
}
