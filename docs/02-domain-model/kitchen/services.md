\# Kitchen Domain Services



\## Purpose



Defines business operations within the Kitchen domain.



\---



\# Kitchen Dispatch Service



\## Responsibilities



\* Receive confirmed Order Items

\* Create Kitchen Tickets

\* Assign preparation priority



\---



\# Preparation Service



\## Responsibilities



\* Start preparation

\* Complete preparation

\* Update preparation status



\---



\## Principles



\* Services operate on Kitchen Ticket Aggregates.

\* Services never modify Ordering Aggregates directly.

\* Services preserve business consistency.



