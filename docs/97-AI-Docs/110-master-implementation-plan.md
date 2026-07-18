# Master Implementation Plan

Version: 1.0

Project: JLek POS

Created: 2026-07-18

Status: Draft (awaiting approval)

---

## 1 Executive Summary

### Current Project State

| Area | Status |
|------|--------|
| Architecture Baseline v1.0 | ✅ Frozen |
| Domain Layer | ✅ 100% (Order, Menu/Catalog, Table, Kitchen, Payment) |
| Application Layer | ✅ 100% (CQRS complete for all modules) |
| Infrastructure Layer | ✅ 100% (EF Core, PostgreSQL, Repositories, DI) |
| API Layer | ✅ 100% (Minimal API endpoints for all modules) |
| Integration Testing | ✅ 155 tests (Catalog 54 + Table 17 + Kitchen 21 + Payment 18 + Reporting 24 + Receipt 21) |
| Printing Infrastructure | ✅ 57 unit tests (Renderer 21 + Adapter 18 + Pipeline 18) |
| CI/CD | ✅ GitHub Actions |
| **Web UI** | ❌ **0% — Not started** |
| Authentication / Authorization | ❌ Not started |
| Dashboard | ❌ Not started |

### Overall Goal

Transform JLek POS from a backend-only API into a fully functional Restaurant Point of Sale system with:

1. A working Web UI for cashiers, kitchen staff, and managers
2. Authentication and authorization for staff roles
3. Real-time kitchen display and order management
4. Complete order-to-payment workflow with receipt printing
5. Reporting dashboard for business insights

---

## 2 Epic List

| Epic | Description | Status |
|------|-------------|--------|
| **Epic 1: Order Management** | Create, modify, confirm, cancel, complete orders | ✅ Backend frozen |
| **Epic 2: Menu/Catalog Management** | Products, categories, ingredients, modifiers | ✅ Backend frozen |
| **Epic 3: Table Management** | Assign, transfer, merge, split, release tables | ✅ Backend frozen |
| **Epic 4: Kitchen Display** | Send orders to kitchen, track preparation status | ✅ Backend frozen |
| **Epic 5: Payment Processing** | Receive payments, refunds, multiple payment methods | ✅ Backend frozen |
| **Epic 6: Receipt Printing** | Customer receipts, kitchen tickets, refund receipts | ✅ Backend frozen |
| **Epic 7: Reporting** | Daily sales, sales by payment method, best sellers | ✅ Backend frozen |
| **Epic 8: Web UI** | Cashier UI, Kitchen Display UI, Manager Dashboard | ❌ Not started |
| **Epic 9: Authentication & Authorization** | Staff login, role-based access control | ❌ Not started |
| **Epic 10: Dashboard** | Real-time business overview, charts, KPIs | ❌ Not started |

---

## 3 Features

### Epic 1: Order Management (✅ Backend Frozen)

| Feature | Priority | Dependencies |
|---------|----------|--------------|
| F1.1 Create Order | P0 | None |
| F1.2 Add Item to Order | P0 | F1.1 |
| F1.3 Remove Item from Order | P0 | F1.1 |
| F1.4 Confirm Order | P0 | F1.2 |
| F1.5 Cancel Order | P0 | F1.1 |
| F1.6 Complete Order | P0 | F1.4 |

### Epic 2: Menu/Catalog Management (✅ Backend Frozen)

| Feature | Priority | Dependencies |
|---------|----------|--------------|
| F2.1 Create/Edit Product | P0 | None |
| F2.2 Manage Categories | P0 | None |
| F2.3 Manage Ingredients | P0 | None |
| F2.4 Manage Modifiers/Option Groups | P0 | F2.1 |
| F2.5 Product Availability/Visibility | P0 | F2.1 |

### Epic 3: Table Management (✅ Backend Frozen)

| Feature | Priority | Dependencies |
|---------|----------|--------------|
| F3.1 Create Table | P0 | None |
| F3.2 Assign Table to Session | P0 | F3.1, F1.1 |
| F3.3 Transfer Table | P1 | F3.2 |
| F3.4 Merge Tables | P1 | F3.2 |
| F3.5 Split Tables | P1 | F3.4 |
| F3.6 Release Table | P0 | F3.2, F5.1 |

### Epic 4: Kitchen Display (✅ Backend Frozen)

