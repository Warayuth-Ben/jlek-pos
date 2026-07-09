\# Create Order



\## Purpose



Creates a new customer order within an existing or newly created Order Session.



This use case starts the ordering process for a customer.



\---



\## Primary Actor



\- Restaurant Staff



\---



\## Supporting Actors



\- Customer

\- POS System



\---



\## Trigger



A customer requests to place an order.



\---



\## Preconditions



\- The restaurant is operating.

\- A valid ServiceChannel has been selected.

\- For Dine-in, a table has been assigned.

\- Menu items are available.



\---



\## Main Flow



1\. Staff starts a new order.

2\. The system creates or selects the active Order Session.

3\. Staff selects menu items.

4\. Staff specifies quantities.

5\. Staff selects options when applicable.

6\. Staff enters customer notes when required.

7\. The system calculates the provisional order total.

8\. The Order is created in Draft status.



\---



\## Alternative Flows



\### Existing Order Session



If an active Order Session already exists, the new Order is added to that session.



\### Invalid Menu Item



Unavailable menu items cannot be added.



\---



\## Postconditions



\- A new Order exists.

\- The Order belongs to an Order Session.

\- Order Items have been created.

\- The Order remains editable.



\---



\## Domain Interaction



Aggregate



\- Order Session



Entities



\- Order

\- Order Item



Value Objects



\- Money

\- Quantity

\- ServiceChannel



\---



\## Domain Events



\- Order Created



\---



\## Failure Conditions



\- Invalid ServiceChannel

\- Table unavailable

\- Menu item unavailable

\- Invalid quantity



\---



\## Related Business Rules



\- Order Creation

\- Item Quantity

\- Item Availability

\- Service Channel



\---



\## Related Domain Objects



\- Order Session

\- Order

\- Order Item

