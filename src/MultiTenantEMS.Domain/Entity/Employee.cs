namespace MultiTenantEMS.Domain.Entity
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
