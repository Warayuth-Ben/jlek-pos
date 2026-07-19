# Platform Foundation

## Why a UI Platform Exists

Every restaurant POS project eventually discovers that screens share more in common than they differ. Cashier, Kitchen, Dashboard, Reports, and Settings all need:
- A consistent skeleton with header, content area, and status
- Context-aware behavior that persists across navigation
- Loading, empty, error, and success states for every operation
- Keyboard-first interaction with touch fallback
- Event-driven feedback without tight coupling

A UI Platform codifies these common requirements into reusable architecture patterns. Each workspace becomes an implementation of the platform rather than a standalone screen.

---

## Platform Vision

The platform is not a POS. The platform is a **Restaurant Operating System**.

A POS is a collection of screens. A Restaurant Operating System is a unified workspace environment where:
- Business state flows naturally from the Domain to every workspace
- Context persists across workspaces so staff never lose their place
- Every workspace shares the same interaction language
- New capabilities can be added by extending the platform, not by building new screens from scratch

---

## Platform Goals

| Goal | Description |
|------|-------------|
| **Workspace Uniformity** | Every workspace follows the same skeleton and interaction rules |
| **Context Preservation** | User context persists across workspace boundaries |
| **Business Alignment** | Every UI structure is derived from Business Rules and State Machines |
| **Framework Independence** | The architecture does not depend on any rendering framework |
| **Developer Efficiency** | New workspaces can be created by implementing platform contracts |
| **Testing Consistency** | Platform-level testing patterns apply to all workspaces |
| **Evolution Without Rewrite** | The platform can be extended without changing existing workspaces |

---

## Relationship to Business Rules

Business Rules define what states exist and what transitions are valid. The platform does not duplicate Business Rules. The platform respects Business Rules by:

1. **Rendering states exactly as defined** — Every State Machine state has exactly one visual representation
2. **Enabling transitions exactly as defined** — Every Transition Matrix entry maps to one UI affordance
3. **Not inventing states** — If a state is not in the State Machine, it does not appear in the UI
4. **Not enabling illegal transitions** — If a transition is not in the Transition Matrix, the UI does not allow it

The platform is **guided by** Business Rules but **does not own** Business Rules.

---

## Relationship to State Machines

The platform provides infrastructure for rendering state machines consistently:

```text
State Machine     → State Machine Renderer (platform)
     │
     ├── States   → StatusBadge, StateList, StateCard
     │
     └── Transitions → ActionButton, QuickAction, CommandPalette
```

Every aggregate state machine (Order, Dining Table, Kitchen Ticket, Bill, Order Session) is rendered through the same platform components. The business meaning of each state is defined by the State Machine document, not by the platform.

---

## Relationship to Workspace

A Workspace is a platform instance configured for a specific business persona.

```text
Platform Skeleton
     │
     ├── Cashier    → Workspace (implements platform contracts)
     ├── Kitchen    → Workspace (implements platform contracts)
     ├── Dashboard  → Workspace (implements platform contracts)
     ├── Reports    → Workspace (implements platform contracts)
     └── Settings   → Workspace (implements platform contracts)
```

Every workspace uses:
- Same Header structure (but different title)
- Same Context Bar pattern (but different context type)
- Same Toolbar pattern (but different actions)
- Same Content Area pattern (but different primary component)
- Same Status Bar pattern (but different metrics)

The platform defines **how** workspaces work. Each workspace defines **what** it does.

---

## Relationship to Components

Components are organized into tiers within the platform:

| Tier | Examples | Scope |
|------|----------|-------|
| **Primitive** | Button, Input, Card, Badge, Icon, Spinner | Platform-wide |
| **Shared** | StatusBadge, SearchBar, Modal, Toast, LoadingSkeleton, EmptyState, ErrorState | Platform-wide |
| **Business** | TableCard, OrderStatusBadge, KitchenOrderCard, MetricCard | Across workspaces |
| **Workspace** | TableGrid, OrderPanel, KitchenQueue, PaymentDialog | Single workspace |

The platform defines the component architecture. Workspaces define workspace-specific components. Business components are shared across workspaces when they represent the same business concept.

---

## Platform Layers

```text
+-------------------------------------------------------+
|                 Business Layer                          |
|  Business Rules → Scenarios → State Machines           |
+-------------------------------------------------------+
                        ↓ derived from
+-------------------------------------------------------+
|              Presentation Architecture                  |
|  00-ui-foundation.md through 05-cashier.md             |
+-------------------------------------------------------+
                        ↓ implemented by
+-------------------------------------------------------+
|                  UI Platform                            |
|  Platform Skeleton → Context → Events → Feedback      |
|  → Interaction → Rendering                             |
+-------------------------------------------------------+
                        ↓ instantiated by
+-------------------------------------------------------+
|                   Workspaces                            |
|  Cashier | Kitchen | Dashboard | Reports | Settings    |
+-------------------------------------------------------+
                        ↓ rendered by
+-------------------------------------------------------+
|               Framework Layer (Blazor)                  |
|  Components → Pages → Layouts → Routing               |
+-------------------------------------------------------+
```

---

## Platform Design Principles

| Principle | Description |
|-----------|-------------|
| **Business First** | All platform structures derive from business concerns |
| **Workspace Before Page** | The workspace is the unit of composition, not the page |
| **Workflow Before Layout** | How the workspace works defines how it looks |
| **Context Before Navigation** | Context persists across workspace boundaries |
| **Architecture Before Components** | The platform skeleton precedes component selection |
| **Consistency Over Novelty** | Workspaces follow patterns, not invent new ones |
| **Framework Agnostic** | The platform does not depend on rendering technology |

---

## Change Boundaries

| Can Change | Cannot Change |
|------------|---------------|
| Platform skeleton (new sections) | Business Rules |
| Context behavior | State Machine definitions |
| Feedback patterns | Aggregate Boundaries |
| Interaction patterns (new gestures) | CQRS |
| Rendering strategy | API Contracts |
| Component organization | Domain Events |

The platform extends Presentation Architecture only. It never redesigns Business, Domain, Application, or Infrastructure layers.