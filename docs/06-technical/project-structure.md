\# Project Structure



\## Purpose



Defines the standard source code organization for the restaurant POS system.



The project structure promotes maintainability, scalability, and clear separation of responsibilities.



\---



\## Principles



\- Organize code by architectural layer.

\- Group related functionality by business context.

\- Keep modules cohesive.

\- Avoid circular dependencies.

\- Follow a consistent folder structure.



\---



\## Recommended Structure



```text

src/

│

├── presentation/

│

├── api/

│

├── application/

│

├── domain/

│

├── infrastructure/

│

├── shared/

│

└── bootstrap/

```



\---



\## Layer Responsibilities



\### presentation/



Contains user interface logic.



Examples:



\- Screens

\- Components

\- View Models



\---



\### api/



Contains API controllers and transport mapping.



Examples:



\- Controllers

\- Request DTOs

\- Response DTOs



\---



\### application/



Contains application operations.



Examples:



\- Ordering

\- Kitchen

\- Payment

\- Table



\---



\### domain/



Contains business logic.



Examples:



\- Aggregates

\- Entities

\- Value Objects

\- Domain Services

\- Domain Events



\---



\### infrastructure/



Contains technical implementations.



Examples:



\- Repository Implementations

\- Database Access

\- Logging

\- External Services



\---



\### shared/



Contains reusable utilities.



Examples:



\- Constants

\- Utilities

\- Shared Types



\---



\### bootstrap/



Application startup.



Examples:



\- Dependency Injection

\- Configuration

\- Application Initialization



\---



\## Rules



\- Every file belongs to exactly one layer.

\- Business logic never exists outside the Domain layer.

\- Infrastructure never contains business rules.

\- Shared code must remain framework-independent whenever possible.

