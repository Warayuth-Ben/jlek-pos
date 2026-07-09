\# New Dine-in Order



\## Purpose



Describes the standard business process for creating a new dine-in order.



\---



\## Actors



\* Customer

\* Restaurant Staff

\* POS System

\* Kitchen Staff



\---



\## Preconditions



\* A table is available.

\* The restaurant is operating.

\* The menu is available for ordering.



\---



\## Trigger



A customer is seated and requests to place an order.



\---



\## Main Flow



1\. Staff selects an available table.

2\. The POS creates a new dine-in order.

3\. Staff adds one or more menu items.

4\. Optional item notes and options are entered.

5\. Staff reviews the order.

6\. The order is confirmed.

7\. The kitchen receives the confirmed items.

8\. Food is prepared.

9\. Food is served to the customer.

10\. Additional orders may be placed during the same dining session.

11\. The customer requests the bill.

12\. Payment is completed.

13\. The bill is closed.

14\. The table becomes available for the next customer.



\---



\## Alternative Flows



\### Customer orders additional items



Additional items are added as an Add-on Order and processed independently by the kitchen.



\---



\### Customer cancels items



Only items permitted by business policy may be cancelled.



\---



\### Customer changes table



The dining session is transferred to another table while preserving business history.



\---



\## Postconditions



\* The order is completed.

\* Payment has been recorded.

\* The dining session has ended.

\* The table is available.

\* All business events have been recorded.



\---



\## Related Business Rules



\* Foundation

\* Service Channel

\* Table

\* Order

\* Item

\* Kitchen

\* Payment



