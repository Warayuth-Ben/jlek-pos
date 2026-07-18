# Reference Modules

Version: 1.0

Project: JLek POS

---

## Purpose

This document summarizes reusable implementation patterns from completed modules.

Future modules should reference this document before starting implementation.

---

## Order Module

Status: Frozen

### Aggregate

| Concept | Type | Details |
|---------|------|---------|
| Order | Aggregate Root | Order lifecycle, order items, business rules, domain events |
| OrderItem | Entity | Belongs to Order, controlled by Order aggregate |

### Repository Pattern

| Method | Return | Description |
|--------|--------|-------------|
| GetByIdAsync(OrderId) | Order? | Single aggregate load |
| GetAllAsync() | IReadOnlyList<Order> | All aggregates |
| AddAsync(Order) | void | Persist + one SaveChangesAsync |
| UpdateAsync(Order) | void | Update + one SaveChangesAsync |

### CQRS Pattern

| Category | Count | Examples |
|----------|-------|----------|
| Commands | 6 | CreateOrder, AddItem, RemoveItem, ConfirmOrder, CompleteOrder, CancelOrder |
| Queries | 2 | GetOrderById, GetOrders |
| Handlers | 8 | One per command/query |

### API Pattern

| Method | Route | Result |
|--------|-------|--------|
| POST | /orders | Created |
| GET | /orders/{id} | Ok / NotFound |
| PUT/DELETE sub-resources | /orders/{id}/items | Ok |

### Response DTO Pattern

```csharp
public record OrderResponseV2(...)
{
    public static OrderResponseV2 FromDomain(Order order) => ...;
}
```

---

## Menu Module

Status: Frozen

### Aggregates

| Concept | Type | Details |
|---------|------|---------|
| Product | Aggregate Root | Owned entities: OptionGroup, Option, Modifier. Owned collections: SuggestedPrices, IngredientIds |
| ProductCategory | Aggregate Root | Independent aggregate |
| Ingredient | Aggregate Root | Independent aggregate, referenced by Product via IngredientId only |

### Repository Pattern

Same 4-method contract as Order Module for each aggregate.

### Key Patterns

- **Aggregate Composition**: Product owns OptionGroups (entities), SuggestedPrices (value objects), IngredientIds (references by ID)
- **Owned Entities**: OptionGroup uses OwnsMany with owned entity pattern
- **Value Collections**: SuggestedPrices and IngredientIds use OwnsMany with value object pattern

---

## Table Module

Status: Frozen

### Aggregate

| Concept | Type | Details |
|---------|------|---------|
| DiningTable | Aggregate Root | Sole aggregate. Owns its status lifecycle. References OrderSession by ID only. |

### CQRS Pattern

| Category | Count | Commands/Queries |
|----------|-------|------------------|
| Commands | 6 | Create, Assign, Transfer, Merge, Split, Release |
| Queries | 3 | GetById, GetAll, GetAvailable (LINQ filter) |

### API Pattern

| Method | Route | Description |
|--------|-------|-------------|
| POST | /tables | Create |
| GET | /tables | Get All |
| GET | /tables/available | Get Available only (LINQ filter in handler) |
| GET | /tables/{id} | Get By Id |
| POST | /tables/{id}/assign | Assign to session |
| POST | /tables/{id}/transfer | Transfer session |
| POST | /tables/{id}/merge | Merge tables |
| POST | /tables/{id}/split | Split merged tables |
| POST | /tables/{id}/release | Release table |

### Key Patterns

- **Multi-Aggregate Operations**: Transfer and Merge update two DiningTables in one handler. Domain method receives the related aggregate as parameter. Handler persists each aggregate separately (two SaveChangesAsync calls).
- **Cross-Bounded-Context References**: DiningTable references OrderSessionId by ID only. No navigation property. No FK constraint.
- **Query Filtering via LINQ**: GetAvailable uses GetAllAsync() + Where() in handler. No additional repository method.

---

## Kitchen Module

Status: Frozen

### Aggregate

