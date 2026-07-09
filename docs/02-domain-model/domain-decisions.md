\# Ubiquitous Language



\## Purpose



Defines the official business terminology used throughout the restaurant POS system.



Every document, source code, database schema, API, and user interface should use these terms consistently.



Each business concept has exactly one preferred name.



\---



\# Core Business Terms



\## Order Session



A complete customer dining or purchasing session.



An Order Session groups all orders created during the same customer visit or transaction.



Examples:



\* Customer orders once.

\* Customer places multiple add-on orders.

\* Customer pays once at the end.



An Order Session is the business owner of the complete customer transaction.



\---



\## Order



A single ordering action created by the customer.



An Order belongs to exactly one Order Session.



Examples:



\* Initial order

\* Add-on order



Each Order is confirmed independently.



\---



\## Order Item



A single menu item requested by the customer.



Each Order Item belongs to exactly one Order.



Order Items contain:



\* Menu Item

\* Quantity

\* Options

\* Notes

\* Unit Price



\---



\## Menu Item



A product that can be ordered.



Examples:



\* Chicken Rice

\* Crispy Chicken

\* Drink



Menu Items are maintained independently from customer orders.



\---



\## Service Channel



The method through which an order is served.



Supported channels include:



\* Dine-in

\* Takeaway

\* Delivery



Each Order belongs to exactly one Service Channel.



\---



\## Dining Table



A physical restaurant table.



Dining Tables are used only for dine-in operations.



A table may host one active Order Session at a time.



\---



\## Kitchen Ticket



A business document sent to the kitchen.



A Kitchen Ticket contains only the items that require preparation.



Multiple Kitchen Tickets may exist within a single Order Session.



\---



\## Kitchen Queue



The operational queue used by the kitchen.



Confirmed Order Items enter the Kitchen Queue for preparation.



The Kitchen Queue reflects the current preparation workload.



\---



\## Bill



The financial summary of an Order Session.



A Bill includes every billable Order belonging to the same Order Session.



Each Order Session has at most one active Bill.



\---



\## Payment



A financial transaction used to settle a Bill.



A Bill may contain one or more Payments.



Examples:



\* Cash

\* QR Payment

\* Credit Card



\---



\## Refund



A financial transaction that returns money after a completed payment.



A Refund never replaces or deletes the original Payment.



\---



\# Business Events



Business Events describe facts that have already occurred.



Examples include:



\* Order Created

\* Order Confirmed

\* Kitchen Started

\* Kitchen Completed

\* Payment Completed

\* Bill Closed



Business Events are immutable.



\---



\# Naming Principles



\* One business concept has one official name.

\* The same term must be used consistently across all documentation.

\* Source code should follow the same terminology whenever possible.

\* Database objects should align with the business language.

\* Alternative names and synonyms should be avoided.



