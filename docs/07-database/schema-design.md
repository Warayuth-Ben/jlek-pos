\# Schema Design



\## Purpose



Defines standards for designing database schemas.



\---



\## Naming



\- Table names use singular nouns.

\- Primary Keys use the suffix "Id".

\- Foreign Keys reference the related Primary Key.

\- Use consistent naming throughout the database.



\---



\## Table Design



Each table represents one business concept.



Tables should avoid storing duplicated information.



\---



\## Primary Keys



Every table must define one Primary Key.



Primary Keys are immutable.



\---



\## Foreign Keys



Foreign Keys enforce relationships between business entities.



\---



\## Audit Columns



Every transactional table should contain:



\- CreatedAt

\- UpdatedAt



Optional:



\- CreatedBy

\- UpdatedBy



\---



\## Soft Delete



Soft Delete should be used only when required by business requirements.



\---



\## Normalization



The database should remain normalized unless denormalization provides measurable business value.



\---



\## Design Principles



\- Simplicity

\- Consistency

\- Maintainability

\- Data Integrity

