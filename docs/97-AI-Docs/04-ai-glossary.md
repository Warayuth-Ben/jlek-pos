# AI Glossary

Version: 1.0

Project: JLek POS

---

# Purpose

This document defines project-specific terminology.

Whenever the AI encounters one of these terms,

the definitions in this document take precedence over general AI knowledge.

---

# Aggregate

A cluster of related domain objects that are treated as a single consistency boundary.

An Aggregate protects Business Rules.

Only the Aggregate Root may be accessed directly.

---

# Aggregate Root

The entry point of an Aggregate.

Responsibilities

- Protect Aggregate integrity
- Enforce Business Rules
- Control all child Entities
- Raise Domain Events

Examples

- Order
- Order Session (future)

---

# Entity

A domain object with identity.

Entities are compared by identity,

not by value.

Examples

- Order
- OrderItem

---

# Value Object

An immutable object without identity.

Value Objects are compared by value.

Examples

- Money
- Quantity
- OrderId
- OrderItemId
- OrderSessionId

---

# Strongly Typed ID

A Value Object representing an identifier.

Purpose

- Prevent ID confusion
- Improve type safety
- Improve readability

---

# Business Rule

A rule describing how the business operates.

Business Rules belong exclusively to the Domain Layer.

Business Rules must never be implemented in

- Infrastructure
- Presentation
- Database
- API

---

# Domain Event

A notification that something meaningful has happened inside the Domain.

Examples

- OrderCreated
- OrderConfirmed
- OrderCompleted

Domain Events belong to the Domain Layer.

---

# Repository

Provides persistence for Aggregates.

Repositories abstract database access.

Repositories must not contain Business Rules.

---

# Application Layer

Coordinates use cases.

Responsibilities

- Commands
- Queries
- Transactions
- Orchestration

The Application Layer does not own Business Rules.

---

# Infrastructure Layer

Contains technical implementation.

Examples

- EF Core
- PostgreSQL
- Repository implementations

No Business Rules belong here.

---

# Presentation Layer

Responsible for HTTP only.

Examples

- Minimal API
- Controllers
- Endpoints

No Business Rules belong here.

---

# Order

Aggregate Root.

Responsible for

- Order lifecycle
- Order Items
- Business Rules
- Domain Events

Order is the only object allowed to modify OrderItem.

---

# OrderItem

Entity.

Represents one menu item inside an Order.

OrderItem cannot exist independently.

OrderItem is controlled by Order.

---

# Order Session

Represents a customer ordering session.

An Order Session may contain multiple Orders.

(Current implementation may still be evolving.)

Always consult documentation before making assumptions.

---

# Add-on Order

A possible future business concept.

Current implementation status

Not Verified.

Do not assume its existence unless documentation explicitly defines it.

---

# Kitchen Queue

Represents the kitchen production workflow.

Current implementation status

Not Implemented.

---

# DTO

Data Transfer Object.

Used for communication outside the Domain.

Rules

- Never expose Domain Entity directly.
- Always return DTOs from APIs.

---

# Verified

Information confirmed by direct evidence.

Evidence may come only from

- Documentation
- Source Code
- Explicit User Instruction

Without evidence,

the information is NOT VERIFIED.

---

# Not Verified

The AI cannot confirm the information.

Not Verified is preferred over assumptions.

---

# Verified Facts

Facts confirmed by evidence.

No interpretation.

---

# Findings

Conclusions derived from Verified Facts.

---

# Recommendations

Suggestions made by the AI.

Recommendations are not facts.

---

# Evidence

Evidence must originate from

- Repository
- Documentation
- Source Code
- User Instruction

Memory is not evidence.

Similarity is not evidence.

Inference is not evidence.

---

# Documentation

Documentation is the Source of Truth.

If documentation conflicts with AI knowledge,

documentation always wins.

---

# Architecture

Architecture defines

- Boundaries
- Responsibilities
- Dependencies

Architecture must not be modified without human approval.

---

# Human Approval

Implementation requires explicit human approval.

Examples

- Approved
- Proceed
- Continue
- Implement

Without approval,

the AI must stop.

---

# Engineering Language

Preferred

- Verified
- Not Verified
- Evidence
- Confirmed
- Unable to Verify
- Observed

Avoid

- probably
- maybe
- likely
- seems
- appears

unless explicitly marked as assumptions.

---

# Golden Rule

When uncertain,

STOP.

Verify.

Ask.

Never Guess.
