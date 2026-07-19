# Cashier Workspace

## Purpose

The Cashier Workspace is the primary ordering and payment interface for restaurant staff. It enables the complete front-of-house workflow: table management, order creation and modification, order confirmation, payment processing, and receipt generation.

This is the **Reference Workspace** for all JLek POS workspaces. Every other workspace follows the same architecture standard.

---

## Business Goal

Enable restaurant staff to serve dine-in customers efficiently by:
- Managing table availability and occupancy
- Creating and modifying orders quickly
- Confirming orders and sending to kitchen
- Processing payments accurately
- Closing tables for the next customer

---

## Personas

| Persona | Role | Responsibilities |
|---------|------|-----------------|
| **Cashier** | Restaurant Staff | Take orders, process payments, manage tables |
| **Owner** | Business Owner | Override discounts, void transactions, view real-time sales |

---

## Primary KPIs

| KPI | Target | Measurement |
|-----|--------|-------------|
| Order Creation Time | < 30 seconds | Time from table selection to order created |
| Item Addition Speed | < 3 seconds per item | Time to search, select, and add an item |
| Confirm to Kitchen | < 2 seconds | Time from confirm click to kitchen ticket created |
| Payment Processing | < 15 seconds | Time from payment dialog open to completion |
| Active Orders Managed | Unlimited | Number of concurrent orders per cashier |
| Table Turnover Accuracy | 100% | No tables left in wrong state |
| Error Rate | < 1% | Incorrect orders, wrong payments |

---

## User Journey

```text
Open Workspace (Cashier loads)
       │
       ▼
  [LOADING] Fetch tables, products, categories
       │
       ▼
  [READY] Table Grid displayed
       │
       ├──────────── Available Table ─────────────┐
       │                                           │
       ▼                                           ▼
  Open Table                                Select Occupied Table
       │                                           │
       ▼                                           ▼
  Create Order                              Order Panel Opens
       │                                           │
       ▼                                           ├── Add Items (Menu Modal)
  Order Panel Opens                                 ├── Update Quantity
       │                                            ├── Add Modifiers
       ├── Add Items (Menu Modal)                   ├── Add Notes
       ├── Update Quantity                          ├── Remove Items
       ├── Add Modifiers                            │
       ├── Add Notes                                ├── Confirm Order (if Draft)
       │                                            │      │
       ├── Confirm Order                            │      ▼
       │      │                                     │  Order → Confirmed
       │      ▼                                     │      │
       │  Order → Confirmed                         │      ├── Payment
       │      │                                     │      │     │
       │      ├── Payment                           │      │     ▼
       │      │     │                               │      │  Payment Dialog
       │      │     ▼                               │      │     │
       │      │  Payment Dialog                     │      │     ├── Cash
       │      │     │                               │      │     ├── Credit Card
       │      │     ├── Cash                        │      │     └── QR Payment
       │      │     ├── Credit Card                 │      │     │
       │      │     └── QR Payment                  │      │     ▼
       │      │     │                               │      │  Payment Complete
       │      │     ▼                               │      │     │
       │      │  Payment Complete                   │      │     ▼
       │      │     │                               │      │  Receipt (optional print)
       │      │     ▼                               │      │     │
       │      │  Receipt (optional print)           │      │     ▼
       │      │     │                               │      │  Close Table
       │      │     ▼                               │      │
       │      │  Close Table                        │      ├── Cancel Order
       │      │                                     │      │
       │      └── Back to Table Grid                │      └── Back to Table Grid
       │                                           │
       └───────────────────────────────────────────┘
```

---

## Screen Flow

```text
Home ───→ Cashier ───→ Kitchen (navigate to see ticket)
  │                       │
  │                       └──→ Back to Cashier
  │
  ├──→ Dashboard (real-time metrics)
  │
  ├──→ Reports (historical data)
  │
  └──→ Settings (configuration)
```

Internal Cashier navigation:

```text
Table Grid ──→ Order Panel ──→ Menu Modal
                                   │
                                   └──→ Back to Order Panel
                                   
Order Panel ──→ Payment Dialog ──→ Receipt Preview
                                      │
                                      └──→ Table Grid (after close)
```

---

## Workspace Layout

