\# Split Tables



\## Purpose



Coordinates separating previously merged Dining Tables.



\---



\## Input



\- Dining Table IDs



\---



\## Preconditions



\- The Dining Tables are currently merged.

\- Split is permitted.



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

Split Dining Tables

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



\- Tables Split



\---



\## Output



\- Dining Tables separated.



\---



\## Failure Conditions



\- Split not permitted.

\- Invalid table configuration.



\---



\## Notes



The resulting Order Sessions follow the business rules defined for table splitting.

