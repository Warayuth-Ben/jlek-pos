# Presentation Model

## Purpose

Presentation Models are immutable objects that transform API DTOs into UI-optimized data. They are the only data type that Business Components consume.

## Responsibilities

| Responsibility | Description |
|---------------|-------------|
| Flatten DTOs | Transform nested DTOs into flat models |
| Format data | Apply currency, date, status formatting |
| Derive properties | Compute values from DTO data (e.g., total from items) |
| Map status | Convert numeric/enum status to visual state |
| Localize | Apply language/regional formatting |
| Never own rules | Business Rules stay in Domain |

## Mapping Rules

| Rule | Description |
|------|-------------|
| One DTO → One Model | Not strictly required, but preferred |
| Model is immutable | Once created, Presentation Model never changes |
| Mapping is stateless | Same DTO always produces same Model |
| No business logic | Formatting and deriving only |
| No API calls | Models are built from DTOs only |

## Examples

### Order DTO → OrderCardModel

```text
OrderResponse (from API)
  ├── id: Guid
  ├── status: int (0=Draft, 1=Confirmed, 2=Completed, 3=Cancelled)
  ├── tableId: Guid
  ├── tableName: string
  ├── items: OrderItemResponse[]
  └── totalAmount: decimal

OrderCardModel (Presentation Model)
  ├── id: string (formatted)
  ├── status: OrderStatus (enum)
  ├── statusText: string ("Draft", "Confirmed", etc.)
  ├── statusColor: string ("gray", "blue", "green", "red")
  ├── tableName: string
  ├── itemCount: int (derived from items.Length)
  ├── totalText: string ("฿110.00")
  └── isEditable: bool (derived: status == Draft)
```

### PaymentResponse → PaymentPanelModel

```text
PaymentPanelModel
  ├── orderId: string
  ├── totalText: string
  ├── method: PaymentMethod
  ├── methodText: string ("Cash", "Credit Card", "QR")
  ├── amountReceived: decimal
  ├── amountReceivedText: string
  ├── changeText: string
  ├── status: PaymentStatus
  ├── statusText: string
  └── canRefund: bool
```

### KitchenTicketResponse → KitchenTicketModel

```text
KitchenTicketModel
  ├── id: string
  ├── ticketNumber: int
  ├── status: KitchenTicketStatus
  ├── statusText: string
  ├── statusColor: string
  ├── items: KitchenItemModel[]
  ├── elapsedMinutes: int (derived from creation time)
  └── isActionable: bool
```

## Rules

| Rule | Description |
|------|-------------|
| DTOs never reach Components | Components consume Models only |
| Models never call APIs | Models are data, not services |
| Models are plain objects | No behavior, no methods except formatting |
| Models are serializable | Safe to pass across boundaries |
| Models are immutable | Replace, never mutate |