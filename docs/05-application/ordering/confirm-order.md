\# Confirm Order



\## Purpose



Coordinates the confirmation of a Draft Order.



The Application Layer validates the request, invokes the Domain Model to confirm the Order, persists the updated Aggregate, and produces the appropriate Domain Events.



\---



\## Input



\- Order Session ID

\- Order ID



\---



\## Preconditions



\- The Order Session exists.

\- The Order exists.

\- The Order is in Draft state.

\- The Order satisfies all business validation rules.



\---



\## Application Flow



```text

Receive Confirm Order Request

&#x20;       │

&#x20;       ▼

Validate Request

&#x20;       │

&#x20;       ▼

Load Order Session

&#x20;       │

&#x20;       ▼

Confirm Order

&#x20;       │

&#x20;       ▼

Persist Order Session

&#x20;       │

&#x20;       ▼

Produce Domain Events

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



\## Produced Domain Events



\- Order Confirmed

\- Kitchen Ticket Created



\---



\## Output



\- Confirmed Order

\- Updated Order Session



\---



\## Failure Conditions



\- Order Session not found.

\- Order not found.

\- Order is not in Draft state.

\- Business validation failed.



\---



\## Notes



The Application Layer does not create Kitchen Tickets directly.



It invokes the Domain Model, which produces the necessary Domain Events.

