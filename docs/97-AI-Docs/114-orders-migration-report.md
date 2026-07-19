# Orders Contract Migration — Validation Report

## Files Changed

| File | Changes |
|------|---------|
| `src/JLek.POS.Application/Features/Orders/Responses/OrderItemResponse.cs` | `Quantity` → `int`, `Money` → `decimal` (both UnitPrice and TotalPrice). Updated `FromDomain()` with `.Value`, `.Amount`. |
| `src/JLek.POS.Application/Features/Orders/Responses/OrderResponseV2.cs` | `OrderId` → `Guid`, `OrderStatus` → `string`, `Money` → `decimal`. Updated `FromDomain()` with `.Value`, `.ToString()`, `.Amount`. |

## Build Result

| Project | Status |
|---------|--------|
| `JLek.POS.Application` | ✅ PASS (0 Errors, 0 Warnings) |
| `JLek.POS.Api` | ✅ PASS (0 Errors, 0 Warnings) |

## ADR-010 Compliance

| Requirement | Applied? | Detail |
|-------------|----------|--------|
| `OrderId` → `Guid` | ✅ | `order.Id.Value` |
| `OrderStatus` → `string` | ✅ | `order.Status.ToString()` |
| `Money` → `decimal` | ✅ | `order.Total.Amount`, `item.UnitPrice.Amount`, `item.TotalPrice.Amount` |
| `Quantity` → `int` | ✅ | `item.Quantity.Value` |
| No Domain types in DTO | ✅ | All primitives |
| `FromDomain()` updated | ✅ | All 7 properties mapped correctly |

## Transformations Applied

```csharp
// OrderResponseV2
OrderId Id              → Guid Id
OrderStatus Status      → string Status
Money Total             → decimal Total
IEnumerable<OrderItemResponse> Items  → unchanged (already conformant)

// OrderItemResponse
Quantity Quantity          → int Quantity
Money UnitPrice           → decimal UnitPrice
Money TotalPrice          → decimal TotalPrice
Guid MenuItemId           → unchanged (already conformant)
```

## Risks

- `IEnumerable<OrderItemResponse>` was already using a primitive-based DTO — no change needed
- No cross-module impact

## Recommendation

Proceed to **Kitchen** module — `KitchenTicketResponse` and `KitchenItemResponse` need `KitchenTicketId` → `Guid` and `KitchenTicketStatus` → `string` changes.