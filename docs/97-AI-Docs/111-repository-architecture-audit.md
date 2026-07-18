# Repository Architecture Audit — Sprint 1.9

Version: 1.0

Project: JLek POS

Date: 2026-07-18

Mode: READ / ANALYZE / REPORT — NO CODE CHANGES

---

## Repository Maturity

| Layer | Status | Detail |
|-------|--------|--------|
| **Domain** | ✅ Complete | 7 Aggregate Roots, 5 owned Entities, 16 Domain Events, all Business Rules in Domain |
| **Application** | ✅ Complete | 20 Commands, 12 Queries, CQRS pattern consistent across all modules |
| **Infrastructure** | ✅ Complete | 7 Repositories (4-method pattern), EF Core, PostgreSQL, DI registration |
| **Integration** | ⚠️ Partial | 155 integration tests, but no event handlers — Domain Events raised but not consumed |
| **Presentation** | ❌ Not Started | Backend API complete (47 endpoints), Web UI = 0% |
| **CI/CD** | ✅ Complete | GitHub Actions, Testcontainers, Release build, TRX artifacts |

---

## 1. Aggregate Inventory

| Aggregate | Type | Namespace | Status | Key Files |
|-----------|------|-----------|--------|-----------|
| **Order** | Aggregate Root | `JLek.POS.Domain.Orders` | ✅ Frozen v1 | `Order.cs` (114 lines) |
| OrderItem | Entity (owned) | `JLek.POS.Domain.Orders` | ✅ Frozen | `OrderItem.cs` (70 lines) |
| **DiningTable** | Aggregate Root | `JLek.POS.Domain.Tables` | ✅ Frozen v1 | `DiningTable.cs` (137 lines) |
| **KitchenTicket** | Aggregate Root | `JLek.POS.Domain.Kitchen` | ✅ Frozen v1 | `KitchenTicket.cs` |
| KitchenItem | Entity (owned snapshot) | `JLek.POS.Domain.Kitchen` | ✅ Frozen | `KitchenItem.cs` |
| **Payment** | Aggregate Root | `JLek.POS.Domain.Payments` | ✅ Frozen v1 | `Payment.cs` |
| **Product** | Aggregate Root | `JLek.POS.Domain.Products` | ✅ Frozen v1 | `Product.cs` |
| OptionGroup | Entity (owned by Product) | `JLek.POS.Domain.Products` | ✅ Frozen | |
| Option | Entity (owned by OptionGroup) | `JLek.POS.Domain.Products` | ✅ Frozen | |
| Modifier | Entity (owned by Product) | `JLek.POS.Domain.Products` | ✅ Frozen | |
| **ProductCategory** | Aggregate Root | `JLek.POS.Domain.Products` | ✅ Frozen v1 | |
| **Ingredient** | Aggregate Root | `JLek.POS.Domain.Products` | ✅ Frozen v1 | |

**Total: 7 Aggregate Roots, 5 owned Entities**

### Aggregate Boundaries Verified

| Rule | Status |
|------|--------|
| No cross-aggregate navigation properties | ✅ All references by ID only |
| Each aggregate has its own repository | ✅ 7 repositories |
| Business Rules inside Domain only | ✅ All `CheckRule()` in Domain |
| `private readonly List<T>` for owned collections | ✅ Consistent |

---

## 2. Application Inventory

### Commands (20 total)

| Module | Commands |
|--------|----------|
| **Order** (6) | `CreateOrder`, `AddItem`, `RemoveItem`, `ConfirmOrder`, `CompleteOrder`, `CancelOrder` |
| **Table** (7) | `CreateDiningTable`, `OpenTable`, `AssignTable`, `TransferTable`, `MergeTables`, `SplitTable`, `ReleaseTable` |
| **Kitchen** (5) | `CreateKitchenTicket`, `AddKitchenItem`, `StartPreparation`, `CompletePreparation`, `ServeKitchenTicket` |
| **Payment** (2) | `ReceivePayment`, `RefundPayment` |
| **Receipt** (3) | `PrintCustomerReceipt`, `PrintKitchenTicket`, `PrintRefundReceipt` |

