\# Shared Domain Events



\## Purpose



Defines business events exchanged between bounded contexts.



Domain Events describe facts that have already occurred.



They are immutable.



\---



\# Ordering Events



\* Order Created

\* Order Confirmed

\* Order Cancelled

\* Order Completed

\* Add-on Order Created



\---



\# Kitchen Events



\* Kitchen Ticket Created

\* Kitchen Preparation Started

\* Kitchen Preparation Completed

\* Food Served



\---



\# Payment Events



\* Bill Calculated

\* Payment Completed

\* Refund Completed

\* Bill Closed



\---



\# Table Events



\* Table Assigned

\* Table Transferred

\* Tables Merged

\* Tables Split

\* Table Released



\---



\## Principles



\* Events are immutable.

\* Events describe completed business actions.

\* Events enable communication between bounded contexts.

\* Events never modify business state directly.



