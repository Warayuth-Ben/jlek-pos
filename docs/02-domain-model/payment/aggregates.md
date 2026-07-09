\# Payment Aggregates



\## Purpose



Defines aggregate boundaries within the Payment domain.



\---



\# Bill Aggregate



\## Aggregate Root



Bill



\## Contains



\* Payments

\* Refunds



\---



\## Business Invariants



\* One Bill belongs to one Order Session.

\* A Bill may contain multiple Payments.

\* Refunds reference completed Payments.

\* Closed Bills cannot be modified.



