\# Add Add-on Order



\## Purpose



Creates an additional Order within an existing Order Session after the original Order has been confirmed.



\---



\## Goal



Allow customers to request additional menu items without modifying previously confirmed Orders.



\---



\## Primary Actor



\- Restaurant Staff



\---



\## Trigger



A customer requests additional menu items after placing the original order.



\---



\## Preconditions



\- An active Order Session exists.

\- At least one Order has already been confirmed.

\- The Bill has not been closed.



\---



\## Input



\- Order Session ID

\- Menu Items

\- Quantity

\- Item Options

\- Customer Notes



\---



\## Main Flow



1\. Staff selects the active Order Session.

2\. Staff starts a new Add-on Order.

3\. Staff selects additional Menu Items.

4\. Staff specifies quantities.

5\. Staff enters customer notes when required.

6\. System validates the Add-on Order.

7\. System creates a new Draft Order.

8\. Staff confirms the Add-on Order.

9\. System publishes the Order Confirmed event.



\---



\## Alternative Flows



\### Multiple Add-on Orders



The customer places multiple Add-on Orders during the same Order Session.



\### Kitchen Already Preparing



Previously confirmed Orders continue through kitchen preparation independently.



\---



\## Business Rules Applied



\- Add-on Order

\- Order Confirmation

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

\- Create a new Order within the existing Order Session.

\- Validate the Add-on Order.

\- Persist the Aggregate.

\- Publish the Order Confirmed event.



\---



\## Domain Events



\- Order Created

\- Order Confirmed



\---



\## Output



\- New confirmed Add-on Order.

\- Order Session updated.

\- Kitchen processing initiated.



\---



\## Failure Conditions



\- Order Session not found.

\- Bill already closed.

\- Invalid Quantity.

\- Menu Item unavailable.



\---



\## Notes



Each Add-on Order is an independent Order within the same Order Session.



Previously confirmed Orders remain unchanged.

