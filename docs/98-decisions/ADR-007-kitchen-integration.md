# ADR-005: Kitchen Integration

## Status

Accepted

---

## Context

Order→Kitchen integration is required: when an Order is Confirmed, a Kitchen Ticket must be created. The integration must preserve Aggregate boundaries and CQRS separation.

Sprint 2.2 confirmed: Direct Handler Call (ConfirmOrderHandler → CreateKitchenTicketCommandHandler) was rejected because it blurs CQRS boundaries and creates coupling.

---

## Decision

1. **Integration Method**: Event-driven (deferred until Event Infrastructure exists)
2. **Trigger**: `OrderConfirmedEvent` raised by `Order.Confirm()`
3. **Handler**: `OrderConfirmedEventHandler`
4. **Handler Injects**: `IKitchenTicketRepository` (NOT `CreateKitchenTicketCommandHandler`)
5. **Flow**:

```
ConfirmOrderCommandHandler
  ├── Load Order
  ├── order.Confirm()
  │     └── raises OrderConfirmedEvent (in memory)
  ├── _orderRepository.UpdateAsync()
  │
  ▼
[SaveChanges → Dispatcher → OrderConfirmedEventHandler]
  │
  ├── Load data from event (OrderId)
  ├── Load Order items (through IOrderRepository read if needed)
  ├── Create KitchenTicket aggregate with item snapshots
  ├── _kitchenRepository.AddAsync()
  └── Done
```

6. **Timing**: Implement only after Event Infrastructure (dispatcher + handler interface) exists

---

## Alternatives Considered

| Alternative | Reason Not Chosen |
|-------------|-------------------|
| Direct Handler Call | Blurs CQRS. Couples Order→Kitchen. Violates project principle of clean aggregate boundaries. |
| UI-initiated | Cashier would need to manually call POST /kitchen after confirming order. Poor UX. |

---

## Trade-offs

| Trade-off | Detail |
|-----------|--------|
| Deferred implementation | Kitchen integration is blocked until Event Infrastructure is implemented. Acceptable because Event Infrastructure is estimated at ~150 lines. |
| Non-atomic | If Kitchen ticket creation fails, Order remains confirmed without a ticket. Accepted v1 trade-off. Compensating action: manual retry or dashboard alert. |

---

## Consequences

1. Order module has no dependency on Kitchen module
2. New integrations (Notification, Analytics) follow the same pattern without modifying Order
3. KitchenTicket receives snapshot data (ItemName, Quantity, Notes) — not references to OrderItem entities

## Repository Evidence

- `Order.cs:87-95` — `Order.Confirm()` raises `OrderConfirmedEvent`
- `OrderConfirmedEvent.cs` — Contains `OrderId`
- `KitchenTicket.cs` — Snapshot aggregate with `KitchenItem` entities
- `KitchenItem.cs` — `ItemName`, `Quantity`, `Notes` — no Product/Order reference

## Human Approval

Approved