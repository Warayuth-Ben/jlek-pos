# Feature Registry

Version: 1.0
Last Updated: 2026-07-18

---

## Feature Lifecycle Stages

| Stage | Description |
|-------|-------------|
| **Designed** | Business scenario + architecture approved |
| **Modeled** | Domain model + events + rules complete |
| **Implemented** | Code written in all layers |
| **API Ready** | API endpoints deployed |
| **Client Ready** | Typed API Client exists |
| **UI Ready** | UI component available |
| **Verified** | Integration tests pass |
| **UAT** | User acceptance testing |
| **Frozen** | No further changes except security/bug fixes |

---

## Epic 1: Order Management

| ID | Name | Stage | Priority | Business Scenarios | API | UI | Tests | Frozen |
|----|------|-------|----------|-------------------|-----|----|-------|--------|
| F1.1 | Create Order | ✅ Frozen | P0 | Q011–Q015 | `POST /orders` | `OrderPanel.razor` | Table/Order | ✅ |
| F1.2 | Add Item | ✅ Frozen | P0 | Q021–Q025 | `POST /orders/{id}/items` | `MenuModal.razor` | Order | ✅ |
| F1.3 | Remove Item | ✅ Frozen | P0 | Q026 | `DELETE /orders/{id}/items/{itemId}` | ❌ Not in UI | Order | ✅ |
| F1.4 | Confirm Order | ✅ Frozen | P0 | Q031–Q032 | `POST /orders/{id}/confirm` | `OrderPanel.razor` | Order | ✅ |
| F1.5 | Cancel Order | ✅ Frozen | P0 | Q033 | `POST /orders/{id}/cancel` | ❌ Not in UI | Order | ✅ |
| F1.6 | Complete Order | ✅ Frozen | P0 | Q034 | `POST /orders/{id}/complete` | ❌ Not in UI | Order | ✅ |

## Epic 2: Menu/Catalog Management

| ID | Name | Stage | Priority | Business Scenarios | API | UI | Tests | Frozen |
|----|------|-------|----------|-------------------|-----|----|-------|--------|
| F2.1 | Create/Edit Product | ✅ Frozen | P0 | Q041–Q045 | `POST/GET /products` | ❌ Not in UI | 31 tests | ✅ |
| F2.2 | Manage Categories | ✅ Frozen | P0 | Q046–Q050 | `POST/GET /categories` | ❌ Not in UI | 13 tests | ✅ |
| F2.3 | Manage Ingredients | ✅ Frozen | P0 | Q051–Q055 | `POST/GET /ingredients` | ❌ Not in UI | 10 tests | ✅ |
| F2.4 | Manage Modifiers | ✅ Frozen | P0 | Q056 | `POST /products/{id}/modifiers` | ❌ Not in UI | Product | ✅ |
| F2.5 | Availability/Visibility | ✅ Frozen | P0 | Q057–Q058 | `POST /products/{id}/availability` | ❌ Not in UI | Product | ✅ |

## Epic 3: Table Management

| ID | Name | Stage | Priority | Business Scenarios | API | UI | Tests | Frozen |
|----|------|-------|----------|-------------------|-----|----|-------|--------|
| F3.0 | Open Table | ✅ Frozen | P0 | Q001 | `POST /tables/{id}/open` | `OrderPanel.razor` | 17 tests | ✅ |
| F3.1 | Create Table | ✅ Frozen | P0 | Q001 | `POST /tables` | ❌ Not in UI | 17 tests | ✅ |
| F3.2 | Assign Table | ✅ Frozen | P0 | Q002 | `POST /tables/{id}/assign` | ❌ Not in UI | 17 tests | ✅ |
| F3.3 | Transfer Table | ✅ Frozen | P1 | Q003 | `POST /tables/{id}/transfer` | ❌ Not in UI | 17 tests | ✅ |
| F3.4 | Merge Tables | ✅ Frozen | P1 | Q004 | `POST /tables/{id}/merge` | ❌ Not in UI | 17 tests | ✅ |
| F3.5 | Split Tables | ✅ Frozen | P1 | Q005 | `POST /tables/{id}/split` | ❌ Not in UI | 17 tests | ✅ |
| F3.6 | Release Table | ✅ Frozen | P0 | Q006 | `POST /tables/{id}/release` | `OrderPanel.razor` | 17 tests | ✅ |

## Epic 4: Kitchen Display

| ID | Name | Stage | Priority | Business Scenarios | API | UI | Tests | Frozen |
|----|------|-------|----------|-------------------|-----|----|-------|--------|
| F4.1 | Send to Kitchen | ✅ Frozen | P0 | Q061 | Via Event: `OrderConfirmedEvent` | Auto-triggered | 21 tests | ✅ |
| F4.2 | View Active Tickets | ✅ Frozen | P0 | Q062–Q063 | `GET /kitchen/active` | ❌ Not in UI | 21 tests | ✅ |
| F4.3 | Start Preparation | ✅ Frozen | P0 | Q064 | `POST /kitchen/{id}/start` | ❌ Not in UI | 21 tests | ✅ |
| F4.4 | Complete Preparation | ✅ Frozen | P0 | Q065 | `POST /kitchen/{id}/complete` | ❌ Not in UI | 21 tests | ✅ |
| F4.5 | Mark as Served | ✅ Frozen | P0 | Q066 | `POST /kitchen/{id}/serve` | ❌ Not in UI | 21 tests | ✅ |

