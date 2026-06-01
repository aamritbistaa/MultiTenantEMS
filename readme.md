# Project Overview - MultiTenant EMS

This is a multi-tenant Employee Management System built using ASP.NET Core 8, PostgreSQL, Clean Architecture, CQRS, and JWT authentication.

Each tenant has its own database, and access is controlled using roles.

---

## Roles
 - Super Admin
 - Admin
 - Employee

## Functionality
 - Super admin can Create/Update/Delete/View Tenant (Admins)
 - Tenant can login to the system and Create/Update/Delete/View Employee in the responding tenant db
 - Employee can view their information 
 
## What it does

* Super admin can manage tenants
* Each tenant gets its own isolated database
* Admins can manage employees inside their tenant
* Employees can only view their own profile
* Authentication is handled using JWT + ASP.NET Identity

## Assumptions
- Delete referring to soft delete
- String as Primary key refers to text for psql, which will be same for Guid also, so i assumed Id to be guid
- Exception messages are displayed only in logs, the request's show the generic messages in case of exception
- The deleted tenant cannot be readded with same email and tenant id

## Tech stack

* ASP.NET Core 8
* PostgreSQL
* Entity Framework Core
* ASP.NET Identity
* JWT
* Clean Architecture
* CQRS

## Known issues

* Soft deleted tenants can still log in
* Identity records are not cleaned up when tenant/user is deleted
* JWT token expiry is set to 2 hours

---

## Default Super Admin

```json
{
  "emailAddress": "assessment@yopmail.com",
  "password": "Tester@123"
}
```

---

## Database setup (EF Core)

Create migration for master DB:

```bash
dotnet ef migrations add UpdateUserTenantRelationship --context MasterDbContext --project src/MultiTenantEMS.Infrastructure --startup-project src/MultiTenantEMS.API --output-dir Persistence/MasterDb/Migrations
```
* It has been configured to support auto migrate on startup for MasterDbContext

Update database:

```bash
dotnet ef database update --context MasterDbContext --project src/MultiTenantEMS.Infrastructure --startup-project src/MultiTenantEMS.API
```

Create tenant DB migration:

```bash
dotnet ef migrations add EmployeeInit --context TenantDbContext --project src/MultiTenantEMS.Infrastructure --startup-project src/MultiTenantEMS.API --output-dir Persistence/TenantDb/Migrations
```

---

## Starting the Project
`cd docker`
`docker compose up`

To access swagger
http://localhost:5000/swagger/index.html

## APIs

### Health Check

| Method | Endpoint | Auth | Description |
|----------|----------|------|-------------|
| GET | /health | Anonymous | Application and database health status |

---

## Auth API (/api/v1/auth)

| Method | Endpoint | Auth | Description | Body |
|--------|----------|------|-------------|------|
| POST | /auth/login | Anonymous | Login user | { emailAddress, password } |
| PUT | /auth/password | Authenticated | Change password | { emailAddress, currentPassword, newPassword } |

---

## Tenant API (/api/v1/tenant)

| Method | Endpoint | Auth | Description | Params / Body |
|--------|----------|------|-------------|---------------|
| GET | /tenant | SuperAdmin | Get all tenants | skip, take |
| GET | /tenant/{id} | SuperAdmin | Get tenant by id | id (GUID) |
| POST | /tenant | SuperAdmin | Create tenant | { name, emailAddress, tenantId, password } |
| PUT | /tenant/{id} | SuperAdmin | Update tenant | { name, emailAddress, tenantId } |
| DELETE | /tenant/{tenantId} | SuperAdmin | Delete tenant | tenantId (string) |

---

## Employee API (/api/v1/employee)

| Method | Endpoint | Auth | Description | Params / Body |
|--------|----------|------|-------------|---------------|
| GET | /employee | Admin | Get all employees | skip, take |
| GET | /employee/{id} | AdminOrEmployee | Get employee by id | id (GUID) |
| POST | /employee | Admin | Create employee | { fullName, emailAddress, password } |
| PUT | /employee/{id} | Admin | Update employee name | fullName (text/plain) |
| DELETE | /employee/{id} | Admin | Delete employee | - |

---