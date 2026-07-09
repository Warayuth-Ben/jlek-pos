\# Request Payment



\## Purpose



Initiates the payment process for an active Order Session.



The system prepares the Bill for customer payment.



\---



\## Goal



Present the customer with the final payable amount.



\---



\## Primary Actor



\- Restaurant Staff



\---



\## Trigger



The customer requests to pay the bill.



\---



\## Preconditions



\- An active Order Session exists.

\- The Bill has not been closed.



\---



\## Input



\- Order Session ID



\---



\## Main Flow



1\. Staff selects the active Order Session.

2\. System loads the Order Session.

3\. System calculates the final Bill.

4\. System presents the payable amount.

5\. Staff confirms payment with the customer.



\---



\## Alternative Flows



\### Empty Order Session



The system rejects payment because no billable items exist.



\---



\## Business Rules Applied



\- Bill Calculation

\- Payment Policy



\---



\## Domain Interaction



\### Aggregate



\- Bill



\### Entities



\- Bill



\---



\## Application Responsibilities



The Application Layer is responsible for:



\- Load the Order Session.

\- Create or load the Bill.

\- Calculate totals.

\- Present the payable amount.



\---



\## Domain Events



\- Bill Calculated



\---



\## Output



\- Bill ready for payment.



\---



\## Failure Conditions



\- Order Session not found.

\- Bill already closed.



\---



\## Notes



No payment is recorded during this use case.



This use case prepares the Bill for payment only.

