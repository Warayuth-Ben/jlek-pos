\# Application Layer Overview



\## Purpose



The Application Layer coordinates business use cases.



It orchestrates domain operations while keeping business rules inside the Domain Model.



The Application Layer does not contain business rules.



\---



\## Responsibilities



The Application Layer is responsible for:



\- Coordinating System Use Cases

\- Validating application inputs

\- Loading Aggregates

\- Invoking Domain behavior

\- Persisting Aggregate changes

\- Publishing Domain Events

\- Returning application results



\---



\## What the Application Layer Does Not Do



The Application Layer does not:



\- Implement business rules

\- Store business state

\- Execute database queries directly

\- Implement presentation logic



\---



\## Processing Flow



```text

System Use Case



↓



Application Handler



↓



Load Aggregate



↓



Execute Domain Logic



↓



Persist Changes



↓



Publish Domain Events



↓



Return Result

```



\---



\## Relationship to Other Documentation



Business Rules



↓



Domain Model



↓



System Use Cases



↓



State Machines



↓



Application Layer



↓



Technical Implementation



\---



\## Design Principles



\- One System Use Case maps to one Application Handler.

\- Handlers coordinate business operations.

\- Business rules belong to the Domain Layer.

\- Application logic remains thin.

\- Handlers are independent of UI, API and database technologies.

