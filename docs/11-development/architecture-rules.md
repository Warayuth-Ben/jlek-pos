\# Architecture Rules



\## Purpose



Define the mandatory architecture rules that every implementation must follow.



These rules protect the integrity of the architecture and prevent architectural drift.



\---



\# Layer Rules



UI



↓



Application



↓



Domain



↓



Infrastructure



Dependencies may only point inward.



\---



\# Mandatory Rules



\## Domain



The Domain Layer:



\- Must not depend on UI.

\- Must not depend on Database.

\- Must not depend on Frameworks.

\- Must not contain infrastructure code.



\---



\## Application



The Application Layer:



\- Coordinates use cases.

\- Calls Domain behavior.

\- Loads and saves Aggregates.

\- Does not contain business rules.



\---



\## Infrastructure



Infrastructure:



\- Implements interfaces.

\- Accesses external systems.

\- Never contains business logic.



\---



\## UI



The UI Layer:



\- Displays information.

\- Collects user input.

\- Never contains business rules.

\- Never accesses repositories directly.



\---



\# Aggregate Rules



\- Never modify Aggregate state externally.

\- Always use Aggregate methods.

\- Respect Aggregate boundaries.



\---



\# Domain Events



\- Publish after successful state changes.

\- Never publish before persistence.



\---



\# Repository Rules



Repositories:



Allowed



\- Load Aggregate

\- Save Aggregate



Not Allowed



\- Business Logic

\- Validation

\- Pricing

\- Workflow



\---



\# General Rules



Never duplicate Business Rules.



Never bypass the Application Layer.



Never allow Infrastructure to call UI.



Always keep dependencies pointing inward.

