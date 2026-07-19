# Store Platform

## Store Responsibilities

Every Workspace owns exactly one Store. The Store manages all Presentation Models for that workspace.

| Responsibility | Description |
|---------------|-------------|
| State Ownership | Holds all Presentation Models for the workspace |
| Selection Ownership | Tracks current selection within the workspace |
| Context Synchronization | Publishes selection changes to the Context Platform |
| Event Subscription | Subscribes to business events relevant to the workspace |
| Lifecycle Management | Initialize, Refresh, Dispose |

## Store Lifecycle

```text
Workspace Activate → Store.Initialize() → Fetch data → Store ready
                                                              │
                                                              ├── User action → Store.HandleEvent()
                                                              ├── Selection → Store.Select()
                                                              ├── Store.Refresh() → refetch data
                                                              │
Workspace Deactivate → Store.Dispose() → Cleanup subscriptions
```

## Store Interface

| Method | Description |
|--------|-------------|
| `Initialize()` | Fetch initial data, subscribe to events |
| `Refresh()` | Re-fetch all data, update Models |
| `HandleEvent(event)` | Process a business/UI event, update Models |
| `Select(entity)` | Set selection, publish context change |
| `ClearSelection()` | Clear selection, publish context change |
| `Dispose()` | Cleanup subscriptions, release resources |

## Per-Workspace Stores

| Store | Presentation Models |
|-------|-------------------|
| **CashierStore** | TableCardModel[], OrderPanelModel, MenuCategoryModel[], MenuProductModel[], PaymentPanelModel |
| **KitchenStore** | KitchenTicketModel[] (Pending, Preparing, Ready) |
| **DashboardStore** | MetricCardModel[], BestSellerModel[], SalesByPaymentModel[] |
| **ReportsStore** | DailySalesModel[], SalesByPaymentModel[], BestSellerModel[] |

## Store Boundaries

| Rule | Description |
|------|-------------|
| One Store per Workspace | No workspace shares a Store |
| Store owns state | Components never hold persistent state |
| Store is replaceable | Store can be swapped without changing components |
| Store is stateless between sessions | Store state is ephemeral |