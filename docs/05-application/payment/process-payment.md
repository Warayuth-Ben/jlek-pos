\# Process Payment



\## Purpose



Coordinates recording a customer payment against a Bill.



\---



\## Input



\- Bill ID

\- Payment Method

\- Payment Amount



\---



\## Preconditions



\- The Bill exists.

\- The Bill is Open or Partially Paid.



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

Record Payment

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



\### Domain Services



\- None



\### Entities



\- Payment



\### Value Objects



\- Money



\---



\## Domain Events



\- Payment Completed



\---



\## Output



\- Payment recorded.

\- Updated Bill.



\---



\## Failure Conditions



\- Bill not found.

\- Invalid payment amount.

\- Unsupported payment method.



\---



\## Notes



Multiple Payments may be recorded until the Bill reaches the Paid state.

