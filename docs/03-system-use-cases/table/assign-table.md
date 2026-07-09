\# Assign Table



\## Purpose



Assigns a Dining Table to a new or existing Order Session.



\---



\## Goal



Associate a customer with an available Dining Table.



\---



\## Primary Actor



\- Restaurant Staff



\---



\## Trigger



A dine-in customer is seated.



\---



\## Preconditions



\- The Dining Table exists.

\- The Dining Table is available.

\- The Order Session exists or can be created.



\---



\## Input



\- Dining Table ID

\- Order Session ID



\---



\## Main Flow



1\. Staff selects an available Dining Table.

2\. System loads the Dining Table.

3\. System verifies availability.

4\. Staff assigns the customer.

5\. System associates the Dining Table with the Order Session.

6\. System updates the table status.



\---



\## Alternative Flows



\### Table Already Occupied



The system rejects the assignment.



\---



\## Business Rules Applied



\- Table Assignment

\- Table Availability



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

\- Validate table availability.

\- Associate the Order Session.

\- Persist the Aggregate.



\---



\## Domain Events



\- Table Assigned



\---



\## Output



\- Dining Table assigned.

\- Table status updated.



\---



\## Failure Conditions



\- Table not found.

\- Table unavailable.



\---



\## Notes



Each Dining Table can host only one active Order Session.

