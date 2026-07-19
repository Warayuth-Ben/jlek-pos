# Rendering Platform

## Purpose

Define how the UI Platform renders content and reacts to data changes. The Rendering Platform sits between the workspace architecture and the framework layer, defining the strategy for how components display business state.

---

## Rendering Strategy

The platform follows a **state-driven rendering** strategy. Every component renders based on its current state, which is derived from API data, user interaction, or system events.

```text
Data Source → State → Component → Render
```

| Layer | Responsibility |
|-------|---------------|
| Data Source | API, cache, user input |
| State | Current situation of the component: Loading, Ready, Empty, Error |
| Component | Visual representation |
| Render | DOM/HTML output (framework) |

---

## Workspace Refresh

| Method | Description | Latency | Use Case |
|--------|-------------|---------|----------|
| **Initial Load** | Full data fetch on workspace entry | 1-2s | First time entering workspace |
| **Manual Refresh** | User clicks refresh button | Immediate | On demand |
| **Auto Polling** | Periodic fetch at interval | 15s | Kitchen queue, dashboard |
| **Optimistic Update** | Assume success, update UI immediately | <10ms | Add item, update quantity |
| **Server Push** (future) | Real-time via SignalR | Real-time | Cross-workspace updates |

### Initial Load Flow

```text
Navigate to workspace → Show skeleton → Fetch all required data in parallel
  │
  ├── All successful → Render full workspace
  └── Any failed → Show error state with retry
```

---

## Component Refresh Strategies

| Method | Description | Use Case |
|--------|-------------|----------|
| **Full re-render** | Entire component tree re-renders | Workspace reload |
| **State update** | Component re-renders based on state change | Item added, state changed |
| **Optimistic update** | UI updates before API confirms | Add item, update quantity |
| **Inline update** | Single field updates without full re-render | Quantity change |

---

## Polling

| Workspace | Interval | Data |
|-----------|----------|------|
| Kitchen | 15s | Active tickets (Pending, Preparing, Ready) |
| Dashboard | 30s | Metrics, recent orders |
| Status Bar | 30s | Aggregate counts |

### Polling Rules

| Rule | Description |
|------|-------------|
| Starts when workspace loads | First poll immediately, then at interval |
| Stops when workspace unloads | No unnecessary network requests |
| Overlap prevented | If previous poll hasn't returned, skip next |
| Errors logged silently | Data remains from previous poll |
| Interval configurable | Settings workspace allows change |

---

## Optimistic Update

| Operation | Optimistic? | Fallback |
|-----------|-------------|----------|
| Add item to order | Yes | Revert on failure, show error |
| Update quantity | Yes | Revert on failure, show error |
| Remove item | Yes | Revert on failure, show error |
| Confirm order | No | Show error, retry |
| Process payment | No | Show error, retry |

### Optimistic Flow

```text
User action → Immediately update UI → API call
  │
  ├── Success: no visual change needed
  └── Failure: revert UI change, show error toast
```

---

## Loading Placeholders

| Component | Type | Count |
|-----------|------|-------|
| Table Grid | Skeleton cards | 8 cards (pulse) |
| Order Panel | Skeleton rows | 4 rows + total |
| Menu Modal | Skeleton products | 6 placeholders |
| Kitchen Queue | Skeleton columns | 3 columns × 2 cards |
| Dashboard Metrics | Skeleton cards | 4 placeholders |
| Reports | Skeleton rows | 10 rows |

---

## Empty States

| Context | Message | Action |
|---------|---------|--------|
| No tables | "No tables created. Create tables in Settings." | [Go to Settings] |
| No products | "No menu items available." | [Go to Settings] |
| No orders | "No orders yet." | — |
| No kitchen tickets | "No active tickets. Waiting for new orders..." | — |
| No dashboard data | "No data available." | — |
| No reports data | "No data for the selected period." | — |
| Search no results | "No results matching '{query}'" | — |

---

## Error States

| Context | Message | Action |
|---------|---------|--------|
| Load failure | "Unable to load {resource}." | [Retry] |
| Action failure | "Unable to {action}. Please try again." | [Retry] |
| Network error | "Connection lost. Your work is saved locally." | Auto-reconnect |
| Server error | "Something went wrong." | [Retry] |

### Error Rendering

```text
Component State = Error → Show error illustration + message + retry button
                           Log detailed error to console
```

---

## Rendering Lifecycle

```text
Component Mounted → State = Loading → Show skeleton
  │
  ├── Fetch data
  │     │
  │     ├── Success → Data? → Yes → Ready → Render
  │     │                       No  → Empty → Show empty state
  │     │
  │     └── Failure → Error → Show error state
  │
  └── User action
        │
        ├── Optimistic → Show result immediately, confirm with API
        └── Non-optimistic → Show spinner, wait for API, then update