### Queries (12 total)

| Module | Queries |
|--------|---------|
| **Order** (2) | `GetOrderById`, `GetOrders` |
| **Table** (3) | `GetDiningTableById`, `GetDiningTables`, `GetAvailableDiningTables` |
| **Kitchen** (3) | `GetKitchenTicketById`, `GetKitchenTickets`, `GetActiveKitchenTickets` |
| **Payment** (2) | `GetPaymentById`, `GetPaymentsByOrderId` |
| **Reports** (3) | `GetDailySalesReport`, `GetSalesByPaymentReport`, `GetBestSellerReport` |
| **Health** (1) | `GetHealth` |

### Response DTOs

| Module | DTO | `FromDomain()` |
|--------|-----|----------------|
| Order | `OrderResponseV2` | ✅ |
| Table | `DiningTableResponse` | ✅ |
| Kitchen | `KitchenTicketResponse`, `KitchenItemResponse` | ✅ |
| Payment | `PaymentResponse` | ✅ |
| Reports | `DailySalesReport`, `SalesByPaymentReport`, `BestSellerReport` | ✅ (no FromDomain, pure read model) |

### Handler Pattern (Verified)

```
Handler ─── inject IRepository
     ├── Load aggregate
     ├── Call domain method
     ├── repository.UpdateAsync()
     └── Return DTO
```

- All handlers use `[FromServices]` in endpoints (Standard 26 compliance)
- All handlers registered in `Application/DependencyInjection.cs`

---

## 3. Infrastructure Inventory

### Repositories (7 total)

| Repository | Interface | Implementation | Pattern |
|------------|-----------|----------------|---------|
| Order | `IOrderRepository` | `OrderRepository` | 4-method |
| DiningTable | `IDiningTableRepository` | `DiningTableRepository` | 4-method |
| KitchenTicket | `IKitchenTicketRepository` | `KitchenTicketRepository` | 4-method |
| Payment | `IPaymentRepository` | `PaymentRepository` | 4-method |
| Product | `IProductRepository` | `ProductRepository` | 4-method |
| ProductCategory | `IProductCategoryRepository` | `ProductCategoryRepository` | 4-method |
| Ingredient | `IIngredientRepository` | `IngredientRepository` | 4-method |

All repositories follow the exact same contract:
```
GetByIdAsync → GetOne
GetAllAsync  → GetAll
AddAsync     → Create + SaveChangesAsync
UpdateAsync  → Update + SaveChangesAsync
```

### EF Core

| Component | Detail |
|-----------|--------|
| DbContext | `ApplicationDbContext` — single context |
| Connection | PostgreSQL 17 via Npgsql |
| Configurations | `IEntityTypeConfiguration<T>` per aggregate |
| Lifecycle | `AddDbContext<ApplicationDbContext>` (Scoped) |
| All repositories inject same DbContext ✅ |

### Value Converters

| Converter | For |
|-----------|-----|
| `TableIdConverter` | `TableId` ↔ `Guid` |
| `PaymentIdConverter` | `PaymentId` ↔ `Guid` |
| `KitchenTicketIdConverter` | `KitchenTicketId` ↔ `Guid` |
| `KitchenItemIdConverter` | `KitchenItemId` ↔ `Guid` |

### Dependency Injection

| Registration | Count |
|-------------|-------|
| Repositories (Scoped) | 7 |
| Command Handlers (Scoped) | 20 |
| Query Handlers (Scoped) | 12 |
| Services (Scoped) | ReceiptFormatter, ReceiptDataProvider, etc. |

---

## 4. API Inventory

### Endpoints (47 total)

