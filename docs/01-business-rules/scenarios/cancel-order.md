\# Cancel Order



\## Purpose



Describes the business process for cancelling an order or one or more order items.



\---



\## Actors



\* Customer

\* Restaurant Staff

\* POS System

\* Kitchen Staff



\---



\## Preconditions



\* An active order exists.

\* Cancellation is permitted by business policy.



\---



\## Trigger



The customer requests to cancel an order or specific items.



\---



\## Main Flow



1\. Staff opens the active order.

2\. The items or order to be cancelled are selected.

3\. The POS validates that cancellation is allowed.

4\. The cancellation is confirmed.

5\. The kitchen is notified if preparation is affected.

6\. Billing is recalculated if necessary.

7\. The cancellation is recorded in the business history.



\---



\## Alternative Flows



\### Kitchen preparation has already been completed



Business policy determines whether cancellation is still allowed.



\---



\### Only some items are cancelled



The remaining items continue through the normal workflow.



\---



\## Postconditions



\* Cancelled items are excluded from future processing.

\* Business history remains complete.

\* Audit records are preserved.



\---



\## Related Business Rules



\* Order

\* Kitchen

\* Payment



