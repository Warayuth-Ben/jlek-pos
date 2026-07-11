# Strategic Domain Design

> This document defines the strategic domain design of the JLek POS project.
>
> The objective is to divide the business into coherent business domains before designing software modules.

---

# Purpose

Large software systems should not be designed as one monolithic model.

Instead,

the business should first be divided into independent domains.

Each domain owns its own responsibilities, language, and business rules.

This separation becomes the foundation for later software architecture.

---

# From Business Worlds to Bounded Contexts

Business Analysis identified several Business Worlds.

These worlds naturally evolve into candidate Bounded Contexts.

Business World

↓

Bounded Context

↓

Software Module

This transformation keeps software aligned with business reality.

---

# Candidate Bounded Contexts

## Ordering Context

Responsibilities

- Receive customer orders
- Manage order lifecycle
- Manage order items
- Handle add-on orders
- Coordinate with kitchen
- Initiate payment

Owns

- Order
- Order Item

Publishes

- OrderCreated
- OrderConfirmed
- OrderCancelled

---

## Kitchen Context

Responsibilities

- Manage kitchen workload
- Schedule production
- Track cooking progress
- Notify food readiness

Owns

Kitchen execution

Consumes

- OrderConfirmed

Publishes

- CookingStarted
- CookingCompleted
- FoodReady

---

## Payment Context

Responsibilities

- Check Bill
- Calculate payment
- Split payment
- Receive payment
- Refund

Owns

Payment lifecycle

Consumes

- CheckBillRequested

Publishes

- PaymentCompleted
- PaymentRefunded

---

## Menu Context

Responsibilities

- Menu information
- Menu availability
- Pricing
- Menu options

Owns

Menu definitions

Publishes

- MenuUpdated

---

## Inventory Awareness Context

Responsibilities

- Ingredient availability
- Out-of-stock information

Publishes

- ItemUnavailable

The context supports operations but does not manage inventory accounting.

---

## Customer Service Context

Responsibilities

- Complaints
- Service recovery
- Alternative suggestions
- Customer communication

Publishes

- RecoveryCompleted

---

# Context Relationships

```text
Menu
   │
   ▼
Ordering ─────────► Kitchen
    │                  │
    ▼                  ▼
Payment        Customer Service

Inventory
     │
     ▼
Ordering
```

Ordering is the central coordination context.

Other contexts collaborate through business events.

---

# Upstream and Downstream

## Menu → Ordering

Ordering depends on menu information.

Menu is Upstream.

Ordering is Downstream.

---

## Ordering → Kitchen

Kitchen depends on confirmed orders.

Ordering is Upstream.

Kitchen is Downstream.

---

## Ordering → Payment

Payment depends on completed ordering.

Ordering is Upstream.

Payment is Downstream.

---

## Customer Service

Customer Service interacts with multiple contexts.

It should remain loosely coupled.

---

# Shared Concepts

Some concepts are shared across contexts.

Examples

- OrderId
- OrderItemId
- MenuId

Shared concepts should remain stable.

Business behavior should not be shared unnecessarily.

---

# Context Communication

Contexts communicate through business events.

Example

```text
OrderConfirmed

↓

Kitchen receives work

↓

CookingStarted

↓

FoodReady

↓

PaymentRequested

↓

PaymentCompleted
```

This reduces direct coupling between contexts.

---

# Context Boundaries

Each context owns its own business decisions.

Ordering

Owns

- Order validation
- Order changes
- Add-on logic

Kitchen

Owns

- Cooking sequence
- Production priority

Payment

Owns

- Financial settlement
- Payment validation

No context should modify another context's internal business rules.

---

# Strategic Design Decisions

Several important strategic decisions emerge.

## Ordering is the Core Domain

Ordering coordinates the entire business.

Most business rules originate here.

---

## Kitchen is an Execution Domain

Kitchen executes work.

It should not own ordering decisions.

---

## Payment is an Independent Domain

Payment follows its own lifecycle.

It should remain independent from Order consistency.

---

## Inventory Awareness Supports Ordering

Inventory influences decisions.

It does not control them.

---

## Customer Service Crosses Multiple Domains

Service Recovery interacts with several contexts.

It should coordinate rather than own business data.

---

# Impact on Software Architecture

The identified contexts become the foundation for

- Domain Layer
- Application Layer
- Module Boundaries
- Domain Events
- CQRS
- Future Microservices (if required)

The software architecture should follow these business boundaries rather than technical layers.

---

# Relationship to the Next Phase

Strategic Domain Design defines

"Where responsibilities belong."

The next document

Software Architecture

defines

"How these responsibilities are implemented."

---

# Summary

Strategic Domain Design transforms business understanding into architectural boundaries.

Instead of dividing software by technology,

the project divides software according to business responsibilities.

This alignment reduces coupling, preserves business language, and supports long-term maintainability.
