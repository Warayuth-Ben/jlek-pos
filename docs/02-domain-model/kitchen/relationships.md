\# Kitchen Relationships



\## Purpose



Defines relationships between Kitchen domain objects.



\---



\## Kitchen Ticket



1 → Many Kitchen Items



\---



\## Kitchen Item



References exactly one confirmed Order Item from the Ordering domain.



\---



Kitchen publishes domain events consumed by:



\* Payment Domain

\* Reporting



