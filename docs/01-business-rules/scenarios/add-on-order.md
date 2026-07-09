\# Add-on Order



\## Purpose



Describes the business process for adding additional items to an existing customer session.



\---



\## Actors



\* Customer

\* Restaurant Staff

\* POS System

\* Kitchen Staff



\---



\## Preconditions



\* An active order already exists.

\* The order is still editable.



\---



\## Trigger



The customer requests additional menu items.



\---



\## Main Flow



1\. Staff opens the existing order.

2\. New menu items are added.

3\. Optional notes and options are entered.

4\. The additional order is confirmed.

5\. Only the newly added items are sent to the kitchen.

6\. The kitchen prepares the new items.

7\. Food is served.

8\. The final bill includes both the original and additional items.



\---



\## Alternative Flows



\### Requested item is unavailable



Staff informs the customer and selects an alternative if required.



\---



\### Customer changes quantity before confirmation



The order is updated before being confirmed.



\---



\## Postconditions



\* The existing order has been extended.

\* Kitchen history records the additional preparation.

\* Billing includes all confirmed items.



\---



\## Related Business Rules



\* Order

\* Item

\* Kitchen

\* Payment



