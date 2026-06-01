## Creating a new migration 
`dotnet ef migrations add "MasterInit" --context MasterDbContext --project src/MultiTenantEMS.Infrastructure/ --startup-project src/MultiTenantEMS.API`
"MasterDb": "Host=localhost;Port=5432;Database=MasterDb;Username=postgres;Password=P@ssw0rd;TimeZone=Asia/Kathmandu;"

## Updating migration
`dotnet ef database update --context MasterDbContext --project src/MultiTenantEMS.Infrastructure/ --startup-project src/MultiTenantEMS.API`
    "MasterDb": "Host=localhost;Port=5432;Database=MasterDb;Username=postgres;Password=P@ssw0rd;TimeZone=Asia/Kathmandu;"


//

dotnet ef migrations add EmployeeInit --context TenantDbContext --project src/MultiTenantEMS.Infrastructure --startup-project src/MultiTenantEMS.API --output-dir Persistence/TenantDb/Migrations

dotnet ef migrations add MasterInit --context MasterDbContext --project src/MultiTenantEMS.Infrastructure --startup-project src/MultiTenantEMS.API --output-dir Persistence/MasterDb/Migrations


//Login Credential for Super Admin 
```
{
  "emailAddress": "assessment@yopmail.com",
  "password": "Tester@123"
}
```


//Signup for Admin
```
{
  "name": "string",
  "emailAddress": "user@example.com",
  "tenantId": "stri",
  "password": "stringst"
}
```


Functionalities,
 - Super admin is Seeded
 - Super admin can Create, Update, View and Delete Tenant (Which in this is implemented just as soft delete) 
 - Admin (tenant) can register user (also referred as Employee) within their tenant
 - Employee can only view their own employee information
 - Users are able to just update their password
  

_Known issues
- When tenant are deleted still they can login and access the previlage as the tenants
- JWT token is valid for 2hr


_Tenant Registration
- Super admin signs up the tenant with responding email and password
- Will check if tenant id is unique or not
- Checks if tenant email is unique or not
- Creates Tenant database
- Adds Tenant to (Tenant table)
- Adds User Login (AspNetUser and ASPNetUserRoleTable)

For this demo i have not let the email to be changed, as it means not the email in tenant/employee be changed in identity associated database


Currently I am using just the soft delete functionality 
If hard delete functionality is needed, in case the tenant is deleted, need to delete the tenant associated database as well.

Known issue:
 -When the tenant or employee gets deleted, the Identity table are unchanged
  
Currently the tenant table has just the soft delete functionality
Employee also has just the soft delete functionality
