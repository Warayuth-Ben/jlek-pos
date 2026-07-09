\# Close Bill



\## Purpose



Closes a fully paid Bill and completes the customer transaction.



\---



\## Goal



Finalize the Order Session after all outstanding balances have been settled.



\---



\## Primary Actor



\- Restaurant Staff



\---



\## Trigger



The outstanding balance reaches zero and staff closes the Bill.



\---



\## Preconditions



\- The Bill exists.

\- The Bill is fully paid.

\- The Bill is still open.



\---



\## Input



\- Bill ID



\---



\## Main Flow



1\. Staff selects the Bill.

2\. System loads the Bill.

3\. System verifies that no outstanding balance remains.

4\. Staff confirms Bill closure.

5\. System closes the Bill.

6\. System completes the associated Order Session.



\---



\## Alternative Flows



\### Outstanding Balance Exists



The system prevents Bill closure.



\---



\## Business Rules Applied



\- Bill Closure

\- Payment Completion



\---



\## Domain Interaction



\### Aggregate



\- Bill



\### Related Aggregate



\- Order Session



\---



\## Application Responsibilities



The Application Layer is responsible for:



\- Load the Bill.

\- Verify payment completion.

\- Close the Bill.

\- Complete the Order Session.

\- Persist the Aggregate.

\- Publish the Bill Closed event.



\---



\## Domain Events



\- Bill Closed



\---



\## Output



\- Bill closed.

\- Order Session completed.



\---



\## Failure Conditions



\- Bill not found.

\- Outstanding balance remains.

\- Bill already closed.



\---



\## Notes



Closing a Bill marks the end of the customer transaction.



A closed Bill cannot be modified.

