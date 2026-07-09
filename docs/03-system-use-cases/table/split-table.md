\# Split Tables



\## Purpose



Separates previously merged Dining Tables.



\---



\## Goal



Restore independent Dining Tables while preserving business consistency.



\---



\## Primary Actor



\- Restaurant Staff



\---



\## Trigger



Customers request to separate tables.



\---



\## Preconditions



\- Tables are currently merged.



\---



\## Input



\- Table IDs



\---



\## Main Flow



1\. Staff selects merged tables.

2\. System validates the split.

3\. System separates the Dining Tables.

4\. System updates table statuses.



\---



\## Alternative Flows



\### Invalid Split



The system rejects the request.



\---



\## Business Rules Applied



\- Table Split



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



\- Load the Dining Tables.

\- Validate split rules.

\- Update the Aggregates.

\- Persist the changes.



\---



\## Domain Events



\- Tables Split



\---



\## Output



\- Tables separated.

\- Table statuses updated.



\---



\## Failure Conditions



\- Split not permitted.



\---



\## Notes



The resulting Order Sessions follow the business rules defined for table splitting.

