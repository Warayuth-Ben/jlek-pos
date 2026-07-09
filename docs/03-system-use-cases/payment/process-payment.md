\# Process Payment



\## Purpose



Records a customer's payment and applies it to the Bill.



\---



\## Goal



Receive payment and reduce the outstanding balance of the Bill.



\---



\## Primary Actor



\- Restaurant Staff



\---



\## Trigger



The customer pays the Bill.



\---



\## Preconditions



\- The Bill exists.

\- The Bill is open.

\- A payable balance remains.



\---



\## Input



\- Bill ID

\- Payment Method

\- Payment Amount



\---



\## Main Flow



1\. Staff selects the Bill.

2\. System loads the Bill.

3\. Staff selects the payment method.

4\. Staff enters the payment amount.

5\. System validates the payment.

6\. System records the Payment.

7\. System updates the Bill balance.



\---



\## Alternative Flows



\### Partial Payment



The customer pays only part of the outstanding balance.



\### Multiple Payments



The customer uses multiple payment methods.



\### Exact Payment



The Bill becomes fully paid.



\---



\## Business Rules Applied



\- Payment Processing

\- Payment Method

\- Bill Settlement



\---



\## Domain Interaction



\### Aggregate



\- Bill



\### Entities



\- Bill

\- Payment



\### Value Objects



\- Money



\---



\## Application Responsibilities



The Application Layer is responsible for:



\- Load the Bill.

\- Validate payment information.

\- Record the Payment.

\- Update the Bill balance.

\- Persist the Aggregate.

\- Publish the Payment Completed event.



\---



\## Domain Events



\- Payment Completed



\---



\## Output



\- Payment recorded.

\- Bill balance updated.



\---



\## Failure Conditions



\- Bill not found.

\- Bill already closed.

\- Invalid payment amount.

\- Unsupported payment method.



\---



\## Notes



A Bill may contain multiple Payments until the outstanding balance reaches zero.

