\# Release Table



\## Purpose



Releases a Dining Table after the associated Order Session has been completed.



\---



\## Goal



Return the Dining Table to the Available state.



\---



\## Primary Actor



\- POS System



\---



\## Trigger



The associated Bill is closed.



\---



\## Preconditions



\- The Bill has been closed.

\- The Order Session has been completed.

\- The Dining Table is occupied.



\---



\## Input



\- Dining Table ID



\---



\## Main Flow



1\. System loads the Dining Table.

2\. System verifies that the Order Session has ended.

3\. System removes the Order Session assignment.

4\. System changes the table status to Available.

5\. System saves the updated Dining Table.



\---



\## Alternative Flows



\### Order Session Still Active



The table cannot be released.



\---



\## Business Rules Applied



\- Table Release

\- Table Lifecycle



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



\- Load the Dining Table.

\- Validate release conditions.

\- Release the table.

\- Persist the Aggregate.

\- Publish the Table Released event.



\---



\## Domain Events



\- Table Released



\---



\## Output



\- Dining Table available for new customers.



\---



\## Failure Conditions



\- Table not found.

\- Order Session still active.



\---



\## Notes



Releasing a Dining Table completes the dine-in seating lifecycle.

