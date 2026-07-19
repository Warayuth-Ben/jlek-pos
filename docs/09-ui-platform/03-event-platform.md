# Event Platform

## Purpose

Define how events flow through the UI Platform. Events are the mechanism by which the UI reacts to business state changes, user actions, and system conditions.

The Event Platform does NOT redesign Domain Events. Domain Events are defined in the Domain Layer and remain unchanged. The Event Platform only explains how events are handled at the UI layer.

---

## Event Types

| Type | Source | Destination | Example |
|------|--------|-------------|---------|
| **Business Event** | Domain / Application Layer | UI Platform | OrderConfirmed, PaymentReceived |
| **UI Event** | User interaction | Current workspace | Button click, keyboard shortcut |
| **Interaction Event** | User gesture | UI Component | Tap, swipe, drag |
| **Rendering Event** | Component lifecycle | Parent component | OnLoad, OnDataReceived |
| **Notification Event** | System condition | User-facing feedback | "Connection lost", "Payment failed" |

---

## Business Events

Business Events originate from the Domain or Application layer. They represent meaningful state changes in business aggregates.

| Event | Source | UI Reaction |
|-------|--------|-------------|
| OrderCreated | Application | Cashier: order panel shows Draft state |
| OrderConfirmed | Application | Cashier: order becomes view-only. Kitchen: new ticket appears |
| OrderCompleted | Application | Cashier: "Order complete" badge. Dashboard: metrics update |
| OrderCancelled | Application | Cashier: order panel closes. Kitchen: ticket removed |
| KitchenTicketCreated | Application | Kitchen: new Pending column ticket |
| PreparationStarted | Application | Kitchen: ticket moves to Preparing column |
| PreparationCompleted | Application | Kitchen: ticket moves to Ready column |
| ItemsServed | Application | Kitchen: ticket removed from queue |
| PaymentReceived | Application | Cashier: "Payment received" toast. Table released |
| TableAssigned | Application | Cashier: table becomes Occupied |
| TableReleased | Application | Cashier: table becomes Available |

### Business Event Flow

```text
Domain Aggregate
       │
       ▼
Domain Event (e.g., OrderConfirmedEvent)
       │
       ▼
Event Handler (Application layer)
       │
       ▼
API Response (HTTP 200)
       │
       ▼
UI Client (receives response or polls)
       │
       ▼
Event Dispatcher (UI platform)
       │
       ├── Toast notification: "Order #1024 sent to kitchen"
       ├── Workspace update: Order Panel switches to view-only
       ├── Kitchen update: New ticket appears in Queue
       └── Dashboard update: Metrics recalculated
```

### Delivery Methods

| Method | Latency | Use Case |
|--------|---------|----------|
| API Response (synchronous) | Immediate | Action → reaction in same workspace |
| Polling (15s) | 15s | Kitchen queue refresh, dashboard metrics |
| SignalR (future) | Real-time | Cross-workspace updates, live kitchen |

---

## UI Events

UI Events originate from user interaction with the interface.

| Event | Source | Handler |
|-------|--------|---------|
| Click | Button, Card | Command execution |
| KeyDown | Keyboard | Shortcut handler |
| Focus | Input field | Context update |
| Blur | Input field | Validation |
| Scroll | Content area | Infinite scroll, position save |
| Resize | Window | Responsive layout adjustment |

### UI Event Flow

```text
User clicks [Confirm Order]
       │
       ▼
UI Event: Click (source: ConfirmButton)
       │
       ▼
Workspace Handler: ConfirmOrderAction
       │
       ├── Disable button (prevent double-click)
       ├── Show spinner
       ├── Execute command: POST /orders/{id}/confirm
       │
       ▼
API Response (success / failure)
       │
       ├── Success → Update order state, toast, navigate
       └── Failure → Show error, enable button
```

---

## Interaction Events

Interaction Events represent physical user gestures.

| Event | Device | Example |
|-------|--------|---------|
| Tap | Touch, Mouse | Select table, click button |
| Double Tap | Touch, Mouse | Quick-select product |
| Long Press | Touch | Context menu |
| Swipe | Touch | Delete item, navigate back |
| Pinch | Touch | Zoom content (reports) |

### Interaction → UI Event Mapping

```text
Gesture           → UI Event
────────────────────────────────
Tap               → Click
Double Tap        → QuickAdd
Long Press        → ContextMenu
Swipe Left        → DeleteItem
Swipe Right       → MarkComplete
Pinch             → Zoom
```

---

## Rendering Events

Rendering Events are lifecycle events within the component tree.

| Event | Trigger | Purpose |
|-------|---------|---------|
| OnLoad | Component mounted | Initial data fetch |
| OnDataReceived | API response | Render data |
| OnError | API failure | Show error state |
| OnEmpty | API returns empty | Show empty state |
| OnUnmount | Component removed | Cleanup (timers, subscriptions) |

### Rendering Lifecycle

```text
Component Mounted
       │
       ▼
OnLoad → Start loading state (skeleton)
       │
       ▼
Fetch data (GET /api/resource)
       │
       ├── Success → OnDataReceived → Ready state
       │                ├── has data → Render content
       │                └── no data  → Empty state
       │
       └── Failure → OnError → Error state with retry
       │
       ▼
OnUnmount → Cleanup
```

---

## Notification Events

Notification Events represent system conditions that require user awareness.

| Event | Severity | Display |
|-------|----------|---------|
| Connection Lost | Error | Status bar: offline indicator |
| Connection Restored | Success | Toast: "Back online" |
| Operation Failed | Error | Toast with retry |
| Operation Succeeded | Success | Toast |
| New Ticket | Info | Kitchen: ticket appears |
| Payment Received | Success | Toast |
| Warning (low stock) | Warning | Toast or badge |

---

## Event Flow Summary

```text
Business Layer                    UI Platform
══════════════════                ════════════════
Domain Event                      │
       │                          │
       ▼                          │
Application Handler               │
       │                          │
       ├── API Response ──────────┤──► UI Event Dispatcher
       │                          │       │
       │                          │       ├── Workspace update
       │                          │       ├── Component state
       │                          │       ├── Toast notification
       │                          │       └── Dashboard refresh
       │                          │
       └── (future: SignalR) ─────┤──► Real-time Event Bus
                                  │
User Interaction                  │
       │                          │
       ├── Click ─────────────────┤──► UI Event Handler
       ├── Keyboard ──────────────┤       │
       ├── Touch ─────────────────┤       ├── Command execution
       └── Voice (future) ────────┤       ├── State transition
                                        └── Feedback
```

---

## Event Rules

| Rule | Description |
|------|-------------|
| **One event, one handler** | Each Business Event has exactly one UI handler per workspace |
| **Events never modify Domain** | UI events are read-only for Domain state |
| **Events are not persisted** | UI events are transient, Business Events are persisted |
| **Polling is a fallback** | Polling exists because SignalR is deferred |
| **Toast is for success/failure** | Toast is the primary non-blocking feedback channel |
| **Modals block interaction** | Modal opening pauses underlying workspace interaction |