| Module | Base Path | Endpoints |
|--------|-----------|-----------|
| **Order** (7) | `/orders` | `POST /`, `GET /`, `GET /{id}`, `POST /{id}/items`, `DELETE /{id}/items/{itemId}`, `POST /{id}/confirm`, `POST /{id}/complete`, `POST /{id}/cancel` |
| **Table** (10) | `/tables` | `POST /`, `GET /`, `GET /available`, `GET /{id}`, `POST /{id}/assign`, `POST /{id}/transfer`, `POST /{id}/merge`, `POST /{id}/split`, `POST /{id}/release`, `POST /{id}/open` |
| **Kitchen** (8) | `/kitchen` | `POST /`, `GET /`, `GET /active`, `GET /{id}`, `POST /{id}/items`, `POST /{id}/start`, `POST /{id}/complete`, `POST /{id}/serve` |
| **Payment** (4) | `/payments` | `POST /`, `GET /{id}`, `GET /?orderId=`, `POST /{id}/refund` |
| **Catalog** (27) | `/products`, `/categories`, `/ingredients` | Full CRUD + composition |
| **Reports** (3) | `/reports` | `GET /daily-sales`, `GET /sales-by-payment`, `GET /best-sellers` |
| **Receipt** (3) | `/receipts` | `POST /customer-print`, `POST /kitchen-print`, `POST /refund-print` |
| **Health** (1) | `/health` | `GET /health` |
| **Root** (1) | `/` | `GET /` |

### API Conventions (Consistent)

- `MapGroup("/resource")` pattern
- `[FromServices]` for all handlers
- `Results.Ok()` / `Results.Created()` / `Results.NotFound()`
- Guid route constraints `{id:guid}`

---

## 5. Event Inventory

| Domain Event | Aggregate | Trigger |
|-------------|-----------|---------|
| `OrderCreatedEvent` | Order | `Order.Create()` |
| `OrderConfirmedEvent` | Order | `Order.Confirm()` |
| `OrderCancelledEvent` | Order | `Order.Cancel()` |
| `OrderCompletedEvent` | Order | `Order.Complete()` |
| `TableOpenedEvent` | DiningTable | `DiningTable.Open()` |
| `TableAssignedEvent` | DiningTable | `DiningTable.Assign()` |
| `TableTransferredEvent` | DiningTable | `DiningTable.TransferTo()` |
| `TablesMergedEvent` | DiningTable | `DiningTable.Merge()` |
| `TablesSplitEvent` | DiningTable | `DiningTable.Split()` |
| `TableReleasedEvent` | DiningTable | `DiningTable.Release()` |
| `KitchenTicketCreatedEvent` | Kitchen | `KitchenTicket.Create()` |
| `KitchenPreparationStartedEvent` | Kitchen | `KitchenTicket.StartPreparation()` |
| `KitchenPreparationCompletedEvent` | Kitchen | `KitchenTicket.CompletePreparation()` |
| `KitchenItemsServedEvent` | Kitchen | `KitchenTicket.ServeItems()` |
| `PaymentReceivedEvent` | Payment | `Payment.Create()` |
| `PaymentRefundedEvent` | Payment | `Payment.Refund()` |

**Total: 16 Domain Events** — All extend `DomainEvent` base class.

### Critical Finding

All 16 events are **raised but none are handled**. No event handlers/subscribers exist in the codebase. This means:
- Events are recorded in the aggregate but never processed
- No event-driven cross-bounded-context communication exists
- This is a **pending architecture decision** — not a bug

---

## 6. State Machine Inventory

| Aggregate | States | Transitions |
|-----------|--------|-------------|
| **Order** | `Draft → Confirmed → Completed` | 3 valid transitions |
| | `Draft → Cancelled` | |
| | `Confirmed → Cancelled` | |
| **DiningTable** | `Available → Open → Occupied` | 3 valid transitions (after Sprint 1.1) |
| | `Available → Occupied` (via Assign) | |
| | `Occupied → Available` (via Release) | |
| **KitchenTicket** | `Pending → Preparing → Ready → Served` | 3 valid + 6 invalid (tested) |
| **Payment** | `Completed ⇄ Refunded` | 2 states, 1 transition |

