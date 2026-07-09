\# Ordering Constraints



\## Purpose



Defines business rules that must always remain true within the Ordering domain.



\---



\# Order Session



\* Must have a unique identity.

\* Owns all Orders in the customer transaction.

\* Cannot be modified after the Bill is closed.



\---



\# Order



\* Belongs to one Order Session.

\* Must contain at least one Order Item.

\* Must belong to one Service Channel.



\---



\# Order Item



\* Quantity must be greater than zero.

\* Price is fixed after confirmation.

\* Notes and options belong to the Order Item.

\* An Order Item belongs to exactly one Order.



