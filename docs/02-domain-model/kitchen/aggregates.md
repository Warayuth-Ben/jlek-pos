\# Kitchen Aggregates



\## Purpose



Defines consistency boundaries within the Kitchen domain.



\---



\# Kitchen Ticket Aggregate



\## Aggregate Root



Kitchen Ticket



\## Contains



\* Kitchen Items



\---



\## Business Invariants



\* Every Kitchen Ticket contains at least one Kitchen Item.

\* Every Kitchen Item belongs to exactly one Kitchen Ticket.

\* Only confirmed Order Items can create Kitchen Items.

\* Completed Kitchen Tickets become immutable.



