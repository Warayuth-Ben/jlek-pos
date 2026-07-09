\# Ordering Relationships



\## Purpose



Defines relationships between Ordering domain objects.



\---



\## Order Session



1 → Many Orders



\---



\## Order



1 → Many Order Items



\---



\## Order



1 → One Service Channel



\---



\## Order Session



1 → One Bill (Payment Context)



\---



\## Order



Publishes Domain Events consumed by:



\* Kitchen

\* Payment

\* Table



