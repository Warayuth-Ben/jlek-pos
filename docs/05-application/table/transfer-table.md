\# Transfer Table



\## Purpose



Coordinates transferring an active Order Session from one Dining Table to another.



\---



\## Input



\- Source Table ID

\- Destination Table ID



\---



\## Preconditions



\- Both Dining Tables exist.

\- Source Table is Occupied.

\- Destination Table is Available.



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

Transfer Dining Table

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



\- Table Transferred



\---



\## Output



\- Order Session transferred.

\- Updated Dining Tables.



\---



\## Failure Conditions



\- Source Table not found.

\- Destination Table not found.

\- Destination Table unavailable.



\---



\## Notes



Business history remains unchanged after the transfer.

