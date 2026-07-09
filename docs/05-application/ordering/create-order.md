\# Create Order



\## Purpose



Coordinates the creation of a new Draft Order within an active Order Session.



The Application Layer orchestrates the operation while delegating all business rules to the Domain Model.



\---



\## Input



\- Service Channel

\- Table ID (Dine-in only)

\- Menu Items

\- Quantities

\- Item Options

\- Customer Notes



\---



\## Preconditions



\- The request is valid.

\- The selected Service Channel exists.

\- For Dine-in, the Dining Table is available.

\- Menu Items are available.



\---



\## Application Flow



```text

Receive Create Order Request

&#x20;       │

&#x20;       ▼

Validate Request

&#x20;       │

&#x20;       ▼

Load or Create Order Session

&#x20;       │

&#x20;       ▼

Create Draft Order

&#x20;       │

&#x20;       ▼

Persist Order Session

&#x20;       │

&#x20;       ▼

Return Result

```



\---



\## Domain Interactions



\### Aggregate



\- Order Session



\### Entities



\- Order

\- Order Item



\### Value Objects



\- Service Channel

\- Quantity

\- Money



\### Repository



\- Order Session Repository



\---



\## Produced Domain Events



\- Order Created



\---



\## Output



\- Draft Order

\- Updated Order Session



\---



\## Failure Conditions



\- Invalid request.

\- Table unavailable.

\- Menu Item unavailable.

\- Invalid quantity.

\- Unable to create Order Session.



\---



\## Notes



The Application Layer coordinates the operation only.



Business rules remain inside the Order Session Aggregate.



No Kitchen Ticket is created until the Order is confirmed.

