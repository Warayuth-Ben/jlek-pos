# JLek POS — Smoke Test Checklist

## Prerequisites

- [ ] API is running (`http://localhost:5287`)
- [ ] Blazor is running (`http://localhost:5062`)
- [ ] Database connected and migrated
- [ ] Seed data created: `powershell -File docs/97-AI-Docs/seed_data.ps1`

## API Smoke Tests

- [ ] `GET /health` → `{"status":"Healthy"}`
- [ ] `GET /categories` → returns list
- [ ] `GET /products` → returns list
- [ ] `GET /tables` → returns list
- [ ] `GET /tables/available` → returns available tables
- [ ] `POST /tables/{id}/open` → table opened
- [ ] `POST /orders` with `{"tableId":"..."}` → order created (HTTP 201)
- [ ] `POST /orders/{id}/items` → item added
- [ ] `POST /orders/{id}/confirm` → order confirmed
- [ ] `GET /kitchen/active` → kitchen ticket created
- [ ] `POST /kitchen/{id}/start` → preparing
- [ ] `POST /kitchen/{id}/complete` → ready
- [ ] `POST /kitchen/{id}/serve` → served
- [ ] `POST /payments` → payment received
- [ ] `POST /tables/{id}/release` → table released
- [ ] `GET /reports/daily-sales` → report generated
- [ ] `GET /reports/best-sellers` → report generated
- [ ] `GET /reports/sales-by-payment` → report generated

## Browser Smoke Tests

- [ ] Open `http://localhost:5062` — Home page loads
- [ ] Navigate to Cashier — page loads, shows tables
- [ ] Open a table — available tables display
- [ ] Create order — order appears in table
- [ ] Add menu items — items appear in order summary
- [ ] Confirm order — order status changes
- [ ] Navigate to Kitchen — ticket appears
- [ ] Start cooking — status changes
- [ ] Mark ready — status changes
- [ ] Mark served — ticket moves to history
- [ ] Navigate to Dashboard — metrics show
- [ ] Navigate to Reports — reports load
- [ ] Navigate to Settings — page renders

## Console Verification

- [ ] Browser console: 0 errors
- [ ] Browser console: 0 failed HTTP requests
- [ ] API logs: no error-level entries
- [ ] API logs: no EF Core exceptions
- [ ] All HTTP responses: no 500 or 404

## Runtime Verification

- [ ] No null reference exceptions
- [ ] No EF Core validation errors
- [ ] No DI resolution failures
- [ ] No unhandled exceptions