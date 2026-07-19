# Component Contract

## Component Categories

| Category | Description | Examples |
|----------|-------------|----------|
| **Primitive** | Pure UI elements, no business knowledge | Button, Input, Card, Badge, Icon, Spinner |
| **Business** | Consumes Presentation Models, renders business state | TableCard, OrderPanel, KitchenTicketCard |
| **Composite** | Combines multiple components into a view | TableGrid, KitchenQueue, PaymentForm |

## Responsibilities

| Category | Responsibilities |
|----------|----------------|
| Primitive | Render. Handle basic interactions (click, input). Never know business state. |
| Business | Consume Presentation Models. Render state. Dispatch Commands. Show loading/empty/error. |
| Composite | Orchestrate Business Components. Manage layout. No business logic. |

## Inputs

| Component Type | Inputs |
|---------------|--------|
| Primitive | Label, value, disabled, onClick |
| Business | Presentation Model(s), callbacks |
| Composite | None (fetches from Store via Workspace) |

## Outputs

| Component Type | Outputs |
|---------------|---------|
| Primitive | Click, Change, Focus |
| Business | Command dispatch, Navigation request |
| Composite | Workspace-level events |

## Component States

| State | Description | Visual |
|-------|-------------|--------|
| **Loading** | Data is being fetched | Skeleton / Spinner |
| **Empty** | Data returned empty | Empty state with action |
| **Error** | Data fetch failed | Error state with retry |
| **Disabled** | Component cannot be interacted with | Grayed out |
| **Ready** | Data available and interactive | Normal render |

## Business Component Rules

| Rule | Description |
|------|-------------|
| Consume Presentation Models only | Never consume DTOs directly |
| Never call APIs | Dispatch Commands instead |
| Never own persistent state | Store owns state |
| Always handle 5 states | Loading, Empty, Error, Disabled, Ready |
| Render status from Model | Status badge color/text comes from Model |