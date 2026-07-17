# Changelog

## 2026-07-17 — Payment Module v1

### Added
- Payment Aggregate (Domain)
- Payment Business Rules (4 rules)
- Payment Domain Events (PaymentReceived, PaymentRefunded)
- Payment CQRS (ReceivePayment, RefundPayment, GetPaymentById, GetPaymentsByOrderId)
- Payment Infrastructure (EF Core, Repository, DI)
- Payment API (4 endpoints)
- Payment Integration Tests (18 tests)
- Global Exception Handling Middleware
- ProblemDetails Response Standardization

### Frozen
- Payment Module v1

### Changed
- Updated Project Status to v1.2
- Updated Reference Modules with Payment Module patterns

## 2026-07-17 — Reporting Module v1

### Added
- IClock interface (Domain)
- IReportingDbContext (read-only abstraction)
- Report DTOs (DailySalesReport, SalesByPaymentReport, BestSellerReport)
- Report Query Handlers (3 handlers — EF Core LINQ, AsNoTracking, database aggregation)
- Report API endpoints (3 endpoints: GET /reports/daily-sales, /sales-by-payment, /best-sellers)
- SystemClock implementation (Infrastructure)
- DI Registrations for IClock and IReportingDbContext
- Reporting Collection Fixture (Integration Tests)
- Reporting Integration Tests (24 tests)

### Architecture
- Pure Read Model: no Aggregate, no Commands, no Domain Events, no Business Rules
- Query-only CQRS with EF Core + AsNoTracking()
- Database aggregation (GroupBy, Sum, Count in SQL via EF Core LINQ)
- Dapper deferred to future milestone

### Frozen
- Reporting Module v1

### Changed
- Updated Project Status to v1.3
- Updated Reference Modules with Reporting Module patterns
- Application.csproj added Microsoft.EntityFrameworkCore 8.0.11
- ApplicationDbContext implements IReportingDbContext
- Application DependencyInjection registers 3 report handlers
- Infrastructure DependencyInjection registers IClock (SystemClock) and IReportingDbContext
- Program.cs registers MapReportingEndpoints()
