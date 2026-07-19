# Context Platform

## What is Context?

Context is the current selection or state that the user is working with. Context persists across workspace boundaries so staff never lose their place.

Example: If a cashier selects Table 8, then navigates to Kitchen to check an order, then to Dashboard — the system remembers "Table 8" is the active context.

---

## Context Types

| Type | Scope | Lifetime | Example |
|------|-------|----------|---------|
| **Global Context** | Application-wide | Entire session | Active workspace, current shift, current user |
| **Workspace Context** | Within workspace | While workspace is active | Cashier: selected table, current order |
| **Selection Context** | Cross-workspace | While selection is active | Selected Table 8 persists across workspaces |
| **Transient Context** | Single action | Action duration | Payment dialog state, confirmation dialog |

---

## Global Context

Global Context is the top-level application state.

| Item | Description | Persists? |
|------|-------------|-----------|
| Active Workspace | Which workspace is currently displayed | Yes (session) |
| Current User | Who is logged in | Yes (session) |
| Shift | Current operational shift | Yes (session) |
| Connection Status | Online / Offline | Continuous |

**Rules:**
- Global Context is initialized once at application start
- Global Context changes only via explicit user action (navigate, login, shift change)
- All workspaces have read access to Global Context

---

## Workspace Context

Workspace Context is the internal state of the current workspace.

| Item | Description | Persists? |
|------|-------------|-----------|
| Table Grid | All tables loaded, scroll position | Within workspace |
| Selected Table | Which table is selected | Within workspace |
| Current Order | Which order is displayed | Within workspace |
| Menu Modal Open | Whether menu modal is open | No |
| Payment Dialog Open | Whether payment is active | No |

**Rules:**
- Workspace Context is initialized when the workspace loads
- Workspace Context is cleared when navigating to a different workspace
- Workspace Context is restored if navigating back (if not explicitly cleared)

---

## Selection Context

Selection Context is the critical cross-workspace concept.

```text
Cashier selects Table 8
       │
       ├── Kithen opens → Context Bar shows "Table 8"
       │                    Kitchen shows tickets for Table 8
       │
       ├── Dashboard opens → Context Bar shows "Table 8"
       │                      Possibly filter by Table 8
       │
       └── Back to Cashier → Table 8 still selected
                              Order Panel still open
```

### Selection Rules

| Rule | Description |
|------|-------------|
| **Selection persists** | Once a user selects an entity, it remains selected across workspaces |
| **Selection is explicit** | Selection happens by clicking/tapping an entity |
| **Selection is clearable** | Selection is cleared by Escape, deselect, or new selection |
| **Selection is visible** | The Context Bar always shows the current selection |
| **Selection drives context** | Workspaces may filter or highlight based on selection |

### Selection Sources

| Selection Type | Source | Example |
|---------------|--------|---------|
| Table | GET /tables → select TableCard | Table 8 |
| Order | GET /orders → select Order Panel | Order #1024 |
| Kitchen Ticket | GET /kitchen → select TicketCard | Ticket #5 |
| Product | GET /products → select ProductCard | Chicken Rice |

---

## Transient Context

Transient Context exists only for the duration of a single interaction.

| Item | Scope | Lifetime |
|------|-------|----------|
| Modal state | Within modal | Until modal closes |
| Form input | Within form | Until form submits or cancels |
| Confirmation dialog | Within dialog | Until confirmed or dismissed |
| Toast notification | Application-wide | Until dismissed or timeout |

**Rules:**
- Transient Context is not shared across workspaces
- Transient Context is destroyed after the action completes
- Transient Context does not affect Global or Selection Context

---

## Navigation Rules

| Rule | Context Behavior |
|------|-----------------|
| Workspace switch | Selection Context preserved, Workspace Context replaced |
| Back navigation | Previous workspace restored with its Workspace Context |
| New selection | Old selection cleared, new selection set |
| Deselect (Escape) | Selection cleared, Context Bar hides |
| Modal open | Transient Context active, underlying workspace frozen |
| Modal close | Transient Context destroyed, workspace restored |
| Application close | All context lost (no persistence in v1.0) |

---

## Context Lifetime

```text
Application Start
       │
       ▼
Global Context Initialized (workspace = Home, user = unknown)
       │
       ▼
User navigates to Cashier ──→ Workspace Context created (Cashier)
       │                            │
       │                            ▼
       │                    User selects Table 8
       │                            │
       │                            ▼
       │                    Selection Context set (Table 8)
       │                            │
       │                            ├── Navigate to Kitchen
       │                            │         │
       │                            │         ▼
       │                            │    Workspace Context replaced (Kitchen)
       │                            │    Selection Context preserved (Table 8)
       │                            │
       │                            ├── Navigate to Dashboard
       │                            │         │
       │                            │         ▼
       │                            │    Workspace Context replaced (Dashboard)
       │                            │    Selection Context preserved (Table 8)
       │                            │
       │                            └── Escape (deselect)
       │                                      │
       │                                      ▼
       │                                 Selection Context cleared
       │
       ▼
User closes application ──→ All context destroyed
```

---

## Context Synchronization

When Selection Context changes, affected workspaces should react.

| Event | Workspace Reaction |
|-------|-------------------|
| Table selected | Kitchen: filter tickets for that table. Dashboard: show metrics for that table. |
| Table deselected | Kitchen: show all tickets. Dashboard: show all metrics. |
| Order confirmed | Kitchen: new ticket appears. Dashboard: metrics update. |
| Payment completed | Cashier: table released. Dashboard: sales metrics update. |

---

## Cross-workspace Context Example

```text
1. Cashier: Table 8 selected
   → Context Bar: "Table 8 — Occupied"
   → Order Panel: Order #1024 (Draft)

2. Cashier → Kitchen (navigate)
   → Context Bar: "Table 8 — Occupied" (preserved)
   → Kitchen queue: filtered to show only Table 8 tickets

3. Kitchen → Dashboard (navigate)
   → Context Bar: "Table 8 — Occupied" (preserved)
   → Dashboard: metrics for Table 8's order visible

4. Dashboard → Cashier (back)
   → Context Bar: "Table 8 — Occupied" (preserved)
   → Order Panel: Order #1024 still open (Workspace Context restored)

5. Escape (deselect)
   → Context Bar: hidden
   → Table 8 deselected
   → Back to Table Grid