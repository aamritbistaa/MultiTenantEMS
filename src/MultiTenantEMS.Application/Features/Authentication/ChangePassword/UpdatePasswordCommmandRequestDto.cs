namespace MultiTenantEMS.Application.Features.Authentication.ChangePassword
{
    public class UpdatePasswordCommmandRequestDto
    {
        public string EmailAddress { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
