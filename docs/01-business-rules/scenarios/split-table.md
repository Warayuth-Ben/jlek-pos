\# Split Table



\## Purpose



Describes the business process for separating a merged dining session.



\---



\## Actors



\* Restaurant Staff

\* POS System



\---



\## Preconditions



\* A merged dining session exists.

\* Business policy allows splitting.



\---



\## Trigger



Customers request separate bills or seating arrangements.



\---



\## Main Flow



1\. Staff selects the merged session.

2\. Orders are assigned to the appropriate tables.

3\. The POS validates the split.

4\. Independent dining sessions are created.

5\. Business history records the split.



\---



\## Alternative Flows



\### Some orders cannot be reassigned



The split is rejected until the issue is resolved.



\---



\## Postconditions



\* Each table has its own dining session.

\* Order ownership remains accurate.



\---



\## Related Business Rules



\* Table

\* Order

\* Payment



