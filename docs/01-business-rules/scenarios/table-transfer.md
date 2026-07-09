\# Table Transfer



\## Purpose



Describes the business process for moving an active dining session to another table.



\---



\## Actors



\* Restaurant Staff

\* POS System



\---



\## Preconditions



\* An active dine-in session exists.

\* A destination table is available.



\---



\## Trigger



The customer changes seating.



\---



\## Main Flow



1\. Staff selects the current table.

2\. Staff selects the destination table.

3\. The POS validates the transfer.

4\. The dining session is moved.

5\. The destination table becomes occupied.

6\. The original table becomes available.

7\. The transfer is recorded.



\---



\## Alternative Flows



\### Destination table is unavailable



The transfer is rejected.



\---



\### Destination table is part of another active session



Business policy determines whether a merge is required instead.



\---



\## Postconditions



\* The dining session continues at the new table.

\* Order history remains unchanged.



\---



\## Related Business Rules



\* Table

\* Order



