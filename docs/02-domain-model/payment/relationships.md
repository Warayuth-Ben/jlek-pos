\# Payment Relationships



\## Purpose



Defines relationships between Payment domain objects.



\---



\## Bill



1 → Many Payments



\---



\## Payment



1 → Many Refunds



\---



\## Bill



References one Order Session from the Ordering domain.



\---



Payment publishes events consumed by:



\* Table

\* Reporting



