\# Payment Constraints



\## Purpose



Defines business rules that must always remain true within the Payment domain.



\---



\# Bill



\* References one Order Session.

\* Cannot be modified after closure.



\---



\# Payment



\* Belongs to one Bill.

\* Amount must be greater than zero.



\---



\# Refund



\* References one completed Payment.

\* Cannot exceed the paid amount.

\* Refund history is immutable.



