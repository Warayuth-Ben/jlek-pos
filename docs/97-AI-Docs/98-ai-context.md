# AI Context

Version: 1.0

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

Presentation contains HTTP concerns only.

No Business Rules belong here.

---

# Development Principles

The following principles are mandatory.

- Never expose Domain Entities directly.
- Always return DTOs.
- Keep Domain pure.
- Application contains use cases only.
- Infrastructure contains technical implementation only.
- Presentation contains HTTP concerns only.

---

# Verified APIs

Verified

POST /orders

Current behavior

- Returns HTTP 201 Created
- Successfully saves data into PostgreSQL

---

# Current Technical Limitations

Current implementation still has the following limitations

- API returns Domain Entities directly.
- Domain Events are serialized to API responses.
- Response DTO layer has not yet been implemented.

These limitations are known and intentional until the next milestone.

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