| Concept | Type | Details |
|---------|------|---------|
| KitchenTicket | Aggregate Root | Sole aggregate. Owns preparation lifecycle. Contains KitchenItem snapshot entities. |
| KitchenItem | Entity (snapshot) | Owned by KitchenTicket. Stores ItemName, Quantity, Notes. No reference to Product or Order. |

### State Machine

```
Pending ──► Preparing ──► Ready ──► Served
```

Each transition triggers a domain event. Served tickets are immutable.

### CQRS Pattern

| Category | Count | Commands/Queries |
|----------|-------|------------------|
| Commands | 5 | Create, AddItem, StartPreparation, CompletePreparation, Serve |
| Queries | 3 | GetById, GetAll, GetActive (LINQ: Where Status != Served + OrderBy TicketNumber) |

### API Pattern

| Method | Route | Description |
|--------|-------|-------------|
| POST | /kitchen | Create ticket (201 Created) |
| GET | /kitchen | Get All |
| GET | /kitchen/active | Get active tickets (LINQ filter) |
| GET | /kitchen/{id} | Get By Id |
| POST | /kitchen/{id}/items | Add kitchen item |
| POST | /kitchen/{id}/start | Start preparation |
| POST | /kitchen/{id}/complete | Complete preparation |
| POST | /kitchen/{id}/serve | Serve items |

### Key Patterns

- **Snapshot Aggregate**: KitchenItem does NOT reference OrderItemId or ProductId. All data (ItemName, Quantity, Notes) is copied at ticket creation. Kitchen never queries Product or Order after ticket creation.
- **State Machine**: Strictly follows verified 4-state lifecycle. Only Pending, Preparing, Ready, Served.
- **TicketNumber**: Passed as parameter from Application layer. Placeholder — needs thread-safe SequenceService for production.
- **Active Queue via GetAllAsync + LINQ**: GetActive uses `GetAllAsync()` + `Where(Status != Served)` + `OrderBy(TicketNumber)`. No new repository method.

---

## Payment Module

Status: Frozen

### Aggregate

| Concept | Type | Details |
|---------|------|---------|
| Payment | Aggregate Root | Sole aggregate. Created in Completed state. References Order by OrderId only. |

### State Machine

```
Completed ⇄ Refunded
```

Payment is created directly in `Completed` state (no Pending for cash POS). The only transition is to `Refunded` (terminal).

### Value Objects

| Value Object | Details |
|--------------|---------|
| PaymentId | Strongly Typed ID (Guid) |
| OrderTotal | Money — copied from Order at creation |
| AmountReceived | Money — what customer paid |
| Change | Money — AmountReceived - OrderTotal |
| PaymentMethod | Enum: Cash, Card, QR, Credit |
| PaymentStatus | Enum: Completed, Refunded |

### Business Rules

| Rule | Location | Enforces |
|------|----------|----------|
| CannotPayCancelledOrderRule | Domain (`Payment.Create(order, amount, method)`) | order.Status != Cancelled |
| CannotPayCompletedOrderRule | Domain (`Payment.Create(order, amount, method)`) | order.Status != Completed |
| CannotAcceptInsufficientPaymentRule | Domain (`Payment.Create(order, amount, method)`) | amountReceived >= order.Total |
| CannotRefundNonCompletedPaymentRule | Domain (`payment.Refund(reason)`) | Status == Completed |

### CQRS Pattern

| Category | Count | Commands/Queries |
|----------|-------|------------------|
| Commands | 2 | ReceivePayment, RefundPayment |
| Queries | 2 | GetPaymentById, GetPaymentsByOrderId (LINQ filter) |

### API Pattern

| Method | Route | Description |
|--------|-------|-------------|
| POST | /payments | Receive payment (201 Created) |
| GET | /payments/{id} | Get By Id |
| GET | /payments?orderId= | Get by OrderId (LINQ filter) |
| POST | /payments/{id}/refund | Refund payment (with Reason) |

### Key Patterns

