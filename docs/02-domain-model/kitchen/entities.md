\# Kitchen Entities



\## Purpose



Defines the entities owned by the Kitchen domain.



\---



\# Kitchen Ticket



\## Description



Represents a kitchen preparation request created from a confirmed order.



A Kitchen Ticket groups one or more Kitchen Items that should be prepared together.



\## Responsibilities



\* Own Kitchen Items

\* Maintain preparation lifecycle

\* Preserve preparation history

\* Support kitchen printing



\## Identity



\* Kitchen Ticket ID



\---



\# Kitchen Item



\## Description



Represents a single order item assigned to the kitchen for preparation.



Each Kitchen Item originates from one confirmed Order Item.



\## Responsibilities



\* Track preparation status

\* Preserve preparation history

\* Record preparation timestamps



\## Identity



\* Kitchen Item ID



