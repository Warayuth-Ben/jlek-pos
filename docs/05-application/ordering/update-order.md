\# Update Order



\## Purpose



Coordinates updates to a Draft Order before it is confirmed.



The Application Layer validates the request, invokes the Domain Model to update the Order, persists the Aggregate, and returns the updated result.



\---



\## Input



\- Order Session ID

\- Order ID

\- Updated Order Items

\- Updated Quantities

\- Updated Item Options

\- Updated Customer Notes



\---



\## Preconditions



\- The Order Session exists.

\- The Order exists.

\- The Order is in Draft state.



\---



\## Application Flow



```text

Receive Update Order Request

&#x20;       │

&#x20;       ▼

Validate Request

&#x20;       │

&#x20;       ▼

Load Order Session

&#x20;       │

&#x20;       ▼

Update Order

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



A single transaction begins before loading the Aggregate and completes after the updated Aggregate has been successfully persisted.



\---



\## Domain Events



\- Order Updated



\---



\## Output



\- Updated Draft Order

\- Updated Order Session



\---



\## Failure Conditions



\- Order Session not found.

\- Order not found.

\- Order is not in Draft state.

\- Invalid update request.



\---



\## Notes



Confirmed Orders cannot be updated.



Customer changes after confirmation must use the Create Add-on Order operation.