- **Cross-Aggregate Rules Inside Domain**: `Payment.Create(order, amountReceived, method)` receives `Order` as parameter and validates its status inside the Domain. This is DDD-correct — Business Rules stay in Domain, not Application.
- **AmountReceived ≠ OrderTotal**: Three separate Money values: `OrderTotal`, `AmountReceived`, `Change`. Rule is `AmountReceived >= OrderTotal`, not `==`. This reflects real POS workflow (customer pays 100 for 85 order, system calculates Change = 15).
- **RefundReason**: Refund accepts an optional `string? reason` for "ลูกค้าเปลี่ยนใจ", "คิดเงินผิด", etc.
- **Domain Event Naming**: `PaymentReceivedEvent` (not `PaymentCreatedEvent`) — reflects business meaning ("ลูกค้าจ่ายเงินแล้ว"), not object creation.
- **Global Exception Handling**: BusinessRuleValidationException returns 409 Conflict with ProblemDetails format.

## Receipt Module

Status: Frozen

### Architecture

Receipt Module is a pure **Output Module**. It has no Aggregate, no Repository, no Data Store, no Business Rules, and no Domain Events.

| Concept | Classification | Justification |
|---------|---------------|---------------|
| Output Module | Command-only | Print is a side effect of business operations. No data to protect. |
| Command Handlers | Application Layer | Inject IReceiptDataProvider, IReceiptFormatter, IReceiptPrinter, IClock. |
| ReceiptFormatter | Formatting Layer | Pure C# — transforms DTOs to ReceiptDocument. No EF Core, no printer logic. |
| ReceiptDocument | Model | Line-based document (Title, Lines, ReceiptNumber, ReprintLabel). Printer-agnostic. |

### Purpose

The Receipt Module formats and prints customer receipts, kitchen tickets, and refund receipts. It abstracts printer hardware through interfaces so that application code never depends on specific printer implementations.

### Dependencies

| Module | Dependency Type | Details |
|--------|----------------|---------|
| Order | Read (via DataProvider) | CustomerReceipt reads Order + OrderItems |
| Payment | Read (via DataProvider) | CustomerReceipt reads Payment. RefundReceipt reads Payment. |
| Kitchen | Read (via DataProvider) | KitchenTicket reads KitchenTicket + KitchenItems |
| IClock | Interface | Print timestamps |

No module depends on Receipt.

### CQRS Pattern

| Category | Count | Details |
|----------|-------|---------|
| Commands | 3 | PrintCustomerReceipt, PrintKitchenTicket, PrintRefundReceipt |
| Queries | 0 | None |
| Handlers | 3 | One per command |

### API Pattern

| Method | Route | Description |
|--------|-------|-------------|
| POST | /receipts/customer-print | Print/reprint customer receipt (with OrderId, IsReprint, Copies) |
| POST | /receipts/kitchen-print | Print kitchen ticket (with TicketNumber, Copies) |
| POST | /receipts/refund-print | Print/reprint refund receipt (with PaymentId, IsReprint, Copies) |

### DTO Flow

```
HTTP Request → CustomerPrintRequest (flat DTO)
  ↓
Command → PrintCustomerReceiptCommand
  ↓
IReceiptDataProvider.GetCustomerReceiptDataAsync()
  ↓  returns CustomerReceiptData (flat DTO — ORDER, no Domain types)
IReceiptFormatter.FormatCustomerReceipt(data, isReprint)
  ↓  returns ReceiptDocument (format-specific lines)
IReceiptPrinter.PrintAsync(document)
  ↓  returns PrintResult (Success, StartedAt, FinishedAt, Duration, etc.)
HTTP Response → 200 OK with PrintResult
```

### Key Patterns