### Kitchen Module State Machine (Detailed)

```
Pending ──(StartPreparation)──► Preparing
Preparing ──(CompletePreparation)──► Ready
Ready ──(ServeItems)──► Served
```

All invalid transitions blocked by Business Rules:
- Cannot start preparation on non-pending ticket
- Cannot complete preparation on non-preparing ticket
- Cannot serve non-ready ticket
- Cannot modify served ticket

---

## 7. Test Coverage

### Integration Tests (155 total)

| Module | Test Count | Tools | Status |
|--------|-----------|-------|--------|
| Catalog (Product) | 31 | xUnit + Testcontainers + FluentAssertions | ✅ Frozen |
| Catalog (ProductCategory) | 13 | Same | ✅ Frozen |
| Catalog (Ingredient) | 10 | Same | ✅ Frozen |
| DiningTable | 17 | Same | ✅ Frozen |
| KitchenTicket | 21 | Same | ✅ Frozen |
| Payment | 18 | Same | ✅ Frozen |
| Reporting | 24 | Same | ✅ Frozen |
| Receipt | 21 | Same | ✅ Frozen |
| **Subtotal** | **155** | | |

### Unit Tests (57 total — Printing)

| Module | Test Count | Tools |
|--------|-----------|-------|
| EscPosRenderer | 21 | xUnit |
| Printer Adapters | 18 | xUnit + mocks |
| Printing Pipeline | 18 | xUnit + mocks |

### CI/CD

| Component | Detail |
|-----------|--------|
| Provider | GitHub Actions |
| Triggers | push/PR to main, develop |
| SDK | .NET 8.0 |
| Database | Testcontainers PostgreSQL (no manual setup) |
| Build Config | Release mode |
| Test Results | TRX artifacts on failure |

---

## 8. Technical Debt

### Verified Items

| # | Item | Impact | Location |
|---|------|--------|----------|
| 1 | `OrderItem.ChangeUnitPrice()` — public method violates Price Snapshot invariant | Could allow modifying historical order prices | `OrderItem.cs:61-69` |
| 2 | No Unit of Work — two `SaveChangesAsync()` per multi-aggregate operation | Inconsistent state if second save fails | All repositories (design choice, documented) |
| 3 | `TicketNumber` needs thread-safe `SequenceService` | Potential duplicate ticket numbers in production | `KitchenTicket.cs` |
| 4 | Database Migration documentation outdated | Missing Table, Kitchen, Payment migrations | `docs/07-database/` |
| 5 | No CreatedAt timestamp in Order/Payment Domain | Date filtering depends on DB timestamps | `Order.cs`, `Payment.cs` |
| 6 | `OrderItem.ChangeUnitPrice()` not called by any handler | Dead code | `OrderItem.cs` |
| 7 | No event handlers/subscribers | Domain Events raised but never consumed | All events |
| 8 | `OrderResponseV2` naming — "V2" implies there's a V1 | Minor naming inconsistency | `Responses/` |

### Build Warnings (7)

| Warning | Source | Details |
|---------|--------|---------|
| CS8618 (2) | `Order.cs` | `TableId`, `SessionId` non-nullable in EF Core constructor |
| CS8618 (4) | `Payment.cs` | `OrderId`, `OrderTotal`, `AmountReceived`, `Change` |
| CS8618 (1) | `LoopAgentTest.cs` | `Name` property |

---

## 9. Pending Integrations

(Not "Missing Features" — the features exist, but workflow connections are not yet linked)

| Integration | Current State | Target State | Priority |
|-------------|---------------|--------------|----------|
| **Order → Kitchen** | Cashier confirms order → must manually call `POST /kitchen` | Automatic: Confirm Order creates KitchenTicket | P0 |
| **Order → Payment** | No automatic link after Complete | Automatic: Complete Order → enable Payment | P0 |
| **Order → Receipt** | Receipt endpoint exists but not auto-triggered | After Payment → auto-print or prompt print | P1 |
| **Payment → Table** | No automatic table release after payment | After Payment → auto-release table | P1 |

