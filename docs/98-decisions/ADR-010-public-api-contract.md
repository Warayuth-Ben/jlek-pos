# ADR-010: Public API Contract Standard

## Status

Accepted

## Context

The current Public API exposes Domain implementation details directly through Application Response DTOs. This causes three systemic problems:

### Problem 1: Serialization Inconsistency

Domain Value Objects serialize as `{"value":"guid"}` while primitive `Guid` types serialize as `"guid"`. Clients cannot predict which format they will receive.

| Endpoint | ID Format | Example |
|----------|-----------|---------|
| `GET /categories` | Value Object (nested) | `{"id":{"value":"e287..."}}` |
| `GET /tables` | Primitive (flat) | `{"id":"e287..."}` |
| `GET /orders` | Primitive or Value Object | Inconsistent |

### Problem 2: Client Coupling

Every frontend client (Web, Mobile, Third-party) must understand Domain Value Objects (`TableId`, `ProductCategoryId`, `Money`) to consume the API. This breaks the encapsulation boundary that the Domain layer should provide.

### Problem 3: Domain Leakage

Domain Value Objects, Enums, and Entities exit the Application layer through Response DTOs. This violates Clean Architecture, where the Application layer should translate Domain concepts into Application concepts before exposing them.

```text
Current (Violation):
  Domain: ProductCategoryId → Application: ProductCategoryResponse.Id (same type) → API → Client

Intended:
  Domain: ProductCategoryId → Application: ProductCategoryResponse.Id (Guid) → API → Client
```

### Evidence

The audit in [Architecture Audit](../97-AI-Docs/111-repository-architecture-audit.md) and the Response DTO audit in the session handoff identified **30 Domain type leaks across 13 Response DTOs** in 5 modules (Tables, Orders, Catalog, Kitchen, Payments).

---

## Decision

**Public API Contracts SHALL NOT expose Domain types.**

Response DTOs in the Application layer represent **Application Contracts**, not Domain Models. All mapping from Domain types to primitives must occur inside the Application layer.

---

## Standards

### 1. ID Mapping

| Domain Type | Public API Type | Example JSON |
|------------|----------------|--------------|
| `TableId` | `Guid` | `"id": "a1b2c3d4-..."` |
| `OrderId` | `Guid` | `"id": "a1b2c3d4-..."` |
| `ProductId` | `Guid` | `"id": "a1b2c3d4-..."` |
| `ProductCategoryId` | `Guid` | `"id": "a1b2c3d4-..."` |
| `IngredientId` | `Guid` | `"id": "a1b2c3d4-..."` |
| `KitchenTicketId` | `Guid` | `"id": "a1b2c3d4-..."` |
| `KitchenItemId` | `Guid` | `"id": "a1b2c3d4-..."` |
| `PaymentId` | `Guid` | `"id": "a1b2c3d4-..."` |
| `OrderSessionId` | `Guid?` | `"activeSessionId": null` |
| `PaymentId` | `Guid` | `"id": "a1b2c3d4-..."` |

**Rule:** Every `*Id` Value Object in a public DTO becomes `Guid` (or `Guid?` for nullable).

### 2. Money Mapping

| Domain Type | Public API Type | Example JSON |
|------------|----------------|--------------|
| `Money` | `decimal` | `"unitPrice": 12.50` |
| `Money` (nullable) | `decimal?` | `"total": null` |

**Rule:** `Money` becomes `decimal`. The Application layer converts during mapping. No precision or currency information is lost because the API operates in a single-currency system (THB).

### 3. Enum Mapping

| Domain Enum | Public API Type | Example JSON |
|------------|----------------|--------------|
| `TableStatus` | `string` | `"status": "Available"` |
| `OrderStatus` | `string` | `"status": "Draft"` |
| `KitchenTicketStatus` | `string` | `"status": "Pending"` |
| `ProductStatus` | `string` | `"status": "Active"` |
| `ProductVisibility` | `string` | `"visibility": "Visible"` |
| `ProductCategoryStatus` | `string` | `"status": "Active"` |
| `IngredientStatus` | `string` | `"status": "Active"` |
| `PaymentStatus` | `string` | `"status": "Completed"` |
| `PaymentMethod` | `string` | `"method": "Cash"` |

