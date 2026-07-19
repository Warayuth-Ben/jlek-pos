# Navigation Architecture

## Purpose

Defines the navigation structure of the JLek POS UI, derived from business workspaces, user roles, and system use cases. Navigation is defined at the architecture level, independent of any rendering framework.

---

## Navigation Structure

```text
Home ─────── Entry point / quick navigation
  │
  ├── Cashier ─────── Order + Table + Payment workspace
  │
  ├── Kitchen ─────── Kitchen Ticket workspace
  │
  ├── Dashboard ───── Real-time operational metrics
  │
  ├── Reports ─────── Historical analysis
  │
  └── Settings ────── System configuration
```

### Navigation Sources

| Nav Item | Derived From | Business Justification |
|----------|-------------|----------------------|
| Home | System Use Cases (all) | All roles need an entry point |
| Cashier | Use Cases: Create Order, Confirm Order, Add Item, Cancel Order, Process Payment. State Machines: Order, Dining Table, Bill | Restaurant Staff primary workspace |
| Kitchen | Use Cases: Start Preparation, Complete Preparation, Serve Items. State Machines: Kitchen Ticket | Kitchen Staff primary workspace |
| Dashboard | Use Cases (read-only): get active counts, get recent orders. State Machines: all (aggregate counts) | Manager needs real-time overview |
| Reports | Queries: GetDailySalesReport, GetSalesByPaymentReport, GetBestSellerReport | Manager needs historical analysis |
| Settings | System configuration (no business state) | Administrator |

---

## Sidebar

### Structure

```text
+---------------------------+
| LOGO                      |  ← Brand / application name
+---------------------------+
|                           |
| Icon   Home               |  ← Active state indicator
| Icon   Cashier            |
| Icon   Kitchen            |
| Icon   Dashboard          |
| Icon   Reports            |
| Icon   Settings           |
|                           |
+---------------------------+
```

### States

| State | Description |
|-------|-------------|
| Expanded | Full labels visible (240px width). Default on desktop. |
| Collapsed | Only icons visible (64px width). Default on tablet / optional on desktop. |
| Hidden | Sidebar hidden. Navigation via mobile drawer overlay. Default on mobile. |

### Badge

Badges display aggregate counts relevant to each workspace.

| Nav Item | Badge Source | Business Rationale |
|----------|-------------|-------------------|
| Cashier | Count of Occupied tables | Quick access to active tables |
| Kitchen | Count of Pending + Preparing tickets | Quick access to pending kitchen work |
| Dashboard | — | No badge needed (aggregate page) |

### Badge Rules

- Badge is a numeric counter displayed on the nav item icon.
- Badge is fetched via query, never computed client-side.
- Zero count hides the badge.
- Badge refreshes on navigation focus and periodic poll.

---

## Top Bar

### Structure

```text
+-------------------------------------------------------+
| [Menu]   Workspace Title    [Notification] [Profile]  |
+-------------------------------------------------------+
```

### Elements

| Element | Purpose | Behavior |
|---------|---------|----------|
| Menu | Toggle sidebar (mobile: open drawer) | Collapse/expand sidebar |
| Workspace Title | Current page name | Derived from active workspace |
| Notification | System alerts | Shows count badge, dropdown on click |
| Profile | User menu | User name, settings, logout |

### Notification Types

| Notification | Source | Display |
|-------------|--------|---------|
| Kitchen ticket ready | Kitchen Ticket state = Ready | Bell icon with count badge |
| Order confirmed | Order state = Confirmed | Informational |
| Payment received | Payment state = Completed | Informational |

---

## Navigation Rules

| Rule | Rationale |
|------|-----------|
| Only one workspace is active at a time | Prevents context confusion |
| Active workspace is highlighted in sidebar | Clear position awareness |
| Navigation preserves workspace state | Avoids unnecessary refetch |
| Mobile navigation is a drawer overlay | Maximizes screen space for content |
| Keyboard navigation: Ctrl+1..6 for workspaces | Power-user efficiency |
| Back navigation returns to previous workspace | Expected UX pattern |

---

## Workspace Navigation

