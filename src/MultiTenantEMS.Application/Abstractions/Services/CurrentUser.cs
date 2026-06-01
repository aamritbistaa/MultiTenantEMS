
namespace MultiTenantEMS.Application.Abstractions.Services
{
    public class CurrentUser
    {
        public Guid Id { get; set; }
        public string TenantId { get; set; }
        public string ConnectionString { get; set; }
    }
}