**Rule:** Enums become `string`. Rationale:
- Strings are self-documenting in API responses.
- Strings are forward-compatible (new enum values don't break clients).
- Strings are debuggable in network logs.
- Frontend clients can inspect `"Draft"` instead of mapping `"0"`.

### 4. Quantity Mapping

| Domain Type | Public API Type | Example JSON |
|------------|----------------|--------------|
| `Quantity` | `int` | `"quantity": 3` |

**Rule:** `Quantity` becomes `int`.

### 5. Collections

| Domain Type | Public API Type | Example JSON |
|------------|----------------|--------------|
| `IReadOnlyCollection<T>` | `List<T>` | `"items": [...]` |

**Rule:** Collection types use `List<T>` for consistency.

### 6. Date & Time

| Domain Type | Public API Type | Format |
|------------|----------------|--------|
| `DateTimeOffset` | `DateTimeOffset` | ISO-8601 |

**Rule:** All date/time values use `DateTimeOffset` in ISO-8601 format with timezone offset.

### 7. Nullable

| Rule | Description |
|------|-------------|
| Explicit nullable types | Use `Type?` for nullable fields |
| No implicit nulls | Every nullable field must be documented |
| No empty string for null | Use JSON `null`, not `""` |

### 8. Domain Objects, Entities, Value Objects

**These are NEVER exposed in the Public API.**

| Domain Concept | Rule |
|---------------|------|
| Domain Objects | Always mapped to Application DTOs |
| Entities | Always mapped to Application DTOs |
| Value Objects | Always mapped to primitives |
| Aggregates | Always mapped to Application DTOs |

---

## Mapping Boundary

```text
Domain Layer:
  ProductCategoryId, TableStatus, Money
       │
       ▼  ──── Mapping happens here (Application Layer) ────
       │
Application DTO (Response):
  Guid Id, string Status, decimal TotalPrice
       │
       ▼  ──── Serialization happens here (ASP.NET Core) ────
       │
Public API (JSON):
  { "id": "guid", "status": "Active", "totalPrice": 12.50 }
```

Mapping occurs only inside the Application layer:
- `FromDomain()` static factory methods on DTOs
- Query Handlers that translate Domain → Application
- Command Handlers that translate Domain → Application

---

## Affected DTOs

| Module | DTO | Leaks | Phase |
|--------|-----|-------|-------|
| Catalog | `ProductCategoryResponse` | 2 | A |
| Catalog | `ProductResponse` | 9 | A |
| Catalog | `IngredientResponse` | 2 | A |
| Tables | `DiningTableResponse` | 5 | B |
| Orders | `OrderResponse` (endpoint) | 3 | C |
| Orders | `OrderItemResponse` | 2 | C |
| Kitchen | `KitchenTicketResponse` | 3 | D |
| Kitchen | `KitchenItemResponse` | 2 | D |
| Payments | `PaymentResponse` | 2 | E |
| **Total** | **13 DTOs** | **30 leaks** | |

Unaffected (no changes needed):
- `HealthResponse`
- `DailySalesReport`
- `SalesByPaymentReport`
- `BestSellerReport`

---

## Migration Strategy

### Phase A: Catalog

Files:
- `src/JLek.POS.Application/Features/Catalog/Responses/ProductCategoryResponse.cs`
- `src/JLek.POS.Application/Features/Catalog/Responses/ProductResponse.cs`
- `src/JLek.POS.Application/Features/Catalog/Responses/IngredientResponse.cs`

Changes:
- Replace `ProductCategoryId` → `Guid`
- Replace `ProductId` → `Guid`
- Replace `IngredientId` → `Guid`
- Replace `ProductStatus` → `string`
- Replace `ProductVisibility` → `string`
- Replace `ProductCategoryStatus` → `string`
- Replace `IngredientStatus` → `string`
- Replace `Money` → `decimal`
- Replace `IReadOnlyCollection<T>` → `List<T>`

Verify:
- Build succeeds
- Integration tests pass
- Swagger shows correct types

### Phase B: Tables

Files:
- `src/JLek.POS.Application/Features/Tables/Responses/DiningTableResponse.cs`

Changes:
- Replace `TableId` → `Guid`
- Replace `TableStatus` → `string`
- Replace `OrderSessionId?` → `Guid?`
- Replace `IReadOnlyCollection<TableId>` → `List<Guid>`

### Phase C: Orders

Files:
- `src/JLek.POS.Application/Features/Orders/Responses/OrderItemResponse.cs`
- Endpoint inline `OrderResponse`

Changes:
- Replace `OrderId` → `Guid`
- Replace `TableId` → `Guid?`
- Replace `OrderStatus` → `string`
- Replace `Quantity` → `int`
- Replace `Money` → `decimal`

### Phase D: Kitchen

Files:
- `src/JLek.POS.Application/Features/Kitchen/Responses/KitchenTicketResponse.cs`
- `src/JLek.POS.Application/Features/Kitchen/Responses/KitchenItemResponse.cs`

Changes:
- Replace `KitchenTicketId` → `Guid`
- Replace `KitchenItemId` → `Guid`
- Replace `OrderId` → `Guid`
- Replace `KitchenTicketStatus` → `string`

### Phase E: Payments

Files:
- `src/JLek.POS.Application/Features/Payments/Responses/PaymentResponse.cs`

Changes:
- Replace `PaymentId` → `Guid`
- Replace `OrderId` → `Guid`

---

## Non-Goals

This ADR explicitly does NOT address:

| Concern | Reason |
|---------|--------|
| Domain redesign | Domain Value Objects are correct internally |
| DDD pattern changes | DDD patterns remain for Domain layer |
| CQRS redesign | CQRS boundaries remain unchanged |
| API route redesign | Routes paths and HTTP methods stay the same |
| Request DTO changes | Request DTOs are reviewed separately |

---

## Consequences

### Positive

- Consistent serialization: every `id` is a flat `"guid"` string
- Clients never see Domain Value Objects
- Application boundary is properly enforced
- Frontend can use typed deserialization (remove `JsonDocument` workaround)
- Swagger/OpenAPI documents show correct primitive types

### Negative

- Every `FromDomain()` method and Query Handler must be updated
- Integration tests that check serialized JSON will break
- Migration requires coordination across 5 modules

### Neutral

- No business logic changes
- No database changes
- No route changes
- Frontend contracts remain compatible (frontend already expects primitives)

---

## Verification

Each phase must pass:

1. `dotnet build` — 0 errors, 0 warnings
2. `dotnet test` — All integration tests pass
3. Manual verification via Swagger UI that types are correct (`Guid` not `ProductCategoryId`)
4. Frontend build — 0 errors

---

## References

- [Architecture Audit: Repository DTO Audit](../97-AI-Docs/111-repository-architecture-audit.md)
- [Session Handoff: Phase 12.1](../97-AI-Docs/91-session-handoff.md)
- ADR-001: Transaction Strategy
- ADR-009: Presentation Architecture
- Clean Architecture: Application Layer boundaries