```text
┌──────────────────────────────────────────────────────────────┐
│ Header: [Logo] Cashier Workspace             [⌛] [🔔] [👤] │
├──────────────────────────────────────────────────────────────┤
│ Toolbar: [🔍 Search Table] [🔄 Refresh] [📊 Dashboard]      │
├─────────────────────────────┬────────────────────────────────┤
│                             │                                │
│   Table Grid                │   Order Panel (when active)    │
│   ┌─────┐ ┌─────┐ ┌─────┐  │   ┌──────────────────────────┐ │
│   │ T1  │ │ T2  │ │ T3  │  │   │ Table: T1                │ │
│   │ Avail│ │ Occ │ │ Occ │  │   │ Order: #1024 (Draft)      │ │
│   └─────┘ └─────┘ └─────┘  │   ├──────────────────────────┤ │
│   ┌─────┐ ┌─────┐ ┌─────┐  │   │ Items:                    │ │
│   │ T4  │ │ T5  │ │ T6  │  │   │ Chicken Rice x2   90 THB │ │
│   │ Avail│ │ Avail│ │ Occ │  │   │ Water x2          20 THB │ │
│   └─────┘ └─────┘ └─────┘  │   │                    ──────  │ │
│                             │   │ Total:            110 THB │ │
│   (scrollable grid)         │   ├──────────────────────────┤ │
│                             │   │ [+ Add Item]             │ │
│                             │   │ [✓ Confirm]    [✗ Cancel]│ │
│                             │   └──────────────────────────┘ │
│                             │                                │
├─────────────────────────────┴────────────────────────────────┤
│ Status Bar: 4 Tables Occupied  |  5 Orders Active  |  Online │
└──────────────────────────────────────────────────────────────┘
```

### Layout Regions

| Region | Description | Behavior |
|--------|-------------|----------|
| **Header** | Workspace title, notifications, profile | Fixed at top, 56px height |
| **Toolbar** | Search, refresh, navigation actions | Below header, 48px height |
| **Table Grid** | All tables displayed as cards | Scrollable, flexible width |
| **Order Panel** | Order detail, items, actions | Slides in from right, 400px width, visible when table selected |
| **Menu Modal** | Product browser with categories | Overlay modal, 80% width, triggered by "Add Item" |
| **Payment Dialog** | Payment form | Overlay modal, 480px width, triggered by "Payment" |
| **Status Bar** | Aggregated counts, connection status | Fixed at bottom, 32px height |

---

## Panel Specification

### 1. Table Grid Panel

| Section | Definition |
|---------|-----------|
| **Purpose** | Display all dining tables with their current status for quick selection |
| **Displayed Information** | Table name, status (Available/Occupied), active session indicator, order count |
| **Business Rules** | Only Available tables can be opened. Occupied tables show order panel on click. |
| **Primary Actions** | Click Available → Open Table. Click Occupied → View Order. |
| **Secondary Actions** | Filter by status, search table name |

#### States

| State | Behavior |
|-------|----------|
| **Loading** | Skeleton cards (6-8 placeholder cards with pulse animation) |
| **Ready** | All table cards displayed with correct status colors |
| **Empty** | "No tables created yet. Create tables in Settings." with [Create Table] button |
| **Error** | "Unable to load tables. [Retry]" with error details in log |

---

### 2. Order Panel

| Section | Definition |
|---------|-----------|
| **Purpose** | Display and manage a single order's items, totals, and lifecycle |
| **Displayed Information** | Table name, order ID, order status, item list (name, qty, unit price, line total), total amount |
| **Business Rules** | Draft: editable. Confirmed: view-only, payment enabled. Completed: view-only. Cancelled: hidden. |
| **Primary Actions** | Add Item, Update Quantity, Remove Item, Confirm Order, Cancel Order, Process Payment |
| **Secondary Actions** | Add notes, print receipt |

#### States

| State | Behavior |
|-------|----------|
| **Loading** | Skeleton: order header + 3 item placeholders |
| **Empty (no items)** | "No items yet. [Add Item]" prominent button |
| **Draft** | Full edit mode: add, remove, update quantity, confirm enabled |
| **Confirmed** | View-only: no edit. Payment button enabled. "Sent to kitchen" indicator |
| **Completed** | View-only: all actions disabled. "Order complete" badge |
| **Error** | "Unable to load order. [Retry]" or inline error on action failure |

---

