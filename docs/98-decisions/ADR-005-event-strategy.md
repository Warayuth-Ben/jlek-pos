# ADR-005: Event Strategy

## Status

Accepted

---

## Context

Sprint 2.1 Event Infrastructure Audit confirmed:

- 16 Domain Events exist and are raised via `RaiseDomainEvent()`
- No event dispatcher exists
- No MediatR or equivalent
- No SaveChanges interceptor/override
- `builder.Ignore(x => x.DomainEvents)` in all EF configurations
- `ClearDomainEvents()` exists but never called
- Events are raised, stored in memory, then lost on next request

Sprint 2.2 Event Architecture Decisions confirmed:

- After Save dispatch
- Outside Transaction (v1)
- No MediatR (v1)
- Custom in-process dispatcher preferred

---

## Decision

1. **Dispatch Timing**: After Save — events are dispatched only after `SaveChangesAsync()` succeeds
2. **Transaction Boundary**: Outside Transaction — each event handler uses its own repository transaction
3. **Dispatcher**: Custom in-process dispatcher (not MediatR for v1)
4. **Handler Interface**: `IDomainEventHandler<T>` where `T : IDomainEvent`
5. **Dispatch Mechanism**: SaveChanges interceptor in `ApplicationDbContext` reads `DomainEvents` from tracked aggregates and calls the dispatcher

---

## Alternatives Considered

| Alternative | Reason Not Chosen |
|-------------|-------------------|
| Before Save | Handler failure would rollback primary operation |
| Inside Transaction | Would require Unit of Work — architecture change |
| MediatR | Adds dependency. Custom dispatcher is simpler for current needs (only ~150 lines). MediatR can be adopted later if pipeline behaviors needed. |
| Outbox | Not needed for v1 — single-process application, no external message broker |

---

## Trade-offs

| Trade-off | Detail |
|-----------|--------|
| Non-atomic dispatch | If event handler fails after save, compensating action needed (acceptable for v1 — restaurant POS, not banking) |
| Simple implementation | Custom dispatcher = ~50 lines. SaveChanges interceptor = ~30 lines. Total ~150 lines, 0 dependencies |
| Future upgrade path | When >5 event types or pipeline behaviors needed, swap custom dispatcher for MediatR |

---

## Consequences

1. All 16 Domain Events can now trigger cross-bounded-context actions
2. Order→Kitchen integration becomes possible via `OrderConfirmedEventHandler`
3. New integrations (Notification, Analytics, Loyalty) require only a new EventHandler — no existing code change
4. `ClearDomainEvents()` must be called after dispatch to prevent memory growth

## Repository Evidence

- `AggregateRoot.cs` — `_domainEvents` list, `RaiseDomainEvent()`, `ClearDomainEvents()`
- `IDomainEvent.cs` — Interface with `OccurredOnUtc`
- `DomainEvent.cs` — Base class setting `OccurredOnUtc`
- All EF configurations — `builder.Ignore(x => x.DomainEvents)`
- Sprint 2.1 Audit — confirmed no dispatcher exists

## Human Approval

Approved