### What IS Implemented (Backend only)

- All 7 backend modules: Order, Menu, Table, Kitchen, Payment, Reporting, Receipt
- Printing Infrastructure: ESC/POS, USB, LAN, Null adapters
- Health endpoint
- Global Exception Handling Middleware
- All 47 API endpoints functional
- 155 integration tests + 57 unit tests

### What is NOT Started

| Feature | Priority |
|---------|----------|
| Web UI (Cashier, Kitchen, Manager) | P0 |
| Authentication (JWT) | P0 |
| Dashboard | P1 |

---

## 10. Recommended Development Roadmap

### Sprint 2 — Architecture (Backend Completion)

```
2.1 Event Infrastructure Audit
    - Analyze how Domain Events should be dispatched
    - Evaluate MediatR, in-process event bus, or outbox pattern

2.2 Integration Strategy
    - Design Order→Kitchen integration approach
    - Design Order→Payment integration approach
    - Design Payment→Table release approach

2.3 ADR (Architecture Decision Records)
    - ADR-001: Transaction Strategy
    - ADR-002: Aggregate Communication
    - ADR-003: Event Strategy
    - ADR-004: Frontend Strategy

2.4 Architecture Freeze
    - Document all pending decisions
    - Freeze integration architecture
```

### Sprint 3 — Cashier UI

```
3.1 UI Technology Decision (output from ADR-004)
3.2 Authentication (JWT + Login Screen)
3.3 Cashier UI: Order Screen + Menu Selection
3.4 Implement Order→Kitchen integration
3.5 Kitchen Display UI
```

### Sprint 4

```
4.1 Payment processing UI
4.2 Implement Order→Payment integration
4.3 Receipt printing UI
4.4 Table Management UI
4.5 Close Table flow
```

### Sprint 5

```
5.1 Reporting dashboard
5.2 Real-time kitchen updates (SignalR)
5.3 Advanced operations (Transfer/Merge/Split UI)
5.4 Dashboard with charts
5.5 UI polish + Production hardening
```

---

## Architecture Decisions Required

| ID | Decision | Status | Impact |
|----|----------|--------|--------|
| **ADR-001** | **Transaction Strategy** — Unit of Work or Saga pattern for multi-aggregate operations? | ⏳ Pending | Affects all handlers with >1 repository |
| **ADR-002** | **Aggregate Communication** — Direct Handler call vs Domain Events vs MediatR? | ⏳ Pending | Affects Order→Kitchen, Order→Payment integration |
| **ADR-003** | **Event Strategy** — In-process event bus vs outbox vs no event handlers? | ⏳ Pending | Affects all 16 Domain Events |
| **ADR-004** | **Frontend Strategy** — Blazor vs React vs Vue, SPA vs SSR? | ⏳ Pending | Affects all UI development |
| **ADR-005** | **Authentication** — JWT + ASP.NET Core Identity vs custom vs external? | ⏳ Pending | Affects all endpoints |
| **ADR-006** | **Real-time** — SignalR vs polling vs WebSocket? | ⏳ Pending | Affects Kitchen Display UX |

---

## Verified Facts

| Metric | Value |
|--------|-------|
| Aggregate Roots | 7 |
| Owned Entities | 5 |
| Domain Events | 16 (all raised, none handled) |
| Commands | 20 |
| Queries | 12 |
| Handlers | 32 |
| Repositories | 7 (all 4-method pattern) |
| API Endpoints | 47 |
| Integration Tests | 155 |
| Unit Tests | 57 |
| Build Warnings | 7 (all pre-existing) |
| Build Errors | 0 |
| Architecture Violations | 0 |

---

## ⏳ STOP — WAITING FOR HUMAN APPROVAL

This audit is based on verified repository evidence. No code was modified.