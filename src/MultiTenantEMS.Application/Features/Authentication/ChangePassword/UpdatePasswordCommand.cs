using MultiTenantEMS.Application.Abstractions.Messaging;

namespace MultiTenantEMS.Application.Features.Authentication.ChangePassword
{
    public class UpdatePasswordCommand : ICommand
    {
        public Guid UserId { get; set; }
        public string EmailAddress { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
