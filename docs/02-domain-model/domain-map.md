\# Domain Map



\## Purpose



Defines the bounded contexts and aggregate boundaries of the restaurant POS domain.



This document is the primary reference for all domain modeling decisions.



\---



\# Domain Structure



```text

Restaurant POS



├── Shared

│

├── Ordering

│

├── Kitchen

│

├── Payment

│

└── Table

```



Each bounded context owns its own business rules, aggregates, entities, and services.



Contexts communicate through Domain Events.



\---



\# Shared Context



\## Responsibility



Provides reusable domain concepts shared by multiple contexts.



\### Contains



\* Value Objects

\* Common Domain Events

\* Shared Domain Services



\---



\# Ordering Context



\## Responsibility



Owns the customer ordering process.



\### Aggregate Root



Order Session



\### Contains



\* Order

\* Order Item



\### Responsibilities



\* Create Order

\* Confirm Order

\* Add-on Order

\* Cancel Order

\* Manage Order Lifecycle

\* Publish Order Events



Collaborates with:



\* Table

\* Kitchen

\* Payment



\---



\# Kitchen Context



\## Responsibility



Owns food preparation.



\### Aggregate Root



Kitchen Queue



\### Contains



\* Kitchen Ticket

\* Kitchen Item



\### Responsibilities



\* Receive confirmed orders

\* Manage kitchen queue

\* Track preparation

\* Complete kitchen work

\* Generate kitchen slips



Collaborates with:



\* Ordering



\---



\# Payment Context



\## Responsibility



Owns financial transactions.



\### Aggregate Root



Bill



\### Contains



\* Payment

\* Refund



\### Responsibilities



\* Calculate bill

\* Apply discounts

\* Accept payments

\* Generate receipts

\* Close bills



Collaborates with:



\* Ordering

\* Table



\---



\# Table Context



\## Responsibility



Owns restaurant seating.



\### Aggregate Root



Dining Table



\### Responsibilities



\* Assign tables

\* Transfer tables

\* Merge tables

\* Split tables

\* Maintain table status



Collaborates with:



\* Ordering

\* Payment



\---



\# Context Relationships



```text

&#x20;           +-----------+

&#x20;           |  Shared   |

&#x20;           +-----------+

&#x20;                 ▲

&#x20;                 │

&#x20;                 │

+---------+   +-----------+   +----------+

|  Table  |<->| Ordering |<->| Kitchen  |

+---------+   +-----------+   +----------+

&#x20;     ▲              │

&#x20;     │              ▼

&#x20;     │        +-----------+

&#x20;     └------->| Payment  |

&#x20;              +-----------+

```



Ordering is the central business context.



Other contexts collaborate through business events while maintaining clear ownership of their own business responsibilities.



\---



\# Aggregate Summary



| Context  | Aggregate Root |

| -------- | -------------- |

| Shared   | None           |

| Ordering | Order Session  |

| Kitchen  | Kitchen Queue  |

| Payment  | Bill           |

| Table    | Dining Table   |



These aggregate roots define the consistency boundaries of the system and serve as the primary units for business operations.



