# API Examples

### Health Check
```bash
curl http://localhost:5000/health
```

### Login
```bash
curl -X POST http://localhost:5000/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d '{"emailAddress":"assessment@yopmail.com","password":"Tester@123"}'
```

### Get Tenants
```bash
curl -X GET http://localhost:5000/api/v1/tenant \
  -H "Authorization: Bearer <JWT_TOKEN>"
```

### Create Tenant
```bash
curl -X POST http://localhost:5000/api/v1/tenant \
  -H "Authorization: Bearer <TOKEN>" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "New Tenant",
    "emailAddress": "admin@newtenant.com",
    "tenantId": "new-tenant",
    "password": "AdminPass@123"
  }'
```

### Create Employee
```bash
curl -X POST http://localhost:5000/api/v1/employee \
  -H "Authorization: Bearer <TOKEN>" \
  -H "Content-Type: application/json" \
  -d '{
    "fullName": "John Doe",
    "emailAddress": "john@tenant.com",
    "password": "EmpPass@123"
  }'
```


## Health Check

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
| PUT | /tenant/{id} | SuperAdmin | Update tenant | { name,  tenantId } |
| DELETE | /tenant/{tenantId} | SuperAdmin | Delete tenant | tenantId (string) |

---

## Employee API (/api/v1/employee)

| Method | Endpoint | Auth | Description | Params / Body |
|--------|----------|------|-------------|---------------|
| GET | /employee | Admin | Get all employees | skip, take |
| GET | /employee/{id} | AdminOrEmployee | Get employee by id | id (GUID) |
| POST | /employee | Admin | Create employee | { fullName, emailAddress, password } |
| PUT | /employee/{id} | Admin | Update employee name | {fullName} |
| DELETE | /employee/{id} | Admin | Delete employee | - |

---

