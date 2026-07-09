\# Merge Tables



\## Purpose



Merges multiple Dining Tables into a single dining session.



\---



\## Goal



Allow customers occupying multiple tables to share one Order Session.



\---



\## Primary Actor



\- Restaurant Staff



\---



\## Trigger



Customers request to combine tables.



\---



\## Preconditions



\- Multiple active Dining Tables exist.

\- Merge is permitted.



\---



\## Input



\- Source Table IDs

\- Target Table ID



\---



\## Main Flow



1\. Staff selects the tables.

2\. System validates the merge.

3\. System associates all selected tables with one Order Session.

4\. System updates table statuses.



\---



\## Alternative Flows



\### Invalid Merge



The system rejects incompatible tables.



\---



\## Business Rules Applied



\- Table Merge



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



\- Load all Dining Tables.

\- Validate merge rules.

\- Update the Dining Table Aggregates.

\- Persist the changes.



\---



\## Domain Events



\- Tables Merged



\---



\## Output



\- Tables merged.

\- Order Session updated.



\---



\## Failure Conditions



\- Invalid table selection.

\- Merge not permitted.



\---



\## Notes



Merged tables continue sharing the same Order Session until separated.

