# Multi-Tenant Employee Management System

A multi-tenant Employee Management System built with **ASP.NET Core 8**, **PostgreSQL**, **Clean Architecture**, **CQRS**, and **Microsoft Identity**. Each tenant has its own isolated PostgreSQL database for complete data isolation.

## Table Of Content
* #### [Architecture Overview](docs/Architectural%20Overview.md)
* #### [Setup Instructions](docs/Setup%20Instructions.md)
* #### [Database Migration Guide](#database-migration)
* #### [Tenant Database Creation Flow](docs/Tenant%20Database%20Creation%20Flow.md)
* #### [API Examples](docs/Api%20Examples.md)
* #### [Seeded Super Admin Credentials](#super-admin-credential)


---
### Functionality

| User Role | Capabilities |
|-----------|-------------|
| **SuperAdmin** | Create/Update/Delete tenants, manage all tenants |
| **Admin** | Create/Update/Delete employees, manage employees, change password |
| **Employee** | View own profile, change password, cannot access other employees |
 
### What it does

* Super admin can manage tenants
* Each tenant gets its own isolated database
* Admins can manage employees inside their tenant
* Employees can only view their own profile
* Authentication is handled using JWT + ASP.NET Identity

### Assumptions
- Delete referring to soft delete
- String as Primary key refers to text for psql, which when used Guid as pk would also reflect as text, so i assumed Id to be guid
- Exception messages are displayed only in logs, the request's show the generic messages in case of exception
- The deleted tenant cannot be readded with same email and tenant id

### Known issues

* Deleted users can still log in
* Identity records are not cleaned up when tenant/user is deleted
* JWT token expiry is set to 2 hours


## Database Migration

Currently, migrations are applied automatically on startup for the master and identity databases. If we want to run migrations manually, we need to update the master database connection string in appsettings.json. Additionally, changes are required in DependencyInjection and TenantDatabaseManager within the Infrastructure project.

### 📌 Create Migration for Master Database

```bash
dotnet ef migrations add MasterInit \
  --context MasterDbContext \
  --project src/MultiTenantEMS.Infrastructure \
  --startup-project src/MultiTenantEMS.API \
  --output-dir Persistence/MasterDb/Migrations
```


### 📌 Create Migration for Tenant Database

```bash
dotnet ef migrations add EmployeeInit \
  --context TenantDbContext \
  --project src/MultiTenantEMS.Infrastructure \
  --startup-project src/MultiTenantEMS.API \
  --output-dir Persistence/TenantDb/Migrations
```

---

## Super Admin Credential
{
  "emailAddress": "assessment@yopmail.com",
  "password": "Tester@123"
}

---

