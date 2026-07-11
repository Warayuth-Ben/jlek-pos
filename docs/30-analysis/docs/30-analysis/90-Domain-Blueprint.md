# Domain Blueprint

> Executive Blueprint of the JLek POS Design Process
>
> This document summarizes the complete reasoning process used to transform business knowledge into software architecture.
>
> It serves as the entry point for anyone who wants to understand the project before reading the detailed analysis documents.

---

# Purpose

The JLek POS project follows a Business-First design approach.

Instead of starting from databases, frameworks, or APIs, the project begins with understanding the real business.

This blueprint summarizes the complete design journey.

---

# Design Journey

The project follows the sequence below.

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

Each step builds upon the previous one.

Skipping a step increases the risk of introducing an incorrect software model.

---

# Analysis Documents

The complete design process consists of the following documents.

| Document                     | Purpose                                               |
| ---------------------------- | ----------------------------------------------------- |
| 00-Design-Methodology        | Overall design process                                |
| 01-Design-Principles         | Design philosophy                                     |
| 10-Business-Analysis         | Understand the business                               |
| 20-Business-Object-Discovery | Discover business concepts                            |
| 30-Object-Classification     | Transform business concepts into domain concepts      |
| 40-Behavior-Analysis         | Understand business behaviors                         |
| 50-Strategic-Domain-Design   | Discover domain boundaries                            |
| 60-Software-Architecture     | Transform business domains into software architecture |

---

# Design Philosophy

The project follows several core principles.

- Business Before Software
- Behavior Before Data
- Business Boundaries Define Software Boundaries
- Discover Business Worlds Before Bounded Contexts
- Consistency Boundaries Before Aggregate Design
- Preserve Business Language
- Preserve Knowledge

These principles guide every design decision.

---

# Key Discoveries

Analysis of the business scenarios produced several important discoveries.

## Order is the center of the business

The restaurant revolves around customer orders.

Tables support the workflow.

Orders own the business transaction.

---

## Kitchen is the primary operational boundary

Business rules change when cooking begins.

This boundary affects almost every workflow.

---

## Behavior defines the Domain

The business operates through actions.

Examples include

- Add Item
- Hold Order
- Start Cooking
- Check Bill
- Split Payment

The software should model these behaviors explicitly.

---

## Business boundaries determine software boundaries

Software modules should follow business responsibilities rather than technical concerns.

---

# Software Architecture

The resulting architecture follows

- Business-first Design
- Domain-Driven Design
- Clean Architecture
- CQRS (where appropriate)
- Event-driven collaboration

Each architectural decision can be traced back to business evidence.

---

# Relationship to Official Documentation

This blueprint belongs to

```
docs/30-analysis
```

It is a design reasoning document.

It does not replace the official specification.

The official project specification remains in

```
docs/00-10
```

If differences are discovered,

the specification always takes precedence until an official architecture decision updates the baseline.

---

# Reading Order

For new contributors, the recommended reading sequence is

1. AI-CONTEXT.md

2. 00-Design-Methodology.md

3. 01-Design-Principles.md

4. 10-Business-Analysis.md

5. 20-Business-Object-Discovery.md

6. 30-Object-Classification.md

7. 40-Behavior-Analysis.md

8. 50-Strategic-Domain-Design.md

9. 60-Software-Architecture.md

10. Official Specification (docs/00-10)

11. Source Code

Following this order provides both business understanding and architectural reasoning before implementation.

---

# Final Statement

Business knowledge is the foundation of JLek POS.

Architecture exists to preserve that knowledge.

Software exists to support the business.

Technology may change.

Frameworks may evolve.

Source code may be rewritten.

Business knowledge should remain.