| Feature | Priority | Dependencies |
|---------|----------|--------------|
| F4.1 Send Order to Kitchen | P0 | F1.4 |
| F4.2 View Active Kitchen Tickets | P0 | F4.1 |
| F4.3 Start Preparation | P0 | F4.1 |
| F4.4 Complete Preparation | P0 | F4.3 |
| F4.5 Mark as Served | P0 | F4.4 |

### Epic 5: Payment Processing (✅ Backend Frozen)

| Feature | Priority | Dependencies |
|---------|----------|--------------|
| F5.1 Receive Payment (Cash) | P0 | F1.6 |
| F5.2 Receive Payment (Card/QR/Credit) | P0 | F1.6 |
| F5.3 Refund Payment | P1 | F5.1 |
| F5.4 View Payment History | P1 | F5.1 |

### Epic 6: Receipt Printing (✅ Backend Frozen)

| Feature | Priority | Dependencies |
|---------|----------|--------------|
| F6.1 Print Customer Receipt | P0 | F5.1 |
| F6.2 Print Kitchen Ticket | P0 | F4.1 |
| F6.3 Print Refund Receipt | P1 | F5.3 |
| F6.4 Reprint Receipt | P1 | F6.1 |

### Epic 7: Reporting (✅ Backend Frozen)

| Feature | Priority | Dependencies |
|---------|----------|--------------|
| F7.1 Daily Sales Report | P1 | F5.1 |
| F7.2 Sales by Payment Method | P1 | F5.1 |
| F7.3 Best Sellers Report | P1 | F5.1 |

### Epic 8: Web UI (❌ Not Started)

| Feature | Priority | Dependencies |
|---------|----------|--------------|
| F8.1 Cashier UI — Order Screen | P0 | F1.1–F1.6 |
| F8.2 Cashier UI — Menu Selection | P0 | F2.1–F2.5 |
| F8.3 Cashier UI — Table Management | P0 | F3.1–F3.6 |
| F8.4 Cashier UI — Payment Screen | P0 | F5.1–F5.4 |
| F8.5 Kitchen Display UI | P0 | F4.1–F4.5 |
| F8.6 Manager Dashboard UI | P1 | F7.1–F7.3 |
| F8.7 Login Screen | P0 | Epic 9 |

### Epic 9: Authentication & Authorization (❌ Not Started)

| Feature | Priority | Dependencies |
|---------|----------|--------------|
| F9.1 Staff Login (JWT) | P0 | None |
| F9.2 Role-based Access (Cashier/Kitchen/Manager) | P0 | F9.1 |
| F9.3 Session Management | P0 | F9.1 |

### Epic 10: Dashboard (❌ Not Started)

| Feature | Priority | Dependencies |
|---------|----------|--------------|
| F10.1 Real-time Sales Overview | P1 | F7.1 |
| F10.2 Order Volume Chart | P1 | F7.1 |
| F10.3 Top-selling Items | P1 | F7.3 |
| F10.4 Table Occupancy View | P1 | F3.1–F3.6 |

---

## 4 Priority

### P0 — Must Have (Core Workflow)

These features form the minimum viable POS workflow:

| Feature | Rationale |
|---------|-----------|
| F1.1–F1.6 Order CRUD | Without orders, there is no POS |
| F2.1–F2.5 Menu Management | Without menu, there are no items to sell |
| F3.1, F3.2, F3.6 Table Assign/Release | Dine-in requires table management |
| F4.1–F4.5 Kitchen Workflow | Kitchen must receive and prepare orders |
| F5.1, F5.2 Receive Payment | Without payment, no revenue |
| F6.1, F6.2 Print Receipt/Ticket | Customer needs receipt, kitchen needs ticket |
| F8.1–F8.5 Web UI | Without UI, the system is unusable |
| F9.1–F9.3 Authentication | Without login, no security |

### P1 — Should Have (Operational Enhancement)

| Feature | Rationale |
|---------|-----------|
| F3.3–F3.5 Transfer/Merge/Split | Common restaurant operations |
| F5.3 Refund | Required for error correction |
| F5.4 Payment History | Audit trail |
| F6.3, F6.4 Refund Receipt / Reprint | Customer service |
| F7.1–F7.3 Reports | Business insights |
| F8.6 Manager Dashboard | Management visibility |
| F10.1–F10.4 Dashboard | Real-time business monitoring |

### P2 — Nice to Have (Future Enhancement)

