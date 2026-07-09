\# Refund Payment



\## Purpose



Coordinates recording a refund for a previously completed Payment.



\---



\## Input



\- Payment ID

\- Refund Amount

\- Refund Reason



\---



\## Preconditions



\- The Payment exists.

\- Refund is permitted.



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

Record Refund

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

\- Refund



\### Value Objects



\- Money



\---



\## Domain Events



\- Refund Completed



\---



\## Output



\- Refund recorded.



\---



\## Failure Conditions



\- Payment not found.

\- Refund exceeds payment amount.

\- Refund not permitted.



\---



\## Notes



Refunds never modify or delete the original Payment.

