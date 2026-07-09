\# Start Preparation



\## Purpose



Starts food preparation for a Kitchen Ticket.



This use case marks the beginning of kitchen operations for a confirmed Kitchen Ticket.



\---



\## Goal



Move a Kitchen Ticket from Pending to Preparing.



\---



\## Primary Actor



\- Kitchen Staff



\---



\## Trigger



Kitchen Staff starts preparing a Kitchen Ticket.



\---



\## Preconditions



\- The Kitchen Ticket exists.

\- The Kitchen Ticket is in Pending status.

\- The Kitchen Ticket contains at least one Kitchen Item.



\---



\## Input



\- Kitchen Ticket ID



\---



\## Main Flow



1\. Kitchen Staff selects a Kitchen Ticket.

2\. System loads the Kitchen Ticket.

3\. Kitchen Staff starts preparation.

4\. System changes the Kitchen Ticket status to Preparing.

5\. System records the preparation start time.



\---



\## Alternative Flows



\### Ticket Already Preparing



The system prevents duplicate preparation.



\### Ticket Already Completed



The Kitchen Ticket cannot return to Preparing status.



\---



\## Business Rules Applied



\- Kitchen Preparation

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

\- Start preparation.

\- Persist the updated Aggregate.

\- Publish the preparation started event.



\---



\## Domain Events



\- Preparation Started



\---



\## Output



\- Kitchen Ticket status updated to Preparing.

\- Preparation start time recorded.



\---



\## Failure Conditions



\- Kitchen Ticket not found.

\- Kitchen Ticket already Preparing.

\- Kitchen Ticket already Completed.



\---



\## Notes



Preparation begins for the entire Kitchen Ticket.



Individual Kitchen Item preparation is managed within the Kitchen Ticket Aggregate.