### 3. Menu Modal

| Section | Definition |
|---------|-----------|
| **Purpose** | Browse and select products to add to the current order |
| **Displayed Information** | Categories (tabs or sidebar), products (name, price), search results |
| **Business Rules** | Products shown only if available. Price shown from suggested price list. |
| **Primary Actions** | Select product → add to order with default quantity 1 (then close modal) |
| **Secondary Actions** | Search by name, filter by category |

#### States

| State | Behavior |
|-------|----------|
| **Loading** | Category tabs skeleton + product grid skeleton |
| **Ready** | Products displayed by category, search active |
| **Empty (no products)** | "No products available. Add products in Settings." |
| **Empty (search)** | "No products matching '{query}'" |
| **Error** | "Unable to load menu. [Retry]" |

---

### 4. Payment Dialog

| Section | Definition |
|---------|-----------|
| **Purpose** | Process payment for a confirmed order |
| **Displayed Information** | Order total, payment methods (Cash, Credit Card, QR), amount received, change due |
| **Business Rules** | Payment method required. Amount must be >= total. Change = received - total. |
| **Primary Actions** | Select payment method, enter amount received, complete payment |
| **Secondary Actions** | Cancel payment, print receipt |

#### States

| State | Behavior |
|-------|----------|
| **Loading** | Payment method options skeleton |
| **Ready** | Payment form displayed with total, method selector, amount input |
| **Processing** | "Processing payment..." spinner, all inputs disabled |
| **Success** | "Payment received! Change: X THB" with receipt option |
| **Insufficient** | "Amount is less than total. Please enter at least X THB." inline error |
| **Error** | "Payment failed. [Retry] [Cancel]" |

---

### 5. Bill Summary (inside Order Panel)

| Section | Definition |
|---------|-----------|
| **Purpose** | Display the current bill total and payment status |
| **Displayed Information** | Subtotal, any adjustments, total amount |
| **Business Rules** | Bill total = sum of (item unit_price * quantity). No tax/discount in v1.0. |
| **Primary Actions** | Trigger payment flow |
| **Secondary Actions** | None |

---

### 6. Receipt Preview

| Section | Definition |
|---------|-----------|
| **Purpose** | Preview receipt before printing |
| **Displayed Information** | Receipt content: restaurant name, items, totals, payment info |
| **Business Rules** | Receipt format defined by ReceiptConfiguration |
| **Primary Actions** | Print receipt, close |
| **Secondary Actions** | Skip print |

---

## Component Inventory

### Workspace Components

| Component | Description | Scope |
|-----------|-------------|-------|
| CashierPage | Root workspace container, manages workspace-level state | This workspace only |
| TableGrid | Grid of all table cards | This workspace only |
| OrderPanel | Single order management panel | This workspace only |

### Business Components

| Component | Description | Reusable |
|-----------|-------------|----------|
| TableCard | Single table display with status | Yes (Dashboard home) |
| OrderStatusBadge | Order state indicator | Yes (Kitchen, Dashboard) |
| TableStatusBadge | Table state indicator | Yes (Dashboard) |
| BillSummary | Order total display | No (Cashier-specific layout) |
| PaymentMethodSelector | Payment type selection | Yes (future: Settings) |
| ReceiptContent | Receipt content renderer | Yes (future: Reports) |

### Shared Components

| Component | Description | Used In |
|-----------|-------------|---------|
| SearchBar | Text search with debounce | Cashier, Settings |
| Modal | Overlay dialog container | Cashier, all workspaces |
| Toast | Non-blocking notification | All workspaces |
| LoadingSkeleton | Placeholder animation | All workspaces |
| EmptyState | Empty state with action | All workspaces |
| ErrorState | Error state with retry | All workspaces |
| StatusBadge | Generic status indicator | All workspaces |
| MetricCard | Numeric metric display | Home, Dashboard |

### Primitive Components

| Component | Description |
|-----------|-------------|
| Button | Action trigger (primary, secondary, danger, ghost) |
| Input | Text input field |
| NumberInput | Numeric input (quantity) |
| Select | Dropdown selector |
| Badge | Small label (count, status) |
| Icon | SVG icon wrapper |
| Card | Content container |
| Spinner | Loading indicator |

---

## Workflow Mapping

