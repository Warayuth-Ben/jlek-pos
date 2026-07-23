# 160 — Design System Outline

Version: 1.0

Series: Design (160–199)

Canonical Source: docs/160-design/160-design-system-outline.md

---

# Purpose

This document is the canonical outline for the Design System document (160-design-system.md).

It defines the structure, scope, and boundaries of the Design System before any content is written.

This document ensures that every future writer — human or AI — understands exactly what the Design System covers, what it does not cover, and where each topic is owned.

---

# Design Series Boundary

## What belongs in 160 (Design Constitution)

- Design philosophy
- Design principles derived from business philosophy
- Constitutional design rules
- Cross-document design consistency rules
- Design-level information architecture
- The bridge between Business, Architecture, and Design

## What belongs in 161-199 (Design Standards)

- Specialized design domains (color, typography, spacing, interaction, layout)
- Component design specifications
- Screen blueprints and layout patterns
- Interaction patterns and motion design
- UI-level design decisions derived from the Design Constitution

## What does NOT belong in the Design Series (160-199)

- Business philosophy (owned by docs/01-business-foundation/)
- Architecture decisions (owned by docs/100-architecture/ and ADRs)
- Implementation code (owned by src/)
- CSS specifications (owned by CSS architecture)
- Framework-specific documentation (owned by implementation guides)
- API contracts (owned by ADR-010)
- Testing strategy (owned by docs/10-testing/)

## The Design Constitution (160) is NOT

- A UI Guideline — UI guidelines belong in docs/09-ui/
- A Component Library — component specifications belong in component documentation
- An Implementation Guide — implementation guides belong in docs/200-implementation/
- A Style Guide with concrete values — those belong in 161-199 Design Standards

---

# Scope

This outline defines the section hierarchy of the Design System document.

It does NOT contain chapter content.

It does NOT define visual design decisions.

It does NOT specify colors, typography, spacing, components, or implementation details.

---

# Audience

- Designers writing the Design System
- AI systems generating Design System content
- Engineers referencing design decisions
- Reviewers verifying Design System completeness

---

# Section Hierarchy

## 1. Introduction

### Why it exists

Explain the purpose of the Design System within the JLek POS project.

### What it covers

The relationship between business philosophy, architecture, and design decisions.

### What it does NOT cover

Visual design tokens, component specifications, CSS, HTML, or framework-specific implementations.

### Topic Owner

docs/01-business-foundation/ — owns business philosophy

docs/100-architecture/ — owns architecture principles

This document (160-design-system.md) — owns the bridge between business, architecture, and design.

---

## 2. Design Philosophy

### Why it exists

Define the foundational principles that guide every design decision.

### What it covers

- Customer-first design principle
- Restaurant operations before interface
- Kitchen boundary as design constraint
- Payment as continuation of service, not termination
- Service recovery design principle
- Information hierarchy as the source of layout decisions

### What it does NOT cover

- UI component behavior
- Screen layouts
- Interaction patterns (those belong to Interaction Philosophy documents)

### Topic Owner

docs/01-business-foundation/ — owns business principles

This document (160-design-system.md) — owns design principles derived from business principles

---

## 3. Information Architecture

### Why it exists

Define how information is structured, prioritized, and presented across the system.

### What it covers

- Information hierarchy rules
- Priority of content over decoration
- Contextual information layering
- Cross-feature information consistency

### What it does NOT cover

- Specific UI layouts
- Navigation structure (owned by docs/09-ui/02-navigation.md)
- Workspace composition (owned by docs/10-presentation/03-workspace-composition.md)

### Topic Owner

docs/00-standards/ — owns information hierarchy principles

This document (160-design-system.md) — owns design-level information architecture

---

## 4. Layout Principles

### Why it exists

Define how spatial relationships express business priorities.

### What it covers

- Content-first layout
- Progressive disclosure
- Workspace-oriented layout structure
- Feature-scoped layout ownership

### What it does NOT cover

- Specific CSS grid definitions (owned by CSS architecture)
- Responsive breakpoints (owned by CSS architecture)
- Component positioning (owned by component documentation)

