# Design Principles

> This document defines the fundamental design principles used throughout the JLek POS project.
>
> These principles guide every architectural and implementation decision.
>
> They are not business rules.
>
> They are principles used to transform business knowledge into software.

---

# Purpose

Business rules explain how the restaurant operates.

Design principles explain how software should be designed to support those business rules.

Whenever multiple design options exist, these principles provide the basis for making consistent decisions.

---

# Principle 1 — Business Before Software

## Statement

Always understand the business before designing the software.

## Why

Business problems exist before software exists.

Software is created to support business operations, not to redefine them.

Design decisions should always be traceable back to real business needs.

## Implication

Never begin with:

- Database
- API
- UI
- Framework
- Source Code

Always begin with:

- Business Workflow
- Business Rules
- Business Language
- Business Constraints

---

# Principle 2 — Behavior Before Data

## Statement

Business behavior is more important than business data.

## Why

Restaurants operate through actions.

Examples include

- Create Order
- Add Item
- Hold Item
- Resume Item
- Start Cooking
- Serve Food
- Split Payment

The database stores information.

The business performs behaviors.

Software should model behaviors first.

## Implication

Prefer methods that express business intent.

Good

- AddItem()
- CheckBill()
- StartCooking()

Avoid generic operations such as

- Update()
- Save()
- Edit()

---

# Principle 3 — Business Boundaries Define Software Boundaries

## Statement

Business boundaries should determine software boundaries.

## Why

The business changes its rules at specific operational points.

Example

Before the kitchen starts cooking

Orders may still change.

After cooking starts

Orders follow different rules.

The software boundary should follow the same transition.

## Implication

Aggregate boundaries should be discovered from business consistency requirements, not database tables.

---

# Principle 4 — Discover Business Worlds Before Bounded Contexts

## Statement

Business domains should emerge from the business itself.

## Why

A restaurant naturally contains multiple operational worlds.

Examples

- Ordering
- Kitchen
- Payment
- Menu
- Inventory
- Customer Service

These business worlds later become candidate Bounded Contexts.

## Implication

Do not design contexts first.

Discover them from the business.

---

# Principle 5 — Consistency Boundaries Before Aggregate Design

## Statement

Aggregate boundaries should emerge from consistency boundaries.

## Why

The purpose of an Aggregate is to maintain business consistency.

It is not simply a collection of related entities.

## Example

An Order must guarantee

- Correct item list
- Correct total amount
- Valid state transitions

These consistency requirements determine the Aggregate.

## Implication

Never start by asking

"What entities belong together?"

Instead ask

"What must remain consistent?"

---

# Principle 6 — Preserve Business Language

## Statement

The software should speak the language of the business.

## Why

Developers and business owners must communicate using the same terminology.

Examples

Preferred

- Check Bill
- Add-on Order
- Kitchen Started

Avoid introducing technical terms that have no business meaning.

## Implication

The Domain Model should use the Ubiquitous Language whenever possible.

---

# Principle 7 — Preserve Knowledge

## Statement

Architecture should preserve knowledge, not only functionality.

## Why

Business knowledge is often more valuable than source code.

Software can be rewritten.

Business experience cannot.

## Implication

Every significant design decision should include

- Why
- Alternatives
- Trade-offs
- Consequences

---

# Relationship Between Principles

The principles should be applied in the following order.

```text
Business

↓

Business Rules

↓

Business Language

↓

Business Behaviors

↓

Business Boundaries

↓

Software Design

↓

Implementation
```

Higher-level principles always take precedence over lower-level implementation concerns.

---

# Summary

The design methodology of JLek POS can be summarized by seven core principles.

1. Business Before Software

2. Behavior Before Data

3. Business Boundaries Define Software Boundaries

4. Discover Business Worlds Before Bounded Contexts

5. Consistency Boundaries Before Aggregate Design

6. Preserve Business Language

7. Preserve Knowledge

Together these principles ensure that the software remains aligned with the real business while preserving architectural consistency over time.
