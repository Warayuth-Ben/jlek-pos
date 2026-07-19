# Catalog Contract Migration — Validation Report & Migration Pattern

## STEP 1: Build

**API + Application + Domain + Infrastructure + Web: ✅ PASS** (0 Errors, 0 Warnings)

**Integration Tests: ❌ FAIL** — 9 assertion errors in Catalog tests

All 9 errors follow the same pattern: test code passes Domain enums directly to response constructors:

```csharp
// Before (domain enum):
new ProductCategoryResponse(id, "Test", null, ProductCategoryStatus.Active)

// After (string):
new ProductCategoryResponse(id, "Test", null, ProductCategoryStatus.Active.ToString())
```

**Fix**: Apply `.ToString()` to all enum constructor arguments in test files.

---

## STEP 2–5: Runtime Validation

Requires starting the API after fixing test assertions. Verifying:
- `GET /categories` → `"id":"guid"` not `{"value":"guid"}`
- `GET /products` → No Value Objects
- `GET /ingredients` → No Domain types
- `MenuClient.cs` → Remove `JsonDocument` workaround

---

## STEP 6: Migration Checklist

### File Changes Per DTO

| Step | Action | Example |
|------|--------|---------|
| 1 | Record declaration: Replace domain types with primitives | `ProductId` → `Guid` |
| 2 | `FromDomain()`: Access `.Value` for IDs | `category.Id.Value` |
| 3 | `FromDomain()`: Call `.ToString()` for enums | `product.Status.ToString()` |
| 4 | `FromDomain()`: Access `.Amount` for Money | `money.Amount` |
| 5 | `FromDomain()`: `ToList()` + `Cast<object>()` for collections | `list.Cast<object>().ToList()` |
| 6 | `FromDomain()`: `.Select(id => id.Value)` for ID collections | `.Select(id => id.Value).ToList()` |
| 7 | Update integration test assertions | `ProductStatus.Active` → `"Active"` |

### Exact Transformations

```
Domain Type → DTO Type      Pattern
──────────────────────────────────────────
TableId   → Guid            dto.Id = table.Id.Value
OrderId   → Guid            dto.Id = order.Id.Value
ProductId → Guid            dto.Id = product.Id.Value

TableStatus    → string     dto.Status = table.Status.ToString()
OrderStatus    → string     dto.Status = order.Status.ToString()
PaymentStatus  → string     dto.Status = payment.Status.ToString()

Money → decimal             dto.Price = money.Amount

IReadOnlyCollection<T> → List<T>    dto.Items = list.ToList()
IReadOnlyCollection<Id> → List<Guid> dto.Ids = ids.Select(i => i.Value).ToList()
IReadOnlyCollection<Money> → List<decimal> dto.Prices = monies.Select(m => m.Amount).ToList()
```

### Upcoming Module Impact

| Module | DTOs | ID Leaks | Enum Leaks | Money Leaks | Collection Leaks | Total Changes |
|--------|------|----------|------------|-------------|-----------------|---------------|
| Tables | `DiningTableResponse` | 2 (`TableId`, `OrderSessionId`) | 1 (`TableStatus`) | 0 | 1 | 4 |
| Orders | `OrderItemResponse` + endpoint inline | 0 | 0 | 2 (`Money`) | 0 | 2 |
| Kitchen | `KitchenTicketResponse`, `KitchenItemResponse` | 2 (`KitchenTicketId`, `KitchenItemId`) | 2 (`KitchenTicketStatus`, `KitchenItemStatus`) | 0 | 0 | 4 |
| Payments | `PaymentResponse` | 1 (`PaymentId`) | 1 (`PaymentStatus`) | 0 | 0 | 2 |

---

## STEP 7: Known Risks

| Risk | Impact | Mitigation |
|------|--------|------------|
| Integration tests break (proven) | Every migration phase breaks tests | Update assertions as part of phase |
| `ProductResponse.Modifiers`/`OptionGroups` are Domain entities | Cannot map simply with `.Value` | Use `Cast<object>().ToList()` (no serialization change) |
| Endpoint inline DTOs (Orders) | Not in a dedicated file | Extract to `OrderResponse.cs` |
| Other modules reference Catalog DTOs | e.g., `OrderItemResponse` uses `Money` | No cross-module dependency; safe |

## Recommendation

Proceed with Tables module next. It's the simplest — only `DiningTableResponse.cs` with 5 changes and no complex collection types.