## Epic 5: Payment Processing

| ID | Name | Stage | Priority | Business Scenarios | API | UI | Tests | Frozen |
|----|------|-------|----------|-------------------|-----|----|-------|--------|
| F5.1 | Receive Payment (Cash) | ✅ Frozen | P0 | Q071 | `POST /payments` | `OrderPanel.razor` | 18 tests | ✅ |
| F5.2 | Receive Payment (Card/QR/Credit) | ✅ Frozen | P0 | Q072–Q074 | `POST /payments` | `OrderPanel.razor` | 18 tests | ✅ |
| F5.3 | Refund Payment | ✅ Frozen | P1 | Q075 | `POST /payments/{id}/refund` | ❌ Not in UI | 18 tests | ✅ |
| F5.4 | View Payment History | ✅ Frozen | P1 | Q076 | `GET /payments?orderId=` | ❌ Not in UI | 18 tests | ✅ |

## Epic 6: Receipt Printing

| ID | Name | Stage | Priority | Business Scenarios | API | UI | Tests | Frozen |
|----|------|-------|----------|-------------------|-----|----|-------|--------|
| F6.1 | Print Customer Receipt | ✅ Frozen | P0 | Q081 | `POST /receipts/customer-print` | `CashierPage → ReceiptClient` | 21 tests | ✅ |
| F6.2 | Print Kitchen Ticket | ✅ Frozen | P0 | Q082 | `POST /receipts/kitchen-print` | ❌ Not in UI | 21 tests | ✅ |
| F6.3 | Print Refund Receipt | ✅ Frozen | P1 | Q083 | `POST /receipts/refund-print` | ❌ Not in UI | 21 tests | ✅ |
| F6.4 | Reprint Receipt | ✅ Frozen | P1 | Q084 | Same as F6.1 with `IsReprint` | ❌ Not in UI | 21 tests | ✅ |

## Epic 7: Reporting

| ID | Name | Stage | Priority | Business Scenarios | API | UI | Tests | Frozen |
|----|------|-------|----------|-------------------|-----|----|-------|--------|
| F7.1 | Daily Sales Report | ✅ Frozen | P1 | Q091 | `GET /reports/daily-sales` | ❌ Not in UI | 24 tests | ✅ |
| F7.2 | Sales by Payment | ✅ Frozen | P1 | Q092 | `GET /reports/sales-by-payment` | ❌ Not in UI | 24 tests | ✅ |
| F7.3 | Best Sellers | ✅ Frozen | P1 | Q093 | `GET /reports/best-sellers` | ❌ Not in UI | 24 tests | ✅ |

## Epic 8: Web UI

| ID | Name | Stage | Priority | Related API | UI File | Integration |
|----|------|-------|----------|-------------|---------|-------------|
| F8.1 | Cashier — Order Screen | 🟡 **UI Ready** | P0 | Order, Menu, Payment | `OrderPanel.razor` | Partial: Create, Add, Confirm, Pay, Release done. Remove, Cancel missing |
| F8.2 | Cashier — Menu Selection | 🟡 **UI Ready** | P0 | Menu | `MenuModal.razor` | Partial: Browse by category, Add done. Search, Favorites missing |
| F8.3 | Cashier — Table Management | 🟡 **UI Ready** | P0 | Table | `TableGrid.razor` | Partial: Open, Release done. Transfer, Merge, Split missing |
| F8.4 | Cashier — Payment Screen | 🟡 **UI Ready** | P0 | Payment | `OrderPanel.razor` | Partial: Cash, Card, PromptPay done. QR display, Credit form missing |
| F8.5 | Kitchen Display | ❌ **Not Started** | P0 | Kitchen API | — | No UI component exists |
| F8.6 | Manager Dashboard | ❌ **Not Started** | P1 | Reports API | — | No UI component exists |
| F8.7 | Login Screen | ❌ **Not Started** | P0 | Auth (not implemented) | — | No UI component exists |

## Epic 9: Authentication & Authorization

| ID | Name | Stage | Priority | Notes |
|----|------|-------|----------|-------|
| F9.1 | Staff Login (JWT) | ❌ Not Started | P0 | No Identity, no JWT middleware |
| F9.2 | Role-based Access | ❌ Not Started | P0 | No role checks anywhere |
| F9.3 | Session Management | ❌ Not Started | P0 | No session middleware |

## Epic 10: Dashboard

| ID | Name | Stage | Priority | Notes |
|----|------|-------|----------|-------|
| F10.1 | Real-time Sales Overview | ❌ Not Started | P1 | No SignalR hub |
| F10.2 | Order Volume Chart | ❌ Not Started | P1 | No charts |
| F10.3 | Top-selling Items | ❌ Not Started | P1 | Best Sellers API exists, no UI |
| F10.4 | Table Occupancy | ❌ Not Started | P1 | Table API exists, no dashboard |

---

## Cross-Reference

| Epic | File |
|------|------|
| Feature Lifecycle | `PROJECT-GOVERNANCE.md` |
| Traceability | `TRACEABILITY-MATRIX.md` |
| Project Status | `99-project-status.md` |
| Master Plan | `110-master-implementation-plan.md` |
| Change Log | `CHANGELOG.md` |

Governance Version

v1.0 

Baseline Approved