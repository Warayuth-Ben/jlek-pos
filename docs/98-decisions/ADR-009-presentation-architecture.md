# ADR-009: Presentation Architecture

## Status

Accepted

---

## Context

The project has a complete backend (7 bounded contexts, 47 API endpoints, 212 tests) and a scaffolded Blazor WebAssembly project (`src/JLek.POS.Web`).

The existing `JLek.POS.Web.csproj` is configured as:
| Property | Value |
|----------|-------|
| Sdk | `Microsoft.NET.Sdk.BlazorWebAssembly` |
| Target | `net8.0` |
| References | `Application`, `Infrastructure`, `Shared` projects directly |

---

## Decision 1: Blazor Hosting Model

**Decision**: **Blazor WebAssembly** (frozen — already scaffolded)

| Option | Pros | Cons |
|--------|------|------|
| **Blazor WebAssembly** ✅ | Runs entirely in browser, can work offline, lower server cost | Larger initial download (~5MB) |
| Blazor Server | Real-time UI, smaller initial download | Requires constant SignalR connection, higher server load |

**Rationale**: The project is already Blazor WebAssembly. Changing would require rewriting `Program.cs` and the project file.

---

## Decision 2: API Communication

**Decision**: **HTTP Client (NOT direct .NET reference)**

| Option | Pros | Cons |
|--------|------|------|
| Direct .NET reference | No serialization, no network | ❌ **Not possible with Blazor WASM** — WASM runs in browser, cannot inject server-side handlers |
| **HTTP Client** ✅ | Works with Blazor WASM natively, decoupled UI from backend | Network latency, serialization cost |

**Why HTTP Client is mandatory**:
- Blazor WebAssembly runs in the browser on the client machine
- It cannot directly reference or instantiate server-side handlers (EF Core, DbContext, repositories)
- The existing HTTP API (47 endpoints) is already designed for this purpose
- The API project (`JLek.POS.Api`) and the Web project (`JLek.POS.Web`) are separate process boundaries

---

## Decision 3: Application Facade (Client-Side)

**Decision**: **Typed API Client services** in the Web project

To prevent the UI from directly knowing HTTP details (URLs, serialization, error handling), we introduce typed client services. This is a **thin layer** that encapsulates API calls.

```
Blazor Page
    │
    ▼
TypedClientService (e.g., IOrderClient, ITableClient)
    │  - Inject HttpClient
    │  - Convert request/response DTOs
    │  - Handle HTTP errors
    ▼
HTTP API (JLek.POS.Api endpoints)
    │
    ▼
Command / Query Handlers
```

**Why not inject handlers directly?** (per feedback)
1. Presentation should not be coupled to Handler contracts
2. If the internal implementation changes (e.g., from direct handler to event-driven), the UI should not be affected
3. The facade provides a stable interface that matches the UI workflow, not the CQRS boundaries

**Example**:
```csharp
// IOrderClient — typed facade
public interface IOrderClient
{
    Task<OrderResponse> CreateOrderAsync(TableId tableId);
    Task<OrderResponse> AddItemAsync(OrderId orderId, Guid menuItemId, Quantity quantity, Money unitPrice);
    Task<OrderResponse> ConfirmOrderAsync(OrderId orderId);
    Task<PaymentResponse> ReceivePaymentAsync(OrderId orderId, decimal amount, PaymentMethod method);
}
```

---

## Decision 4: Authentication Strategy

**Decision**: **ASP.NET Core Identity + JWT**

| Aspect | Choice |
|--------|--------|
| Provider | ASP.NET Core Identity |
| Token | JWT (JSON Web Token) |
| Storage | Same PostgreSQL database |
| UI | Login page + token management in WASM |
| Role | Cashier, Kitchen, Manager |

**Recommendation**: Implement as Sprint 5.1 before any user-facing features.

---

## Decision 5: Navigation Structure

```
Layout
├── Header (shop name, user info, logout)
├── Sidebar (role-based navigation)
│   ├── Dashboard         (Manager only)
│   ├── Cashier           (Cashier role)
│   │   ├── Order Screen
│   │   └── Payment Screen
│   ├── Kitchen           (Kitchen role)
│   │   └── Ticket Display
│   ├── Tables            (Both)
│   │   └── Table Map
│   └── Reports           (Manager only)
│       ├── Daily Sales
│       ├── By Payment
│       └── Best Sellers
└── Main Content Area
```

---

## Decision 6: Layout Strategy

**Decision**: **Single `MainLayout` with role-based sidebar navigation**

- `MainLayout.razor` — already exists
- `NavMenu.razor` — already exists, to be extended with role-based visibility
- CSS: Bootstrap 5 (standard Blazor template) → POS-specific theme

---

## Decision 7: Component Architecture

```
Pages (top-level routes)
├── Cashier/
│   ├── CashierPage.razor          ← Main cashier screen
│   ├── OrderPanel.razor           ← Order creation & management
│   ├── MenuGrid.razor             ← Menu item selection
│   └── PaymentPanel.razor         ← Payment processing
│
├── Kitchen/
│   ├── KitchenDisplay.razor       ← Kitchen ticket display
│   └── TicketDetail.razor         ← Individual ticket view
│
├── Tables/
│   ├── TableMap.razor             ← Visual table layout
│   └── TableDetail.razor          ← Single table management
│
├── Reports/
│   ├── DailySalesReport.razor
│   ├── SalesByPaymentReport.razor
│   └── BestSellerReport.razor
│
└── Auth/
    └── Login.razor                ← Authentication page

Shared Components
├── Layout/
│   ├── MainLayout.razor
│   └── NavMenu.razor
│
├── UI/
│   ├── LoadingSpinner.razor       ← Busy indicator
│   ├── ErrorAlert.razor           ← Error display
│   └── ConfirmDialog.razor        ← Confirmation modal
```

