\# Relationships



\## Purpose



Defines relationship principles between database tables.



Relationships preserve data integrity and reflect the ownership defined in the Domain Model.



\---



\## Relationship Types



The database supports:



\- One-to-One

\- One-to-Many

\- Many-to-Many (via junction tables)



\---



\## Relationship Principles



\- Relationships must reflect Aggregate boundaries.

\- Child records always belong to a parent record.

\- Foreign Keys must preserve referential integrity.

\- Relationships must not violate Aggregate ownership.



\---



\## Aggregate Mapping



Each Aggregate Root owns its related data.



Examples:



\- Order Session owns Orders.

\- Order owns Order Items.

\- Bill owns Payments.

\- Kitchen Ticket owns Kitchen Items.



\---



\## Cascade Rules



Cascade operations should be used carefully.



Recommended:



\- Cascade Update: Allowed

\- Cascade Delete: Avoid unless explicitly required



\---



\## Referential Integrity



\- Every Foreign Key must reference a valid Primary Key.

\- Orphan records are not permitted.

\- Broken relationships must be prevented by database constraints.



\---



\## Design Principles



\- Explicit ownership

\- Referential integrity

\- Aggregate consistency

