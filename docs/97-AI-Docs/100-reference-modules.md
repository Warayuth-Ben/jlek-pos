# Reference Modules

Version: 1.0

Project: JLek POS

---

## Purpose

This document summarizes reusable implementation patterns from completed modules.

Future modules should reference this document before starting implementation.

---

## Order Module

Status: Frozen

### Aggregate

| Concept | Type | Details |
|---------|------|---------|
| Order | Aggregate Root | Order lifecycle, order items, business rules, domain events |
| OrderItem | Entity | Belongs to Order, controlled by Order aggregate |

### Repository Pattern

| Method | Return | Description |
|--------|--------|-------------|
| GetByIdAsync(OrderId) | Order? | Single aggregate load |
| GetAllAsync() | IReadOnlyList<Order> | All aggregates |
| AddAsync(Order) | void | Persist + one SaveChangesAsync |
| UpdateAsync(Order) | void | Update + one SaveChangesAsync |

### CQRS Pattern

| Category | Count | Examples |
|----------|-------|----------|
| Commands | 6 | CreateOrder, AddItem, RemoveItem, ConfirmOrder, CompleteOrder, CancelOrder |
| Queries | 2 | GetOrderById, GetOrders |
| Handlers | 8 | One per command/query |

### API Pattern

| Method | Route | Result |
|--------|-------|--------|
| POST | /orders | Created |
| GET | /orders/{id} | Ok / NotFound |
| PUT/DELETE sub-resources | /orders/{id}/items | Ok |

### Response DTO Pattern

```csharp
public record OrderResponseV2(...)
{
    public static OrderResponseV2 FromDomain(Order order) => ...;
}
```

---

## Menu Module

Status: Frozen

### Aggregates

| Concept | Type | Details |
|---------|------|---------|
| Product | Aggregate Root | Owned entities: OptionGroup, Option, Modifier. Owned collections: SuggestedPrices, IngredientIds |
| ProductCategory | Aggregate Root | Independent aggregate |
| Ingredient | Aggregate Root | Independent aggregate, referenced by Product via IngredientId only |

### Repository Pattern

Same 4-method contract as Order Module for each aggregate.

### CQRS Pattern

| Aggregate | Commands | Queries |
|-----------|----------|---------|
| Product | 13 | 2 |
| ProductCategory | 5 | 2 |
| Ingredient | 3 | 2 |

### API Pattern

| Group | Method | Route |
|-------|--------|-------|
| Products | POST/GET/PUT/DELETE | /products/{id}/... |
| Categories | POST/GET/PUT | /categories/{id}/... |
| Ingredients | POST/GET/PUT | /ingredients/{id}/... |

### Key Patterns

- **Aggregate Composition**: Product owns OptionGroups (entities), SuggestedPrices (value objects), IngredientIds (references by ID)
- **Owned Entities**: OptionGroup uses OwnsMany with owned entity pattern
- **Value Collections**: SuggestedPrices and IngredientIds use OwnsMany with value object pattern
- **EF Core Configuration**: IEntityTypeConfiguration<T> per aggregate
- **Strongly Typed ID Converters**: ValueConverter<TId, Guid> for each strongly typed ID

---

## Table Module

Status: Frozen

### Aggregate

| Concept | Type | Details |
|---------|------|---------|
| DiningTable | Aggregate Root | Sole aggregate. Owns its status lifecycle. References OrderSession by ID only. |

### Repository Pattern

Same 4-method contract as Order Module.

### CQRS Pattern

| Category | Count | Commands/Queries |
|----------|-------|------------------|
| Commands | 6 | Create, Assign, Transfer, Merge, Split, Release |
| Queries | 3 | GetById, GetAll, GetAvailable (LINQ filter) |

### API Pattern

| Method | Route | Description |
|--------|-------|-------------|
| POST | /tables | Create |
| GET | /tables | Get All |
| GET | /tables/available | Get Available only (LINQ filter in handler) |
| GET | /tables/{id} | Get By Id |
| POST | /tables/{id}/assign | Assign to session |
| POST | /tables/{id}/transfer | Transfer session |
| POST | /tables/{id}/merge | Merge tables |
| POST | /tables/{id}/split | Split merged tables |
| POST | /tables/{id}/release | Release table |

### Key Patterns

- **Multi-Aggregate Operations**: Transfer and Merge update two DiningTables in one handler. Domain method receives the related aggregate as parameter. Handler persists each aggregate separately (two SaveChangesAsync calls).
- **Cross-Bounded-Context References**: DiningTable references OrderSessionId by ID only. No navigation property. No FK constraint.
- **Query Filtering via LINQ**: GetAvailable uses GetAllAsync() + Where() in handler. No additional repository method.
- **Business Rules**: Rules are in Domain (CheckRule). Handler has existence validation only.

### Testing Pattern

```csharp
[Collection("Catalog")]
public sealed class DiningTableTests : IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    
    // IAsyncLifetime: InitializeAsync() ensures DB created
    // Helper methods: SeedTableAsync(), SeedOccupiedTableAsync()
    
    // Each test: Arrange → Act → Assert
    // Verifies: HTTP status, Response DTO, Database persistence, Aggregate state
}
```

### CI/CD Pattern

```yaml
# .github/workflows/dotnet-ci.yml
# Trigger: push/PR to main/develop
# Steps: Checkout → Setup .NET 8.0 → Restore → Build (Release) → Test (Release)
# Docker available for Testcontainers (no manual PostgreSQL)
# Test artifacts uploaded on failure (TRX format)
```

---

## Implementation Order for Future Modules

1. Business Rules (docs/01-business-rules/)
2. Architecture Design (docs/96-architecture/)
3. Domain (aggregate, rules, events)
4. Repository Contract (4-method interface)
5. Infrastructure (EF config, converter, repository, DI)
6. CQRS (commands, queries, handlers, response DTO)
7. API (Minimal API endpoints)
8. Integration Tests (Testcontainers + WebApplicationFactory)
9. CI (GitHub Actions workflow)
10. Freeze (documentation update)

---

## Patterns Catalog

| Pattern | First Used In | Description |
|---------|---------------|-------------|
| AggregateRoot<TId> | Order Module | Base class for all aggregate roots |
| Static Create() factory | Order Module | Domain object creation pattern |
| Private EF Core constructor | Order Module | Required by EF Core for materialization |
| CheckRule(IBusinessRule) | Order Module | Business rule validation |
| RaiseDomainEvent() | Order Module | Domain event recording |
| 4-method Repository | Order Module | GetById, GetAll, Add, Update |
| One SaveChangesAsync per method | Order Module | Transaction boundary |
| IEntityTypeConfiguration<T> | Order Module | EF Core configuration |
| ValueConverter<TId, Guid> | Order Module | Strongly typed ID persistence |
| FromDomain() factory | Order Module | Response DTO mapping |
| MapGroup("/resource") | Order Module | API endpoint grouping |
| Results.Ok/Created/NotFound | Order Module | HTTP response pattern |
| OwnsMany owned entities | Menu Module | Entity collection persistence |
| OwnsMany value collections | Menu Module | Value object collection persistence |
| [Collection("X")] + IAsyncLifetime | Menu Module | Integration test fixture pattern |
| Testcontainers PostgreSQL | Menu Module | Database per test class |
| Multi-aggregate domain method | Table Module | Domain method receives related aggregate |
| GetAllAsync + LINQ filter | Table Module | Query filtering without new repo method |
| GitHub Actions CI | Table Module | CI pipeline with Docker + Testcontainers |