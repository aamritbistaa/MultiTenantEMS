## Tenant Database Creation

Tenant database creation follows a **database-per-tenant isolation model**, where each new tenant gets its own dedicated PostgreSQL database containing only business-specific data.

---

### 1. Triggering the Creation

A **SuperAdmin** initiates tenant creation via:

```
POST /api/v1/tenant
```

After authentication and validation, the system proceeds with provisioning.

---

### 2. Master Database Registration

Before any physical database is created, a record is stored in the **Master Database**:

* Tenant metadata (Name, Email, TenantId)
* Generated connection string
* Status flags (IsDeleted = false)

This ensures the tenant is tracked centrally even before provisioning completes.

---

### 3. Physical Database Creation (PostgreSQL)

The system dynamically creates a new PostgreSQL database for the tenant:

```
CREATE DATABASE {tenantId}
```

This ensures **complete physical isolation** of tenant business data at the database level.

---

### 4. Schema Initialization (EF Core Migrations)

Once the database is created:

* A new `TenantDbContext` is configured using the tenant-specific connection string
* EF Core migrations are executed against the new database

This automatically creates only **business-related tables**, 
* Employees


Each tenant database shares the same schema structure but remains fully isolated.

---

### 5. Identity Setup (Centralized System)

Unlike tenant databases, authentication is handled globally using ASP.NET Identity.

During tenant creation:

* A default **Admin user** is created using `UserManager<ApplicationUser>`
* The user is stored in the **central Identity database**
* The user is assigned a role (Admin)

---

### 6. Completion Output

Finally, the system returns:

* TenantId

```
HTTP 201 Created
```

---
