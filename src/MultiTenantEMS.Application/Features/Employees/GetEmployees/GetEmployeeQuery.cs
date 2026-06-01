using MultiTenantEMS.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEMS.Application.Features.Employees.GetEmployees
{
    public class GetEmployeeQuery : IQuery<GetEmployeeQueryResponseDto>
    {
        public Guid Id { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
