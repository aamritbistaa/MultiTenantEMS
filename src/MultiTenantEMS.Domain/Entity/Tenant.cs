using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEMS.Domain.Entity
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name{ get; set; }
        public string EmailAddress { get; set; }
        public string TenantId{ get; set; }
        public string DbConnStr{ get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
