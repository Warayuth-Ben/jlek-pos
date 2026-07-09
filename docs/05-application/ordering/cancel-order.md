\# Cancel Order



\## Purpose



Coordinates the cancellation of an existing Order.



\---



\## Input



\- Order Session ID

\- Order ID

\- Cancellation Reason



\---



\## Preconditions



\- The Order Session exists.

\- The Order exists.

\- Cancellation is permitted.



\---



\## Application Flow



```text

Receive Cancel Order Request

&#x20;       │

&#x20;       ▼

Validate Request

&#x20;       │

&#x20;       ▼

Load Order Session

&#x20;       │

&#x20;       ▼

Cancel Order

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



\### Repository



\- Order Session Repository



\---



\## Transaction Boundary



The cancellation is performed within a single transaction.



\---



\## Domain Events



\- Order Cancelled



\---



\## Output



\- Cancelled Order

\- Updated Order Session



\---



\## Failure Conditions



\- Order Session not found.

\- Order not found.

\- Cancellation not permitted.



\---



\## Notes



Cancelled Orders remain part of the business history and are never physically removed.

