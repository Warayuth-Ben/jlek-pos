\# Merge Tables



\## Purpose



Coordinates merging multiple Dining Tables into a single Order Session.



\---



\## Input



\- Source Table IDs

\- Target Table ID



\---



\## Preconditions



\- All Dining Tables exist.

\- Merge is permitted.



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

Merge Dining Tables

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



\- Tables Merged



\---



\## Output



\- Dining Tables merged.

\- Updated Order Session.



\---



\## Failure Conditions



\- Invalid table selection.

\- Merge not permitted.



\---



\## Notes



Merged Dining Tables continue sharing the same Order Session.

