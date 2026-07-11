# Business Analysis

> This document summarizes the business analysis of the JLek POS project.
>
> The analysis is based on Business Scenarios Q001–Q146.
>
> It is intended to explain the business from an architectural perspective before any software design begins.

---

# Purpose

Before software can be designed, the business must first be understood.

The purpose of this document is to summarize the operational characteristics of the restaurant and identify the patterns that repeatedly appear throughout the business scenarios.

Rather than documenting individual scenarios, this document extracts the underlying business knowledge that drives the entire system.

---

# Business Overview

JLek POS is designed for a traditional restaurant.

The restaurant serves customers through multiple service channels.

Examples include

- Dine-in
- Takeaway
- Pickup

Although these channels appear different, they all share the same operational workflow.

The software should therefore model the business workflow rather than individual service channels.

---

# Core Business Objective

The objective of the restaurant is not simply to sell food.

Its real objective is to fulfill customer orders correctly while maintaining operational efficiency and food quality.

Every business rule ultimately supports one or more of the following goals.

- Correct orders
- Fast service
- Food quality
- Accurate payment
- Customer satisfaction
- Efficient kitchen operation

---

# Core Business Philosophy

The business scenarios reveal several recurring principles.

## Business First

Business determines how the software should behave.

Software must never force the restaurant to change its operational workflow.

---

## Customer Decision

Whenever multiple solutions exist, the customer should make the final decision.

Examples include

- Out of stock
- Alternative menu
- Late orders
- Order cancellation

The software provides information.

The customer decides.

---

## Kitchen Boundary

The kitchen represents the most important operational boundary.

Before cooking begins

Orders remain flexible.

After cooking begins

Different business rules apply.

This transition changes the behavior of the entire system.

---

## Data Integrity

Correct business history is more important than editing existing records.

Business scenarios consistently prefer

- Add-on Order
- New Order
- Cancel and Create

instead of modifying completed transactions.

---

# Operational Characteristics

The restaurant exhibits several important characteristics.

## Flexible Workflow

The workflow is intentionally flexible.

Employees may

- Hold orders
- Resume orders
- Move tables
- Merge tables
- Serve partially
- Delay production

The software should support this flexibility without compromising data integrity.

---

## Dynamic Kitchen

The kitchen does not strictly follow FIFO.

Priority changes according to operational needs.

Examples include

- Small orders inserted first
- Partial serving
- Rush orders
- Recovery after mistakes

The kitchen queue is therefore dynamic rather than sequential.

---

## Service Recovery

Mistakes are treated as part of normal restaurant operations.

The software should assist staff in resolving problems rather than preventing every possible mistake.

Examples include

- Remake food
- Refund
- Compensation
- Alternative menu

---

## Human-centered Operation

Restaurant staff continuously make business decisions.

The software should support these decisions rather than replace them.

Business judgment remains essential.

---

# Business Worlds

Analysis of the scenarios reveals several operational worlds.

These worlds represent natural areas of responsibility within the restaurant.

- Ordering
- Kitchen
- Payment
- Customer Service
- Menu
- Inventory Awareness
- Packaging

These worlds form the basis for later domain analysis.

---

# Key Discoveries

## Discovery 1

The restaurant revolves around Orders rather than Tables.

Tables may change.

Orders remain.

---

## Discovery 2

Business rules are primarily driven by operational state changes.

Examples include

- Kitchen Started
- Check Bill
- Payment Completed

These state transitions introduce new business constraints.

---

## Discovery 3

Behavior is more significant than stored data.

The restaurant operates through actions.

Examples include

- Add Item
- Hold Order
- Start Cooking
- Complete Payment

The software should model these behaviors explicitly.

---

## Discovery 4

Business flexibility must coexist with data correctness.

The restaurant values operational freedom.

The software is responsible for preserving consistency.

---

# Relationship to Later Analysis

This document provides the business foundation for all later analysis.

It is followed by

- Business Object Discovery
- Object Classification
- Behavior Analysis
- Strategic Domain Design
- Software Architecture

Each subsequent document builds upon the understanding established here.

---

# Summary

Business Analysis transforms individual business scenarios into architectural knowledge.

Instead of focusing on individual events, it identifies recurring business patterns, operational constraints, and business philosophy.

These discoveries provide the foundation for the remaining design process.
