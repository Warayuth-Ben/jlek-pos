\# Repository Pattern



\## Purpose



Defines how Aggregates are persisted and retrieved.



Repositories provide an abstraction between the Domain Model and the persistence mechanism.



\---



\## Responsibilities



Repositories are responsible for:



\- Loading Aggregates

\- Persisting Aggregates

\- Removing Aggregates (when permitted)



Repositories do not contain business logic.



\---



\## Repository Principles



\- One repository per Aggregate Root.

\- Repositories return complete Aggregates.

\- Repositories never expose database implementation details.

\- Repositories never enforce business rules.



\---



\## Aggregate Ownership



Repositories operate only on Aggregate Roots.



Child Entities are persisted through their owning Aggregate.



\---



\## Persistence Rules



\- Persist Aggregate as a whole.

\- Maintain Aggregate consistency.

\- Preserve transactional integrity.



\---



\## Query Rules



Repositories may support business queries required by the Application Layer.



Complex reporting queries should be implemented separately.



\---



\## Naming Convention



Examples:



\- OrderSessionRepository

\- BillRepository

\- DiningTableRepository

\- KitchenTicketRepository



\---



\## Design Principles



\- Persistence Ignorance

\- Aggregate Consistency

\- Encapsulation

\- Separation of Concerns

