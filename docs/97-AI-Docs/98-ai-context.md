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

Current Aggregates

Order Context

- Order
- OrderItem

Catalog Context

- Product
- ProductCategory
- Ingredient

Owned Entities

- OptionGroup
- Option
- Modifier

---

# Application Layer

Current implementation

Order Module

- CQRS complete

Catalog Module

- CQRS complete

Application layer uses

- Commands
- Queries
- Command Handlers
- Query Handlers

Repositories define all aggregate access.

Application never accesses DbContext directly.

Queries

- GetOrderById
- GetOrders

CQRS is used to separate commands from queries.

---

# Infrastructure

Current implementation

- Repository Pattern
- Repository Contracts
- Repository Implementations
- EF Core Configuration
- Strongly Typed ID Converters
- Aggregate Loading
- PostgreSQL
- Dependency Injection

Repositories return fully loaded aggregate roots required by business rules.

---

# Presentation

Current implementation

- Minimal API
- Swagger
- Request DTOs
- Response DTOs
- DTO Mapping

Implemented Modules

- Order Module
- Catalog Module

Presentation contains HTTP concerns only.

Business Rules belong exclusively to the Domain Layer.

Domain entities must never be returned directly.

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

Verified Catalog APIs

Products

POST /products
GET /products
GET /products/{id}
PUT /products/{id}/details
PUT /products/{id}/category
PUT /products/{id}/availability
PUT /products/{id}/visibility
POST /products/{id}/suggested-prices
DELETE /products/{id}/suggested-prices
POST /products/{id}/option-groups
DELETE /products/{id}/option-groups/{optionGroupId}
POST /products/{id}/modifiers
DELETE /products/{id}/modifiers/{modifierId}
POST /products/{id}/ingredients
DELETE /products/{id}/ingredients/{ingredientId}

Categories

POST /categories
GET /categories
GET /categories/{id}
PUT /categories/{id}/rename
PUT /categories/{id}/reorder
POST /categories/{id}/hide
POST /categories/{id}/show

Ingredients

POST /ingredients
GET /ingredients
GET /ingredients/{id}
PUT /ingredients/{id}/rename
PUT /ingredients/{id}/availability

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

| Concept          | Classification                     |
| ---------------- | ---------------------------------- |
| Product          | Aggregate Root                     |
| Product Category | Aggregate Root                     |
| Ingredient       | Aggregate Root                     |
| Option Group     | Entity (inside Product)            |
| Option           | Entity (inside Option Group)       |
| Modifier         | Entity (inside Product)            |
| Suggested Price  | Value Object                       |
| Availability     | Value Object                       |
| Selection Rule   | Value Object (inside Option Group) |

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

---

# Reuse Guidance

The Order Module and Menu Module are now the reference implementations for future development.

Future bounded contexts should reuse the same implementation pattern.

Reference order

1. Domain
2. Repository Contract
3. Repository Implementation
4. EF Core Configuration
5. CQRS
6. Minimal API
7. Documentation

---

# Frozen Menu Module v1

Status

Frozen

## Verified Architecture

The following implementation has been completed and verified.

- Domain
- Infrastructure
- Application
- API

## Frozen Components

### Domain

- Product Aggregate
- ProductCategory Aggregate
- Ingredient Aggregate
- Aggregate Boundaries
- Business Rules
- Domain Events
- Strongly Typed IDs
- Value Objects

### Infrastructure

- Repository Contracts
- Repository Implementations
- EF Core Configurations
- Strongly Typed ID Converters
- Aggregate Loading
- ApplicationDbContext

### Application

- CQRS
- Commands
- Queries
- Command Handlers
- Query Handlers
- Response DTO Mapping

### Presentation

- Minimal API
- Catalog Endpoints
- Request DTOs
- Response DTOs
- Swagger Integration

## AI Guidance

Future AI implementations should reuse the existing Order Module and Menu Module patterns.

Do not redesign

- Aggregate Boundaries
- Repository Contracts
- CQRS
- Persistence
- API Contracts

unless new verified business requirements require architectural changes.

---

# Reference Implementations

The following modules are considered reference implementations.

Future modules should follow these implementations unless verified business requirements require architectural changes.

## Order Module

Reference for

- Aggregate Root
- Business Rules
- Domain Events
- Repository Pattern
- CQRS
- Response DTO Mapping
- Minimal API
- EF Core Configuration

## Menu Module

Reference for

