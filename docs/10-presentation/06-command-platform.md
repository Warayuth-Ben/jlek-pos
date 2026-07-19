# Command Platform

## Purpose

Define how user actions are dispatched from the UI to the Application layer. Commands are the write-side of the Presentation Architecture.

## Command Lifecycle

```text
User Action (click Confirm)
       │
       ▼
Component dispatches Command → Command → Command Handler → API call
       │                                                    │
       │                                                    ├── Success → Store updates Model(s)
       │                                                    └── Failure → Store sets error state
       │
       ▼
Store notifies Workspace → Workspace triggers feedback
```

## Command Types

| Type | Description | Examples |
|------|-------------|----------|
| **Workspace Command** | Action within current workspace | ConfirmOrder, AddItem, StartPreparation |
| **Global Command** | Action available from any workspace | Navigate, Refresh, OpenHelp |
| **Command Palette** (future) | Searchable list of all commands | Ctrl+K |

## Command Interface

| Property | Description |
|----------|-------------|
| `type` | Command identifier (e.g., "confirmOrder") |
| `payload` | Data needed to execute (e.g., orderId) |
| `onSuccess` | Callback on success |
| `onError` | Callback on failure |

## Command Handler

Each Command has exactly one Handler. The Handler maps the Command to an API call.

```text
ConfirmOrderCommand
       │
       ▼
ConfirmOrderHandler
       │
       ├── POST /orders/{id}/confirm
       ├── Success → Update OrderPanelModel status to Confirmed
       ├── Toast: "Order sent to kitchen"
       └── Failure → Show error, revert state
```

## Workspace Commands

| Workspace | Commands |
|-----------|----------|
| Cashier | OpenTable, CreateOrder, AddItem, RemoveItem, UpdateQuantity, ConfirmOrder, CancelOrder, ProcessPayment, ReleaseTable |
| Kitchen | StartPreparation, CompletePreparation, ServeItems |
| Dashboard | Refresh (trigger Store refresh) |
| Reports | ChangeDateRange, Refresh |

## Global Commands

| Command | Shortcut | Description |
|---------|----------|-------------|
| NavigateToHome | Ctrl+1 | Navigate to Home workspace |
| NavigateToCashier | Ctrl+2 | Navigate to Cashier |
| NavigateToKitchen | Ctrl+3 | Navigate to Kitchen |
| NavigateToDashboard | Ctrl+4 | Navigate to Dashboard |
| NavigateToReports | Ctrl+5 | Navigate to Reports |
| NavigateToSettings | Ctrl+6 | Navigate to Settings |
| RefreshWorkspace | Ctrl+R | Refresh current workspace |
| OpenCommandPalette | Ctrl+K | Open command palette (future) |

## Command Rules

| Rule | Description |
|------|-------------|
| One Command per action | Each user action maps to exactly one Command |
| Commands are async | Commands never block the UI |
| Commands have feedback | Every Command triggers success or error feedback |
| Commands are traceable | Commands are logged with correlation ID |