| Feature | Rationale |
|---------|-----------|
| Additional report types (Hourly, Monthly, Kitchen Performance, Table Usage) | Deeper analytics |
| Bluetooth printer support | Hardware flexibility |
| Cloud printer support | Remote printing |
| PDF receipt export | Digital record keeping |

---

## 5 Dependency Graph

```
                    ┌──────────────────────┐
                    │  F9. Authentication   │
                    └──────────┬───────────┘
                               │
                    ┌──────────▼───────────┐
                    │   F8. Web UI (Shell)  │
                    └──────────┬───────────┘
                               │
        ┌──────────────────────┼──────────────────────┐
        │                      │                      │
        ▼                      ▼                      ▼
┌───────────────┐    ┌──────────────────┐    ┌────────────────┐
│ F2. Menu Mgmt │    │  F1. Order Mgmt  │    │ F3. Table Mgmt │
│ (Products,    │◄──►│  (Create → Add   │◄──►│ (Assign →      │
│  Categories,  │    │   → Confirm →    │    │  Release)      │
│  Ingredients) │    │   → Complete)    │    │                │
└───────────────┘    └────────┬─────────┘    └────────────────┘
                              │
                    ┌─────────▼─────────┐
                    │  F4. Kitchen      │
                    │  (Send → Start →  │
                    │   Complete → Serve)│
                    └─────────┬─────────┘
                              │
                    ┌─────────▼─────────┐
                    │  F5. Payment      │
                    │  (Receive/Refund) │
                    └─────────┬─────────┘
                              │
                    ┌─────────▼─────────┐
                    │  F6. Receipt      │
                    │  (Print/Reprint)  │
                    └─────────┬─────────┘
                              │
                    ┌─────────▼─────────┐
                    │  F7. Reporting    │
                    │  F10. Dashboard   │
                    └───────────────────┘
```

### Key Dependency Rules

1. **Order → Kitchen**: Confirmed orders can be sent to kitchen
2. **Order → Payment**: Only completed orders can be paid
3. **Payment → Receipt**: Receipt prints after successful payment
4. **Table → Order**: Table must be assigned before order can be created for dine-in
5. **Payment → Table**: Table can only be released after payment is complete
6. **All → Reporting**: Reports aggregate data from all modules

---

## 6 Sprint Proposal

### Sprint 1: Foundation + Order Core UI

**Goal**: Working order creation with menu selection

| Feature | Description |
|---------|-------------|
| F9.1 | Staff Login (JWT) — backend only |
| F8.7 | Login Screen |
| F8.1 | Cashier UI — Order Screen (Create Order, Add Item, Remove Item) |
| F8.2 | Cashier UI — Menu Selection (browse products, categories) |
| F1.1–F1.3 | Order Create/Add/Remove (API already frozen, UI integration) |

**Deliverable**: Cashier can log in, browse menu, create order, add/remove items

---

### Sprint 2: Order Completion + Kitchen

**Goal**: Complete order workflow + kitchen receives tickets

| Feature | Description |
|---------|-------------|
| F1.4–F1.6 | Confirm/Cancel/Complete Order (UI integration) |
| F4.1 | Send Order to Kitchen (UI trigger) |
| F8.5 | Kitchen Display UI (view active tickets) |
| F4.2–F4.5 | Kitchen status updates (Start/Complete/Serve) |
| F6.2 | Print Kitchen Ticket |

**Deliverable**: Cashier confirms order → Kitchen sees ticket → Kitchen prepares → marks served

---

### Sprint 3: Table Management + Payment

**Goal**: Full dine-in workflow with payment

| Feature | Description |
|---------|-------------|
| F8.3 | Cashier UI — Table Management (table layout view) |
| F3.1–F3.2, F3.6 | Table Assign/Release (UI integration) |
| F8.4 | Cashier UI — Payment Screen |
| F5.1–F5.2 | Receive Payment (Cash/Card/QR/Credit) |
| F6.1 | Print Customer Receipt |

**Deliverable**: Complete dine-in flow: Assign table → Order → Kitchen → Pay → Release table → Print receipt

---

### Sprint 4: Advanced Operations + Reporting

**Goal**: Table transfer/merge/split, refunds, reports