- **Output Module**: No Aggregate, no Repository, no Data Store, no Business Rules. Only Commands.
- **Flat DTO Isolation**: IReceiptDataProvider returns flat DTOs only (CustomerReceiptData, KitchenTicketReceiptData, RefundReceiptData). Never exposes Order, Payment, or KitchenTicket Domain entities.
- **Formatter/Printer Separation**: ReceiptFormatter is pure C# formatting logic — no EF Core, no printer hardware knowledge. Printer implementations (IReceiptPrinter, IKitchenPrinter) handle device-specific rendering.
- **ReceiptDocument Model**: Printer-agnostic document format. Lines have Text, Alignment, Bold, DoubleWidth, DoubleHeight. Any printer adapter can convert this to device-specific format (ESC/POS, PDF, HTML).
- **Enhanced PrintResult**: Includes Success, ErrorMessage, StartedAt, FinishedAt, Duration, PrinterName, Copies, Status.
- **Reprint with Label**: Reprint uses the same data + `isReprint: true` flag, not a separate endpoint. Document includes "*** REPRINT ***" label.
- **NullPrinter**: Default printer for development and CI. Always returns successful PrintResult. No hardware required.
- **ReceiptConfiguration**: Loaded from appsettings.json (ShopName, Address, Phone, TaxId, Footer, PaperWidth). Not hardcoded.
- **Explicit Print (v1)**: No auto-print triggered by Domain Events. Cashier clicks "Print" explicitly after successful payment.

---

## Shared Infrastructure Patterns

### 4-Method Repository Contract (all modules)

```csharp
Task<T?> GetByIdAsync(TId id, CancellationToken ct);
Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct);
Task AddAsync(T entity, CancellationToken ct);
Task UpdateAsync(T entity, CancellationToken ct);
```

### EF Core Configuration Pattern

```csharp
builder.ToTable("TableName");
builder.HasKey(x => x.Id);
builder.Property(x => x.Id).HasConversion(new IdConverter()).ValueGeneratedNever();
builder.Property(x => x.Status).HasConversion<int>();
builder.Ignore(x => x.DomainEvents);
// OwnsMany for owned entities/collections
```

### Integration Testing Pattern

- CustomWebApplicationFactory<Program>
- Testcontainers PostgreSQL (one container per test class)
- xUnit Collection Fixture + IAsyncLifetime
- FluentAssertions
- No mocking. No InMemory database.

### CI/CD Pattern

- GitHub Actions
- .NET 8.0 SDK
- Docker (for Testcontainers)
- Steps: Restore → Build (Release) → Test (Release)
- TRX artifacts on failure

---

## Reporting Module

Status: Frozen

### Architecture

Reporting Module is a pure **Read Model**. It has no Aggregate, no Repository, no Commands, and no Domain Events.

| Concept | Classification | Justification |
|---------|---------------|---------------|
| Read Model | Query-only | No business invariants to protect. Pure data aggregation for analysis. |
| Query Handlers | Application Layer | Inject IReportingDbContext (read-only) and IClock. No Repository pattern. |
| Report DTOs | Response only | Flat DTOs with computed fields (TotalRevenue, NetRevenue, Rank). No FromDomain(). |

### Dependencies

| Module | Dependency Type | Details |
|--------|----------------|---------|
| Order | Read-only | DailySales, BestSellers read Order and OrderItem |
| Payment | Read-only | DailySales, SalesByPayment read Payment |
| IClock | Interface | Injected into all query handlers for time-based defaults |

No module depends on Reporting.

### CQRS Pattern

| Category | Count | Details |
|----------|-------|---------|
| Queries | 3 | GetDailySalesReport, GetSalesByPaymentReport, GetBestSellerReport |
| Commands | 0 | None (Read Model only) |
| Handlers | 3 | One per query |

### API Pattern

| Method | Route | Description |
|--------|-------|-------------|
| GET | /reports/daily-sales | Daily revenue, orders, items sold, refunds (with optional date) |
| GET | /reports/sales-by-payment | Sales grouped by Cash/Card/QR/Credit (with optional date range) |
| GET | /reports/best-sellers | Top menu items ranked by quantity sold (with optional limit, date range) |

### Response DTO Pattern

Report DTOs are flat records with computed fields (not FromDomain mapping):

