\# Update Order



\## Purpose



Updates a Draft Order before it is confirmed.



This use case allows restaurant staff to modify the contents of an existing Draft Order.



\---



\## Goal



Modify a Draft Order while maintaining business consistency.



\---



\## Primary Actor



\- Restaurant Staff



\---



\## Trigger



Staff updates an existing Draft Order.



\---



\## Preconditions



\- The Order exists.

\- The Order is in Draft status.

\- The Order belongs to an active Order Session.



\---



\## Input



\- Order ID

\- Updated Menu Items

\- Updated Quantity

\- Updated Item Options

\- Updated Customer Notes



\---



\## Main Flow



1\. Staff opens an existing Draft Order.

2\. System loads the Order.

3\. Staff adds, removes, or modifies Order Items.

4\. Staff updates quantities, options, or notes.

5\. System validates the updated Order.

6\. System recalculates the provisional Order total.

7\. System saves the updated Order.



\---



\## Alternative Flows



\### Remove Order Item



Staff removes one or more Order Items.



\### Update Item Quantity



Staff changes the quantity of an existing Order Item.



\### Update Item Notes



Staff changes customer notes.



\---



\## Business Rules Applied



\- Order Modification

\- Item Quantity

\- Item Availability

\- Order Lifecycle



\---



\## Domain Interaction



\### Aggregate



\- Order Session



\### Entities



\- Order

\- Order Item



\### Value Objects



\- Money

\- Quantity



\---



\## Application Responsibilities



The Application Layer is responsible for:



\- Load the Order Session.

\- Locate the target Order.

\- Validate the Order state.

\- Apply requested changes.

\- Persist the updated Aggregate.



\---



\## Domain Events



\- Order Updated



\---



\## Output



\- Draft Order updated.

\- Order total recalculated.



\---



\## Failure Conditions



\- Order not found.

\- Order already confirmed.

\- Order cancelled.

\- Invalid Quantity.

\- Menu Item unavailable.



\---



\## Notes



Only Draft Orders can be updated.



After confirmation, customer changes must be handled using the Add Add-on Order use case.

