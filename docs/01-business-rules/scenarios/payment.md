\# Payment



\## Purpose



Describes the business process for settling a customer's bill.



\---



\## Actors



\* Customer

\* Restaurant Staff

\* POS System



\---



\## Preconditions



\* The customer requests the bill.

\* The bill has been calculated.



\---



\## Trigger



The customer is ready to pay.



\---



\## Main Flow



1\. Staff opens the bill.

2\. The POS calculates the final amount.

3\. Discounts or adjustments are applied if applicable.

4\. The customer selects one or more payment methods.

5\. Payment is processed.

6\. A receipt is generated if required.

7\. The bill is closed.



\---



\## Alternative Flows



\### Split payment



Multiple payment methods are accepted until the bill is fully settled.



\---



\### Payment failure



The bill remains open until payment is completed.



\---



\## Postconditions



\* Payment has been recorded.

\* The bill is closed.

\* Business history has been updated.



\---



\## Related Business Rules



\* Payment

\* Order