| Feature | Description |
|---------|-------------|
| F3.3–F3.5 | Transfer/Merge/Split Tables |
| F5.3 | Refund Payment |
| F5.4 | Payment History |
| F6.3–F6.4 | Refund Receipt / Reprint |
| F7.1–F7.3 | Reports (Daily Sales, By Payment, Best Sellers) |
| F8.6 | Manager Dashboard UI |

**Deliverable**: Full restaurant POS with all operations and reporting

---

### Sprint 5: Dashboard + Polish

**Goal**: Real-time dashboard, performance, edge cases

| Feature | Description |
|---------|-------------|
| F10.1–F10.4 | Dashboard (Sales overview, charts, table occupancy) |
| Error handling edge cases | Network errors, timeout handling |
| UI polish | Loading states, empty states, confirmation dialogs |
| Performance | API response time optimization, UI lazy loading |

**Deliverable**: Production-ready POS system

---

## 7 Definition of Ready

Before implementation begins on any feature, the following must be complete:

| Criteria | Description |
|----------|-------------|
| **Business Scenario** | Clear description of what the user does and what the system does |
| **Architecture** | Architecture design reviewed and approved (frozen for backend, new for UI) |
| **Acceptance Criteria** | Specific, testable conditions that define "done" |
| **Implementation Tasks** | Broken down into small, independently verifiable tasks |
| **Dependencies Met** | All upstream features are complete |
| **Git Checkpoint** | Clean commit or branch created before starting |

---

## 8 Definition of Done

A feature is complete only when ALL of the following are satisfied:

| Criteria | Description |
|----------|-------------|
| ✅ Build Passes | `dotnet build` succeeds with no errors |
| ✅ Tests Pass | All existing + new tests pass (`dotnet test`) |
| ✅ Architecture Preserved | No architecture drift from frozen decisions |
| ✅ Business Rules Preserved | All business rules remain in Domain layer |
| ✅ Naming Correct | Follows project naming conventions |
| ✅ No Analyzer Violations | No warnings or analyzer errors |
| ✅ Self Review Complete | AI has reviewed its own work |
| ✅ Human Review Complete | Human has approved the implementation |
| ✅ Documentation Updated | AI Context and Project Status updated if needed |
| ✅ Commit Ready | Changes are committed with meaningful message |

---

## 9 Master Checklist

### Backend (✅ Complete — Frozen)

- [x] F1.1 Create Order
- [x] F1.2 Add Item
- [x] F1.3 Remove Item
- [x] F1.4 Confirm Order
- [x] F1.5 Cancel Order
- [x] F1.6 Complete Order
- [x] F2.1 Create/Edit Product
- [x] F2.2 Manage Categories
- [x] F2.3 Manage Ingredients
- [x] F2.4 Manage Modifiers/Option Groups
- [x] F2.5 Product Availability/Visibility
- [x] F3.1 Create Table
- [x] F3.2 Assign Table
- [x] F3.3 Transfer Table
- [x] F3.4 Merge Tables
- [x] F3.5 Split Tables
- [x] F3.6 Release Table
- [x] F4.1 Send to Kitchen (Create KitchenTicket)
- [x] F4.2 View Active Tickets
- [x] F4.3 Start Preparation
- [x] F4.4 Complete Preparation
- [x] F4.5 Mark as Served
- [x] F5.1 Receive Payment (Cash)
- [x] F5.2 Receive Payment (Card/QR/Credit)
- [x] F5.3 Refund Payment
- [x] F5.4 View Payment History
- [x] F6.1 Print Customer Receipt
- [x] F6.2 Print Kitchen Ticket
- [x] F6.3 Print Refund Receipt
- [x] F6.4 Reprint Receipt
- [x] F7.1 Daily Sales Report
- [x] F7.2 Sales by Payment Method
- [x] F7.3 Best Sellers Report
- [x] Printing Infrastructure (ESC/POS, USB, LAN, Null)
- [x] Global Exception Handling Middleware
- [x] CI/CD Pipeline (GitHub Actions)

### Frontend / UI (❌ Not Started)

- [ ] F8.1 Cashier UI — Order Screen
- [ ] F8.2 Cashier UI — Menu Selection
- [ ] F8.3 Cashier UI — Table Management
- [ ] F8.4 Cashier UI — Payment Screen
- [ ] F8.5 Kitchen Display UI
- [ ] F8.6 Manager Dashboard UI
- [ ] F8.7 Login Screen
- [ ] F9.1 Staff Login (JWT)
- [ ] F9.2 Role-based Access
- [ ] F9.3 Session Management
- [ ] F10.1 Real-time Sales Overview
- [ ] F10.2 Order Volume Chart
- [ ] F10.3 Top-selling Items
- [ ] F10.4 Table Occupancy View

