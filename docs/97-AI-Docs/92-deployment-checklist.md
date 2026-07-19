# JLek POS — Deployment Checklist

## Pre-Deployment

- [ ] Full solution build: `dotnet build`
- [ ] Verify 0 Errors, 0 Warnings
- [ ] Integration tests pass: `dotnet test`
- [ ] Database backup taken (if upgrading)
- [ ] Connection string verified in `appsettings.json`
- [ ] PostgreSQL 17 is running and accessible

## Deployment Steps

- [ ] Publish API: `dotnet publish src/JLek.POS.Api -c Release`
- [ ] Publish Blazor: `dotnet publish src/JLek.POS.Web -c Release`
- [ ] Deploy API binary to server
- [ ] Deploy Blazor static files to web server
- [ ] Apply database migration (auto-applied on startup)
- [ ] Start API service
- [ ] Verify health endpoint: `GET /health` → `{"status":"Healthy"}`
- [ ] Verify Swagger UI: `/swagger`
- [ ] Start Blazor application
- [ ] Configure CORS if needed (default: AllowAnyOrigin)

## Post-Deployment Verification

- [ ] Verify Blazor home page loads
- [ ] Verify Cashier page loads
- [ ] Verify Kitchen page loads
- [ ] Verify Payment endpoint: `POST /payments`
- [ ] Verify Reports endpoint: `GET /reports/daily-sales`
- [ ] Verify server logs (no errors)
- [ ] Verify database has no orphan records
- [ ] Run smoke test checklist

## Rollback Plan

- [ ] Stop API service
- [ ] Restore previous database backup
- [ ] Deploy previous API version
- [ ] Verify health endpoint
- [ ] Verify all pages load

## Tag Release

- [ ] `git tag -a v1.0.0 -m "v1.0.0"`
- [ ] `git push origin main --tags`