```csharp
public sealed record DailySalesReport
{
    public DateOnly Date { get; init; }
    public int TotalOrders { get; init; }
    public decimal TotalRevenue { get; init; }
    public decimal TotalRefunds { get; init; }
    public decimal NetRevenue { get; init; }
    public decimal AverageOrderValue { get; init; }
    public int TotalItemsSold { get; init; }
}
```

### Key Patterns

- **Pure Read Model**: No Aggregate, no Business Rules, no Domain Events. Only queries.
- **EF Core + AsNoTracking()**: All queries use `AsNoTracking()` for read-only access. No Dapper, no Raw SQL, no Views.
- **Database Aggregation**: BestSellers uses `SelectMany + GroupBy + Sum` which EF Core translates to SQL GROUP BY + aggregate functions.
- **IReportingDbContext**: Read-only interface exposing only `DbSet<Order>`, `DbSet<Payment>`, `DbSet<KitchenTicket>`. Prevents accidental writes.
- **IClock Interface**: All time-based queries use `IClock` (Today, UtcNow) for testability.
- **Endpoint Validation**: BestSellers validates `limit > 0` and `dateFrom <= dateTo` → `Results.ValidationProblem()`. No exceptions for client errors.
- **Integration Testing**: Reuses existing Testcontainers + WebApplicationFactory pattern. Seeds completed orders + payments independently.

### Query Data Flow

```
HTTP Request
  ↓
Minimal API Endpoint (read query params)
  ↓
Query Handler (inject IReportingDbContext, IClock)
  ↓
EF Core LINQ with AsNoTracking()
  ↓
PostgreSQL (GROUP BY, SUM, COUNT, ORDER BY in SQL)
  ↓
Report DTO
  ↓
Results.Ok(report)
```

---

## Printing Infrastructure

Status: Frozen

### Architecture

```
ReceiptDocument (from Receipt Module — FROZEN)
    ↓
IRenderer.RenderAsync(ReceiptDocument) → PrintPayload
    ↓
PrintPayload { byte[] Data, MimeType, Format }
    ↓
IPrinterAdapter.PrintAsync(PrintPayload) → PrintResult
    │
    ├── UsbPrinterAdapter (depends on ISerialPort)
    │     └── SerialPortAdapter (wraps System.IO.Ports.SerialPort)
    │
    ├── LanPrinterAdapter (depends on ITcpClient)
    │     └── TcpClientAdapter (wraps System.Net.Sockets.TcpClient)
    │
    └── NullPrinterAdapter (development/CI, always online)
```

### Key Patterns

- **Renderer ≠ Adapter**: Renderer is pure byte formatting (no IO). Adapter is transport only (no formatting). Completely independent.
- **Connectionless Design**: Adapters manage connect/disconnect internally. `PrintAsync(PrintPayload)` is the only method needed for printing.
- **Stateless Renderer**: `EscPosRenderer` has no mutable state. Same document → same bytes. Thread-safe. Pure.
- **No Fluent API**: `EscPosRenderer` has a single `RenderAsync(ReceiptDocument)` method. No `SetAlignment()`, `SetBold()` exposed.
- **ISerialPort / ITcpClient Abstractions**: Adapters depend on abstractions, not on `System.IO.Ports.SerialPort` or `System.Net.Sockets.TcpClient` directly. All testable with mocks.
- **Never Throws**: All exceptions (IOException, SocketException, TimeoutException, OperationCanceledException) are caught. Returns `PrintResult { Success = false }`.
- **Resource Cleanup**: Ports closed and TCP disconnected in `finally` blocks — on both success and failure.
- **Thai Encoding**: CP874 (Windows-874) via ESC/POS code page command (ESC t 0x11). Fallback to CP437.
- **PrintPayload**: Generic byte output with MimeType and Format. Not tied to ESC/POS.

### CQRS Pattern

Printing Infrastructure is output-only. No Commands, no Queries. It is a library consumed by Receipt Module's `IReceiptPrinter` implementation.

### Layer Diagram