- Aggregate Composition
- Owned Entities
- Aggregate References by ID
- Strongly Typed IDs
- Repository Contracts
- Repository Implementations
- EF Core Configurations
- Value Converters
- CQRS
- Minimal API
- Swagger

---

# Coding Standards

The following implementation conventions are frozen.

## Domain

- AggregateRoot<TId>
- Entity<TId>
- ValueObject
- Strongly Typed IDs
- Static Create() factories
- Private EF Core constructors
- RaiseDomainEvent()
- CheckRule()

## Infrastructure

- One repository per Aggregate Root
- One SaveChangesAsync() per repository method
- Aggregate loading via Include()/ThenInclude()
- ApplyConfigurationsFromAssembly()

## Application

- One handler per command/query
- Repository access only
- No DbContext usage
- Response DTOs only

## Presentation

- Minimal API
- MapGroup()
- Results.Ok()
- Results.Created()
- Results.NotFound()
- Request DTOs
- Response DTOs

---

# AI Implementation Rules

Future implementations should follow this order.

1. Business Rules
2. Domain
3. Repository Contracts
4. Infrastructure
5. CQRS
6. API
7. Testing
8. Documentation

Do not skip steps.

Do not redesign completed modules.

Reuse verified implementations whenever possible.

---

# Human Review Policy

Every implementation milestone must

- Build successfully
- Perform Self Review
- Receive Human Approval

before continuing to the next milestone.

---

# Git Workflow

Before implementation

- Create Git checkpoint

After implementation

- Build
- Self Review
- Human Review
- Update documentation
- Commit

---

# Project Development Pattern

Every business module follows the same lifecycle.

Business Rules

↓

Architecture Design

↓

Aggregate Boundary

↓

Repository Contract

↓

CQRS Design

↓

Application Flow

↓

API Contract

↓

Domain Implementation

↓

Infrastructure Implementation

↓

Application Implementation

↓

API Implementation

↓

Testing

↓

Freeze

---

# Bounded Contexts

Current

✅ Ordering

- Order
- OrderItem

✅ Catalog

- Product
- ProductCategory
- Ingredient

Planned

- Table
- Kitchen
- Payment
- Reporting

Rules

- Each bounded context owns its Aggregate Roots.
- Cross-context communication is by identity only.
- Business Rules never cross Aggregate boundaries.

---

# Module Dependencies

Catalog
↓
Ordering
↓
Kitchen
↓
Payment
↓
Reporting

Table interacts with Ordering.

Reporting consumes data from every module.

Dependencies always point downward.

Future modules must preserve this dependency direction.

---

# Aggregate Rules

Every Aggregate Root

- owns its entities
- enforces its own invariants
- is loaded completely before modification
- is persisted through its repository only

Never

- modify child entities directly
- bypass Aggregate Root
- expose internal collections
- reference another Aggregate by navigation property

Cross-aggregate references use IDs only.

---

# Implementation Checklist

Before implementation

☐ Read AI documentation

☐ Verify repository evidence

☐ Review frozen decisions

☐ Create Git checkpoint

Implementation

☐ Build

☐ Self Review

☐ Human Review

☐ Update AI Context

☐ Update Project Status

☐ Commit

☐ Push

---

# Freeze Policy

Frozen artifacts must not be redesigned.

Allowed

- Bug Fixes
- Security Fixes
- Performance Improvements

Not Allowed

- Aggregate redesign
- CQRS redesign
- Repository redesign
- API redesign

Architectural changes require

- Verified Business Rules
- Human Approval
- New Milestone

---

# Project Philosophy

Business Rules

↓

Architecture

↓

Implementation

↓

Testing

↓

Freeze

↓

Reuse

Never implement first and design later.

Business Rules are the source of architecture.

Architecture is the source of implementation.

Implementation is the source of evidence.

Documentation is the source of long-term knowledge.

---

# Naming Conventions

Domain

Aggregate Root

- Order
- Product
- ProductCategory
- Ingredient

Entities

- OrderItem
- OptionGroup
- Option
- Modifier

Value Objects

- Money
- Quantity
- SelectionRule

Strongly Typed IDs

- OrderId
- ProductId
- ProductCategoryId
- IngredientId
- OptionGroupId
- OptionId
- ModifierId

Repositories

- IOrderRepository
- IProductRepository
- IProductCategoryRepository
- IIngredientRepository

Handlers

<CreateCommand>

<CreateCommandHandler>

