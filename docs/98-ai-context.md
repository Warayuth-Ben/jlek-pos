# AI Context
-----
## Latest Progress

Completed

- Domain Foundation completed
- Order Aggregate implemented
- Order State Machine aligned with documentation
- Business Rules aligned with state machine
- Domain Events implemented
- Build PASS
- Health Check PASS

Current Focus

Architecture Validation before implementing the next Aggregate.

Next Target

Order Session Aggregate.
----

## Purpose

This document provides context for future AI sessions.

It summarizes the project architecture, development philosophy, completed work, and current implementation status.

This is not user documentation.

This document exists to help future AI assistants continue development without rediscovering previous decisions.

---

# Project Overview

Project

JLek POS

Architecture

- Clean Architecture
- Domain Driven Design
- Documentation First Development

Primary Goal

Build a long-term maintainable restaurant POS system.

Understanding architecture is more important than writing code quickly.

---

# Current Status

Project Initialization

? Complete

Documentation

? Complete

Domain Foundation

? Complete

Build

? PASS

Health Check

? PASS

Current Milestone

Ready to begin Order Aggregate implementation.

---

# Completed Documentation

Completed

- Engineering Standards
- Business Rules
- Domain Model
- System Use Cases
- State Machines

Documentation is considered frozen.

Only modify documentation when business rules or architecture intentionally change.

---

# Completed Domain Foundation

Results

- Error
- Result

Primitives

- Entity
- AggregateRoot
- ValueObject
- IDomainEvent
- DomainEvent

Rules

- IBusinessRule
- BusinessRuleValidationException

Value Objects

- Money
- Quantity

The Domain Foundation is complete and stable.

---

# Architecture Rules

Always preserve Clean Architecture.

Dependencies always flow inward.

Business

?

Domain

?

Application

?

Infrastructure

?

Presentation

Never reverse dependencies.

Documentation is the Single Source of Truth.

Implementation follows documentation.

Never invent business rules.

---

# Repository Philosophy

The repository is the project's memory.

It contains

- Documentation
- Business Knowledge
- Architecture
- Design Decisions
- Source Code

Documentation has higher priority than implementation.

---

# Development Workflow

Always follow this order

Review Documentation

?

Design

?

Implement

?

Build

?

Health Check

?

Commit

Never skip Build.

Never skip Health Check.

Prefer small commits.

---

# AI Working Rules

Always understand existing architecture before adding code.

Prefer extending existing code over redesigning.

Avoid unnecessary abstractions.

Avoid premature optimization.

Do not rename folders unless necessary.

Do not redesign completed documentation.

Explain architectural reasoning before implementation whenever appropriate.

---

# Lessons Learned

Project initialization established several important rules.

- Build after every milestone.
- Perform Health Check after every phase.
- Keep documentation stable.
- Avoid unnecessary refactoring.
- Follow standard .NET project structure.
- Prefer incremental commits.

---

# Next Milestone

Order Aggregate

Recommended implementation order

1. OrderId
2. OrderItemId
3. OrderStatus
4. OrderItem
5. Order
6. Domain Events
7. Business Rules

Complete the Aggregate before moving to the Application layer.

---

# Recent Progress

The Domain Foundation has been completed successfully.

Completed components

Results

- Error
- Result

Primitives

- Entity
- AggregateRoot
- ValueObject
- IDomainEvent
- DomainEvent

Rules

- IBusinessRule
- BusinessRuleValidationException

Value Objects

- Money
- Quantity

The solution builds successfully.

Health Check passed.

The project is now ready to begin implementing the first Aggregate.

------------
- PowerShell here-strings are preferred for creating source files.
- Generate complete source files instead of partial snippets.
- Keep implementation incremental.
-----

---

# Update AI Context

When the project owner requests

Update AI Context

The AI should automatically perform the following tasks.

## 1. Update AI Documentation

Update

- docs/97-handoff.md
- docs/98-ai-context.md
- docs/99-project-status.md

---

## 2. Update Current Progress

Record

- Current Phase
- Current Milestone
- Completed Work
- Next Milestone
- Build Status
- Health Check Status

---

## 3. Record Session Summary

Summarize

- What was implemented
- New source files
- Refactoring performed
- Architecture decisions
- Business decisions

---

## 4. Record Lessons Learned

Append any important development lessons discovered during the session.

Avoid duplicates whenever possible.

---

## 5. Update AI Context

Update long-term project knowledge when necessary.

Examples

- New architecture decisions
- New development workflow
- New coding standards
- New project conventions

Do not overwrite existing decisions without reason.

---

## 6. Generate Complete Files

When updating AI documentation,

always generate complete file contents.

Do not generate partial snippets.

Provide PowerShell commands using Here-String with Set-Content.

---

## 7. Preserve Existing Context

Never remove useful historical information unless it is obsolete.

Prefer extending existing documentation.

Avoid unnecessary documentation churn.

---

## 8. Finish Checklist

Before finishing

Confirm

- Build Status
- Health Check
- Current Milestone
- Next Milestone
- Documentation Status

Ensure the project is ready for the next development session.

-----

# Notes

The project owner prefers

- Maintainability over speed.
- Architecture over shortcuts.
- Understanding before implementation.
- Small incremental progress.
- Stable foundations before adding features.

Future AI assistants should preserve these principles throughout the project.
