using MultiTenantEMS.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
