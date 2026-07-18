# Traceability Matrix

Version: 1.0
Last Updated: 2026-07-18

---

## Traceability Chain

Every Business Scenario → Feature → Domain → Application → API → Typed Client → UI → Test → Documentation

---

## Epic 1: Order Management

| BS | Feature | Domain | Application | API | Typed Client | UI | Test | Doc |
|----|---------|--------|-------------|-----|-------------|----|------|-----|
| Q011–Q015 | F1.1 Create Order | `Order.Create()` | `CreateOrderCommandHandler` | `POST /tables/{tableId}/orders` | `IOrderClient.CreateAsync()` | `OrderPanel.razor` | Table/Order | `order-module.md` |
| Q021–Q025 | F1.2 Add Item | `Order.AddItem()` | `AddItemCommandHandler` | `POST /orders/{id}/items` | `IOrderClient.AddItemAsync()` | `MenuModal.razor` | Order | `order-module.md` |
| Q026 | F1.3 Remove Item | `Order.RemoveItem()` | `RemoveItemCommandHandler` | `DELETE /orders/{id}/items/{itemId}` | ❌ Not in client | ❌ Not in UI | Order | `order-module.md` |
| Q031–Q032 | F1.4 Confirm Order | `Order.Confirm()` | `ConfirmOrderCommandHandler` | `POST /orders/{id}/confirm` | `IOrderClient.ConfirmAsync()` | `OrderPanel.razor` | Order | `order-module.md` |
| Q033 | F1.5 Cancel Order | `Order.Cancel()` | `CancelOrderCommandHandler` | `POST /orders/{id}/cancel` | ❌ Not in client | ❌ Not in UI | Order | `order-module.md` |
| Q034 | F1.6 Complete Order | `Order.Complete()` | `CompleteOrderCommandHandler` | `POST /orders/{id}/complete` | ❌ Not in client | ❌ Not in UI | Order | `order-module.md` |

## Epic 2: Menu/Catalog Management

| BS | Feature | Domain | Application | API | Typed Client | UI | Test | Doc |
|----|---------|--------|-------------|-----|-------------|----|------|-----|
| Q041–Q045 | F2.1 Create/Edit Product | `Product` | `CreateProductCommandHandler` | `POST/GET /products` | `IMenuClient` (read-only) | ❌ Not in UI | 31 tests | `menu-module.md` |
| Q046–Q050 | F2.2 Manage Categories | `ProductCategory` | `CreateProductCategoryCommandHandler` | `POST/GET /categories` | `IMenuClient.GetCategoriesAsync()` | ❌ Not in UI | 13 tests | `menu-module.md` |
| Q051–Q055 | F2.3 Manage Ingredients | `Ingredient` | `CreateIngredientCommandHandler` | `POST/GET /ingredients` | ❌ Not in client | ❌ Not in UI | 10 tests | `menu-module.md` |
| Q056 | F2.4 Manage Modifiers | `Product.AddModifier()` | `AddModifierCommandHandler` | `POST /products/{id}/modifiers` | ❌ Not in client | ❌ Not in UI | Product tests | `menu-module.md` |
| Q057–Q058 | F2.5 Availability | `Product.SetAvailability()` | `SetAvailabilityCommandHandler` | `POST /products/{id}/availability` | ❌ Not in client | ❌ Not in UI | Product tests | `menu-module.md` |

## Epic 3: Table Management

| BS | Feature | Domain | Application | API | Typed Client | UI | Test | Doc |
|----|---------|--------|-------------|-----|-------------|----|------|-----|
| Q001 | F3.0 Open Table | `DiningTable.Open()` | `OpenTableCommandHandler` | `POST /tables/{id}/open` | `ITableClient.OpenAsync()` | `OrderPanel.razor` | 17 tests | `table-module.md` |
| Q001 | F3.1 Create Table | `DiningTable.Create()` | `CreateDiningTableCommandHandler` | `POST /tables` | ❌ Not in client | ❌ Not in UI | 17 tests | `table-module.md` |
| Q002 | F3.2 Assign Table | `DiningTable.Assign()` | `AssignTableCommandHandler` | `POST /tables/{id}/assign` | ❌ Not in client | ❌ Not in UI | 17 tests | `table-module.md` |
| Q003 | F3.3 Transfer Table | `DiningTable.TransferTo()` | `TransferTableCommandHandler` | `POST /tables/{id}/transfer` | ❌ Not in client | ❌ Not in UI | 17 tests | `table-module.md` |
| Q004 | F3.4 Merge Tables | `DiningTable.MergeWith()` | `MergeTablesCommandHandler` | `POST /tables/{id}/merge` | ❌ Not in client | ❌ Not in UI | 17 tests | `table-module.md` |
| Q005 | F3.5 Split Tables | `DiningTable.Split()` | `SplitTableCommandHandler` | `POST /tables/{id}/split` | ❌ Not in client | ❌ Not in UI | 17 tests | `table-module.md` |
| Q006 | F3.6 Release Table | `DiningTable.Release()` | `ReleaseTableCommandHandler` | `POST /tables/{id}/release` | `ITableClient.ReleaseAsync()` | `OrderPanel.razor` | 17 tests | `table-module.md` |

