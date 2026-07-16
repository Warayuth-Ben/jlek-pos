# Table Module Architecture

Version: 1.0

Status: Draft (awaiting freeze)

---

## Purpose

Defines the architecture of the Table Module.

This document is derived from:

- Business Rules: `docs/01-business-rules/table/`
- System Use Cases: `docs/03-system-use-cases/table/`
- Application Flows: `docs/05-application/table/`
- Ubiquitous Language: `docs/02-domain-model/ubiquitous-language.md`
- Verified Architecture Patterns: Order Module and Menu Module (frozen)

---

## Business Responsibilities

The Table Module is responsible for:

- **Table Status Management** — Tracking whether a table is Available or Occupied
- **Table Assignment** — Assigning a table to a dine-in order session
- **Table Transfer** — Moving an active dining session from one table to another
- **Table Merge** — Combining multiple tables into a single dining session
- **Table Split** — Separating a merged session into independent tables
- **Table Release** — Returning a table to Available after the session ends

These rules apply only to dine-in operations.

---

## Domain Model

### Aggregate Root

| Concept | Classification | Justification |
|---------|---------------|---------------|
| DiningTable | Aggregate Root | Owns its status lifecycle. Consistency boundary: table cannot be Available and Occupied simultaneously. |

### Value Objects

| Concept | Classification | Source |
|---------|---------------|--------|
| TableId | Value Object | Already exists at `JLek.POS.Domain.ValueObjects.TableId` |
| TableStatus | Value Object (enum) | Reuse ProductStatus/IngredientStatus pattern |
| TableAssignment | Value Object | Tracks which OrderSessionId is assigned (immutable reference) |

### Enums

| Enum | Values | Reuse Pattern |
|------|--------|---------------|
| TableStatus | Available, Occupied | Reuse ProductStatus/IngredientStatus pattern |

### Domain Events

| Event | Trigger |
|-------|---------|
| TableAssigned | Table assigned to an order session |
| TableTransferred | Session moved to another table |
| TablesMerged | Multiple tables combined into one session |
| TablesSplit | Merged session separated |
| TableReleased | Table returned to Available |

### Business Rules

All business rules are sourced from `docs/01-business-rules/table/` and are frozen.

---

## Aggregate Boundaries

**DiningTable** is the sole Aggregate Root.

| Owned By DiningTable | Type | Rationale |
|----------------------|------|-----------|
| TableId | Value Object | Identity |
| TableStatus | Value Object (enum) | Status lifecycle |
| ActiveSessionId | Value Object (optional, nullable) | References OrderSession by ID only — not a navigation property |

### Cross-Aggregate References

- DiningTable references OrderSession **by ID only** (OrderSessionId)
- DiningTable does **not** own OrderSession
- OrderSession is an existing Domain concept owned by the Ordering Module
- The implementation and lifecycle of OrderSession remain the responsibility of the Ordering Module and are outside the scope of Table Module v1

### Boundary Justification

1. A table must enforce its status invariants independently (cannot be both Available and Occupied)
2. Table operations (transfer, merge, split) involve multiple DiningTables but must be consistent within the Table context
3. The business can manage tables independently from orders (e.g., maintenance, reservation)
4. Tables have a lifecycle independent of orders
5. Clear separation of concerns between Table (physical seating) and Order (financial transaction)

---

## Repository Contract

Reuses the frozen repository pattern exactly:

```
IDiningTableRepository
{
    Task<DiningTable?> GetByIdAsync(TableId id, CancellationToken ct);
    Task<IReadOnlyList<DiningTable>> GetAllAsync(CancellationToken ct);
    Task AddAsync(DiningTable table, CancellationToken ct);
    Task UpdateAsync(DiningTable table, CancellationToken ct);
}
```

No additional methods. The frozen pattern is:
- `GetByIdAsync` — load single aggregate
- `GetAllAsync` — load all aggregates
- `AddAsync` — persist new aggregate (one SaveChangesAsync)
- `UpdateAsync` — persist modified aggregate (one SaveChangesAsync)

### Available Tables Query