---

## 10 Risks

| Risk | Impact | Mitigation |
|------|--------|------------|
| **R1: UI Technology Choice** | Choosing wrong framework causes rework | Evaluate Blazor (native .NET) vs React/Vue before Sprint 1 |
| **R2: Real-time Kitchen Updates** | Kitchen needs live updates without page refresh | SignalR (native .NET) for real-time communication |
| **R3: Authentication Implementation** | JWT implementation may affect all endpoints | Design auth middleware early, test with integration tests |
| **R4: Printer Hardware Compatibility** | ESC/POS may vary by printer model | Use frozen EscPosRenderer with configurable code pages |
| **R5: Multi-table Merge Complexity** | Merge/split operations have edge cases | Covered by existing 17 Table integration tests |
| **R6: Concurrent Order Operations** | Two cashiers modifying same order | Aggregate Root pattern already handles this via status checks |
| **R7: Database Migration for New Features** | Schema changes may require migration | Use EF Core migrations; test with Testcontainers |
| **R8: UI State Management** | Complex order state across screens | Define clear state management pattern before UI implementation |

---

## 11 Recommendations

### Recommended Implementation Order

```
Sprint 1 ────► Sprint 2 ────► Sprint 3 ────► Sprint 4 ────► Sprint 5
Foundation      Kitchen          Payment         Advanced        Dashboard
+ Order Core    Integration      + Table         Operations      + Polish
```

### Key Architectural Decisions to Make Before Sprint 1

1. **UI Framework**: Blazor (recommended — native .NET, shared types, SignalR built-in) vs React/Vue
2. **Authentication**: JWT with ASP.NET Core Identity or custom middleware
3. **Real-time**: SignalR for kitchen display updates
4. **UI Project Structure**: New `JLek.POS.Web` project (already exists in solution) or separate SPA

### Reuse Strategy

| Component | Reuse From |
|-----------|------------|
| API Client Types | C# DTOs → TypeScript types (auto-generate or manual mapping) |
| Validation Rules | Domain Business Rules → UI validation |
| Response DTOs | Existing `FromDomain()` patterns → UI models |
| Integration Test Pattern | Testcontainers + WebApplicationFactory → UI integration tests |
| Error Handling | Existing ProblemDetails → UI error display |

---

## Appendix A: Architecture Compliance

This plan preserves all frozen architecture decisions:

| Frozen Decision | Status |
|-----------------|--------|
| Clean Architecture | ✅ Preserved — UI is a new Presentation layer, not a replacement |
| DDD | ✅ Preserved — Domain unchanged |
| CQRS | ✅ Preserved — Backend CQRS unchanged |
| Repository Pattern | ✅ Preserved — Backend repositories unchanged |
| Aggregate Boundaries | ✅ Preserved — No aggregate boundary changes |
| Business Rules | ✅ Preserved — No business rule changes |
| API Contracts | ✅ Preserved — No API contract changes |
| Module Dependencies | ✅ Preserved — No dependency direction changes |

## 12 Traceability Matrix

Maps every Feature to its source documentation and repository evidence.

| Feature | Business Rules Source | Architecture Source | Repository Evidence |
|---------|----------------------|-------------------|---------------------|
| F1.1–F1.6 Order CRUD | docs/01-business-rules/order/ | docs/96-architecture/order-module.md | src/JLek.POS.Domain/Orders/Order.cs |
| F2.1–F2.5 Menu/Catalog | docs/01-business-rules/catalog/ | docs/96-architecture/menu-module.md | src/JLek.POS.Domain/Products/Product.cs |
| F3.1–F3.6 Table Mgmt | docs/01-business-rules/table/ | docs/96-architecture/table-module.md | src/JLek.POS.Domain/Tables/DiningTable.cs |
| F4.1–F4.5 Kitchen | docs/01-business-rules/kitchen/ | docs/96-architecture/kitchen-module.md | src/JLek.POS.Domain/Kitchen/KitchenTicket.cs |
| F5.1–F5.4 Payment | docs/01-business-rules/payment/ | docs/96-architecture/payment-module.md | src/JLek.POS.Domain/Payments/Payment.cs |
| F6.1–F6.4 Receipt | docs/01-business-rules/receipt/ | docs/96-architecture/receipt-module.md | src/JLek.POS.Application/Features/Receipt/ |
| F7.1–F7.3 Reporting | docs/01-business-rules/reporting/ | docs/96-architecture/reporting-module.md | src/JLek.POS.Application/Features/Reports/ |
| F8.1–F8.7 Web UI | docs/09-ui/ | docs/96-architecture/ui-architecture.md | src/JLek.POS.Web/ (existing project) |
| F9.1–F9.3 Auth | docs/06-technical/authentication.md | docs/96-architecture/auth-architecture.md | Not yet implemented |
| F10.1–F10.4 Dashboard | docs/09-ui/dashboard.md | docs/96-architecture/dashboard-architecture.md | Not yet implemented |

