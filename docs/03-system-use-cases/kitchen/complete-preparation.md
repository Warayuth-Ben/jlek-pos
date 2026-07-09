\# Complete Preparation



\## Purpose



Marks a Kitchen Ticket as fully prepared.



Food is ready to be served to the customer.



\---



\## Goal



Move a Kitchen Ticket from Preparing to Ready.



\---



\## Primary Actor



\- Kitchen Staff



\---



\## Trigger



Kitchen Staff completes preparation.



\---



\## Preconditions



\- The Kitchen Ticket exists.

\- The Kitchen Ticket is in Preparing status.



\---



\## Input



\- Kitchen Ticket ID



\---



\## Main Flow



1\. Kitchen Staff selects the Kitchen Ticket.

2\. System loads the Kitchen Ticket.

3\. Kitchen Staff confirms completion.

4\. System changes the Kitchen Ticket status to Ready.

5\. System records the completion time.



\---



\## Alternative Flows



\### Already Ready



The system ignores duplicate completion requests.



\---



\## Business Rules Applied



\- Kitchen Completion

\- Kitchen Lifecycle



\---



\## Domain Interaction



\### Aggregate



\- Kitchen Ticket



\### Entities



\- Kitchen Ticket

\- Kitchen Item



\---



\## Application Responsibilities



The Application Layer is responsible for:



\- Load the Kitchen Ticket.

\- Validate the current status.

\- Complete preparation.

\- Persist the updated Aggregate.

\- Publish the preparation completed event.



\---



\## Domain Events



\- Preparation Completed



\---



\## Output



\- Kitchen Ticket status updated to Ready.

\- Completion time recorded.



\---



\## Failure Conditions



\- Kitchen Ticket not found.

\- Kitchen Ticket not in Preparing status.



\---



\## Notes



A Ready Kitchen Ticket is waiting to be served to the customer.