The "list available tables" use case is handled by the Application Query Handler:

```
// Application Query Handler
GetAllAsync()
↓
LINQ Where(t => t.Status == TableStatus.Available)
↓
Map to Response DTOs
```

This reuses the exact pattern from `GetOrdersQueryHandler` and `GetProductsQueryHandler`. No repository contract change is required.

---

## CQRS Design

### Commands

| Command | Description | Application Validates | Domain Validates |
|---------|-------------|---------------------|------------------|
| AssignTable | Assign available table to session | Table exists, table is Available | Status transition |
| TransferTable | Move session to another table | Both tables exist, destination Available | Transfer rules |
| MergeTables | Combine tables into one session | All tables exist | Merge rules |
| SplitTables | Separate merged session | Tables exist and are merged | Split rules |
| ReleaseTable | Return table to Available | Table exists and is Occupied | Session completed |

### Queries

| Query | Description |
|-------|-------------|
| GetTableById | Load single table |
| GetTables | Load all tables |

Available tables are filtered via GetAllAsync + LINQ in the query handler.

---

## Application Flow

All commands follow the same pattern:

```
validate (aggregate existence, cross-aggregate references)
↓
load aggregate(s) via repository
↓
call domain method
↓
repository.UpdateAsync()
↓
return Response DTO
```

### Transfer Operation and Transaction Consistency

Transfer involves two aggregate updates (source and destination). The current architecture accepts this trade-off because:

- There is no verified business requirement requiring atomic multi-aggregate transactions
- The project intentionally preserves the existing repository and transaction pattern (one SaveChangesAsync per repository method)
- This is consistent with the Menu Module, where composition operations (e.g., AddOptionGroup) also perform single-aggregate updates
- Future transaction support should only be introduced after a verified business requirement

---

## API Contract

| Method | Route | Use Case |
|--------|-------|----------|
| POST | `/tables` | Create |
| GET | `/tables` | Get All |
| GET | `/tables/{id}` | Get By Id |
| POST | `/tables/{id}/assign` | Assign to session |
| POST | `/tables/{id}/transfer` | Transfer to another table |
| POST | `/tables/merge` | Merge tables |
| POST | `/tables/{id}/split` | Split |
| POST | `/tables/{id}/release` | Release |

Route conventions reuse Order/Menu API patterns: `MapGroup("/tables")`, `Results.Ok`, `Results.Created`, `Results.NotFound`.

---

## Persistence Strategy

| Concept | Table | Pattern |
|---------|-------|---------|
| DiningTable | `DiningTables` | One table per Aggregate Root (reuse Product pattern) |
| TableStatus | Stored as `int` | HasConversion (reuse ProductStatus/IngredientStatus) |
| ActiveSessionId | Column `ActiveSessionId` (nullable Guid) | Cross-aggregate reference by ID only |
| TransferHistory | `TableTransferHistory` | Owned collection (reuse SuggestedPrices pattern) |

No owned entities. Transfer history is recorded as an owned value collection for audit trail.

---

## Risks and Trade-offs

### Accepted Trade-offs

| Issue | Position |
|-------|----------|
| Transfer non-atomicity | Accepted. No verified business requirement for atomic multi-aggregate transactions. Consistent with Menu Module pattern. |
| OrderSession not yet fully implemented | Accepted. Table depends only on OrderSessionId as a cross-aggregate identity reference. The lifecycle of OrderSession remains with the Ordering Module. |

### Non-Concerns

| Concern | Resolution |
|---------|------------|
| GetAvailableTablesAsync | Removed from contract. Use GetAllAsync + LINQ (reuse Product pattern). |
| OrderSession introduction | Not required by Table Module. Reference by ID only. |

---

## Architecture Freeze Checklist

- [x] Architecture frozen for v1
- [x] Domain model designed
- [x] Aggregate boundaries justified
- [x] Repository contract follows frozen pattern (no drift)
- [x] CQRS designed
- [x] Application flow documented
- [x] API contract defined
- [x] Persistence strategy defined
- [x] Risks and trade-offs documented
- [ ] Human approval received