Each workspace defines its own internal navigation flow based on the relevant Use Cases and State Machines.

### Cashier Workspace

```text
Table Grid (default)
  │
  ├── Select Table (Occupied) → Order Panel
  │                              │
  │                              ├── Add Item → Menu Modal
  │                              ├── Confirm Order
  │                              ├── Cancel Order
  │                              └── Process Payment → Payment Dialog
  │                                                   │
  │                                                   └── Close Table → Back to Table Grid
  │
  └── Select Table (Available) → Open Table → Create Order → Order Panel
```

### Kitchen Workspace

```text
Kitchen Queue (default)
  │
  ├── Column: Pending
  │     └── Ticket Card → Start Preparation → Moves to Preparing
  │
  ├── Column: Preparing
  │     └── Ticket Card → Complete Preparation → Moves to Ready
  │
  └── Column: Ready
        └── Ticket Card → Serve Items → Removed from queue
```

### Dashboard Workspace

```text
Dashboard (default)
  │
  ├── Metric Row: Open Tables | Active Orders | Preparing | Ready to Serve
  │
  ├── Section: Best Sellers (table, top 5)
  │
  ├── Section: Sales by Payment (table)
  │
  └── Section: Recent Orders (table)
```

### Reports Workspace

```text
Reports (default)
  │
  ├── Daily Sales Report (with date filter)
  │
  ├── Sales by Payment Report
  │
  └── Best Sellers Report
```

### Settings Workspace

```text
Settings (default)
  │
  ├── Section: General (Shop Name, Language, Refresh Interval)
  │
  ├── Section: Printer (Type, Paper Width, Copies)
  │
  └── Section: About (Version, Build)
```

---

## Quick Actions

| Action | Workspace | Keyboard | Description |
|--------|-----------|----------|-------------|
| New Order | Cashier | Ctrl+N | Opens selected table, creates new order |
| Confirm Order | Cashier | Ctrl+Enter | Confirms the current order |
| Process Payment | Cashier | Ctrl+P | Opens payment dialog |
| Start Cooking | Kitchen | Ctrl+S | Starts preparation on selected ticket |
| Mark Ready | Kitchen | Ctrl+R | Marks selected ticket as ready |
| Serve | Kitchen | Ctrl+D | Marks selected ticket as served |

---

## Badges

| Badge | Source Query | Refresh |
|-------|-------------|---------|
| Occupied Tables | GET /tables (filter Status=Occupied) | On focus, manual refresh |
| Pending Kitchen Tickets | GET /kitchen/active (filter Status=Pending) | Poll 15s |
| Ready Kitchen Tickets | GET /kitchen/active (filter Status=Ready) | Poll 15s |

---

## Notifications

| Notification | Trigger | Action |
|-------------|---------|--------|
| Order #{n} confirmed | Domain Event: OrderConfirmed | Navigate to Kitchen |
| Ticket #{n} ready | Domain Event: KitchenPreparationCompleted | Navigate to Cashier |
| Payment received | Domain Event: PaymentReceived | Informs Cashier |

---

## Future Expansion

| Area | Navigation Impact | Architecture Note |
|------|------------------|-------------------|
| Authentication | Login page, role-based sidebar | Add Auth workspace, add role guard to all nav items |
| Multiple shifts | Shift selection before workspace | Add Shift workspace before main navigation |
| Menu management | Admin → Manage Products/Categories | Add Menu management workspace under Settings or new Admin |
| Inventory | Admin → Manage Ingredients | Add Inventory workspace under Settings or new Admin |
| Real-time updates | Replace polling with server push | Workspace state updates via subscription instead of poll |

---

## Navigation and Business Rules

| Business Rule | Navigation Impact |
|--------------|------------------|
| Order: Confirmed orders cannot be modified | Confirm Order button hidden after Confirmed state |
| Order: Completed orders are immutable | All action buttons hidden after Completed state |
| Kitchen Ticket: Served tickets are immutable | All action buttons hidden after Served state |
| Dining Table: Available tables cannot be released | Release Table button hidden for Available tables |
| Dining Table: Occupied tables cannot be assigned | Assign Table button hidden for Occupied tables |