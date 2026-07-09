\# Bill Calculation



\## Purpose



Defines the business rules for calculating the amount payable by the customer.



\---



\## Business Rules



\### 1. Every bill has a calculated total.



The total is derived from all active order items.



\---



\### 2. Cancelled items are excluded from bill calculation.



Only billable items contribute to the final amount.



\---



\### 3. Business adjustments are applied according to business policy.



Examples include service charges, taxes, or discounts.



\---



\### 4. The calculated amount must be finalized before payment is accepted.



