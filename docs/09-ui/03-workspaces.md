# Workspace Architecture

## Purpose

Define the business workspace architecture for JLek POS. A Workspace is a business-aligned collection of workflows, components, and data serving one primary persona. Workspaces are defined independently of UI framework, page structure, or component library.

---

## Workspace Contract

Every workspace must define the following contract.

| Section | Description |
|---------|-------------|
| **Purpose** | Why this workspace exists |
| **Business Goal** | What business outcome this workspace achieves |
| **Actors** | Who interacts with this workspace |
| **Entry Criteria** | Conditions required to enter this workspace |
| **Exit Criteria** | Conditions that end the workspace session |
| **Primary Workflow** | The main business flow executed here |
| **Supporting Workflows** | Secondary flows that support the primary |
| **Business States** | All business states relevant to this workspace (from State Machines) |
| **UI States** | Workspace-level UI states: loading, empty, active, error |
| **Primary Actions** | Commands this workspace executes |
| **Secondary Actions** | Read-only queries, navigation |
| **Workspace Components** | Components this workspace needs |
| **Data Dependencies** | Queries, endpoints, data contracts |
| **Keyboard Shortcuts** | Power-user shortcuts for frequent actions |
| **Responsive Behavior** | How this workspace adapts to different screens |
| **Performance Target** | Maximum acceptable response time |
| **Accessibility** | Keyboard navigation, screen reader, color contrast |
| **Future Extensions** | Known future enhancements |

---

## Workspace Dependency Diagram

```text
Business Layer (Business Rules, Scenarios Q001-Q146)
              │
              ▼
    Workspace Layer (6 workspaces)
              │
              ├── Home        ─── Entry point, aggregates
              ├── Cashier     ─── Order + Table + Payment
              ├── Kitchen     ─── Kitchen Ticket lifecycle
              ├── Dashboard   ─── Operational metrics
              ├── Reports     ─── Historical analysis
              └── Settings    ─── System configuration
              │
              ▼
   Workflow Layer (Primary + Supporting flows per workspace)
              │
              ▼
   Component Layer (Reusable UI components)
```

---

## Workspace Interaction Matrix

