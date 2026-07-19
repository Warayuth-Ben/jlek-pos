# Tables Contract Migration — Validation Report

## Files Changed

| File | Changes |
|------|---------|
| `src/JLek.POS.Application/Features/Tables/Responses/DiningTableResponse.cs` | `TableId` → `Guid`, `TableStatus` → `string`, `OrderSessionId?` → `Guid?`, `IReadOnlyCollection<TableId>` → `List<Guid>`. Updated `FromDomain()` with `.Value`, `.ToString()`, `.Select(id => id.Value).ToList()` |

## Build Result

| Project | Status |
|---------|--------|
| `JLek.POS.Application` | ✅ PASS (0 Errors, 0 Warnings) |
| `JLek.POS.Api` | ✅ PASS (0 Errors, 0 Warnings) |
| Full solution | ⏳ Integration tests pending fix |

## ADR-010 Compliance

| Requirement | Applied? | Detail |
|-------------|----------|--------|
| `TableId` → `Guid` | ✅ | `table.Id.Value` |
| `TableStatus` → `string` | ✅ | `table.Status.ToString()` |
| `OrderSessionId?` → `Guid?` | ✅ | `table.ActiveSessionId?.Value` |
| `IReadOnlyCollection<TableId>` → `List<Guid>` | ✅ | `.Select(id => id.Value).ToList()` |
| No Domain types in DTO | ✅ | All primitives |
| `FromDomain()` updated | ✅ | All 5 properties mapped correctly |

## Transformations Applied

```csharp
// Record declaration
TableId Id           → Guid Id
TableStatus Status   → string Status
OrderSessionId?      → Guid? ActiveSessionId
IReadOnlyCollection  → List<Guid> MergedTableIds

// FromDomain() mapping
table.Id.Value
table.Status.ToString()
table.ActiveSessionId?.Value
table.MergedTableIds.Select(id => id.Value).ToList()
```

## Risks

- Integration test `DiningTableTests.cs` will need enum→string assertion updates (same pattern as Catalog)
- No cross-module impact — Tables DTO is self-contained

## Recommendation

**Proceed to Orders module** — `OrderItemResponse` needs `Quantity` → `int` and `Money` → `decimal` changes.