\# Create Kitchen Ticket



\## Purpose



Creates a Kitchen Ticket from a confirmed Order.



A Kitchen Ticket represents a preparation request sent to the kitchen.



\---



\## Goal



Create a Kitchen Ticket containing all confirmed Order Items that require preparation.



\---



\## Primary Actor



\- POS System



\---



\## Trigger



An Order is confirmed.



\---



\## Preconditions



\- The Order exists.

\- The Order has been confirmed.

\- The Order contains at least one item requiring kitchen preparation.



\---



\## Input



\- Order Session ID

\- Order ID



\---



\## Main Flow



1\. System receives the confirmed Order.

2\. System identifies all Order Items requiring preparation.

3\. System creates a new Kitchen Ticket.

4\. System creates Kitchen Items for each applicable Order Item.

5\. System assigns the Kitchen Ticket to the active kitchen workload.

6\. System stores the Kitchen Ticket.



\---



\## Alternative Flows



\### No Kitchen Items



If the Order contains no items requiring preparation, no Kitchen Ticket is created.



\### Multiple Add-on Orders



Each confirmed Add-on Order creates its own Kitchen Ticket.



\---



\## Business Rules Applied



\- Kitchen Ticket Creation

\- Order Confirmation

\- Kitchen Preparation



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



\- Load the confirmed Order.

\- Create a Kitchen Ticket.

\- Persist the Kitchen Ticket Aggregate.

\- Publish the Kitchen Ticket Created event.



\---



\## Domain Events



\- Kitchen Ticket Created



\---



\## Output



\- Kitchen Ticket created.

\- Kitchen Items created.



\---



\## Failure Conditions



\- Order not found.

\- Order not confirmed.

\- No items require preparation.



\---



\## Notes



Each confirmed Order creates its own Kitchen Ticket.



Multiple Kitchen Tickets may exist within the same Order Session when customers place Add-on Orders.

