# Ordering Architecture

## Purpose

Defines the high-level architecture of the Ordering Context.

This document bridges the gap between the Domain Model and the implementation.

Implementation must follow this document.

---

# Aggregate Boundary

Ordering Context owns one Aggregate Root.

Aggregate Root

- Order Session

Contained Entities

- Order
- Order Item

Aggregate ownership

Order Session
    +-- Orders
            +-- Order Items

---

# Responsibilities

Order Session

- Own customer transaction
- Accept new Orders
- Accept Add-on Orders
- Coordinate Order lifecycle
- Publish Ordering Domain Events

Order

- Own Order Items
- Maintain Order lifecycle
- Confirm Order

Order Item

- Store Menu Item
- Store Quantity
- Store Unit Price
- Store Options
- Store Notes

---

# Cross Context Collaboration

Ordering collaborates with

- Kitchen
- Payment
- Table

Ordering never performs Kitchen or Payment business directly.

Communication occurs through Domain Events.

---

# Repository

Ordering exposes one Aggregate Repository.

Repository

- IOrderSessionRepository

Repositories always load and save Aggregate Roots.

---

# Implementation Rules

Implementation must preserve Aggregate boundaries.

Entities never modify other Aggregates directly.

Cross-context communication must use Domain Events.

Business Rules belong inside the Aggregate whenever possible.

Application Layer coordinates use cases.

Infrastructure provides persistence only.