| Business Scenario | Use Case | State Machine Transition | UI Action | Visual Feedback |
|-------------------|----------|------------------------|-----------|-----------------|
| Q001: Customer arrives | Open Table | Table: Available → Occupied | Click Available table → Open Table | Table card changes to Occupied color |
| Q002: Staff takes order | Create Order | Order: (new) → Draft | Click Open Table → Auto-create | Order panel opens with Draft badge |
| Q005: Add item to order | Add Item | Order: add OrderItem | Menu Modal → Select product | Item appears in list with animation, total updates |
| Q010: Update quantity | Update Order | Order: update OrderItem | Click qty → edit → confirm | Line total and grand total update |
| Q012: Confirm order | Confirm Order | Order: Draft → Confirmed | Click Confirm button | Order panel switches to view-only, "Sent to kitchen" toast |
| Q020: Process payment | Process Payment | Bill: Open → Paid | Payment Dialog → Enter amount → Pay | Success toast, receipt preview, table release |
| Q025: Close table | Release Table | Table: Occupied → Available | Auto after payment | Table card returns to Available, cleared |
| Q030: Cancel order | Cancel Order | Order: * → Cancelled | Click Cancel → Confirm | Order panel closes, item list hidden |
| Q040: Transfer table | Transfer Table | Table: Transfer | Click Transfer → Select destination | Both table cards update |

---

## State Mapping

### Order States

| Order State | Visual Color | Actions Enabled | Components Visible |
|-------------|-------------|-----------------|-------------------|
| **Draft** | Status Draft (gray) | Add Item, Update Qty, Remove Item, Confirm, Cancel | Menu Modal, Order Panel (editable) |
| **Confirmed** | Status Confirmed (blue) | Payment only | Order Panel (view-only), Payment Dialog |
| **Completed** | Status Completed (green) | None (read-only) | Order Panel (view-only) |
| **Cancelled** | Status Cancelled (red) | None (hidden) | Hidden from active views |

### Kitchen Ticket States (displayed in Cashier for awareness)

| Kitchen State | Visual | Display Location |
|--------------|--------|-----------------|
| **Pending** | Status Pending | Order panel: "Waiting for kitchen" |
| **Preparing** | Status Preparing | Order panel: "Cooking..." |
| **Ready** | Status Ready | Order panel badge: "Ready to serve" |
| **Served** | Status Served | Order panel: "All items served" |

### Dining Table States

| Table State | Visual Color | Actions | Next State Trigger |
|-------------|-------------|---------|-------------------|
| **Available** | Status Available (green) | Open Table | Click Open |
| **Occupied** | Status Occupied (orange) | Select, Transfer, Merge, Split | Click → Order Panel |
| **Merged** | Same as Occupied | Select parent table | Click parent → merged order |

### Bill States

| Bill State | Visual Color | Display | Actions |
|------------|-------------|---------|---------|
| **Open** | Status Draft | Bill Summary with total | Process Payment |
| **Paid** | Status Completed | "Paid" badge | Print Receipt |
| **Refunded** | Status Cancelled | "Refunded" badge | None |

---

## Keyboard Shortcuts

| Shortcut | Context | Action |
|----------|---------|--------|
| **F1** | Global | Open Help |
| **F2** | Table Grid | Focus search bar |
| **Ctrl+N** | Table Grid | Open selected Available table (if one selected) |
| **Ctrl+N** | Order Panel | Add new item (open Menu Modal) |
| **Enter** | Menu Modal | Select highlighted product |
| **Ctrl+Enter** | Order Panel | Confirm current order |
| **Ctrl+C** | Order Panel | Cancel current order (with confirmation) |
| **Ctrl+P** | Order Panel | Open Payment Dialog |
| **Ctrl+Shift+P** | Payment Dialog | Process payment (if amount valid) |
| **Escape** | Any modal | Close modal and return to previous panel |
| **Escape** | Order Panel | Deselect table, return to Table Grid |
| **Tab** | Any form | Move to next input field |
| **Shift+Tab** | Any form | Move to previous input field |
| **Arrow Up/Down** | Menu Modal | Navigate product list |
| **Arrow Left/Right** | Menu Modal | Switch category tab |
| **F5** | Table Grid | Refresh table data |
| **Ctrl+=** | Order Panel | Increase item quantity |
| **Ctrl+-** | Order Panel | Decrease item quantity |
| **Delete** | Order Panel | Remove selected item (with confirmation) |