### Traceability Rules

1. Every Feature must trace to at least one Business Rules document
2. Every Feature must trace to at least one Architecture document
3. Repository evidence must be verified before implementation begins
4. If source documentation does not exist, it must be created before implementation
5. AI must never implement a Feature without verified traceability

---

## 13 Sprint Exit Criteria

Each Sprint must satisfy ALL criteria before the next Sprint begins.

### Sprint 1: Foundation + Order Core UI

| Criteria | Description |
|----------|-------------|
| ✅ Build | `dotnet build` succeeds |
| ✅ Tests | All existing 155 integration tests + 57 unit tests pass |
| ✅ Auth | JWT login working (backend + UI) |
| ✅ Order UI | Cashier can create order, browse menu, add/remove items |
| ✅ API Integration | UI successfully calls all Order + Menu endpoints |
| ✅ No Regression | Existing frozen modules unchanged |
| ✅ Architecture | No architecture drift |
| ✅ Review | Human review completed |

### Sprint 2: Order Completion + Kitchen

| Criteria | Description |
|----------|-------------|
| ✅ Build | `dotnet build` succeeds |
| ✅ Tests | All existing + new kitchen UI tests pass |
| ✅ Order Flow | Confirm, cancel, complete order from UI |
| ✅ Kitchen UI | Kitchen display shows active tickets, status updates work |
| ✅ Real-time | Kitchen receives updates without manual refresh |
| ✅ Print | Kitchen ticket prints successfully |
| ✅ Review | Human review completed |

### Sprint 3: Table Management + Payment

| Criteria | Description |
|----------|-------------|
| ✅ Build | `dotnet build` succeeds |
| ✅ Tests | All existing + new table/payment UI tests pass |
| ✅ Table UI | Assign, release table from UI |
| ✅ Payment UI | Receive cash/card/QR/credit payment from UI |
| ✅ Receipt | Customer receipt prints after payment |
| ✅ Full Flow | Assign table → Order → Kitchen → Pay → Release → Receipt |
| ✅ Review | Human review completed |

### Sprint 4: Advanced Operations + Reporting

| Criteria | Description |
|----------|-------------|
| ✅ Build | `dotnet build` succeeds |
| ✅ Tests | All existing + new tests pass |
| ✅ Transfer/Merge/Split | Table operations work from UI |
| ✅ Refund | Refund payment + print refund receipt |
| ✅ Reports | Daily sales, by payment, best sellers display in UI |
| ✅ Manager UI | Manager can view reports |
| ✅ Review | Human review completed |

### Sprint 5: Dashboard + Polish

| Criteria | Description |
|----------|-------------|
| ✅ Build | `dotnet build` succeeds |
| ✅ Tests | All tests pass |
| ✅ Dashboard | Real-time sales, charts, table occupancy |
| ✅ Error Handling | Network errors, timeouts handled gracefully |
| ✅ UI Polish | Loading states, empty states, confirmations |
| ✅ Performance | API response < 500ms, UI loads < 2s |
| ✅ Review | Human review completed |

---

## 14 AI Execution Rules

These rules govern how AI must behave during implementation of this plan.

### Rule 1: Follow AI Workflow

Every implementation task must follow the 14-phase AI Workflow defined in `docs/97-AI-Docs/03-ai-workflow.md`:

```
Documentation → Verification → Existing Implementation Review → Understanding → Analysis → Design → Evidence Audit → Human Review → Approval → Implementation → Build Verification → Runtime Verification → Self Review → Documentation Update
```

### Rule 2: One Milestone at a Time

