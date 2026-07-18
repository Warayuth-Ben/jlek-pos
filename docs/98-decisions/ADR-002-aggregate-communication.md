# ADR-002: Aggregate Communication

## Status

Accepted

---

## Context

When a domain event occurs in one aggregate, other aggregates may need to react. For example, when an Order is Confirmed, a Kitchen Ticket should be created. The project must define how aggregates communicate without violating Aggregate boundaries.

---

## Decision

1. **Method**: Event-driven (deferred until Event Infrastructure exists)
2. **No Direct Handler Calls**: Command Handlers must NOT call other Command Handlers
3. **No Aggregate References**: Aggregates communicate by ID only — never by navigation property
4. **Flow**:

```
Aggregate A (e.g., Order)
  │
  ├── Domain method raises DomainEvent
  │
  ▼
[Event Infrastructure: Dispatcher → EventHandler]
  │
  ▼
EventHandler
  ├── Injects Repository for Aggregate B (NOT the Command Handler)
  ├── Creates/modifies Aggregate B directly
  ├── Repository persists Aggregate B
  └── Done
```

---

## Alternatives Considered

| Alternative | Reason Not Chosen |
|-------------|-------------------|
| Direct Handler Call | Blurs CQRS. Creates coupling. Every new integration modifies existing handler. |
| Shared Database | Violates Aggregate boundary. Cannot enforce Business Rules independently. |
| Cross-aggregate navigation properties | Violates DDD. |

---

## Trade-offs

| Trade-off | Detail |
|-----------|--------|
| Deferred | Integration is blocked until Event Infrastructure exists. Acceptable — Event Infrastructure is ~150 lines. |
| Eventual consistency | If EventHandler fails, target aggregate is not updated. Acceptable v1 trade-off. |

---

## Consequences

1. Command Handlers remain pure — they handle one use case and know nothing about side effects
2. New integrations = new EventHandler, no existing code change
3. Each aggregate can be tested independently

## Repository Evidence

- All aggregate roots — cross-aggregate references by ID only (e.g., `DiningTable.ActiveSessionId` = `OrderSessionId`)
- No `INavigation` or `ICollection<OtherAggregate>` properties exist

## Human Approval

Approved