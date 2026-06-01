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
