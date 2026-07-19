# Presentation Foundation

## Why Presentation Architecture Exists

Between the Application layer (API endpoints, DTOs) and the UI Platform (workspaces, components) there is a gap. The Application layer speaks in DTOs — flat data contracts optimized for HTTP transport. The UI Platform speaks in visual state — loading, empty, error, and business states defined by State Machines.

Presentation Architecture bridges this gap without coupling UI to API.

## Architecture Diagram

```text
Application Layer (Backend) ──→ DTO ──→ Presentation Model ──→ Store
                                                                   │
                                                                   ▼
                                                              Workspace
                                                                   │
                                                                   ▼
                                                         Business Component
                                                                   │
                                                                   ▼
                                                        Primitive Component
                                                                   │
                                                                   ▼
                                                          Framework Layer
```

## Relationships

| Concept | Relationship |
|---------|-------------|
| Backend | DTOs are consumed by Presentation layer, never by Components |
| Frontend Platform | Stores implement the platform's context and feedback |
| Workspace | Each workspace owns one Store |
| Components | Business Components consume Presentation Models only |
| Business Rules | Presentation never owns Business Rules |

## Layer Boundaries

| Layer | Responsibilities |
|-------|-----------------|
| DTO | HTTP transport, API contract |
| Presentation Model | Immutable, UI-optimized, framework-independent |
| Store | Owns state, manages lifecycle, handles events |
| Workspace | Orchestrates workflow, delegates to Store |
| Component | Consumes Presentation Models, renders state |