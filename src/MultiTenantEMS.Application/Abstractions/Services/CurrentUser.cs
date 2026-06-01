
namespace MultiTenantEMS.Application.Abstractions.Services
{
    public class CurrentUser
    {
        public string TenantId { get; set; }
        public string ConnectionString { get; set; }
    }
}
