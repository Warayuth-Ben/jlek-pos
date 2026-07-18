# ADR-006: Payment Integration

## Status

Accepted

---

## Context

Payment is a cashier-initiated workflow step. When an Order is Completed, the cashier must collect payment from the customer. The question is whether this should be automatic (event-driven) or explicit (user-initiated).

---

## Decision

1. **Payment remains Explicit (user-initiated)**
   - Cashier clicks "Receive Payment" in the UI
   - Cashier enters Amount Received and Payment Method
   - These values cannot be determined by a Domain Event alone

2. **No automatic Payment trigger from Order.Complete() or OrderConfirmed**

3. **Flow**:
```
Cashier clicks "Receive Payment"
  │
  ▼
ReceivePaymentCommandHandler
  ├── Load Order
  ├── Load DiningTable (if dine-in)
  ├── Payment.Create(order, amountReceived, method)
  │     └── Validates: Order not cancelled, not already completed, amount sufficient
  ├── _paymentRepository.AddAsync()
  └── Return PaymentResponse
```

---

## Alternatives Considered

| Alternative | Reason Not Chosen |
|-------------|-------------------|
| Event-driven after Order.Complete | Payment requires user input (amount, method). Cannot be automated. |
| Auto-prompt | UI can prompt cashier to collect payment, but the action itself remains explicit. |

---

## Trade-offs

| Trade-off | Detail |
|-----------|--------|
| More UI steps | Cashier must explicitly click to receive payment. Acceptable — this is the real restaurant workflow. |
| Clear separation | Payment has its own command, its own handler, its own aggregate. |

---

## Consequences

1. Payment module remains independent. No coupling to Order.
2. Payment.Create() receives Order as a parameter to validate cross-aggregate rules (order status, total amount)
3. Future: `PaymentReceivedEvent` can trigger Table Release and Receipt Printing

## Repository Evidence

- `Payment.cs` — `Payment.Create(order, amountReceived, method)` — cross-aggregate validation in Domain
- `ReceivePaymentCommandHandler.cs` — Loads Order + DiningTable, creates Payment, persists
- `PaymentReceivedEvent.cs` — Contains `PaymentId`, `OrderId`, `Amount`

## Human Approval

Approved