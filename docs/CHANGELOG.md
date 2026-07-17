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

## 2026-07-17 — Receipt Module v1

### Added
- Receipt DTOs (CustomerReceiptData, KitchenTicketReceiptData, RefundReceiptData)
- ReceiptDocument + ReceiptLine model
- PrintResult model with timing
- IReceiptFormatter + ReceiptFormatter (pure formatting, no infrastructure)
- IReceiptDataProvider + ReceiptDataProvider (returns flat DTOs only)
- IReceiptPrinter + IKitchenPrinter abstractions
- NullReceiptPrinter + NullKitchenPrinter (development printers)
- 3 Commands + Handlers (PrintCustomerReceipt, PrintKitchenTicket, PrintRefundReceipt)
- ReceiptConfiguration (from appsettings.json)
- DI Registrations for all Receipt services
- Minimal API endpoints (3 POST operations)
- Request DTOs (CustomerPrintRequest, KitchenPrintRequest, RefundPrintRequest)
- Collection Fixture + Integration Tests (21 tests)

### Architecture
- Pure Output Module: no Aggregate, no Repository, no Data Store, no Business Rules
- Command-only CQRS (3 commands, 0 queries)
- Flat DTO isolation — no Domain types leaked from DataProvider
- Formatter separated from Printer — ReceiptFormatter is pure C#, Printer is abstracted
- NullPrinter for development/CI — real printers deferred to next milestone

### Frozen
- Receipt Module v1

### Changed
- Updated Project Status to v1.4
- Updated Reference Modules with Receipt Module patterns
- Application DependencyInjection registers 3 receipt handlers + ReceiptFormatter + ReceiptConfiguration
- Infrastructure DependencyInjection registers IReceiptDataProvider, NullReceiptPrinter, NullKitchenPrinter
- Program.cs registers MapReceiptEndpoints()
