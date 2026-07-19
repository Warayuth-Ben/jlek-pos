# Feedback Platform

## Purpose

Define how the UI Platform communicates system state to the user. Feedback is the UI's way of telling the user what happened, what is happening, and what needs attention.

---

## Feedback Types

| Type | Intensity | Duration | Interaction |
|------|-----------|----------|-------------|
| **Loading** | Low | Until complete | None (passive) |
| **Saving** | Medium | Until complete | None (passive) |
| **Printing** | Low | Until complete | None (passive) |
| **Success** | Medium | 2-3 seconds | Optional dismiss |
| **Warning** | Medium | Until dismissed | Required dismiss |
| **Error** | High | Until resolved | Required action |
| **Attention** | High | Until acknowledged | Required acknowledge |

---

## Feedback Mechanisms

| Mechanism | Type | Location | Persistence |
|-----------|------|----------|-------------|
| **Toast** | Success, Warning, Error, Info | Top right | Auto-dismiss or manual |
| **Dialog** | Confirmation, Error | Centered overlay | Until dismissed |
| **Drawer** | Detail, Form | Right edge | Until closed |
| **Inline** | Validation, Error | Next to component | Until corrected |
| **Status Bar** | Connection, Count | Bottom bar | Continuous |
| **Badge** | Count, Notification | Icon or nav item | Until cleared |
| **Spinner** | Loading, Saving | Inside button or area | Until complete |
| **Skeleton** | Loading | Content placeholder | Until loaded |
| **Banner** | Attention, Warning | Top of workspace | Until dismissed |

---

## When to Use Each Mechanism

### Loading

| Mechanism | When |
|-----------|------|
| **Skeleton** | Initial content load (tables, orders, reports) |
| **Spinner in button** | Action in progress (confirm, save, pay) |
| **Spinner in area** | Section refresh (metrics update) |

### Saving

| Mechanism | When |
|-----------|------|
| **Spinner in button** | Saving form data |
| **Disabled button** | Prevent double-submit during save |

### Printing

| Mechanism | When |
|-----------|------|
| **Toast** | "Receipt printing..." / "Receipt printed" |
| **Spinner** | While print job is queued |

### Success

| Mechanism | When | Example |
|-----------|------|---------|
| **Toast** | Operation completed successfully | "Order #1024 sent to kitchen!" |
| **Inline update** | State change is visible | Table card changes color |

### Warning

| Mechanism | When | Example |
|-----------|------|---------|
| **Toast** | Non-critical condition | "Low stock: Chicken Rice" |
| **Inline** | Field validation | "Amount is less than total" |

### Error

| Mechanism | When | Example |
|-----------|------|---------|
| **Toast + Retry** | Operation failed, can retry | "Payment failed. [Retry]" |
| **Error state** | Component failed to load | "Unable to load tables. [Retry]" |
| **Dialog** | Critical error requiring decision | "Connection lost. [Reconnect] [Offline mode]" |
| **Inline** | Field validation error | "Required field" |

### Attention

| Mechanism | When | Example |
|-----------|------|---------|
| **Banner** | System-wide condition | "Kitchen printer disconnected" |
| **Badge** | New items requiring action | Kitchen badge: 3 pending |

### Notification

| Mechanism | When | Example |
|-----------|------|---------|
| **Toast** | Informational event | "New order received" |
| **Activity Feed** | Event history | Recent events list |

---

## Feedback Decision Tree

```text
Is there an active operation?
       │
       ├── Yes → Show spinner / skeleton
       │          │
       │          └── Complete → Success? → Yes → Toast + state update
       │                                        → No  → Error state + toast + retry
       │
       └── No → Is there a system condition?
                  │
                  ├── Warning → Toast (warning style)
                  ├── Error → Toast (error style) + inline error
                  ├── Attention → Banner or badge
                  └── Info → Toast (info style)
```

---

## Feedback Location Map

```text
┌─────────────────────────────────────────────────────┐
│ Banner (system-wide attention)                      │
├─────────────────────────────────────────────────────┤
│                                                       │
│  Primary Content                                      │
│  ┌─────────────────────────────────────────────────┐ │
│  │ Skeleton (loading)                              │ │
│  │ Error state (failed)                            │ │
│  │ Empty state (no data)                           │ │
│  │ Normal state (data)                             │ │
│  │   ┌───────────────────────────────────────────┐ │ │
│  │   │ Inline error (field validation)           │ │ │
│  │   └───────────────────────────────────────────┘ │ │
│  └─────────────────────────────────────────────────┘ │
│                                                       │
│  Action Area                                          │
│  ┌─────────────────────────────────────────────────┐ │
│  │ Button: [Spinner] [✓ Confirm]                  │ │
│  └─────────────────────────────────────────────────┘ │
│                                                       │
├─────────────────────────────────────────────────────┤
│ Status Bar: Connection | Counts | Last Refresh      │
└─────────────────────────────────────────────────────┘

Toast (top right): Success | Warning | Error | Info
Dialog (center): Confirmation | Critical Error
Drawer (right): Detail view | Form
Badge (sidebar): Notification count
```

---

## Activity Feed

The Activity Feed shows a chronological list of recent events.

| Event Type | Display |
|------------|---------|
| Order confirmed | "Order #1024 sent to kitchen" — timestamp |
| Payment received | "Payment 110 THB received for Table 8" — timestamp |
| Table opened | "Table 8 opened" — timestamp |
| Table released | "Table 8 released" — timestamp |
| Kitchen started | "Ticket #5: Started cooking" — timestamp |
| Kitchen ready | "Ticket #5: Ready to serve" — timestamp |

**Rules:**
- Activity Feed is collapsible
- Activity Feed shows max 20 entries
- Activity Feed refreshes on workspace load
- Activity Feed is read-only