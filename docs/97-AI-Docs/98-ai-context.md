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

- Minimal API

Documentation

- Swagger

---

# Architecture Overview

Presentation (API)

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

Business Rules belong to Aggregate Roots.

Aggregates are responsible for protecting domain invariants.

---

# Infrastructure

Current implementation

- Repository Pattern
- EF Core Configuration
- PostgreSQL Migration

Infrastructure contains technical implementation only.

Business Rules do not belong here.

---

# Presentation

Current implementation

- Minimal API
- Swagger
- Request DTOs
- Response DTOs

Presentation contains HTTP concerns only.

Presentation is responsible for mapping Domain models to HTTP responses.

No Business Rules belong here.

---

# Development Principles

The following principles are mandatory.

- Never expose Domain Entities directly.
- Always return Response DTOs.
- Keep Domain pure.
- Application contains use cases only.
- Infrastructure contains technical implementation only.
- Presentation contains HTTP concerns only.
- Business Rules belong to Aggregate Roots.
- Handlers perform orchestration only.

---

# API Pattern

All HTTP endpoints should follow this pattern.

HTTP Request

↓

Request DTO

↓

Command / Query

↓

Handler

↓

Repository

↓

Aggregate Root

↓

Response DTO

↓

HTTP Response

This pattern is the standard API implementation pattern for JLek POS.

---

# Verified APIs

Verified

- POST /orders
- GET /orders/{id}
- POST /orders/{id}/items

Verified behavior

- POST returns HTTP 201 Created.
- GET by id returns HTTP 200 OK.
- Unknown id returns HTTP 404 Not Found.
- Add Item returns HTTP 200 OK.
- All endpoints verified through Swagger.
- Response DTOs are returned instead of Domain Entities.

---

# Current Technical Limitations

Known technical debt

High Priority

- Global Exception Middleware
- Validation Layer

Medium Priority

- GET /orders

Low Priority

- API Versioning
- Pagination
- Response mapping organization

---

# AI Mandatory Reading

Before performing implementation,

read

1. 00-start-here.md

2. 01-ai-engineering-standard.md

3. 02-ai-constitution.md

4. 03-ai-workflow.md

5. 04-ai-glossary.md

6. 99-project-status.md

---

# AI Reminder

Project documentation

is the Source of Truth.

Repository

is the Source of Evidence.

Human

is the Source of Decisions.