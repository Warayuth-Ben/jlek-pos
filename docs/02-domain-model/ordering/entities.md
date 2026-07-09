\# Ordering Entities



\## Purpose



Defines the entities owned by the Ordering domain.



Entities have a unique identity and encapsulate business behavior throughout their lifecycle.



The Ordering domain owns the customer ordering process from the creation of the first order until the Order Session is completed.



\---



\# Order Session



\## Description



Represents a complete customer transaction.



An Order Session begins when the first order is created and ends when the associated Bill is closed.



An Order Session is the Aggregate Root of the Ordering domain.



\## Responsibilities



\* Own all Orders within the customer transaction

\* Maintain the Order Session lifecycle

\* Accept new Orders

\* Accept Add-on Orders

\* Publish domain events

\* Coordinate the ordering process



\## Identity



\* Order Session ID



\---



\# Order



\## Description



Represents a single ordering action performed by the customer.



Examples include:



\* Initial Order

\* Add-on Order



An Order always belongs to exactly one Order Session.



\## Responsibilities



\* Own Order Items

\* Maintain the Order lifecycle

\* Record confirmation status

\* Publish Order-related events



\## Identity



\* Order ID



\---



\# Order Item



\## Description



Represents a single menu item requested by the customer.



Each Order Item belongs to exactly one Order.



\## Responsibilities



\* Store the selected Menu Item

\* Store Quantity

\* Store Unit Price

\* Store selected Options

\* Store customer Notes

\* Maintain preparation status



\## Identity



\* Order Item ID



\## Value Objects



\* Quantity

\* Money (Unit Price)



\---



\# Entity Relationships



Order Session



\* Owns one or more Orders.



Order



\* Belongs to one Order Session.

\* Owns one or more Order Items.



Order Item



\* Belongs to one Order.

\* References one Menu Item.



