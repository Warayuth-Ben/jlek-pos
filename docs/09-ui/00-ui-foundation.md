# UI Architecture Foundation

## Vision

To provide a restaurant POS interface that reflects business state in real time, enables staff to complete tasks with minimal cognitive load, and never introduces business state not owned by the Domain.

## Mission

Define a Presentation Architecture derived exclusively from Business Rules, System Use Cases, and Aggregate State Machines — independent of any rendering technology, framework, or component library.

## Design Philosophy

### Presentation Reflects, Not Invents

The UI never creates, derives, or infers business state. Every displayed status, label, and transition is a direct projection of Domain state received through Application contracts.

### Every State Has a Visual Representation

Every state defined in the State Machines (Order, Kitchen Ticket, Dining Table, Bill, Order Session) must have exactly one corresponding visual representation. No state is hidden. No state is inferred.

### Every Transition Has a Trigger

Every state transition defined in the Transition Matrix must have a corresponding UI affordance — a button, gesture, or system action. Transitions not in the matrix must not be enabled.

### Role Before Screen

Workspaces are defined by actor roles, not by data entities. Each workspace serves one primary persona. Cross-workspace navigation is minimized.

## Core Principles

| Principle | Description |
|-----------|-------------|
| **Business-Driven** | All UI structures originate from Business Rules, Use Cases, and State Machines |
| **State-First** | UI components render state. They do not own state. |
| **One Workspace Per Role** | Cashier, Kitchen, Manager each have their own workspace |
| **Explicit Transitions** | Every allowed state transition is visually explicit |
| **No Dead Ends** | Every screen provides a path forward or back |
| **Consistency Over Novelty** | Reuse patterns before creating new ones |
| **Error Visibility** | All errors are visible and recoverable |
| **Performance by Default** | Fast interactions for high-frequency tasks |

## UI Architecture Layers

```text
+--------------------------------------------------+
|                 Business Layer                     |
|  Business Rules → Use Cases → State Machines      |
+--------------------------------------------------+
                        ↓ derives
+--------------------------------------------------+
|             Presentation Architecture              |
|  Workspace → Page → Component → State Display     |
+--------------------------------------------------+
                        ↓ contracts
+--------------------------------------------------+
|              Application Contracts                 |
|  Commands ↓   Queries ↓   Events ↑                |
+--------------------------------------------------+
                        ↓ implements
+--------------------------------------------------+
|              Technical Implementation              |
|  Blazor / React / Vue (decoupled)                 |
+--------------------------------------------------+
```

The Presentation Architecture is independent of the technical implementation layer. Changing the rendering framework must not change the architecture.

## Workspace Definition

A Workspace is a collection of pages, components, and actions serving one business persona.

| Workspace | Primary Persona | Business Context |
|-----------|----------------|-----------------|
| **Cashier** | Restaurant Staff | Order Management, Table Management, Payment |
| **Kitchen** | Kitchen Staff | Kitchen Ticket lifecycle |
| **Dashboard** | Manager | Real-time operational overview |
| **Reports** | Manager | Historical analysis |
| **Settings** | Administrator | System configuration |
| **Home** | All | Entry point, quick navigation |

### Workspace References

- **Cashier** — Use Cases: Create Order, Confirm Order, Add Item, Cancel Order, Process Payment. State Machines: Order, Dining Table, Bill.
- **Kitchen** — Use Cases: Start Preparation, Complete Preparation, Serve Items. State Machines: Kitchen Ticket.
- **Dashboard** — State Machine summary: aggregate counts by state.
- **Reports** — Query-only. No state mutations.
- **Settings** — System configuration. No business state.

## Component Definition

A Component is a reusable UI element that displays state derived from one or more Application contracts.

### Component Types

| Type | Description | Example |
|------|-------------|---------|
| **State Badge** | Renders a single business state | OrderStatusBadge (Draft, Confirmed, Completed, Cancelled) |
| **Entity Card** | Renders an aggregate summary | TableCard (name, status, session info) |
| **State List** | Renders a collection filtered by state | KitchenQueue (Pending, Preparing, Ready columns) |
| **Action Button** | Triggers a single state transition | ConfirmOrderButton, StartPreparationButton |
| **Form** | Collects validated input for a Command | CreateOrderForm, PaymentForm |
| **Dashboard Metric** | Renders a single computed value | OpenTablesCount, TodaySales |

### Component Rules

- Components never call Application commands directly. Commands are invoked by workspace-level orchestrators.
- Components receive state as parameters. They do not fetch state themselves.
- Components enable/disable transitions based on the current aggregate state.
- A component may be reused across workspaces if it represents the same business concept.

## Decision Rules

| Rule | Application |
|------|-------------|
| If a state has no corresponding State Machine, do not render it | Prevents invented states |
| If a transition is not in the Transition Matrix, do not enable it | Prevents illegal actions |
| If a Use Case requires data, define a Query contract | Prevents direct data access |
| If two roles need the same component, share the component definition | Encourages reuse |
| If a component becomes role-specific, move it to the workspace | Keeps shared surface minimal |
| If an error originates from the Domain, display the Domain error message | Preserves business semantics |
| If an error originates from Infrastructure, translate it to a user message | Hides technical details |

## Naming Convention

| Artifact | Pattern | Example |
|----------|---------|---------|
| Workspace | `{Role}Workspace` | CashierWorkspace |
| Page | `{Entity}{View}` | TableGridView, OrderDetailPage |
| Component | `{Entity}{Type}` | TableCard, OrderStatusBadge |
| State Badge | `{Entity}StatusBadge` | OrderStatusBadge |
| Action | `{Transition}Button` | ConfirmOrderButton |
| Form | `{Command}Form` | PaymentForm |
| Metric | `{Metric}Card` | OpenTablesCard |

## Do & Don't

### Do

- Derive page structure from Use Cases
- Derive component states from State Machines
- Enable transitions only when the Transition Matrix allows
- Group components by business role, not by data type
- Use consistent terminology across all workspaces
- Separate read (Query) from write (Command) at the UI layer
- Display Domain error messages directly to users

### Don't

- Don't create UI state that doesn't exist in the Domain
- Don't enable transitions that aren't in the Transition Matrix
- Don't allow navigation to workspaces the current role cannot access
- Don't expose raw IDs, timestamps, or technical fields to users
- Don't implement business validation in the UI layer
- Don't assume data availability — always handle empty/loading/error states
- Don't create new navigation patterns — reuse existing ones