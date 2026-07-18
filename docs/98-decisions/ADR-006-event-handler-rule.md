# ADR-003.1: Event Handler Rule

## Status

Accepted

---

## Context

Event Handlers will be introduced as part of the Event Strategy (ADR-005). Without clear rules, Event Handlers risk becoming fat — accumulating multiple responsibilities and eventually duplicating Command Handler logic.

Sprint 2.2 identified the risk: "อย่าให้ Event Handler กลายเป็น Mini Command Handler"

---

## Decision

1. **Event Handlers coordinate Repositories, NOT Command Handlers**
   - Event Handlers must NOT call Command Handlers
   - Event Handlers must NOT call other Event Handlers

2. **Event Handlers must remain thin**
   - One responsibility per handler
   - Inject only the repository needed for that handler's single purpose
   - Do not chain multiple operations in one handler

3. **Correct Pattern**

```
OrderConfirmedEventHandler
  ├── Inject IKitchenTicketRepository
  ├── Create KitchenTicket aggregate directly
  ├── _kitchenRepository.AddAsync()
  └── Done
```

4. **Incorrect Pattern**

```
OrderConfirmedEventHandler
  ├── Call CreateKitchenTicketCommandHandler    ← ❌ Handler calls Handler
  ├── Call SendEmailService                    ← ❌ Too many responsibilities
  ├── Call AnalyticsService                    ← ❌ Event Handler becomes fat
  └── ...
```

---

## Alternatives Considered

| Alternative | Reason Not Chosen |
|-------------|-------------------|
| Allow Handler→Handler calls | Blurs CQRS boundary. Couples handlers. Difficult to test. |
| Single Handler for all side effects | Violates Single Responsibility. Harder to maintain. |

---

## Trade-offs

| Trade-off | Detail |
|-----------|--------|
| More handler classes | Each integration = one new handler class. More files, but each is simple and testable. |
| Clear separation | Command Handlers = use cases. Event Handlers = reactions to domain events. No overlap. |

---

## Consequences

1. Adding new integration (Notification, Analytics, Loyalty) = add new EventHandler. No existing code change.
2. Each EventHandler can be tested independently with its repository mock.
3. No risk of Event Handlers becoming fat Command Handlers.

## Repository Evidence

- Sprint 2.1 Audit — Event Infrastructure

## Human Approval

Approved