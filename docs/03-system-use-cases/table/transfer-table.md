\# Transfer Table



\## Purpose



Transfers an active Order Session from one Dining Table to another.



\---



\## Goal



Move customers to another available table without affecting their Order Session.



\---



\## Primary Actor



\- Restaurant Staff



\---



\## Trigger



Customers request to change tables.



\---



\## Preconditions



\- Source table exists.

\- Destination table exists.

\- Destination table is available.

\- The Order Session is active.



\---



\## Input



\- Source Table ID

\- Destination Table ID



\---



\## Main Flow



1\. Staff selects the current table.

2\. Staff selects the destination table.

3\. System validates the destination table.

4\. System transfers the Order Session.

5\. System updates both table statuses.



\---



\## Alternative Flows



\### Destination Occupied



The transfer is rejected.



\---



\## Business Rules Applied



\- Table Transfer



\---



\## Domain Interaction



\### Aggregate



\- Dining Table



\### Related Aggregates



\- Order Session



\### Entities



\- Dining Table



\---



\## Application Responsibilities



The Application Layer is responsible for:



\- Load both Dining Tables.

\- Validate transfer rules.

\- Transfer the Order Session.

\- Persist the Aggregates.



\---



\## Domain Events



\- Table Transferred



\---



\## Output



\- Order Session transferred.

\- Table statuses updated.



\---



\## Failure Conditions



\- Table not found.

\- Destination unavailable.



\---



\## Notes



Business history remains unchanged after a transfer.

