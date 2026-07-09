\# Ordering Aggregates



\## Purpose



Defines consistency boundaries within the Ordering domain.



Aggregates protect business invariants.



\---



\# Order Session Aggregate



\## Aggregate Root



Order Session



\## Contains



\* Orders

\* Order Items



\## Business Invariants



\* Every Order belongs to exactly one Order Session.

\* Every Order belongs to one Service Channel.

\* Every Order contains at least one Order Item.

\* Closed Order Sessions cannot be modified.

\* Business history is immutable.



\---



\## Aggregate Responsibilities



\* Maintain transaction consistency.

\* Accept new orders.

\* Accept add-on orders.

\* Coordinate order lifecycle.

\* Publish domain events.