<GetQuery>

<GetQueryHandler>

---

# Repository Rules

Repositories

- return Aggregate Roots only
- never return DTOs
- never contain business rules
- never access Presentation layer

Repositories are responsible for

- persistence
- aggregate loading

Repositories are NOT responsible for

- validation
- business decisions
- mapping

---

# Handler Rules

Command Handlers

- load aggregate
- invoke domain behavior
- persist aggregate
- return response

Query Handlers

- load aggregate
- map to response DTO

Handlers

- never contain business rules
- never access DbContext
- never bypass repositories

---

# API Rules

Endpoints

- receive HTTP requests
- map request DTOs
- invoke Application layer
- return response DTOs

Endpoints

must not

- implement business rules
- access repositories
- access DbContext
- expose Domain entities

---

# EF Core Rules

Use

- IEntityTypeConfiguration<T>

- Strongly Typed ID Converters

- ApplyConfigurationsFromAssembly()

Collections

- private readonly List<T>

Public

- IReadOnlyCollection<T>

Aggregate loading

- Include()
- ThenInclude()

One repository method

↓

One SaveChangesAsync()

---

# Documentation Rules

AI Context

contains

- stable knowledge

Project Status

contains

- implementation progress

Business Rules

contain

- source of truth

Repository

contains

- source of evidence

Never duplicate business rules into AI Context.

---

# Evidence Rules

Architecture decisions

must be supported by

- Business Rules

or

- Repository evidence

Never assume.

Never invent.

When evidence is missing

state

Not Yet Verified.

---

# Future Expansion

Future modules

- Table
- Kitchen
- Payment
- Reporting
- Authentication
- Authorization
- Web UI

must reuse

- Order Module

and

- Menu Module

before introducing new architectural patterns.

---

# AI Decision Rules

When uncertainty exists

AI must

1. Search Business Rules

2. Search Repository

3. Reuse existing implementation

4. State assumptions explicitly

5. Wait for Human Approval

AI must never redesign verified architecture without explicit approval.

Consistency is preferred over innovation.

---

# Layer Responsibilities

Presentation

Responsibilities

- HTTP
- Routing
- Request DTOs
- Response DTOs

Must Not

- Business Rules
- EF Core
- DbContext

---

Application

Responsibilities

- Use Cases
- CQRS
- Handler Orchestration

Must Not

- Business Rules
- SQL
- HTTP

---

Domain

Responsibilities

- Business Rules
- Aggregate Invariants
- Domain Events

Must Not

- Infrastructure
- EF Core
- HTTP

---

Infrastructure

Responsibilities

- EF Core
- Database
- Repository
- Persistence

Must Not

- Business Rules
- HTTP

---

# Dependency Rules

Allowed

Presentation
↓

Application

↓

Domain

Infrastructure
↓

Domain

Infrastructure
↓

Application

Not Allowed

Domain
↓

Application

Domain
↓

Infrastructure

Application
↓

Presentation

Presentation
↓

Infrastructure

---

# Aggregate Checklist

Every Aggregate Root should have

- Strongly Typed ID
- Static Create()
- Private constructor
- EF Core constructor
- Business methods
- Business rules
- Domain events (when verified)
- Repository
- EF Configuration

Before creating a new Aggregate,

verify this checklist.

---

# Module Completion Checklist

Architecture

□ Business Rules

□ Ubiquitous Language

□ Aggregate Boundary

□ Repository Contract

□ CQRS

□ Application Flow

□ API Contract

□ Persistence

Implementation

□ Domain

□ Infrastructure

□ Application

□ API

Verification

□ Build

□ Self Review

□ Human Review

□ Documentation

□ Git Commit

□ Freeze

---

# Definition of Done

A milestone is complete only when

- Build succeeds
- Architecture unchanged
- Self Review complete
- Human Review complete
- Documentation updated
- Git committed
- AI Context updated
- Project Status updated

Implementation alone does not complete a milestone.

---

# Project Quality Gates

Before Merge

- Build
- Review
- Documentation
- Git Diff Review

Before Freeze

- Architecture Review
- DDD Review
- CQRS Review
- Repository Review

Before New Module

- Previous module frozen

---

# Performance Principles

Optimize only after correctness.

Correctness

↓

Architecture

↓

Maintainability

↓

Performance

Avoid premature optimization.

---

# Testing Strategy

Testing order

1. Domain

2. Application

3. API

4. Integration

