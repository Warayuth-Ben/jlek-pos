\# Kitchen Constraints



\## Purpose



Defines business rules that must always remain true within the Kitchen domain.



\---



\# Kitchen Ticket



\* Must contain at least one Kitchen Item.

\* Cannot be created from an unconfirmed Order.

\* Becomes immutable after completion.



\---



\# Kitchen Item



\* References exactly one confirmed Order Item.

\* Cannot belong to multiple Kitchen Tickets.

\* Cannot return to a previous preparation state after completion.



