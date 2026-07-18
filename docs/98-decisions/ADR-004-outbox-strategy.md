# ADR-004: Outbox Strategy

## Status

Accepted

---

## Context

Outbox pattern ensures reliable event delivery by persisting events in the same database transaction as the aggregate, then dispatching them asynchronously. This prevents event loss if the dispatcher crashes after saving the aggregate but before dispatching events.

Sprint 2.2 considered whether Outbox is needed for v1.

---

## Decision

1. **No Outbox for v1**
   - The project has no external message broker (RabbitMQ, Kafka, Service Bus)
   - Event dispatch is in-process only
   - Event loss on crash is acceptable for v1
   - The project explicitly accepts non-atomic multi-aggregate operations

2. **Outbox becomes needed when**:
   - An external message broker is introduced
   - Events must be reliably delivered to another service
   - Guaranteed delivery becomes a verified business requirement

---

## Alternatives Considered

| Alternative | Reason Not Chosen |
|-------------|-------------------|
| Implement Outbox now | Over-engineering for current needs. Single-process, no external subscribers. |
| In-memory dispatch only | Chosen. Simple, zero dependencies, sufficient for v1. |

---

## Trade-offs

| Trade-off | Detail |
|-----------|--------|
| Event loss possible | If dispatcher crashes after SaveChanges but before dispatch, events are lost. Acceptable for v1. |
| Simplicity | No new tables, no background workers, no serialization. |

---

## Consequences

1. Event dispatch is fire-and-forget for v1
2. If a handler fails, the event is not retried automatically
3. If guaranteed delivery becomes required, Outbox can be added at that time

## Repository Evidence

- Sprint 2.1 Audit — no message broker, no external subscribers

## Human Approval

Approved