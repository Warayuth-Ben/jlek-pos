\# Close Bill



\## Purpose



Coordinates closing a fully paid Bill and completing the associated Order Session.



\---



\## Input



\- Bill ID



\---



\## Preconditions



\- The Bill exists.

\- The Bill is in Paid state.



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

Close Bill

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



\- Bill

\- Order Session



\### Domain Services



\- None



\### Entities



\- Bill



\### Value Objects



\- None



\---



\## Domain Events



\- Bill Closed

\- Order Session Completed

\- Table Released



\---



\## Output



\- Closed Bill.

\- Completed Order Session.



\---



\## Failure Conditions



\- Bill not found.

\- Bill is not fully paid.

\- Bill already Closed.



\---



\## Notes



Closing a Bill completes the business transaction.



Subsequent modifications are no longer permitted.

