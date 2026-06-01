using MultiTenantEMS.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEMS.Application.Features.Tenants.GetTenantById
{
    public class GetTenantByIdQuery : IQuery<GetTenantByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