---

## Mouse Flow

| Action | Mouse Gesture | Result |
|--------|--------------|--------|
| Open Available Table | Click Available table card | Table opens, order created, Order Panel appears |
| Select Occupied Table | Click Occupied table card | Order Panel appears with existing order |
| Add Item | Click [+ Add Item] button | Menu Modal opens |
| Select Product | Click product card/row | Product added to order, modal closes |
| Update Quantity | Click quantity field → type new value | Line total recalculated |
| Remove Item | Click [×] on item row → confirm | Item removed, total updated |
| Confirm Order | Click [✓ Confirm] button | Order state changes to Confirmed |
| Cancel Order | Click [✗ Cancel] → confirm dialog | Order state changes to Cancelled |
| Process Payment | Click [Process Payment] | Payment Dialog opens |
| Submit Payment | Click [Pay] button | Payment processed |
| Close Table | Automatic after payment | Table returns to Available |
| Print Receipt | Click [Print] button | Receipt printed (or queued) |

---

## Touch Flow

| Gesture | Context | Action |
|---------|---------|--------|
| Tap | Table Grid | Select table (same as click) |
| Tap & Hold | Table Grid | Open table context menu (future: quick actions) |
| Swipe Left | Order Panel item | Reveal delete button |
| Tap | Product in Menu Modal | Add product to order |
| Tap | [+ Add Item] | Open Menu Modal |
| Tap | [✓ Confirm] | Confirm order |
| Tap | Payment method | Select payment type |
| Number Pad | Payment amount | Enter received amount |
| Tap | [Pay] | Complete payment |

---

## Information Priority

### High Priority (Must be immediately visible)

| Information | Location | Rationale |
|-------------|----------|-----------|
| Table name + status | Table card | Staff must know which tables are free/occupied |
| Order total | Bill Summary | Customer needs to see running total |
| Item list | Order Panel | Staff needs item overview |
| Order status | Order Panel header | Staff needs to know what actions are allowed |
| Payment amount due | Payment Dialog | Required to complete transaction |

### Medium Priority (Visible but secondary)

| Information | Location | Rationale |
|-------------|----------|-----------|
| Product price | Menu Modal | Needed when selecting items |
| Line item total | Order Panel | Customer may ask per-item cost |
| Table occupancy time | Tooltip on card | Manager may ask how long table has been occupied |
| Kitchen status | Badge on Order Panel | Staff should know if food is ready |

### Low Priority (Available on demand)

| Information | Location | Rationale |
|-------------|----------|-----------|
| Order ID | Order Panel header | Only needed for support/reference |
| Product description | Menu Modal tooltip | Rarely needed in fast ordering |
| Payment timestamp | Receipt | Printed on receipt, not needed in workflow |
| Cashier name | Header profile | Only needed for audit |

---

## Loading States

| Component | Loading Pattern | Duration |
|-----------|----------------|----------|
| Table Grid | 8 skeleton cards (pulse) | Until GET /tables responds |
| Order Panel | 4 skeleton rows + total placeholder | Until GET /orders/{id} responds |
| Menu Modal | Category tabs skeleton + 6 product skeleton cards | Until GET /products responds |
| Payment Dialog | Spinner on payment button | Until payment API responds |
| Confirm Action | Confirm button shows spinner, disabled | Until POST /orders/{id}/confirm responds |

---

## Empty States

| Context | Empty State | Action |
|---------|-------------|--------|
| No tables | "No tables created. Create tables in Settings." | Button: [Go to Settings] |
| No products | "No menu items available. Add products in Settings." | Button: [Go to Settings] |
| No items in order | "No items yet. Add items to begin." | Button: [+ Add Item] (prominent) |
| Search no results | "No tables matching '{query}'" | — |
| No payments history | "No payments for this order." | — |

---

## Error States

| Context | Error Message | Action |
|---------|--------------|--------|
| Table Grid load | "Unable to load tables." | Button: [Retry] |
| Order load | "Unable to load order." | Button: [Retry] or [Back to Tables] |
| Add Item | "Unable to add item. Please try again." | Dismissable toast |
| Confirm Order | "Unable to confirm order. Kitchen may be unavailable." | Button: [Retry] |
| Process Payment | "Payment failed. Check payment method and try again." | Button: [Retry] or [Cancel] |
| Network Error | "Connection lost. Your work is saved locally." | Reconnect banner |
| Server Error | "Something went wrong. Please try again later." | Button: [Retry] with error reference |