5. End-to-End

Business Rules should be tested before infrastructure behavior.

## Verified Integration Testing Pattern

Menu Module v1 integration tests implement the following pattern.

- xUnit
- Testcontainers PostgreSQL (one container per test class)
- CustomWebApplicationFactory<Program>
- FluentAssertions
- Collection Fixture pattern for container lifecycle
- No mocking
- No fake repositories
- No InMemory database

Every test verifies:

- HTTP Status Code
- Response DTO
- Database persistence
- Aggregate state / Business Rules

Future modules should reuse this testing infrastructure and pattern.

---

# Refactoring Policy

Refactoring is allowed when

- behavior does not change
- architecture is preserved
- documentation remains valid

Architecture refactoring requires

- Human Approval

and

- new milestone

---

# AI Memory Rules

AI should remember

- frozen architecture
- verified decisions
- implementation patterns

AI should not remember

- temporary tasks
- work in progress
- assumptions
- unverified ideas

Long-term memory must contain

only

verified project knowledge.

---

# Stability Levels

Every project artifact has one of the following states.

Draft

- Under discussion
- May change freely

Verified

- Confirmed by repository evidence
- Human reviewed

Frozen

- Approved architecture
- Stable implementation
- Reuse without redesign

Deprecated

- No longer used
- Keep only for historical reference

---

# Decision Sources

Priority order

1. Human Decision

2. Business Rules

3. Repository Evidence

4. Frozen Documentation

5. Existing Implementation

AI assumptions are always lowest priority.

---

# Change Management

Changes to frozen components require

- verified business requirement

- architecture review

- human approval

Minor implementation improvements

may proceed

## without redesign.

# Reuse Before Create

Before creating

- new Aggregate

- new Repository

- new Value Object

- new Business Rule

AI must verify

whether an existing implementation already satisfies the requirement.

---

# Consistency Policy

Project consistency is preferred over isolated optimization.

When multiple valid implementations exist,

reuse the existing project pattern.

---

# Architecture Priorities

Priority

1. Correct Business Rules

2. Preserve Architecture

3. Consistent Implementation

4. Readability

5. Performance

---

# Code Review Checklist

Review

□ Architecture

□ Aggregate Boundaries

□ Repository Contracts

□ CQRS

□ Coding Style

□ Naming

□ Documentation

□ Build

□ Self Review

---

# Documentation Hierarchy

Business Rules

↓

Architecture Decisions

↓

AI Context

↓

Project Status

↓

Implementation

↓

Comments

---

# Repository Evidence Policy

Repository evidence confirms

implementation

Business Rules confirm

intended behavior

When they differ

Business Rules win

Implementation becomes a bug.

---

# Golden Rule

The project values

Consistency

over Cleverness

Architecture

over Convenience

Business

over Technology

Evidence

over Assumptions

Human Decisions

over AI Decisions

---

# Architectural Principles

The architecture should evolve slowly.

Business functionality may evolve quickly.

Architecture changes require significantly stronger evidence than implementation changes.

Prefer extending existing patterns over introducing new ones.

---

# Project Vocabulary

The following terminology is standardized.

Business Rule

Aggregate

Entity

Value Object

Repository

Handler

Use Case

Module

Bounded Context

Feature

Milestone

Frozen

Verified

Draft

These terms should be used consistently across

- documentation
- source code
- pull requests
- AI conversations

---

# Evolution Strategy

The project evolves by

Module

↓

Review

↓

Freeze

↓

Reuse

New modules should reuse previously frozen implementations whenever possible.

---

# Backward Compatibility

Changes should preserve

- public API contracts

- repository contracts

- aggregate behavior

unless a new milestone explicitly introduces breaking changes.

---

# Technical Debt Policy

Technical debt must be documented.

Do not silently accept technical debt.

Each item should include

- reason

- impact

- planned milestone

for resolution.

---

# Review Levels

Implementation Review

↓

Architecture Review

↓

DDD Review

↓

CQRS Review

↓

Human Approval

---

# AI Collaboration

Multiple AI systems may participate.

Every AI should

- reuse verified knowledge

- avoid redesign

- document assumptions

- wait for human approval when evidence is insufficient.

---

# Documentation Lifecycle

Business Rules

↓

Architecture

↓

Implementation

↓

Review

↓

Freeze

↓

Maintenance

---

# Project Growth Strategy

The project should grow

Horizontally

through additional modules

rather than

