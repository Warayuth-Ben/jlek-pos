\# Shared Value Objects



\## Purpose



Defines immutable business values shared across multiple domains.



Value Objects have no identity. Two Value Objects with the same values are considered equal.



\---



\# Money



\## Description



Represents a monetary value.



Money is used for all financial values throughout the system, including unit prices, totals, payments, discounts, and refunds.



\## Business Rules



\* Immutable

\* Cannot be partially modified

\* Supports arithmetic operations

\* Currency must remain consistent during calculations



Used by:



\* Ordering

\* Payment



\---



\# Quantity



\## Description



Represents the quantity of an order item.



\## Business Rules



\* Must be greater than zero

\* Immutable

\* Supports comparison and arithmetic operations



Used by:



\* Ordering

\* Kitchen



\---



\# ServiceChannel



\## Description



Represents how the customer receives an order.



\## Supported Values



\* Dine-in

\* Takeaway

\* Delivery



\## Business Rules



\* Every Order belongs to exactly one ServiceChannel.

\* ServiceChannel is immutable after Order confirmation.



Used by:



\* Ordering

\* Kitchen

\* Payment



\---



\# Timestamp



\## Description



Represents a business point in time.



Examples include:



\* Order Created

\* Order Confirmed

\* Kitchen Started

\* Payment Completed

\* Bill Closed



Used by every domain.