```
Printer Adapter  ────  IPrinterAdapter (abstraction)
    │
    ├── UsbPrinterAdapter
    │     └── ISerialPort ──── SerialPortAdapter (System.IO.Ports)
    │
    ├── LanPrinterAdapter
    │     └── ITcpClient ──── TcpClientAdapter (System.Net.Sockets)
    │
    └── NullPrinterAdapter (mock)
```

### Projects

| Project | Responsibility | Files |
|---------|---------------|-------|
| `JLek.POS.Printing.Models` | Data types | 4 (PrintPayload, PrinterStatus, PrinterConfiguration, PrinterFormat) |
| `JLek.POS.Printing.Abstractions` | Interfaces | 2 (IRenderer, IPrinterAdapter) |
| `JLek.POS.Printing.Infrastructure` | Implementations | 8 (SerialPort, TcpClient, USB, LAN, Null adapters + DI) |
| `JLek.POS.Printing.Renderer` | ESC/POS formatting | 5 (EscPosRenderer, Commands, Constants, CodePages, Options) |
| `JLek.POS.Printing.Infrastructure.Tests` | Unit tests | 2 (18 adapter tests + 18 pipeline tests = 36) |
| `JLek.POS.Printing.Renderer.Tests` | Unit tests | 1 (21 renderer tests) |

### Encoding Support

| Code Page | ESC/POS Command | Used For |
|-----------|----------------|----------|
| CP437 (0x00) | ESC t 0x00 | US/Europe default |
| CP874 (0x11) | ESC t 0x11 | Thai |
| CP850 (0x02) | ESC t 0x02 | Western Europe |
| CP932 (0x01) | ESC t 0x01 | Japanese |

## Implementation Order for Future Modules

1. Business Rules (docs/01-business-rules/)
2. Architecture Design (docs/96-architecture/)
3. Domain (aggregate, rules, events)
4. Repository Contract (4-method interface)
5. Infrastructure (EF config, converter, repository, DI)
6. CQRS (commands, queries, handlers, response DTO)
7. API (Minimal API endpoints)
8. Integration Tests (Testcontainers + WebApplicationFactory)
9. CI (GitHub Actions workflow)
10. Freeze (documentation update)

---

## Patterns Catalog

| Pattern | First Used In | Description |
|---------|---------------|-------------|
| AggregateRoot<TId> | Order Module | Base class for all aggregate roots |
| Static Create() factory | Order Module | Domain object creation pattern |
| Private EF Core constructor | Order Module | Required by EF Core for materialization |
| CheckRule(IBusinessRule) | Order Module | Business rule validation |
| RaiseDomainEvent() | Order Module | Domain event recording |
| 4-method Repository | Order Module | GetById, GetAll, Add, Update |
| One SaveChangesAsync per method | Order Module | Transaction boundary |
| IEntityTypeConfiguration<T> | Order Module | EF Core configuration |
| ValueConverter<TId, Guid> | Order Module | Strongly typed ID persistence |
| FromDomain() factory | Order Module | Response DTO mapping |
| MapGroup("/resource") | Order Module | API endpoint grouping |
| Results.Ok/Created/NotFound | Order Module | HTTP response pattern |
| OwnsMany owned entities | Menu Module | Entity collection persistence |
| OwnsMany value collections | Menu Module | Value object collection persistence |
| [Collection("X")] + IAsyncLifetime | Menu Module | Integration test fixture pattern |
| Testcontainers PostgreSQL | Menu Module | Database per test class |
| Multi-aggregate domain method | Table Module | Domain method receives related aggregate |
| GetAllAsync + LINQ filter | Table Module | Query filtering without new repo method |
| GitHub Actions CI | Table Module | CI pipeline with Docker + Testcontainers |
| Snapshot Aggregate pattern | Kitchen Module | Owned entity stores copied data, no cross-ref |
| State machine with 4 states | Kitchen Module | Domain-enforced lifecycle transitions |
| SeedTicketAtStatusAsync helper | Kitchen Module | Advances aggregate through state for testing |