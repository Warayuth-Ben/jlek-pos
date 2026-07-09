\# Payment Entities



\## Purpose



Defines entities owned by the Payment domain.



\---



\# Bill



\## Description



Represents the financial summary of an Order Session.



\## Responsibilities



\* Calculate totals

\* Apply discounts

\* Track payment status

\* Coordinate bill closure



\## Identity



\* Bill ID



\---



\# Payment



\## Description



Represents a completed payment transaction.



\## Responsibilities



\* Record payment amount

\* Record payment method

\* Record payment timestamp



\## Identity



\* Payment ID



\---



\# Refund



\## Description



Represents a refund issued after a completed payment.



\## Responsibilities



\* Record refund amount

\* Link to the original payment

\* Preserve refund history



\## Identity



\* Refund ID



