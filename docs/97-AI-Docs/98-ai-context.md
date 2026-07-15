# AI Context

Version: 1.1

Project: JLek POS

---

# Purpose

This document contains long-lived project knowledge.

The information in this document changes infrequently and should be considered stable context for every AI participating in the project.

This document is NOT intended to track development progress.

Current progress is maintained in

99-project-status.md

---

# System Overview

Project Name

JLek POS

System Type

Restaurant Point of Sale (POS)

Architecture

Clean Architecture

Design Methodology

Domain-Driven Design (DDD)

Application Pattern

CQRS

---

# Technology Stack

Framework

- .NET 8

Database

- PostgreSQL 17

ORM

- EF Core

API

- ASP.NET Core Minimal API

Documentation

- Swagger (OpenAPI)

---

# Architecture Overview

Presentation

↓

Application

↓

Domain

↓

Infrastructure

Dependencies always point inward.

The Domain Layer must remain independent.

---

# Domain Model

Current implemented concepts

- Aggregate Root
- Entity
- Value Objects
- Strongly Typed IDs
- Domain Events
- Business Rules

Current Aggregate

- Order
- OrderItem

---

# Application Layer

Current implementation

Commands

- CreateOrder
- AddItem
- RemoveItem
- ConfirmOrder
- CompleteOrder

Queries

- GetOrderById
- GetOrders

CQRS is used to separate commands from queries.

---

# Infrastructure

Current implementation

- Repository Pattern
- EF Core Configuration
- PostgreSQL
- Aggregate Loading
- Dependency Injection

Repositories returning Aggregate Roots must load all state required by Business Rules.

---

# Presentation

Current implementation

- Minimal API
- Swagger
- Response DTO
- DTO Mapping

Presentation contains HTTP concerns only.

Business Rules belong exclusively to the Domain Layer.

Domain Entities must never be returned directly.

---

# Development Principles

The following principles are mandatory.

- Never expose Domain Entities directly.
- Always return DTOs.
- Keep Domain pure.
- Application contains use cases only.
- Infrastructure contains technical implementation only.
- Presentation contains HTTP concerns only.
- Business Rules remain inside Domain.

---

# Verified APIs

Verified

POST /orders

GET /orders

GET /orders/{id}

POST /orders/{id}/items

DELETE /orders/{id}/items/{itemId}

POST /orders/{id}/confirm

POST /orders/{id}/complete

Current behavior

- Returns Response DTOs
- Successfully persists data into PostgreSQL
- Preserves Clean Architecture
- Preserves CQRS
- Preserves DDD

---

# Current Technical Limitations

Current implementation intentionally defers

- OrderResponse currently exposes only Order-level information.
- OrderItemResponse has not yet been implemented.
- Global Exception Handling has not yet been implemented.
- ProblemDetails responses have not yet been implemented.

These limitations are intentional and belong to the next milestone.

---

# Frozen Milestone

Order API v1

Status

Frozen

Business Rules

Frozen

Architecture

Frozen

API Contracts

Frozen

Bug fixes remain allowed.

Enhancements require a new milestone.

---

# AI Mandatory Reading

Before performing implementation,

read

1. 00-start-here.md

2. 01-ai-engineering-standard.md

3. 02-ai-constitution.md

4. 03-ai-workflow.md

5. 04-ai-glossary.md

6. 05-ai-prompts.md

7. 99-project-status.md

---

# AI Reminder

Project documentation

is the Source of Truth.

Repository

is the Source of Evidence.

Human

is the Source of Decisions.

---
# Frozen Domain Decisions

## Menu Module Aggregate Boundary (Verified)

This aggregate boundary has been verified through Business Rules documentation and repository evidence.

| Concept | Classification |
|----------|----------------|
| Product | Aggregate Root |
| Product Category | Aggregate Root |
| Ingredient | Aggregate Root |
| Option Group | Entity (inside Product) |
| Option | Entity (inside Option Group) |
| Modifier | Entity (inside Product) |
| Suggested Price | Value Object |
| Availability | Value Object |
| Selection Rule | Value Object (inside Option Group) |

## Notes

- This decision is frozen for Menu Module v1.
- Future business requirements may require revisiting aggregate boundaries.
- AI should reuse this verified decision during onboarding instead of re-analyzing it unless Business Rules or repository evidence changes.
---
# Frozen Architecture Decisions

- Repository Contract
- CQRS
- Application Flow
- API Contract
--------