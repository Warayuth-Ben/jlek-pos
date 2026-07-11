# Business Object Discovery

> This document identifies the business objects that exist within the JLek POS business domain.
>
> The objective is to discover how the restaurant thinks about its business before translating those concepts into software.

---

# Purpose

Business does not think in terms of databases.

Restaurant staff never discuss

- Tables
- Columns
- APIs
- Entities

Instead, they naturally speak about the objects they interact with every day.

Examples include

- Orders
- Customers
- Kitchen
- Bills
- Payments
- Menus

These are called Business Objects.

Business Objects represent concepts that exist in the business regardless of whether software exists.

---

# What is a Business Object?

A Business Object is something that has meaning within the business.

It represents an operational concept that restaurant staff understand and work with daily.

At this stage we intentionally avoid software terminology.

A Business Object is **not yet**

- Entity
- Aggregate
- Value Object
- Database Table

Those classifications will come later.

---

# Business Worlds

Analysis of Q001–Q146 reveals that the restaurant naturally operates within several business worlds.

Each world contains its own responsibilities and vocabulary.

---

# Sales World

The Sales World is responsible for accepting customer orders and coordinating the selling process.

Business Objects include

- Order
- Order Item
- Add-on Order
- Bill
- Sale

These objects represent the commercial side of the restaurant.

---

# Customer World

The Customer World focuses on customer interaction.

Business Objects include

- Customer
- Customer Group
- Customer Request
- Customer Decision
- Complaint

The business consistently places customer decisions above software assumptions.

---

# Kitchen World

The Kitchen World transforms orders into food.

Business Objects include

- Kitchen
- Kitchen Queue
- Kitchen Job
- Cooking
- Serving

The kitchen represents the operational boundary of the restaurant.

Many business rules change once cooking begins.

---

# Payment World

The Payment World handles financial settlement.

Business Objects include

- Payment
- Split Payment
- Refund
- Discount
- Debt

These concepts have their own business lifecycle.

---

# Menu World

The Menu World defines what can be sold.

Business Objects include

- Menu
- Menu Option
- Standard Recipe
- Portion

The menu is independent from customer orders.

---

# Inventory Awareness World

The restaurant maintains awareness of inventory availability.

Business Objects include

- Ingredient
- Availability
- Out of Stock

The business focuses on operational availability rather than inventory accounting.

---

# Packaging World

Packaging is treated as part of customer service.

Business Objects include

- Package
- Bag
- Sauce
- Soup
- Spoon
- Label

Packaging affects customer experience and operational efficiency.

---

# Service World

The restaurant handles unexpected situations through service recovery.

Business Objects include

- Service Recovery
- Trust
- Alternative Suggestion
- Communication

These objects support operational flexibility rather than production.

---

# Relationship Between Business Worlds

The restaurant does not operate as a single process.

Instead, multiple business worlds collaborate.

```text
Customer

↓

Sales

↓

Kitchen

↓

Serving

↓

Payment

↓

Completed
```

Supporting Worlds

```text
Menu
Inventory
Packaging
Service
```

These worlds continuously provide information and business decisions throughout the workflow.

---

# Important Observation

Business Objects are business concepts.

They are **not software classifications**.

For example

Kitchen Queue

appears to be an important object.

However,

at this stage we intentionally avoid deciding whether it should become

- Aggregate
- Entity
- Projection
- Service

That decision belongs to the next phase.

---

# Why This Step Matters

Many software projects immediately transform business concepts into entities.

Doing so often introduces incorrect models.

Instead,

JLek POS separates

Business Discovery

from

Software Design.

This allows architectural decisions to emerge naturally from business understanding.

---

# Relationship to the Next Phase

This document only identifies Business Objects.

The next document

Object Classification

will determine

what each Business Object should become inside the software model.

---

# Summary

Business Objects describe how the restaurant understands its own operation.

They provide the bridge between Business Analysis and Domain Design.

Only after discovering these objects should the project begin classifying them into software concepts.
