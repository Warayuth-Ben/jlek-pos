# Order State Machine

## Purpose

Defines the lifecycle of an Order from creation until completion or cancellation.

The Order State Machine is the authoritative specification for all Order state transitions.

---

## Lifecycle Diagram

```text
                  Confirm Order
Draft --------------------------------▶ Confirmed
  │                                        │
  │ Cancel Order                           │ Serve Items
  ▼                                        ▼
Cancelled                           Completed
```

---

## States

| State | Editable | Final | Description |
|--------|:--------:|:-----:|-------------|
| Draft | Yes | No | The Order is being created and may be modified. |
| Confirmed | No | No | The Order has been accepted and is awaiting fulfillment. |
| Completed | No | Yes | All associated Kitchen Tickets have been served. |
| Cancelled | No | Yes | The Order has been cancelled and can no longer be processed. |

---

## Transition Matrix

| Current State | Actor | Trigger | Guard | Next State |
|---------------|-------|----------|-------|------------|
| Draft | Restaurant Staff | Confirm Order | Order contains at least one Order Item | Confirmed |
| Draft | Restaurant Staff | Cancel Order | Cancellation permitted | Cancelled |
| Confirmed | Kitchen Staff | Serve Items | All Kitchen Tickets are Served | Completed |

---

## Constraints

- Every Order belongs to exactly one Order Session.
- Draft Orders may be modified.
- Confirmed Orders cannot be structurally modified.
- Customer changes after confirmation must create a new Add-on Order.
- Completed Orders are immutable.
- Cancelled Orders are immutable.
- Direct transitions not defined in the Transition Matrix are prohibited.

---

## Related System Use Cases

- Create Order
- Confirm Order
- Update Order
- Create Add-on Order
- Cancel Order
- Serve Items

---

## Related Domain Objects

### Aggregate

- Order Session

### Entities

- Order
- Order Item

### Value Objects

- Money
- Quantity

---

## Notes

The Order lifecycle is independent of payment.

Payment completion is governed by the Bill Aggregate.

Kitchen preparation is governed by the Kitchen Ticket Aggregate.