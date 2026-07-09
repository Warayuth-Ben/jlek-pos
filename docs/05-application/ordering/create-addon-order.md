\# Create Add-on Order



\## Purpose



Coordinates the creation of an additional Order within an existing active Order Session.



\---



\## Input



\- Order Session ID

\- Menu Items

\- Quantities

\- Item Options

\- Customer Notes



\---



\## Preconditions



\- The Order Session exists.

\- At least one Order has already been confirmed.

\- The Bill is not closed.



\---



\## Application Flow



```text

Receive Create Add-on Order Request

&#x20;       │

&#x20;       ▼

Validate Request

&#x20;       │

&#x20;       ▼

Load Order Session

&#x20;       │

&#x20;       ▼

Create Add-on Order

&#x20;       │

&#x20;       ▼

Persist Order Session

&#x20;       │

&#x20;       ▼

Collect Domain Events

&#x20;       │

&#x20;       ▼

Commit Transaction

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



\### Repository



\- Order Session Repository



\---



\## Transaction Boundary



The entire operation is executed within a single transaction.



\---



\## Domain Events



\- Order Created



\---



\## Output



\- New Draft Add-on Order

\- Updated Order Session



\---



\## Failure Conditions



\- Order Session not found.

\- Bill already closed.

\- Invalid request.



\---



\## Notes



An Add-on Order is a new Order within the existing Order Session.



Existing Orders remain unchanged.

