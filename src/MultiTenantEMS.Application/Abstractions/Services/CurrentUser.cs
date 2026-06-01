using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEMS.Application.Abstractions.Services
{
    public class CurrentUser
    {
        public string TenantId { get; set; }
        public string ConnectionString { get; set; }
    }
}
