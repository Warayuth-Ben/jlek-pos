\# Ordering Repositories



\## Purpose



Defines persistence contracts for Ordering aggregates.



Repositories load and save Aggregate Roots.



\---



\# Order Session Repository



Responsibilities



\* Find by ID

\* Find active sessions

\* Save session

\* Load session history



\---



Repositories never expose persistence details to the domain model.



