# Design Methodology

> This document defines the design methodology used throughout the JLek POS project.
>
> It describes **how** business knowledge is transformed into software architecture.
>
> This document is part of **30-analysis**.
>
> It is **NOT** an official specification.
>
> It exists to explain the reasoning behind the design process.

---

# Purpose

The purpose of this document is to define a repeatable design process.

Instead of jumping directly into software design, the project follows a structured path from real business knowledge to implementation.

The goal is to ensure that every architectural decision can be traced back to an actual business need.

---

# Core Philosophy

The design process follows one fundamental principle.

> Understand the business first.
>
> Design the software second.

Software should never invent business rules.

Software should only model and support the real business.

---

# Overall Design Flow

The JLek POS project follows the workflow below.

```text
Business Interview

↓

Business Knowledge

↓

Business Analysis

↓

Business Object Discovery

↓

Object Classification

↓

Behavior Analysis

↓

Strategic Domain Design

↓

Software Architecture

↓

Implementation

↓

Knowledge Preservation
```

Every step builds upon the previous one.

Skipping steps increases the risk of incorrect design decisions.

---

# Step 1 — Business Interview

Objective

Collect business knowledge from the real restaurant.

Output

- Business Scenarios
- Questions & Answers
- Real operational workflow

At this stage no software design is performed.

---

# Step 2 — Business Analysis

Objective

Extract business rules.

Output

- Business Rules
- Operational Rules
- Exception Rules
- Business Philosophy

The goal is to understand how the restaurant operates.

---

# Step 3 — Business Object Discovery

Objective

Identify the objects that exist in the business.

Examples

- Order
- Order Item
- Customer
- Kitchen
- Menu
- Payment

These are business concepts.

They are **not software objects yet**.

---

# Step 4 — Object Classification

Objective

Classify each Business Object.

Possible classifications include

- Aggregate
- Entity
- Value Object
- Domain Service
- Domain Policy
- Workflow
- Projection

Classification happens only after understanding the business.

---

# Step 5 — Behavior Analysis

Objective

Understand what each Business Object can do.

Behavior is more important than data.

Examples

- Add Item
- Hold Item
- Resume Item
- Start Cooking
- Split Payment

Business behavior defines software behavior.

---

# Step 6 — Strategic Domain Design

Objective

Discover business boundaries.

Activities include

- Business Worlds
- Bounded Contexts
- Context Map
- Shared Kernel
- Context Relationships

The objective is to determine where each responsibility belongs.

---

# Step 7 — Software Architecture

Objective

Transform the domain design into software architecture.

Examples

- Clean Architecture
- CQRS
- Domain Events
- Project Structure
- Layer Responsibilities

---

# Step 8 — Implementation

Objective

Translate the architecture into source code.

Implementation should never redefine business rules.

---

# Step 9 — Knowledge Preservation

Objective

Preserve the reasoning behind the design.

Outputs include

- Knowledge Book
- Architecture Decisions
- Lessons Learned

The goal is that future developers understand not only how the system works, but also why it was designed that way.

---

# Design Principles

Throughout every step, the following principles apply.

- Business before Software
- Behavior before Data
- Business Boundaries define Software Boundaries
- Discover Business Worlds before Bounded Contexts
- Consistency Boundaries before Aggregate Design

---

# Relationship to Official Documentation

This document belongs to

```
docs/30-analysis
```

It is a design and reasoning document.

It does not replace the official specification.

Official specifications remain inside

```
docs/00-10
```

Whenever a conflict exists

Official Specification always takes precedence.

---

# Expected Outcome

Following this methodology should produce

- Consistent Domain Models
- Traceable Architecture Decisions
- Business-driven Software Design
- Maintainable Software
- Long-term Knowledge Preservation