- Implement only ONE feature per milestone
- Each milestone must be independently buildable, testable, and reviewable
- Stop after completing the approved milestone
- Do not automatically proceed to the next milestone

### Rule 3: Git Checkpoint Before Implementation

Before any file modification:
```
git commit -m "checkpoint: before <feature-name>"
```
or create a branch:
```
git checkout -b feature/<feature-name>
```

### Rule 4: Read Before Code

Before writing any code:
1. Read the relevant Business Rules documentation
2. Read the relevant Architecture documentation
3. Read the existing implementation (repository evidence)
4. Verify all references

### Rule 5: Never Guess

If any of the following is unclear:
- Business Rules
- Architecture
- Implementation pattern
- API contract

STOP and ask for clarification.

### Rule 6: Preserve Frozen Architecture

- Do NOT modify frozen modules
- Do NOT change aggregate boundaries
- Do NOT change business rules
- Do NOT change API contracts
- Do NOT change repository contracts
- Do NOT change CQRS pattern

### Rule 7: Reuse Before Create

Before creating any new:
- Class
- Interface
- Method
- Pattern

Verify that an existing implementation can be reused.

### Rule 8: Self Review Before Completion

Before marking any task complete:
- [ ] Build passes
- [ ] All tests pass
- [ ] Architecture preserved
- [ ] Business rules preserved
- [ ] Naming follows conventions
- [ ] No analyzer violations
- [ ] Documentation updated if needed

### Rule 9: Report Format

Every implementation report must use:

```
## Verified Facts
## Findings
## Recommendations
```

### Rule 10: Stop Conditions

Stop immediately and report when:
- Documentation is insufficient
- Repository evidence cannot be verified
- Business rules are unclear
- Architecture is unclear
- Human approval has not been received

---

## 15 Implementation Metrics

Track the following metrics for every Sprint.

### Quality Metrics

| Metric | Target | Measurement |
|--------|--------|-------------|
| Build Success Rate | 100% | `dotnet build` exit code |
| Test Pass Rate | 100% | `dotnet test` pass/fail count |
| Test Coverage | ≥ 80% new code | `dotnet test --collect:"XPlat Code Coverage"` |
| Architecture Drift | 0 violations | Manual review per Sprint |
| Business Rule Violations | 0 violations | Manual review per Sprint |
| Analyzer Warnings | 0 | `dotnet build --no-restore` warning count |

### Progress Metrics

| Metric | Target | Measurement |
|--------|--------|-------------|
| Features per Sprint | 3–5 | Count of completed features |
| Files Modified | As needed | `git diff --stat` |
| Lines of Code | As needed | `git diff --stat` |
| New Tests | ≥ 5 per new feature | Count of new test methods |
| Review Cycle Time | < 1 day | Time from submission to approval |

### Performance Metrics

| Metric | Target | Measurement |
|--------|--------|-------------|
| API Response Time (p95) | < 500ms | Integration test timing |
| UI Page Load | < 2s | Browser DevTools |
| Real-time Latency | < 1s | SignalR round-trip |
| Database Query Time (p95) | < 100ms | EF Core logging |

### Risk Metrics

| Metric | Target | Measurement |
|--------|--------|-------------|
| Blocked Features | 0 | Count of features blocked by dependencies |
| Unresolved Risks | 0 | Count of risks from Section 10 not mitigated |
| Rework Required | 0 | Count of features requiring re-implementation |

### Reporting

After each Sprint, update:
1. `docs/97-AI-Docs/99-project-status.md` — Overall progress
2. `docs/97-AI-Docs/110-master-implementation-plan.md` — Sprint checklist
3. `docs/CHANGELOG.md` — Sprint summary

---

## Appendix B: Technology Stack Recommendations

| Layer | Recommended | Alternative |
|-------|-------------|-------------|
| UI Framework | Blazor WebAssembly (native .NET) | React, Vue, Angular |
| Real-time | SignalR | WebSocket, gRPC |
| Authentication | JWT + ASP.NET Core Identity | Auth0, Firebase Auth |
| UI Component Library | MudBlazor or Radzen (Blazor) | Material-UI (React) |
| Charts | Chart.js (via Blazor wrapper) | D3.js, ApexCharts |
| HTTP Client | Generated C# client → TypeScript | Swagger Codegen |
| State Management | Blazor built-in | Redux (React), Pinia (Vue) |