### Topic Owner

docs/09-ui/ — owns UI-level layout

docs/10-presentation/ — owns presentation-level layout

This document (160-design-system.md) — owns layout principles at the design level

---

## 5. Interaction Philosophy

### Why it exists

Define how the system communicates with users and supports the service workflow.

### What it covers

- Service before data completeness
- Error recovery as first-class design concern
- Feedback without interruption
- Undo before confirmation
- Context preservation across operations

### What it does NOT cover

- Specific interaction patterns (owned by 111-interaction-philosophy.md and 112-interaction-patterns.md)
- Component interaction states (owned by component documentation)
- Animation specifications (owned by motion design documents)

### Topic Owner

docs/00-standards/111-interaction-philosophy.md — owns interaction philosophy

This document (160-design-system.md) — owns interaction principles at the design level

---

## 6. Visual Language

### Why it exists

Define the visual vocabulary without specifying concrete values.

### What it covers

- Purpose of color in the system (not color values)
- Purpose of typography (not font choices)
- Purpose of spacing (not spacing values)
- Role of visual hierarchy in supporting information hierarchy
- Consistency principles across features

### What it does NOT cover

- Color tokens (owned by CSS variables)
- Font specifications (owned by CSS typography)
- Spacing scale (owned by CSS foundation)
- Component visual design (owned by component documentation)

### Topic Owner

docs/09-ui/01-design-system.md — owns UI-level design system

src/JLek.POS.Web/wwwroot/css/ — owns CSS implementation

This document (160-design-system.md) — owns the rationale behind visual language

---

## 7. Component Philosophy

### Why it exists

Define the principles for component creation, composition, and reuse.

### What it covers

- Component responsibility boundaries
- Composition over configuration
- Feature ownership over cross-feature coupling
- Component contract principles
- When to create a component vs. when to specialize

### What it does NOT cover

- Specific component specifications (owned by component documentation)
- Component API design (owned by docs/10-presentation/04-component-contract.md)
- CSS implementation (owned by CSS architecture)

### Topic Owner

docs/10-presentation/04-component-contract.md — owns component contracts

docs/00-standards/ — owns component rules

This document (160-design-system.md) — owns component philosophy

---

## 8. Consistency Rules

### Why it exists

Define the rules that maintain design consistency across all features.

### What it covers

- Terminology consistency
- Visual consistency principles
- Behavioral consistency principles
- Cross-reference rules between design documents
- Versioning rules for design decisions

### What it does NOT cover

- Implementation-specific consistency (owned by coding standards)
- API contract consistency (owned by ADR-010)
- Documentation consistency (owned by 160-writing-guide.md)

### Topic Owner

docs/00-standards/ — owns standards

docs/160-design/160-writing-guide.md — owns documentation consistency

This document (160-design-system.md) — owns design consistency rules

---

## 9. Related Documents

| Document | Relationship |
|----------|-------------|
| docs/01-business-foundation/00-business-foundation.md | Source of design philosophy |
| docs/00-standards/ | Source of information hierarchy and component rules |
| docs/09-ui/01-design-system.md | UI-level design system |
| docs/10-presentation/04-component-contract.md | Component contract ownership |
| docs/160-design/160-writing-guide.md | Writing standard for Design Series |
| docs/160-design/160-generation-prompt.md | AI generation prompt for this document |
| docs/00-standards/111-interaction-philosophy.md | Interaction philosophy ownership |

---

# Table of Contents

1. Introduction
2. Design Philosophy
3. Information Architecture
4. Layout Principles
5. Interaction Philosophy
6. Visual Language
7. Component Philosophy
8. Consistency Rules
9. Related Documents

---

# Versioning

This outline follows the versioning rules defined in docs/160-design/160-writing-guide.md.

Version: 1.0

Status: Draft

---

# Document Ownership

**Owner:** Design Documentation Series

**Review Cycle:** Every major design change or architecture change

**Canonical Location:** docs/160-design/160-design-system-outline.md

---

# Change Log

| Version | Date | Change |
|---------|------|--------|
| 1.0 | 2026-07-23 | Initial outline created |