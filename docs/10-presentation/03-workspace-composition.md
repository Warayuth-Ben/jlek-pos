# Workspace Composition

## Layer Diagram

```text
Workspace
   │
   ▼
Store ─── owns Presentation Models, manages state
   │
   ▼
Business Components ─── consume Models, dispatch Commands
   │
   ▼
Primitive Components ─── Button, Input, Card, Badge
```

## Responsibility of Each Layer

| Layer | Responsibilities |
|-------|-----------------|
| **Workspace** | Orchestrates workflow. Creates Store. Renders skeleton (Header, Toolbar, Content, Status Bar). Handles navigation events. |
| **Store** | Fetches data. Maps DTOs to Presentation Models. Holds state. Manages selection. Subscribes to events. |
| **Business Components** | Consume Presentation Models. Render business state. Dispatch Commands on user action. Never call APIs. |
| **Primitive Components** | Pure UI elements. No business knowledge. Reusable across all workspaces. |

## Composition Rules

| Rule | Description |
|------|-------------|
| Workspace owns Store | Workspace creates and manages Store lifecycle |
| Store owns Models | Store creates and updates all Presentation Models |
| Components consume Models | Components receive Models as input parameters |
| Components dispatch Commands | Components call Commands, never APIs directly |
| One-way data flow | Data flows down from Store → Component |
| Events flow up | User actions flow up as Events/Commands |

## Dependency Rules

| Can Reference | Cannot Reference |
|-------------|-----------------|
| Workspace → Store | Component → API |
| Store → DTOs | Component → Store (directly) |
| Component → Model | Model → Store |
| Component → Command | Store → Component |