---

## Success Feedback

| Action | Feedback Type | Message | Duration |
|--------|---------------|---------|----------|
| Table Opened | Toast | "Table T1 opened" | 2 seconds |
| Item Added | Inline animation + toast | "Chicken Rice added" | 2 seconds |
| Item Removed | Inline animation + toast | "Item removed" | 2 seconds |
| Order Confirmed | Toast (green) | "Order #1024 sent to kitchen!" | 3 seconds |
| Payment Complete | Dialog + toast | "Payment received. Change: 20 THB" | Until dismissed |
| Table Closed | Toast | "Table T1 is now available" | 2 seconds |
| Order Cancelled | Toast (orange) | "Order #1024 cancelled" | 3 seconds |

---

## Printing Flow

```text
[Payment Complete]
       │
       ├── Auto-print enabled? ──→ Yes ──→ Send to printer queue
       │                                   │
       │                                   └──→ Receipt preparing toast
       │
       └── Auto-print disabled? ──→ Show [Print Receipt] button
                                      │
                                      └──→ Manual print on click

[Order Confirmed]
       │
       ├── Kitchen auto-print ──→ Yes ──→ Send kitchen ticket to printer
       │
       └── Kitchen auto-print ──→ No ───→ Kitchen displays on screen
```

### Print Jobs

| Print Job | Trigger | Content | Printer |
|-----------|---------|---------|---------|
| Customer Receipt | Payment complete | Items, totals, payment, change | Main receipt printer |
| Kitchen Ticket | Order confirmed | Order items with quantities, table number | Kitchen printer |
| Refund Receipt | Refund processed | Refund items, amount, reason | Main receipt printer |

---

## Offline Behavior

| Scenario | Behavior |
|----------|----------|
| **Network lost during ordering** | Current order state preserved. Buttons disabled for actions requiring API (Confirm, Payment). "Connection lost" banner shown. |
| **Network lost during payment** | Payment cannot proceed. Order held in current state. "Payment unavailable offline" message. |
| **Network restored** | Data re-fetched automatically. Banner dismissed. User can continue. |
| **Network lost on table grid** | Last known table state displayed with "Last updated: X min ago" indicator. |

### v1.0 Limitation

Offline order creation and queuing is not supported in v1.0. All operations require an active API connection.

---

## Permission Matrix

| Action | Cashier | Manager | Owner |
|--------|---------|---------|-------|
| Open Table | ✅ | ✅ | ✅ |
| Create Order | ✅ | ✅ | ✅ |
| Add Item | ✅ | ✅ | ✅ |
| Remove Item | ✅ | ✅ | ✅ |
| Confirm Order | ✅ | ✅ | ✅ |
| Cancel Order | ✅ | ✅ | ✅ |
| Process Payment | ✅ | ✅ | ✅ |
| Apply Discount | ❌ | ✅ | ✅ |
| Void Order | ❌ | ✅ | ✅ |
| Refund Payment | ❌ | ✅ | ✅ |
| Override Price | ❌ | ✅ | ✅ |
| Access Reports | ❌ | ✅ | ✅ |
| Workspace Settings | ❌ | ❌ | ✅ |

---

## Responsive Behavior

| Breakpoint | Table Grid | Order Panel | Menu Modal | Payment Dialog |
|------------|-----------|-------------|------------|----------------|
| **Desktop (≥1024px)** | Multi-column grid (4-6 cols) | Side panel 400px fixed | 80% width overlay | 480px centered |
| **Tablet (≥768px)** | 3-column grid | Bottom sheet (slides up) | Full screen | 90% width |
| **Mobile (<768px)** | 2-column grid | Full screen with back | Full screen | Full screen |

### Desktop Layout

```text
┌───────────────┐ ┌───────────────────────────────┐
│               │ │                               │
│  Table Grid   │ │       Order Panel             │
│  4-6 columns  │ │       (400px fixed)           │
│               │ │                               │
└───────────────┘ └───────────────────────────────┘
```

### Tablet Layout

