# Object Classification

> This document transforms Business Objects into Domain Concepts.
>
> It bridges the gap between Business Analysis and Domain-Driven Design.
>
> No implementation decisions are made here.
>
> The objective is to classify business concepts into appropriate software concepts.

---

# Purpose

Business Objects describe how the restaurant thinks.

Software, however, must represent those concepts using different types of domain objects.

Not every Business Object becomes an Entity.

Some become

- Aggregate
- Entity
- Value Object
- Domain Service
- Domain Policy
- Workflow
- Projection

The purpose of this document is to determine the most appropriate classification for each Business Object.

---

# Classification Process

Every Business Object should be evaluated using the following questions.

1. Does it have its own identity?

2. Does it have an independent lifecycle?

3. Does it own business rules?

4. Does it maintain business consistency?

5. Is it simply descriptive information?

6. Is it an operation rather than an object?

The answers determine the appropriate classification.

---

# Aggregate Candidates

## Order

Business Evidence

The restaurant manages every transaction through Orders.

An Order

- accepts items
- changes state
- coordinates the kitchen
- requests payment

Reasoning

Order owns important business rules and consistency.

Classification

Aggregate Root

Confidence

★★★★★

---

## Payment

Business Evidence

Payment has its own lifecycle.

Examples

- Pending
- Split
- Completed
- Refunded

Reasoning

Payment represents an independent business transaction.

Classification

Aggregate Root

Confidence

★★★★★

---

## Menu

Business Evidence

Menus exist independently of customer orders.

Reasoning

Menu has its own lifecycle and business rules.

Classification

Aggregate Root

Confidence

★★★★☆

---

# Entity Candidates

## Order Item

Business Evidence

Each food item progresses independently through production.

Examples

- Waiting
- Cooking
- Ready
- Served

Reasoning

Items require identity and state tracking.

Classification

Entity

Confidence

★★★★★

---

## Customer Request

Business Evidence

Customer requests may change or be completed independently.

Examples

- Extra sauce
- No cucumber
- Separate bag

Classification

Entity

Confidence

★★★☆☆

---

# Value Object Candidates

## Money

Represents monetary values.

No identity.

Classification

Value Object

★★★★★

---

## Quantity

Represents item quantity.

Value Object

★★★★★

---

## Menu Option

Represents configurable menu choices.

Value Object

★★★★★

---

## Customer Note

Descriptive information.

Value Object

★★★★★

---

## Queue Position

Represents temporary ordering.

Value Object

★★★★☆

---

## Reservation Time

Represents an agreed pickup time.

Value Object

★★★★☆

---

# Domain Service Candidates

Some business logic belongs to neither Order nor Payment.

---

## Pricing Service

Responsible for

- subtotal
- discount
- total

Reason

Calculation logic is shared.

---

## Kitchen Scheduling Service

Responsible for

- queue ordering
- priority
- insertion

Reason

Queue management spans multiple Orders.

---

## Service Recovery Service

Responsible for

- remake
- refund
- compensation

Reason

Recovery decisions involve multiple business objects.

---

## Inventory Decision Service

Responsible for

availability decisions.

Reason

Inventory awareness influences ordering but does not belong to Orders.

---

# Domain Policies

Policies define business decisions.

---

## Before Kitchen Policy

Orders remain editable.

---

## After Kitchen Policy

Orders become restricted.

---

## Customer Decision Policy

Customers choose among alternatives.

---

## Fair Queue Policy

Priority may change while preserving fairness.

---

## Food Quality Policy

Quality always takes precedence over speed.

---

# Workflows

The following Business Objects represent processes rather than persistent objects.

Examples

- Ordering
- Cooking
- Serving
- Payment
- Pickup
- Service Recovery

Classification

Workflow

---

# Projection Candidates

Some concepts are read models.

---

## Kitchen Queue

Represents the current production queue.

It is derived from Orders.

Classification

Projection

Confidence

★★★★★

---

## Dashboard

Projection

---

## Daily Report

Projection

---

## History

Projection

---

# Classification Summary

| Business Object    | Classification |
| ------------------ | -------------- |
| Order              | Aggregate Root |
| Payment            | Aggregate Root |
| Menu               | Aggregate Root |
| Order Item         | Entity         |
| Customer Request   | Entity         |
| Money              | Value Object   |
| Quantity           | Value Object   |
| Menu Option        | Value Object   |
| Customer Note      | Value Object   |
| Queue Position     | Value Object   |
| Pricing            | Domain Service |
| Kitchen Scheduling | Domain Service |
| Service Recovery   | Domain Service |
| Inventory Decision | Domain Service |
| Kitchen Queue      | Projection     |
| Dashboard          | Projection     |
| Reports            | Projection     |

---

# Important Observation

Classification should never be driven by database structure.

Instead,

classification should emerge naturally from

- business responsibility
- lifecycle
- consistency
- behavior

Only after completing this classification should aggregate boundaries be discussed.

---

# Relationship to the Next Phase

This document answers

"What is each Business Object?"

The next phase answers

"How do these objects behave together?"

Behavior Analysis introduces

- State Machines
- Commands
- Queries
- Domain Events
- Business Behaviors

These behaviors will validate the classifications made in this document.

---

# Summary

Object Classification transforms Business Objects into Domain Concepts.

It is the critical bridge between understanding the business and designing a robust Domain Model.

Every later architectural decision depends upon the correctness of this classification.
