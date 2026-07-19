# Cashier Screen Flow — Detailed Specification

## User Journey

```text
Start (Cashier opens workspace)
  │
  ▼
[1] Table Grid — Select Table
  │
  ├── Available → [2] Open Table + Create Order
  │                    │
  │                    ▼
  │               Order Panel (Empty)
  │                    │
  │                    ▼
  │               [3] Add Items (Menu Modal)
  │                    │
  │                    ▼
  │               [4] Adjust Quantity
  │                    │
  │                    ▼
  │               [5] Add Note (optional)
  │                    │
  │                    ▼
  │               [6] Confirm Order → Kitchen
  │                    │
  │                    ▼
  │               [7] Kitchen Processing (waiting)
  │                    │
  │                    ▼
  │               [8] Payment Dialog
  │                    │
  │                    ▼
  │               [9] Receipt
  │                    │
  │                    ▼
  │               [10] Close Table → Back to [1]
  │
  └── Occupied → Order Panel (Existing Order)
                    │
                    ├── Edit (if Draft)
                    ├── Payment (if Confirmed)
                    └── View (if Completed)
```

---

## Screen 1: Table Grid

### Purpose
Display all dining tables. Entry point for all cashier operations.

### Entry Condition
User navigates to Cashier workspace. API `GET /tables` returns table list.

### Exit Condition
User selects a table → proceeds to Order Panel.

### User Actions
| Action | Trigger | Result |
|--------|---------|--------|
| Click Available table | Click/tap | Opens table + creates order |
| Click Occupied table | Click/tap | Shows order panel |
| Search tables | Type in search bar | Filters grid |
| Filter by status | Select dropdown | Shows Available/Occupied only |
| Refresh | Click refresh button | Reloads table data |

### API Calls
| Call | When | Method | Endpoint |
|------|------|--------|----------|
| Load tables | On enter + refresh | GET | `/tables` |
| Open table | On table click | POST | `/tables/{id}/open` |
| Create order | After open | POST | `/orders?tableId={id}` |

### Possible Errors
| Error | Cause | Display |
|-------|-------|---------|
| Tables load failed | Network/server down | "Unable to load tables" + [Retry] |
| Open table failed | Concurrency (already open) | "Table already occupied" toast |
| Create order failed | Server error | "Unable to create order" + [Retry] |

### Success Flow
1. Cashier sees table grid with colored cards
2. Clicks "T1" (Available/Green)
3. `POST /tables/{id}/open` → 200 OK
4. `POST /orders` → 201 Created
5. Order Panel slides in on right side

### State Transitions
| Table State | Visual | Next State Trigger |
|-------------|--------|-------------------|
| Available | Green | Click → Open Table |
| Occupied | Orange | Click → View Order |
| Loading | Skeleton cards | API response |
| Error | Error message | [Retry] click |

---

## Screen 2: Open Table + Create Order

### Purpose
Initialize a new dining session. Two sequential API calls.

### Entry Condition
Cashier clicks an Available table card.

### Exit Condition
Order Panel opens with a Draft order.

### User Actions
None — happens automatically on table click.

### API Calls
| Step | Method | Endpoint | On Success |
|------|--------|----------|------------|
| 1 | POST | `/tables/{id}/open` | Table status → Occupied |
| 2 | POST | `/orders?tableId={id}` | Returns OrderResponse with Draft status |

### Possible Errors
| Error | Cause | Handling |
|-------|-------|----------|
| Table already occupied | Concurrency | Show toast "Table was just taken". Refresh grid. |
| Create order fails after table opened | Network | Table is Occupied but no order. Show error. Allow retry. |

### State Transitions
| State | Description |
|-------|-------------|
| Opening | Spinner on selected table card |
| Order created | Panel opens with empty Draft order |
| Error | Table card reverts to Available, error toast |

---

## Screen 3: Add Items (Menu Modal)

### Purpose
Browse menu and select items to add to the order.

### Entry Condition
Cashier clicks [+ Add Item] in Order Panel.

### Exit Condition
Cashier clicks a product → item added to order → modal stays open. OR Cashier clicks [×] → modal closes.

### User Actions
| Action | Result |
|--------|--------|
| Click product card | Adds item (qty 1), modal stays open |
| Double-click product | Adds item, modal closes |
| Click category tab | Filters products |
| Type in search | Filters by name (200ms debounce) |
| Click [×] | Closes modal |

### API Calls
| Call | When | Endpoint |
|------|------|----------|
| Load categories | Modal open | `GET /categories` |
| Load products | Modal open | `GET /products` |
| Filter by category | Tab click | `GET /products?categoryId={id}` |
| Add item | Product click | `POST /orders/{id}/items` |

### Possible Errors
| Error | Display |
|-------|---------|
| Menu load failed | "Unable to load menu" + [Retry] |
| Add item failed | Toast "Unable to add item" |

### State Transitions
| Modal State | Visual |
|-------------|--------|
| Loading | Category tab skeletons + 6 product card skeletons |
| Ready | Full product grid |
| Adding | Brief highlight on clicked product |
| Error | Error message in modal |

---

## Screen 4: Adjust Quantity

### Purpose
Change item quantities inline in the Order Panel.

### Entry Condition
Item exists in the order list.

### Exit Condition
Quantity changes reflected in line total and grand total.

### User Actions
| Action | Result |
|--------|--------|
| Click [+] | Quantity +1, line total recalculates |
| Click [-] | Quantity -1. If qty reaches 0, remove item. |
| Click [×] | Remove item with confirmation dialog |

