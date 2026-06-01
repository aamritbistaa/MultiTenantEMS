using MediatR;
using MultiTenantEMS.Application.Abstractions.Authentication;
using MultiTenantEMS.Application.Abstractions.Messaging;
using MultiTenantEMS.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEMS.Application.Features.Authentication.ChangePassword
{
    internal class UpdatePasswordCommandHandler(IIdentityService identityService) : ICommandHandler<UpdatePasswordCommand>
    {
        public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await identityService.ChangePassword(request.EmailAddress, request.CurrentPassword, request.NewPassword);

                if (!result.Succeeded)
                {
                    return Result.Failure(string.Join(", ", result.Errors));
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
