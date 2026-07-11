# AI Context

Current milestone:

Backend Foundation completed successfully.

Current technology stack

- .NET 8
- Clean Architecture
- CQRS
- EF Core
- PostgreSQL 17
- Minimal API
- Swagger

Implemented

- Aggregate Root
- Entity
- ValueObject
- Strongly Typed IDs
- Repository
- Domain Events
- Business Rules
- EF Core Configuration
- PostgreSQL Migration

Verified

POST /orders

returns

201 Created

and saves data into PostgreSQL successfully.

Current limitations

API currently returns Domain Entity directly.

DomainEvents are serialized to the client.

Need DTO layer before expanding APIs.

Next priorities

1.
Create Response DTOs

2.
Remove DomainEvents from API responses

3.
GET /orders

4.
GET /orders/{id}

5.
Add Item

6.
Confirm Order

7.
Complete Order

8.
Menu Module

9.
Table Module

10.
Kitchen Queue

Development rules

Never expose Domain Entity directly.

Always return DTOs.

Keep Domain pure.

Application contains use cases.

Infrastructure contains EF Core only.

Presentation contains HTTP only.