| Workspace | Navigate To | Reads | Writes | Events | Notifications |
|-----------|-------------|-------|--------|--------|---------------|
| **Home** | Cashier, Kitchen, Dashboard, Reports, Settings | GET /tables, GET /kitchen/active | None | Navigate | None |
| **Cashier** | Home, Kitchen | GET /tables, GET /orders/{id}, GET /products, GET /categories, GET /payments/{id} | POST /orders, POST /orders/{id}/items, POST /orders/{id}/confirm, POST /orders/{id}/cancel, POST /orders/{id}/complete, POST /tables/{id}/open, POST /tables/{id}/release, POST /payments | OrderCreated, OrderConfirmed, PaymentReceived | Ticket ready |
| **Kitchen** | Home, Cashier | GET /kitchen, GET /kitchen/active | POST /kitchen/{id}/start, POST /kitchen/{id}/complete, POST /kitchen/{id}/serve | PreparationStarted, PreparationCompleted, ItemsServed | New order confirmed |
| **Dashboard** | Home, Cashier, Kitchen, Reports | GET /tables, GET /kitchen/active, GET /orders, GET /reports/* | None | None | None |
| **Reports** | Home, Dashboard | GET /reports/daily-sales, GET /reports/sales-by-payment, GET /reports/best-sellers | None | None | None |
| **Settings** | Home | Client-side config | Client-side config (no backend) | None | None |

---

## 1. Home Workspace

### Workspace Contract

| Section | Definition |
|---------|-----------|
| **Purpose** | Entry point providing quick overview and navigation to all workspaces |
| **Business Goal** | Enable staff to quickly assess current restaurant state and navigate to the correct workspace |
| **Actors** | All roles: Restaurant Staff, Kitchen Staff, Manager |
| **Entry Criteria** | Application loads successfully, API health check passes |
| **Exit Criteria** | User navigates to another workspace |
| **Business Scenarios** | Q001-Q146 (all — derived from complete business scope) |
| **Use Cases** | None (aggregate view only) |
| **State Machines** | All (aggregate counts by state) |

### Business States

None. Home displays aggregate counts of all State Machine states.

### UI States

| State | Condition |
|-------|-----------|
| Loading | Initial data fetch in progress |
| Ready | All metrics loaded |
| Partial | Some data sources unavailable (show loaded, indicate missing) |
| Error | API unavailable, show retry |

### Primary Actions

| Action | Target |
|--------|--------|
| Navigate to Cashier | Cashier workspace |
| Navigate to Kitchen | Kitchen workspace |
| Navigate to Dashboard | Dashboard workspace |

### Primary KPI

- Open Tables count
- Active Orders count
- Preparing count
- Ready to Serve count

### Critical Information

| Data | Source |
|------|--------|
| Open Tables count | GET /tables (filter Available = false) |
| Active Orders count | GET /orders (filter Active) |
| Preparing count | GET /kitchen/active (filter Preparing) |
| Ready count | GET /kitchen/active (filter Ready) |

### Data Dependencies

- GET /tables (count by status)
- GET /kitchen/active (count by status)
- GET /orders (count by status)

---

## 2. Cashier Workspace

### Workspace Contract

| Section | Definition |
|---------|-----------|
| **Purpose** | Primary workspace for restaurant staff to manage tables, orders, and payments |
| **Business Goal** | Serve customers efficiently: open tables, take orders, process payments |
| **Actors** | Restaurant Staff (Cashier, Waitstaff) |
| **Entry Criteria** | Workspace loaded, tables data available |
| **Exit Criteria** | Shift ends, user navigates away |

### Business Sources

| Source | References |
|--------|-----------|
| **Business Scenarios** | Q001-Q050 (ordering), Q051-Q080 (table management), Q081-Q110 (payment) |
| **Use Cases** | Create Order, Confirm Order, Add Item, Remove Item, Cancel Order, Assign Table, Transfer Table, Merge Tables, Split Tables, Release Table, Process Payment, Request Payment, Close Bill |
| **State Machines** | Order (Draft → Confirmed → Completed → Cancelled), Dining Table (Available → Occupied), Bill (Open → Closed), Order Session |

### Business States

| State Machine | States Displayed |
|---------------|-----------------|
| Order | Draft, Confirmed, Completed, Cancelled |
| Dining Table | Available, Occupied |
| Bill | Open, Paid, Refunded |
| Order Session | Active, Completed |

### UI States

| State | Condition |
|-------|-----------|
| Loading | Initial table grid load |
| Ready | Tables displayed, grid ready |
| Table Selected | User clicked a table, order panel open |
| Order Active | Order panel shows draft order with items |
| Payment Active | Payment dialog open |
| Empty | No tables created |
| Error | API error on any action |

### Primary Workflow

```text
Table Grid (default view)
  │
  ├── Select Available Table → Open Table → Create Order
  │                                            │
  │                                            ├── Add Items (Menu Modal)
  │                                            ├── Update Quantities
  │                                            └── Confirm Order
  │
  ├── Select Occupied Table → Order Panel
  │                            │
  │                            ├── Add Items (Menu Modal)
  │                            ├── Confirm Order (if Draft)
  │                            ├── Cancel Order (if Draft/Confirmed)
  │                            └── Process Payment (if Confirmed/Completed)
  │                                 │
  │                                 └── Payment Dialog → Close Table
  │
  └── Quick actions from Table Card
       ├── Open Table (if Available)
       └── Process Payment (if Occupied + Confirmed)
```

### Supporting Workflows

| Workflow | Trigger |
|----------|---------|
| Transfer Table | Table occupied, need to move to another table |
| Merge Tables | Two occupied tables request combined service |
| Split Tables | Single table splits into separate bills |
| Cancel Order | Customer cancels before service |
| Customer Print | Print receipt for customer |
| Kitchen Print | Print ticket for kitchen |

### Primary Actions

| Action | Command | Trigger | State Machine Transition |
|--------|---------|---------|------------------------|
| Open Table | POST /tables/{id}/open | Click Available table | Dining Table: Reserved → Available |
| Create Order | POST /orders (with tableId) | After table opened | Order: (new) → Draft |
| Add Item | POST /orders/{id}/items | Select product | Order: add OrderItem |
| Confirm Order | POST /orders/{id}/confirm | Click Confirm | Order: Draft → Confirmed |
| Cancel Order | POST /orders/{id}/cancel | Click Cancel | Order: * → Cancelled |
| Process Payment | POST /payments | Fill payment form | Bill: Open → Paid |
| Release Table | POST /tables/{id}/release | After payment | Dining Table: Occupied → Available |

### Secondary Actions

| Action | Type |
|--------|------|
| View table details | Query |
| View order items | Query |
| Calculate bill summary | Query |
| Product search | Query |

### Critical Information

| Data | Source |
|------|--------|
| All tables with status | GET /tables |
| Available tables | GET /tables/available |
| Order with items | GET /orders/{id} |
| Products by category | GET /products, GET /categories |
| Payment status | GET /payments/{id} |

### Workspace Components

| Component | Business Source |
|-----------|----------------|
| TableGrid | Dining Table State Machine (all states) |
| TableCard | Dining Table state (Available/Occupied) |
| OrderPanel | Order State Machine + Order Items |
| MenuModal | Product catalog |
| BillSummary | Bill State Machine |
| PaymentDialog | Payment use case |
| ReceiptPreview | Receipt use case |
| OrderStatusBadge | Order State Machine (all states) |
| TableStatusBadge | Dining Table State Machine (all states) |

### Keyboard Shortcuts

| Shortcut | Action |
|----------|--------|
| Ctrl+N | New Order (selected table) |
| Ctrl+Enter | Confirm Order |
| Ctrl+C | Cancel Order |
| Ctrl+P | Process Payment |
| Escape | Close modal / deselect table |

### Error Conditions

| Error | Display |
|-------|---------|
| Table already occupied | "Table is occupied" message |
| Order already confirmed | Confirm button disabled |
| Insufficient payment | "Amount is less than total" |
| Network error | "Connection lost. Retry." |
| Product unavailable | Item shown but order disabled |

### Empty State

No tables created: "Create tables in Settings or contact administrator."

### Loading State

Table grid shows skeleton cards (pulsing placeholders).

### Success State

- Order confirmed: brief green toast "Order #X confirmed"
- Payment received: "Payment received. Change: X THB"
- Table released: Table card returns to Available state

### Permission

Full access to all Cashier functions. No role-based restrictions in v1.0.

### Future Extensions

- Multiple bill splits
- Partial payments (split across methods)
- Order notes for kitchen

---

## 3. Kitchen Workspace

### Workspace Contract

| Section | Definition |
|---------|-----------|
| **Purpose** | Primary workspace for kitchen staff to manage food preparation |
| **Business Goal** | Prepare and serve food efficiently: see new orders, prepare, mark ready, serve |
| **Actors** | Kitchen Staff (Chef, Cook) |
| **Entry Criteria** | Workspace loaded, active tickets available |
| **Exit Criteria** | User navigates away |

### Business Sources

| Source | References |
|--------|-----------|
| **Business Scenarios** | Q051-Q080 (kitchen operations) |
| **Use Cases** | Create Kitchen Ticket, Start Preparation, Complete Preparation, Serve Items |
| **State Machines** | Kitchen Ticket (Pending → Preparing → Ready → Served) |

### Business States

| State Machine | States Displayed |
|---------------|-----------------|
| Kitchen Ticket | Pending, Preparing, Ready, Served |

### UI States

| State | Condition |
|-------|-----------|
| Loading | Initial queue load |
| Ready | Queue displayed with all columns |
| Empty | No active tickets |
| Polling | Regular auto-refresh active |
| Error | API error during poll |

### Primary Workflow

```text
Kitchen Queue (4 columns)
  │
  ├── Pending Column
  │     └── Ticket Card → Start Preparation → Moves to Preparing
  │
  ├── Preparing Column
  │     └── Ticket Card → Complete Preparation → Moves to Ready
  │
  └── Ready Column
        └── Ticket Card → Serve Items → Removed from queue
```

### Supporting Workflows

| Workflow | Trigger |
|----------|---------|
| View ticket details | Click ticket card |
| Print kitchen ticket | Auto-print on new ticket |

### Primary Actions

| Action | Command | Trigger | State Machine Transition |
|--------|---------|---------|------------------------|
| Start Preparation | POST /kitchen/{id}/start | Click pending ticket | Kitchen Ticket: Pending → Preparing |
| Complete Preparation | POST /kitchen/{id}/complete | Click preparing ticket | Kitchen Ticket: Preparing → Ready |
| Serve Items | POST /kitchen/{id}/serve | Click ready ticket | Kitchen Ticket: Ready → Served |

### Critical Information

| Data | Source |
|------|--------|
| Active tickets (Pending) | GET /kitchen/active (filter Pending) |
| Active tickets (Preparing) | GET /kitchen/active (filter Preparing) |
| Active tickets (Ready) | GET /kitchen/active (filter Ready) |
| Ticket details | GET /kitchen/{id} |

### Workspace Components

| Component | Business Source |
|-----------|----------------|
| KitchenQueue | Kitchen Ticket State Machine (4 columns) |
| KitchenOrderCard | Kitchen Ticket (ticket number, items, status) |
| KitchenStatusBadge | Kitchen Ticket State Machine (all states) |
| KitchenToolbar | Filter, refresh controls |

### Keyboard Shortcuts

| Shortcut | Action |
|----------|--------|
| Ctrl+S | Start Preparation (selected ticket) |
| Ctrl+R | Complete Preparation (selected ticket) |
| Ctrl+D | Serve (selected ticket) |
| Space | Select next available ticket |

### Performance Target

- Initial load: < 2 seconds
- Poll refresh: 15 seconds (configurable)
- State transition response: < 1 second

### Empty State

"No active tickets. Waiting for new orders..."

### Loading State

Queue columns show skeleton cards.

### Error Conditions

| Error | Display |
|-------|---------|
| Ticket already processed | "Ticket is no longer in this state" |
| Network error during poll | "Connection lost. Retrying..." |

### Permission

Full access to Kitchen functions.

### Future Extensions

- Real-time updates via SignalR (replace polling)
- Ticket reorder (drag and drop)
- Estimated preparation time display

---

## 4. Dashboard Workspace

### Workspace Contract

| Section | Definition |
|---------|-----------|
| **Purpose** | Real-time operational overview for managers |
| **Business Goal** | Enable manager to assess current restaurant performance at a glance |
| **Actors** | Manager, Owner |
| **Entry Criteria** | Workspace loaded, data available |
| **Exit Criteria** | User navigates away |

### Business Sources

| Source | References |
|--------|-----------|
| **Business Scenarios** | Q111-Q130 (management, reporting) |
| **Use Cases** | None (read-only queries) |
| **State Machines** | All (aggregate counts and summaries) |

### UI States

| State | Condition |
|-------|-----------|
| Loading | Initial data load |
| Ready | All sections loaded |
| Partial | Some sections loaded |
| Empty | No operational data |
| Error | Some data sources unavailable |

### Primary Actions (View-only)

| Action | Type |
|--------|------|
| View open tables count | Metric card |
| View active orders | Metric card |
| View preparing count | Metric card |
| View ready to serve count | Metric card |
| View best sellers | Table (top 5) |
| View sales by payment | Table |
| View recent orders | Table |

### Critical Information

| Data | Source |
|------|--------|
| Open Tables | GET /tables (filter Occupied) |
| Active Orders | GET /orders (filter Confirmed) |
| Preparing count | GET /kitchen/active (filter Preparing) |
| Ready to serve | GET /kitchen/active (filter Ready) |
| Best Sellers (top 5) | GET /reports/best-sellers |
| Sales by payment | GET /reports/sales-by-payment |
| Daily sales total | GET /reports/daily-sales |

### Workspace Components

| Component | Business Source |
|-----------|----------------|
| MetricCard | Aggregate counts by state |
| DashboardTable | Report data display |

### Performance Target

- Initial load: < 3 seconds
- Data refresh: manual refresh

---

## 5. Reports Workspace

### Workspace Contract

| Section | Definition |
|---------|-----------|
| **Purpose** | Historical analysis workspace for managers |
| **Business Goal** | Analyze sales data, identify trends, make business decisions |
| **Actors** | Manager, Owner |
| **Entry Criteria** | Workspace loaded, report data available |
| **Exit Criteria** | User navigates away |

### Business Sources

| Source | References |
|--------|-----------|
| **Business Scenarios** | Q111-Q130 (management, reporting) |
| **Use Cases** | None (read-only queries) |
| **State Machines** | Completed states only (Order: Completed, Payment: Completed) |

### UI States

| State | Condition |
|-------|-----------|
| Loading | Report data loading |
| Ready | Reports displayed |
| Date Filter Active | Date filter applied, data refreshed |
| Empty | No data for selected period |
| Error | Report query failed |

### Primary Actions (View-only, Date-filtered)

| Action | Type |
|--------|------|
| View Daily Sales | Table with date filter |
| View Sales by Payment | Table |
| View Best Sellers | Table (top N) |

### Critical Information

| Data | Source |
|------|--------|
| Daily sales | GET /reports/daily-sales?date={date} |
| Sales by payment | GET /reports/sales-by-payment?from={d}&to={d} |
| Best sellers | GET /reports/best-sellers?count={n} |

### Keyboard Shortcuts

| Shortcut | Action |
|----------|--------|
| Ctrl+D | Focus date picker |

---

## 6. Settings Workspace

### Workspace Contract

| Section | Definition |
|---------|-----------|
| **Purpose** | System configuration workspace |
| **Business Goal** | Configure restaurant settings, printer, view system info |
| **Actors** | Administrator |
| **Entry Criteria** | Workspace loaded |
| **Exit Criteria** | User navigates away |

### Business Sources

None. Settings is system configuration with no business state.

### UI States

| State | Condition |
|-------|-----------|
| Ready | Settings form displayed |
| Saved | Settings saved (future) |

### Primary Actions (Future)

| Action | Type |
|--------|------|
| Save shop name | Local config |
| Select language | Local config |
| Configure printer | Local config |

### Sections

| Section | Fields |
|---------|--------|
| General | Shop Name, Language, Refresh Interval |
| Printer | Type, Paper Width, Copies |
| About | Version, Build |

---

## Workspace State Machine Mapping

| Aggregate | State | Home | Cashier | Kitchen | Dashboard | Reports |
|-----------|-------|------|---------|---------|-----------|---------|
| **Order** | Draft | — | ✅ Edit | — | — | — |
| **Order** | Confirmed | ✅ Count | ✅ View | ✅ Queue | ✅ Count | — |
| **Order** | Completed | — | — | — | ✅ Count | ✅ Data |
| **Order** | Cancelled | — | ✅ View | — | — | — |
| **Kitchen Ticket** | Pending | — | — | ✅ Queue | — | — |
| **Kitchen Ticket** | Preparing | ✅ Count | — | ✅ Queue | ✅ Count | — |
| **Kitchen Ticket** | Ready | ✅ Count | — | ✅ Queue | ✅ Count | — |
| **Kitchen Ticket** | Served | — | — | — | — | — |
| **Dining Table** | Available | ✅ Count | ✅ Grid | — | ✅ Count | — |
| **Dining Table** | Occupied | ✅ Count | ✅ Grid | — | ✅ Count | — |
| **Bill** | Open | — | ✅ Panel | — | — | — |
| **Bill** | Paid | — | ✅ History | — | ✅ Count | ✅ Data |

---

## Critical Actions Per Workspace

| Workspace | Critical Action | Frequency | Impact |
|-----------|----------------|-----------|--------|
| Cashier | Confirm Order | High | Starts kitchen workflow |
| Cashier | Process Payment | High | Revenue recording |
| Cashier | Add Item | High | Order accuracy |
| Kitchen | Start Preparation | High | Kitchen throughput |
| Kitchen | Complete Preparation | High | Service speed |
| Kitchen | Serve Items | High | Customer satisfaction |

---

## Error Handling Per Workspace

| Workspace | Error | Handling |
|-----------|-------|----------|
| All | API unavailable | Show retry button, log error, display cached data |
| Cashier | Order conflict | "Order was modified by another user" |
| Kitchen | State conflict | "Ticket already processed by another user" |
| Dashboard | Partial data | Show available data, indicate missing |
| Reports | No data | "No data for selected period" |

---

## Future Integration Points

| Integration | Workspace | Impact |
|-------------|-----------|--------|
| SignalR | Kitchen, Dashboard | Replace polling with push |
| Authentication | All | Add role-based access, login/logout |
| Menu Management | Settings → Admin | Add Product/Category/Ingredient management |
| Inventory | Settings → Admin | Add ingredient stock tracking |
| Multiple Shifts | Home → Shift Select | Shift selection before workspace |