## Epic 4: Kitchen Display

| BS | Feature | Domain | Application | API | Typed Client | UI | Test | Doc |
|----|---------|--------|-------------|-----|-------------|----|------|-----|
| Q061 | F4.1 Send to Kitchen | `KitchenTicket.Create()` | Via `OrderConfirmedEvent` | Auto via Event | Auto | Auto | 21 tests | `kitchen-module.md` |
| Q062–Q063 | F4.2 View Active Tickets | — | `GetActiveKitchenTicketsQueryHandler` | `GET /kitchen/active` | ❌ Not in client | ❌ Not in UI | 21 tests | `kitchen-module.md` |
| Q064 | F4.3 Start Preparation | `KitchenTicket.StartPreparation()` | `StartPreparationCommandHandler` | `POST /kitchen/{id}/start` | ❌ Not in client | ❌ Not in UI | 21 tests | `kitchen-module.md` |
| Q065 | F4.4 Complete Preparation | `KitchenTicket.CompletePreparation()` | `CompletePreparationCommandHandler` | `POST /kitchen/{id}/complete` | ❌ Not in client | ❌ Not in UI | 21 tests | `kitchen-module.md` |
| Q066 | F4.5 Mark as Served | `KitchenTicket.Serve()` | `ServeKitchenTicketCommandHandler` | `POST /kitchen/{id}/serve` | ❌ Not in client | ❌ Not in UI | 21 tests | `kitchen-module.md` |

## Epic 5: Payment Processing

| BS | Feature | Domain | Application | API | Typed Client | UI | Test | Doc |
|----|---------|--------|-------------|-----|-------------|----|------|-----|
| Q071 | F5.1 Receive Payment (Cash) | `Payment.Create()` | `ReceivePaymentCommandHandler` | `POST /payments` | `IPaymentClient.ReceivePaymentAsync()` | `OrderPanel.razor` | 18 tests | `payment-module.md` |
| Q072–Q074 | F5.2 Payment (Card/QR/Credit) | Same | Same handler | Same endpoint | Same client | `OrderPanel.razor` | 18 tests | `payment-module.md` |
| Q075 | F5.3 Refund Payment | `Payment.Refund()` | `RefundPaymentCommandHandler` | `POST /payments/{id}/refund` | ❌ Not in client | ❌ Not in UI | 18 tests | `payment-module.md` |
| Q076 | F5.4 Payment History | — | `GetPaymentsByOrderIdQueryHandler` | `GET /payments?orderId=` | ❌ Not in client | ❌ Not in UI | 18 tests | `payment-module.md` |

## Epic 6: Receipt Printing

| BS | Feature | Domain | Application | API | Typed Client | UI | Test | Doc |
|----|---------|--------|-------------|-----|-------------|----|------|-----|
| Q081 | F6.1 Print Customer Receipt | — | `PrintCustomerReceiptCommandHandler` | `POST /receipts/customer-print` | `IReceiptClient.PrintCustomerReceiptAsync()` | `CashierPage → ReceiptClient` | 21 tests | `receipt-module.md` |
| Q082 | F6.2 Print Kitchen Ticket | — | `PrintKitchenTicketCommandHandler` | `POST /receipts/kitchen-print` | ❌ Not in client | ❌ Not in UI | 21 tests | `receipt-module.md` |
| Q083 | F6.3 Print Refund Receipt | — | `PrintRefundReceiptCommandHandler` | `POST /receipts/refund-print` | ❌ Not in client | ❌ Not in UI | 21 tests | `receipt-module.md` |
| Q084 | F6.4 Reprint Receipt | — | Same as F6.1 with IsReprint | Same endpoint | Same client | ❌ Not in UI | 21 tests | `receipt-module.md` |

## Epic 7: Reporting

| BS | Feature | Domain | Application | API | Typed Client | UI | Test | Doc |
|----|---------|--------|-------------|-----|-------------|----|------|-----|
| Q091 | F7.1 Daily Sales | `IClock` | `GetDailySalesReportQueryHandler` | `GET /reports/daily-sales` | ❌ Not in client | ❌ Not in UI | 24 tests | `reporting-module.md` |
| Q092 | F7.2 Sales by Payment | — | `GetSalesByPaymentReportQueryHandler` | `GET /reports/sales-by-payment` | ❌ Not in client | ❌ Not in UI | 24 tests | `reporting-module.md` |
| Q093 | F7.3 Best Sellers | — | `GetBestSellerReportQueryHandler` | `GET /reports/best-sellers` | ❌ Not in client | ❌ Not in UI | 24 tests | `reporting-module.md` |

---

## Legend

| Symbol | Meaning |
|--------|---------|
| ✅ | Complete and frozen |
| 🟡 | Partially implemented |
| ❌ | Not started / Not in client / Not in UI |
| — | Not applicable (no Domain entity for this layer) |

---

## Cross-Reference

| Document | Location |
|----------|----------|
| Feature Registry | `FEATURE-REGISTRY.md` |
| Master Plan | `110-master-implementation-plan.md` |
| Business Scenarios | `docs/03-system-use-cases/` |
| Architecture Docs | `docs/96-architecture/` |
| ADR Documents | `docs/98-decisions/` |