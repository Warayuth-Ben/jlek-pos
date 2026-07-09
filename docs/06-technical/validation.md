\# Validation



\## Purpose



Defines validation responsibilities across the system.



\---



\## Validation Layers



\- Presentation Validation

\- API Validation

\- Application Validation

\- Domain Validation



\---



\## Presentation Validation



Responsibilities



\- Required fields

\- Input formatting

\- Basic user feedback



Presentation validation improves usability only.



\---



\## API Validation



Responsibilities



\- Request schema

\- Data type validation

\- Required properties



\---



\## Application Validation



Responsibilities



\- Authorization

\- Resource existence

\- Business workflow preconditions



Examples



\- Order Session exists.

\- Dining Table exists.

\- Bill exists.



\---



\## Domain Validation



Responsibilities



\- Business Rules

\- Aggregate invariants

\- State transition validation



Examples



\- Draft Order cannot be completed.

\- Closed Bill cannot accept payments.

\- Occupied Table cannot be assigned again.



\---



\## Validation Principles



\- Validate as early as practical.

\- Business Rules belong only to the Domain.

\- Never duplicate business validation outside the Domain.

\- Validation failures must never partially modify the system.



\---



\## Validation Result



Validation returns either:



\- Success

\- Validation Error