Vertically

through increasingly complex aggregates.

---

# Core Engineering Values

The project values

Correctness

Consistency

Maintainability

Traceability

Evidence

Long-term sustainability

over

Speed

Novelty

Premature optimization

Architectural experimentation

---

# AI Roles

Different AI systems may perform different responsibilities.

Architecture AI

- Reviews architecture
- Reviews DDD
- Reviews CQRS
- Reviews Aggregate boundaries

Implementation AI

- Implements code
- Follows frozen architecture

Review AI

- Reviews implementation
- Detects architecture drift

Documentation AI

- Updates documentation
- Maintains AI Context
- Maintains Project Status

Human

- Makes final decisions
- Resolves ambiguity
- Approves milestones

---

# Approval Levels

Level 1

Implementation

↓

Self Review

Level 2

Human Review

↓

Documentation Update

Level 3

Milestone Freeze

↓

Git Commit

↓

Next Milestone

---

# AI Escalation Rules

AI must stop and ask for approval when

- architecture changes

- repository contracts change

- aggregate boundaries change

- business rules are unclear

- evidence is insufficient

Continue automatically only when

- implementation follows frozen decisions.

---

# Evidence Confidence

High

- Business Rules
- Frozen Documentation
- Human Decision

Medium

- Repository Evidence

Low

- Existing comments
- AI assumptions

Never treat assumptions as verified facts.

---

# Module Independence

Each module should

- compile independently

- expose only public contracts

- avoid hidden dependencies

Modules communicate only through

- contracts

or

- identifiers

---

# Code Ownership

Business Rules

↓

Domain

Persistence

↓

Infrastructure

Use Cases

↓

Application

HTTP

↓

Presentation

Ownership should never overlap.

---

# AI Review Checklist

Before approval verify

□ Architecture

□ Build

□ Naming

□ CQRS

□ Aggregate boundaries

□ Repository pattern

□ Layer responsibilities

□ Documentation

□ Frozen decisions

□ Git readiness

---

# Module Freeze Checklist

Before freezing a module

□ Architecture frozen

□ Domain complete

□ Infrastructure complete

□ Application complete

□ API complete

□ Build passes

□ Documentation updated

□ Human approval completed

□ Git checkpoint created

---

# Release Checklist

Every release should include

□ Version

□ Changelog

□ Build Verification

□ Documentation

□ Human Approval

□ Git Tag

---

# Knowledge Preservation

Knowledge should be preserved in the following order

Business Rules

↓

Architecture Decisions

↓

Engineering Handbook

↓

AI Context

↓

Project Status

↓

Source Code

Never rely on AI memory alone.

Long-term knowledge must exist in documentation.

---

# Architecture Evolution Policy

Architecture should evolve by extension,
not replacement.

Preferred

Existing Pattern
↓
Extend
↓
Review
↓
Freeze

Avoid

Existing Pattern
↓
Replace Everything

---

# Pattern Catalog

Verified Patterns

Order Pattern

- Aggregate
- Repository
- CQRS
- API

Menu Pattern

- Aggregate Composition
- Owned Entities
- Value Collections
- Aggregate References
- Integration Testing Infrastructure (Testcontainers + WebApplicationFactory)

Future Patterns

- Table
- Kitchen
- Payment

---

# Reuse Priority

Before writing code

Reuse

1 Existing Aggregate

2 Existing Value Object

3 Existing Repository Pattern

4 Existing CQRS Pattern

5 Existing API Pattern

Only create new implementations when reuse is impossible.

---

# Simplicity Rule

When two implementations satisfy
the same business requirement,

prefer

the simpler implementation.

Do not introduce abstraction
without verified need.

---

# YAGNI Policy

Do not implement

- future features

- speculative abstractions

- unused extension points

Implement only verified business requirements.

---

# Technical Decision Record

Every important technical decision should include

Problem

Decision

Alternatives

Trade-offs

Evidence

Human Approval

---

# Naming Stability

Public names should change rarely.

Prefer

stable naming

over

perfect naming.

---

# Breaking Change Policy

Breaking changes require

- Human Approval

- Documentation Update

- New Milestone

- Migration Strategy

---

# Long-term Maintainability

Prefer code that is

easy to understand

over

clever but difficult to maintain.

Future maintainers
are more important than
short-term implementation speed.

---

# Engineering Motto

Design once.

Review carefully.

Freeze confidently.

Reuse everywhere.

---
