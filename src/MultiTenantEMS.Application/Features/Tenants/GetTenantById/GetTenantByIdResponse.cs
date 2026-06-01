namespace MultiTenantEMS.Application.Features.Tenants.GetTenantById
{
    public class GetTenantByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string TenantId { get; set; }
    }
}