```text
┌────────────────────────────────────┐
│       Table Grid (3 cols)         │
│                                    │
├────────────────────────────────────┤
│   Order Panel (bottom sheet)      │
│   (slides up when table selected) │
└────────────────────────────────────┘
```

---

## Accessibility

### Keyboard Navigation

| Element | Tab Order | Keyboard Behavior |
|---------|-----------|-------------------|
| Toolbar | 1-3 | F2 focuses search, Tab to Refresh |
| Table Grid | 4-20 | Arrow keys to navigate, Enter to select |
| Order Panel | 21-30 | Tab through items, actions at bottom |
| Confirm Button | Tab order end | Ctrl+Enter shortcut |
| Cancel Button | Before Confirm | Escape available |

### Focus Order

```text
Search → Refresh → Table Cards → [Modal opens] → 
→ Modal content → Close modal → 
→ Order Panel items → Add/Confirm/Cancel → 
→ [Payment Dialog opens] → Payment form → Pay/Cancel
```

### Contrast Requirements

| Element | Background | Text | Ratio |
|---------|------------|------|-------|
| Available Table | Green (#22c55e) | White | 4.5:1 |
| Occupied Table | Orange (#f97316) | White | 4.5:1 |
| Draft Status | Gray (#6b7280) | White | 4.5:1 |
| Confirmed Status | Blue (#3b82f6) | White | 4.5:1 |
| Error Text | — | Red (#ef4444) | 4.5:1 on background |
| Body Text | Dark (#1f2937) | White (#f9fafb) | 13:1 |

### ARIA Requirements (Conceptual)

| Element | ARIA Attribute | Purpose |
|---------|---------------|---------|
| Table Grid | role="grid", aria-label="Dining tables" | Screen reader identifies table grid |
| Table Card | role="gridcell", aria-selected | Indicates selected table |
| Order Panel | role="region", aria-label="Order for {table}" | Identifies order panel |
| Menu Modal | role="dialog", aria-modal="true" | Identifies modal |
| Status Badge | aria-label="{state} status" | Describes status |
| Toast | role="status", aria-live="polite" | Announces notifications |
| Confirm Button | aria-disabled when not allowed | Indicates disabled state |

---

## Performance Targets

| Operation | Target | Measurement |
|-----------|--------|-------------|
| Workspace Load (initial) | < 2 seconds | Time from navigation to Table Grid ready |
| Table Grid Refresh | < 1 second | Time from refresh trigger to updated grid |
| Product Search Response | < 500ms | Time from keystroke to filtered results |
| Add Item to Order | < 300ms | Time from product selection to item in list |
| Update Quantity | < 200ms | Time from quantity change to updated total |
| Confirm Order | < 1 second | Time from confirm click to order confirmed |
| Payment Processing | < 2 seconds | Time from pay click to payment confirmed |
| Menu Modal Open | < 500ms | Time from click to modal ready |
| Payment Dialog Open | < 300ms | Time from click to form ready |

---

## Future Extensions

| Feature | Description | Architecture Impact |
|---------|-------------|-------------------|
| **QR Ordering** | Customers scan QR code to view menu and order from their phone | New "Self-Service" integration. Cashier still manages payment. |
| **Grab/FoodPanda Integration** | External delivery orders appear in Cashier workspace | New "Delivery Orders" panel or filter in Table Grid. New Delivery table type. |
| **LINE MAN Integration** | Same as Grab but for LINE MAN platform | Same as above, multi-platform order aggregation. |
| **KDS (Kitchen Display System)** | Replace paper kitchen tickets with screen display | Kitchen Workspace becomes more prominent. Real-time updates via SignalR. |
| **Customer Display** | Secondary screen showing customer-facing order info | New "CustomerDisplay" panel — read-only order summary for customer. |
| **Multi-Payment Split** | Split bill across multiple payment methods | Payment Dialog extended with split selection. |
| **Discounts & Promotions** | Apply discount codes or percentage discounts | New Discount field in Order Panel. Permission-gated. |
| **Table QR Association** | Link QR code to each table for self-ordering | New field on Dining Table. QR code generation in Settings. |
| **Voice Ordering** | Staff voice input for items | New microphone icon in Menu Modal. Speech-to-text integration. |
| **Multi-language Menu** | Support Thai and English menu display | Product model extended with language fields. Language toggle in Header. |