---

## Decision 8: State Management

**Decision**: **Per-page service injection (no global state store)**

| Why not Flux/Redux | Why this is sufficient |
|--------------------|----------------------|
| Blazor WASM has DI built-in | Each page injects its own `IOrderClient` |
| State management overhead | POS screens are focused and short-lived |

**Pattern**:
```
Page Component
  │
  ├── injects typed client (e.g., IOrderClient)
  ├── calls client method on button click
  └── updates local UI state
```

---

## Decision 9: Validation Strategy

**Decision**: **Client-side validation + server-side validation**

| Layer | Validation |
|-------|-----------|
| **UI (Blazor)** | `DataAnnotations` on input models, `EditForm` + `DataAnnotationsValidator` |
| **API** | `ExceptionHandlingMiddleware` (existing) returns structured errors |
| **Domain** | `CheckRule()` — `BusinessRuleValidationException` → HTTP 400 |

---

## Decision 10: Error Handling

| Component | Purpose |
|-----------|---------|
| `ErrorBoundary` (Blazor) | Catch unhandled render exceptions |
| `ExceptionHandlingMiddleware` (existing) | API-level error handling |
| `ErrorAlert.razor` component | Display validation/error messages |

---

## Decision 11: Loading / Busy Indicators

**Decision**: **`LoadingSpinner.razor`** + `bool IsBusy` pattern in each component.

---

## Architecture Diagram

```
┌─────────────────────────────────────────────────────────┐
│               JLek.POS.Web (Blazor WASM)                 │
│                                                          │
│  ┌──────────────┐  ┌────────────────────────────────┐   │
│  │ Auth Pages    │  │ Cashier / Kitchen / etc Pages │   │
│  └──────┬───────┘  └──────────┬─────────────────────┘   │
│         │                     │                          │
│         │    ┌────────────────▼──────────────┐           │
│         │    │  Typed API Client Services     │           │
│         │    │  (IOrderClient, ITableClient,  │           │
│         │    │   IKitchenClient, IPaymentClient)          │
│         │    └────────────────┬──────────────┘           │
│         │                     │                          │
│         ▼                     ▼                          │
│  ┌─────────────────────────────────────────────────┐    │
│  │              HttpClient + JWT Token               │    │
│  └──────────────────────┬──────────────────────────┘    │
└─────────────────────────┼───────────────────────────────┘
                          │ HTTP
                          ▼
┌──────────────────────────────────────────────────────────┐
│                 JLek.POS.Api (ASP.NET Core)               │
│                                                          │
│  ┌──────────────────────────────────────────────────┐   │
│  │  JWT Authentication Middleware                    │   │
│  └──────────────────────┬───────────────────────────┘   │
│                         │                               │
│  ┌──────────────────────▼───────────────────────────┐   │
│  │  47 API Endpoints (via MapGroup)                  │   │
│  │  ├── /orders/* → OrderEndpoints                  │   │
│  │  ├── /tables/* → TableEndpoints                  │   │
│  │  ├── /kitchen/* → KitchenEndpoints               │   │
│  │  ├── /payments/* → PaymentEndpoints              │   │
│  │  ├── /products/* → CatalogEndpoints              │   │
│  │  ├── /reports/* → ReportingEndpoints             │   │
│  │  └── /receipts/* → ReceiptEndpoints              │   │
│  └──────────────────────┬───────────────────────────┘   │
│                         │                               │
│  ┌──────────────────────▼───────────────────────────┐   │
│  │  Command / Query Handlers → Domain Layer         │   │
│  └──────────────────────────────────────────────────┘   │
└──────────────────────────────────────────────────────────┘
```

---

## Alternatives Considered

| Alternative | Reason Not Chosen |
|-------------|-------------------|
| React/Vue frontend | Additional language/framework. Blazor already scaffolded. |
| Blazor Server | Scaffolded as WASM. WASM can work offline. |
| Direct handler injection | ❌ **Not possible with Blazor WASM** — browser cannot run server-side DI |
| gRPC-Web | Not needed. REST API already exists. |

---

## Consequences

1. UI development starts with creating typed client services
2. No changes needed to the backend API — it's already functional
3. Authentication (JWT + Identity) must be implemented before user-facing features
4. Each typed client encapsulates HTTP logic from the UI
5. The existing reference to `Application` and `Infrastructure` in the Web project should be removed — they are not usable from WASM and create confusion

## Repository Evidence

- `src/JLek.POS.Web/JLek.POS.Web.csproj` — Blazor WebAssembly project
- `src/JLek.POS.Web/Program.cs` — WASM entry point
- `src/JLek.POS.Api/Endpoints/` — 47 existing API endpoints
- `src/JLek.POS.Api/Middleware/ExceptionHandlingMiddleware.cs` — Error handling

## Business References

- `docs/97-AI-Docs/110-master-implementation-plan.md`
- `docs/97-AI-Docs/111-repository-architecture-audit.md` — Presentation: Not Started

## Human Approval

✅ Approved