# ADR-001: Transaction Strategy

## Status

Accepted

---

## Context

The project has 7 repositories, each with its own `SaveChangesAsync()` call. Multi-aggregate operations (e.g., Create Order + Assign Table) involve two repositories and two SaveChanges calls. There is no shared transaction boundary or Unit of Work.

Sprint 2.1 confirmed: `ClearDomainEvents()` exists but never called. Each repository commits independently.

---

## Decision

1. **No explicit Unit of Work for v1**
   - Each repository's `SaveChangesAsync()` is its own transaction boundary
   - Multi-aggregate operations are non-atomic
   - This is an accepted trade-off, not a bug

2. **Consistent with existing pattern**
   - The Table Module documentation explicitly states: "Transfer non-atomicity is accepted. No verified business requirement for atomic multi-aggregate transactions."
   - This principle applies to all multi-aggregate operations, not just Transfer

3. **Compensating actions**
   - If a second SaveChanges fails, manual reconciliation is acceptable for v1
   - Future: can add Unit of Work or Outbox if a verified business requirement emerges

---

## Alternatives Considered

| Alternative | Reason Not Chosen |
|-------------|-------------------|
| Unit of Work | Architecture change. Not required by current business requirements. Would add complexity. Violates Production Rule: "No new architectural patterns without verified business requirement." |
| Shared DbContext transaction | Would require modifying all repositories to share a transaction scope. Current design intentionally keeps repositories independent. |

---

## Trade-offs

| Trade-off | Detail |
|-----------|--------|
| Inconsistent state possible | If Save 1 succeeds and Save 2 fails, data is partially committed. Acceptable for v1. |
| Simplicity | No new patterns. No new dependencies. No changes to existing repositories. |

---

## Consequences

1. All existing handlers continue to work without changes
2. Multi-aggregate operations (Create Order + Assign Table, Transfer/Merge/Split) remain non-atomic
3. If a business requirement for atomic transactions emerges, a Unit of Work can be added at that time
4. `ClearDomainEvents()` should be called after dispatch (once Event Infrastructure exists) to prevent memory growth

## Repository Evidence

- `DiningTableRepository.cs` — `SaveChangesAsync()` per method
- `OrderRepository.cs` — `SaveChangesAsync()` per method
- All 7 repositories follow the same pattern
- `docs/96-architecture/table-module.md` lines 179-186 — non-atomicity accepted

## Business References

- `docs/96-architecture/table-module.md`

## Human Approval

Approved