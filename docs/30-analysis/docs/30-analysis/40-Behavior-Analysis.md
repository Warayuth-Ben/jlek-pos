# Behavior Analysis

> This document analyzes the behaviors of the Domain Objects identified during Object Classification.
>
> The objective is to understand how the business operates through actions rather than data.

---

# Purpose

Restaurants do not operate through data.

Restaurants operate through actions.

Examples include

- Create Order
- Add Item
- Hold Item
- Start Cooking
- Complete Payment

These actions represent the real behavior of the business.

Software should model these behaviors explicitly.

---

# Why Behavior Matters

Most software projects begin by modeling data.

This project intentionally begins with behavior.

Business behavior defines

- Business rules
- State transitions
- Consistency boundaries
- Domain events

Understanding behavior first produces a more accurate Domain Model.

---

# Primary Business Behaviors

Analysis of the Business Scenarios reveals several recurring behaviors.

---

# Ordering

Responsibilities

- Create Order
- Add Item
- Remove Item
- Modify Item
- Hold Order
- Resume Order
- Confirm Order
- Cancel Order

Business Owner

Ordering

---

# Kitchen

Responsibilities

- Accept Order
- Start Cooking
- Pause Cooking
- Resume Cooking
- Complete Cooking
- Notify Ready

Business Owner

Kitchen

---

# Serving

Responsibilities

- Deliver Food
- Partial Serving
- Complete Serving

Business Owner

Restaurant Staff

---

# Payment

Responsibilities

- Request Check Bill
- Calculate Total
- Split Payment
- Receive Payment
- Complete Payment
- Refund

Business Owner

Cashier

---

# Customer Service

Responsibilities

- Receive Complaint
- Evaluate Situation
- Offer Alternatives
- Recover Service

Business Owner

Restaurant

---

# Behavior Ownership

Every behavior should have one clear business owner.

| Behavior         | Owner   |
| ---------------- | ------- |
| Add Item         | Order   |
| Remove Item      | Order   |
| Confirm Order    | Order   |
| Hold Item        | Order   |
| Resume Item      | Order   |
| Start Cooking    | Kitchen |
| Complete Cooking | Kitchen |
| Check Bill       | Payment |
| Receive Payment  | Payment |
| Refund           | Payment |

Ownership reduces ambiguity.

---

# State-Driven Behaviors

Most behaviors are only valid in certain states.

Example

Order

Draft

↓

Confirmed

↓

Kitchen Started

↓

Ready

↓

Served

↓

Paid

↓

Completed

Examples

Add Item

Allowed

Before Kitchen Started

Not Allowed

After Kitchen Started

---

Split Payment

Allowed

Before Payment Completed

Not Allowed

After Payment Completed

---

# Behavior Dependencies

Many behaviors depend upon earlier behaviors.

Example

Create Order

↓

Confirm Order

↓

Kitchen Started

↓

Kitchen Completed

↓

Serve

↓

Payment

↓

Complete

The business process is sequential even though implementation may be asynchronous.

---

# Domain Events

Every important behavior creates a business event.

Examples

Ordering

- OrderCreated
- OrderConfirmed
- OrderCancelled

Kitchen

- CookingStarted
- CookingCompleted
- FoodReady

Payment

- CheckBillRequested
- PaymentCompleted
- PaymentRefunded

Service

- ComplaintReceived
- RecoveryCompleted

These events become communication points between contexts.

---

# Commands

Behaviors are initiated through Commands.

Examples

Ordering

- CreateOrder
- AddItem
- CancelItem
- ConfirmOrder

Kitchen

- StartCooking
- CompleteCooking

Payment

- RequestCheckBill
- ReceivePayment
- SplitPayment

Commands express business intent.

---

# Queries

Queries retrieve information without changing business state.

Examples

- Get Order
- Get Kitchen Queue
- Get Payment Status
- Get Order History
- Get Daily Sales

Queries never modify business behavior.

---

# Business Observations

Several important patterns appear repeatedly.

## Business is Behavior Driven

The restaurant performs actions.

It does not edit records.

---

## State Controls Behavior

The current business state determines what actions are permitted.

---

## Events Connect Business Worlds

Each completed behavior produces information that other parts of the business consume.

---

## Commands Represent Business Language

Business never says

Update Order

Instead

Business says

- Add Item
- Hold Order
- Check Bill
- Split Payment

The software should use the same language.

---

# Relationship to the Next Phase

Behavior Analysis explains

"What business objects do."

The next document

Strategic Domain Design

answers

"How should responsibilities be divided between different business domains?"

---

# Summary

Behavior Analysis transforms static business concepts into active business models.

Instead of focusing on stored information, it focuses on business actions, ownership, state transitions, and communication between business worlds.

These behaviors become the foundation for strategic domain design.
