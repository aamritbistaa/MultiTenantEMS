# Setup Instructions

## Required Software
- Docker Desktop (with Docker Compose enabled and Linux containers mode)
- Visual Studio 2022
- Git

---

## Quick Start

### Step 1: Clone Repository
```bash
git clone https://github.com/aamritbistaa/MultiTenantEMS.git
cd MultiTenantEMS
```

### Step 2: Configure Environment
```bash
cd docker
cp .env.example .env
```

Edit `.env` with your configuration

### Step 3: Start Services
```bash
# From docker directory
docker compose up -d
```

Wait 10-15 seconds for services to initialize.

### Step 4: Verify Installation
```bash
# Check running containers
docker compose ps

# Test health endpoint
curl http://localhost:5000/health

# Expected response: {"status":"Healthy",...}
```

### Step 5: Access Swagger UI
Open browser and navigate to:
```
http://localhost:5000/swagger/index.html
```

### Step 6: Login with Default Super admin Credentials
```json
{
  "emailAddress": "assessment@yopmail.com",
  "password": "Tester@123"
}
```

---