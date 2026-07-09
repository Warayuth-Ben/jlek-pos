\# Release Table



\## Purpose



Coordinates releasing a Dining Table after the associated Order Session has been completed.



\---



\## Input



\- Dining Table ID



\---



\## Preconditions



\- The Dining Table exists.

\- The associated Order Session is Completed.



\---



\## Application Flow



```text

Receive Request

&#x20;       │

&#x20;       ▼

Validate Request

&#x20;       │

&#x20;       ▼

Load Required Domain Objects

&#x20;       │

&#x20;       ▼

Release Dining Table

&#x20;       │

&#x20;       ▼

Persist Changes

&#x20;       │

&#x20;       ▼

Collect Domain Events

&#x20;       │

&#x20;       ▼

Return Result

```



\---



\## Domain Coordination



\### Aggregates



\- Dining Table

\- Order Session



\### Domain Services



\- None



\### Entities



\- Dining Table



\### Value Objects



\- None



\---



\## Domain Events



\- Table Released



\---



\## Output



\- Dining Table available for a new customer.



\---



\## Failure Conditions



\- Dining Table not found.

\- Order Session not completed.



\---



\## Notes



Releasing the Dining Table completes the seating lifecycle.