### API Calls
| Action | Endpoint |
|--------|----------|
| Update qty | `PUT /orders/{id}/items/{itemId}?qty=N` |
| Remove item | `DELETE /orders/{id}/items/{itemId}` |

### UX Note
No API call on every [+]/[-] click. Batch updates. Send API call on blur or after 500ms idle.

---

## Screen 5: Add Note (Optional)

### Purpose
Add a note to an order item (e.g., "less spicy", "no ice").

### Entry Condition
Cashier clicks a note icon/button on an item row.

### Exit Condition
Note saved and displayed inline on the item row.

### API Call
`PUT /orders/{id}/items/{itemId}?note={text}`

---

## Screen 6: Confirm Order

### Purpose
Send the order to the kitchen. This is a point of no return for editing.

### Entry Condition
Order is in Draft state with at least one item.

### Exit Condition
Order state changes to Confirmed. Kitchen ticket created. Panel switches to view-only.

### User Actions
| Action | Result |
|--------|--------|
| Click [✓ Confirm] | Sends to kitchen |
| Click [✗ Cancel] | Cancels order (with confirmation) |

### API Calls
| Action | Endpoint |
|--------|----------|
| Confirm | `POST /orders/{id}/confirm` |
| Cancel | `POST /orders/{id}/cancel` |

### Possible Errors
| Error | Display |
|-------|---------|
| Confirm failed (kitchen down) | "Unable to confirm. Kitchen unavailable." + [Retry] |
| Cancel failed | "Unable to cancel" + [Retry] |

### State Transitions
| Order State Before | Action | Order State After | Panel Behavior |
|-------------------|--------|-------------------|----------------|
| Draft | Confirm | Confirmed | View-only, Payment enabled |
| Draft | Cancel | Cancelled | Panel closes |
| Confirmed | Payment | — | Opens Payment Dialog |

---

## Screen 7: Kitchen Processing

### Purpose
Show kitchen status for the confirmed order.

### Entry Condition
Order is Confirmed.

### Exit Condition
Kitchen status changes to Served (displayed in panel).

### Display
| Kitchen State | Badge/Label |
|---------------|-------------|
| Pending | "Waiting for kitchen" |
| Preparing | "Cooking..." |
| Ready | "Ready to serve" |
| Served | "All items served" |

### Polling
Poll `GET /kitchen/tickets?orderId={id}` every 5 seconds while Order is Confirmed.

---

## Screen 8: Payment Dialog

### Purpose
Process payment for a confirmed order.

### Entry Condition
Order is Confirmed. Cashier clicks [Process Payment].

### Exit Condition
Payment success → show Receipt Dialog. Payment cancelled → back to Order Panel.

### User Actions
| Action | Result |
|--------|--------|
| Select Cash | Shows amount input, pre-filled with total |
| Select Card | Amount = total, Pay enabled |
| Select QR | Amount = total, Pay enabled |
| Edit amount | Change auto-calculates |
| Click [✓ Pay] | Processes payment |
| Click [Cancel] | Closes dialog |

### API Call
`POST /payments` with body `{ "orderId": "...", "method": "Cash", "amountReceived": 200 }`

### Possible Errors
| Error | Display |
|-------|---------|
| Amount < total | "Amount must be at least {total}" inline error |
| Payment failed | "Payment failed" + [Retry] [Cancel] |
| Network lost | "Payment unavailable offline" |

### State Transitions
| Dialog State | Visual |
|--------------|--------|
| Ready | Method selector, amount hidden |
| Cash selected | Amount input visible, pre-filled |
| Card selected | Amount = total, Pay enabled |
| Processing | Spinner on Pay button, all disabled |
| Success | "Payment received! Change: X" |
| Error | Error message + [Retry] |

---

## Screen 9: Receipt

### Purpose
Show receipt after successful payment.

### Entry Condition
Payment successful.

### Exit Condition
Print + Close Table → back to Table Grid. Skip → back to Order Panel (occupied).

### User Actions
| Action | Result |
|--------|--------|
| [🖨️ Print & Close] | Print receipt + release table |
| [✓ Close Table] | Release table without print |
| [✕ Skip] | Keep table occupied, close receipt |

### API Calls
| Action | Endpoint |
|--------|----------|
| Print receipt | `POST /receipts/customer` (or queue to printer) |
| Release table | `POST /tables/{id}/release` |

---

## Screen 10: Close Table

### Purpose
Release the table back to Available state.

### Entry Condition
Receipt dialog shown (or payment complete if auto-close).

### Exit Condition
Table returns to Available in grid.

### API Call
`POST /tables/{id}/release`

### Auto-close Option
If "Auto close table after payment" setting is enabled, skip receipt dialog entirely and go straight to Table Grid.

---

## Summary of All API Calls Per Journey

| Step | API |
|------|-----|
| 1. Load workspace | `GET /tables` |
| 2. Open table | `POST /tables/{id}/open` |
| 3. Create order | `POST /orders` |
| 4. Load menu | `GET /categories`, `GET /products` |
| 5. Add item | `POST /orders/{id}/items` |
| 6. Update qty | `PUT /orders/{id}/items/{itemId}?qty=N` |
| 7. Remove item | `DELETE /orders/{id}/items/{itemId}` |
| 8. Confirm order | `POST /orders/{id}/confirm` |
| 9. Cancel order | `POST /orders/{id}/cancel` |
| 10. Kitchen status | `GET /kitchen/tickets?orderId={id}` |
| 11. Process payment | `POST /payments` |
| 12. Print receipt | `POST /receipts/customer` |
| 13. Release table | `POST /tables/{id}/release` |