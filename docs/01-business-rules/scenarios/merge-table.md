\# Merge Tables



\## Purpose



Describes the business process for combining multiple dining tables into a single dining session.



\---



\## Actors



\* Restaurant Staff

\* POS System



\---



\## Preconditions



\* Two or more active tables exist.

\* Business policy allows merging.



\---



\## Trigger



Customers decide to dine together.



\---



\## Main Flow



1\. Staff selects the participating tables.

2\. The POS validates the merge.

3\. A single dining session is created.

4\. Existing orders become part of the merged session.

5\. Business history records the merge.



\---



\## Alternative Flows



\### One table cannot be merged



The operation is cancelled.



\---



\## Postconditions



\* One dining session manages all participating tables.

\* Historical relationships remain traceable.



\---



\## Related Business Rules



\* Table

\* Order



