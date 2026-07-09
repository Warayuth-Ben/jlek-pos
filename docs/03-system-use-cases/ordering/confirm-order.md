\# Confirm Order



\## Purpose



Confirms a Draft Order and makes it ready for operational processing.



Confirmation represents the business commitment to fulfill the customer's order.



\---



\## Goal



Transition a Draft Order to Confirmed status.



\---



\## Primary Actor



\- Restaurant Staff



\---



\## Trigger



Staff confirms the customer's order.



\---



\## Preconditions



\- The Order exists.

\- The Order is in Draft status.

\- The Order contains at least one Order Item.



\---



\## Input



\- Order ID



\---



\## Main Flow



1\. Staff reviews the Draft Order.

2\. The system validates the Order.

3\. The system changes the Order status to Confirmed.

4\. The system publishes an Order Confirmed event.

5\. Kitchen processing becomes available.



\---



\## Alternative Flows



\### Validation Failed



The system rejects confirmation until all validation errors are resolved.



\---



\## Business Rules Applied



\- Order Confirmation

\- Order Lifecycle



\---



\## Domain Interaction



\### Aggregate



\- Order Session



\### Entities



\- Order



\---



\## Domain Events



\- Order Confirmed



\---



\## Output



\- Confirmed Order

\- Order Session updated

\- Kitchen processing enabled



\---



\## Failure Conditions



\- Order not found

\- Order already confirmed

\- Order cancelled

\- Empty Order



\---



\## Notes



Confirmation freezes the business content of the Order.



Subsequent customer requests are handled as Add-on Orders rather than modifying the confirmed Order.

