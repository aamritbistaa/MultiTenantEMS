using MultiTenantEMS.Application.Abstractions.Messaging;

namespace MultiTenantEMS.Application.Features.Authentication.Login
{
    public class LoginCommand : ICommand<LoginResponse>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
