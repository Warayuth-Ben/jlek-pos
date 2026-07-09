\# System Architecture



\## Purpose



Defines the overall software architecture of the restaurant POS system.



\---



\## Architecture Style



The system follows Domain-Driven Design (DDD) with Clean Architecture principles.



\---



\## Architecture Layers



```text

Presentation



↓



API



↓



Application



↓



Domain



↓



Infrastructure



↓



Database

```



\---



\## Layer Responsibilities



\### Presentation



\- User Interface

\- User Interaction

\- Input Collection



\---



\### API



\- HTTP Endpoints

\- Request Mapping

\- Response Mapping



\---



\### Application



\- Coordinate use cases

\- Invoke Domain operations

\- Return application results



\---



\### Domain



\- Business Rules

\- Aggregates

\- Entities

\- Value Objects

\- Domain Services

\- Domain Events



\---



\### Infrastructure



\- Database

\- Repository Implementations

\- Logging

\- Configuration

\- External Services



\---



\### Database



\- Persistent Storage



\---



\## Dependency Direction



```text

Presentation

&#x20;       │

&#x20;       ▼

API

&#x20;       │

&#x20;       ▼

Application

&#x20;       │

&#x20;       ▼

Domain



Infrastructure ─────▶ Domain



Infrastructure ─────▶ Application

```



\---



\## Architectural Principles



\- Dependencies point inward.

\- Domain has no infrastructure dependency.

\- Business rules remain independent of frameworks.

\- Infrastructure can be replaced without changing business logic.

