\# Refund Payment



\## Purpose



Records a refund for a previously completed Payment.



\---



\## Goal



Return money to the customer while preserving payment history.



\---



\## Primary Actor



\- Restaurant Staff



\---



\## Trigger



An approved refund is requested.



\---



\## Preconditions



\- The Payment exists.

\- The Payment has been completed.

\- Refund is permitted by business rules.



\---



\## Input



\- Payment ID

\- Refund Amount

\- Refund Reason



\---



\## Main Flow



1\. Staff selects the completed Payment.

2\. System loads the Payment.

3\. Staff enters the refund information.

4\. System validates the refund.

5\. System records the Refund.

6\. System updates the Bill.



\---



\## Alternative Flows



\### Partial Refund



Only part of the Payment is refunded.



\### Full Refund



The complete Payment is refunded.



\---



\## Business Rules Applied



\- Refund Policy

\- Payment Policy



\---



\## Domain Interaction



\### Aggregate



\- Bill



\### Entities



\- Payment

\- Refund



\### Value Objects



\- Money



\---



\## Application Responsibilities



The Application Layer is responsible for:



\- Load the Bill.

\- Locate the Payment.

\- Validate the refund.

\- Record the Refund.

\- Persist the Aggregate.

\- Publish the Refund Completed event.



\---



\## Domain Events



\- Refund Completed



\---



\## Output



\- Refund recorded.

\- Bill updated.



\---



\## Failure Conditions



\- Payment not found.

\- Refund exceeds payment amount.

\- Refund not permitted.



\---



\## Notes



Refunds never delete the original Payment.

