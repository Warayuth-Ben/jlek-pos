# 160 — Generation Prompt

Version: 1.0

Series: Design (160–199)

Canonical Source: docs/160-design/160-generation-prompt.md

---

# Purpose

This document provides the official AI prompt for generating the Design System document (160-design-system.md).

Any AI model — including future models — can use this prompt to generate the Design System document consistently.

This prompt ensures that every generation follows the project philosophy, respects existing documentation, and produces content that is ready for human review.

---

# Usage Instructions

1. Read all documents listed in the **Mandatory Reading** section below.
2. Read this prompt in its entirety.
3. Follow the **Generation Rules** precisely.
4. Generate `160-design-system.md` content using the section hierarchy defined in `160-design-system-outline.md`.
5. After generation, run the **Self-Verification Checklist**.

---

# Mandatory Reading

Before generating the Design System, the AI must read the following documents in order.

## Business Foundation

- docs/01-business-foundation/00-business-foundation.md
- docs/01-business-foundation/02-customer-before-system.md
- docs/01-business-foundation/03-the-restaurant-before-the-software.md
- docs/01-business-foundation/04-kitchen-is-the-boundary.md
- docs/01-business-foundation/05-payment-is-not-the-end.md

## Design Foundation

- docs/160-design/160-design-system-outline.md
- docs/160-design/160-writing-guide.md
- docs/00-standards/writing-style.md
- docs/00-standards/document-templates.md

## Architecture & AI

- docs/97-AI-Docs/01-ai-engineering-standard.md
- docs/97-AI-Docs/02-ai-constitution.md
- docs/97-AI-Docs/98-ai-context.md
- docs/97-AI-Docs/100-reference-modules.md
- docs/README.md

---

# Generation Rules

## Derive, Do Not Invent

All principles in the Design System must be derived from the project documentation listed in Mandatory Reading.

The AI must NEVER:

- Invent generic design principles not specific to this project
- Use generic UX principles (e.g., "keep it simple" or "don't make me think") unless those principles are explicitly stated in project documentation
- Reference design methodologies (e.g., Material Design, Atomic Design, or any external design system) unless those methodologies are explicitly documented in the project
- Import concepts from other design systems or frameworks

## Explain WHY Before HOW

Every design principle must first explain WHY it exists before explaining HOW it manifests.

Example:

> **WHY:** The kitchen is the operational boundary. Once food leaves the kitchen, modifications cannot be undone without affecting service.
>
> **HOW:** Design decisions that affect the kitchen workflow must be irreversible after production begins.

Without a WHY, a design principle has no foundation.

## Canonical Documentation Style

The generated content must follow the writing standards defined in:

- docs/160-design/160-writing-guide.md
- docs/00-standards/writing-style.md

Key requirements:

- Professional Markdown
- No placeholders
- No TODO items
- No filler text
- Production-ready content
- Clear separation of sections
- Consistent terminology

## Avoid Implementation

The Design System must operate at the design level.

It must NOT specify:

- How to implement in code
- CSS classes or selectors
- HTML structure
- Blazor components or Razor syntax
- Framework-specific patterns
- Database schema
- API contracts

## Avoid UI Specification

The Design System must NOT define:

- Specific screen layouts
- Button placements
- Navigation structure
- Workspace composition
- Page wireframes
- User flow diagrams

These topics belong to:

- docs/09-ui/ (UI documentation)
- docs/10-presentation/ (presentation architecture)
- docs/00-standards/111-interaction-philosophy.md (interaction philosophy)

## Avoid Component Documentation

The Design System must NOT specify:

- Component API
- Component properties or parameters
- Component states
- Component behavior in different contexts
- Component rendering logic

## Avoid Framework-Specific Details

The Design System must NOT reference:

- Blazor
- CSS
- HTML
- JavaScript
- .NET
- Any other framework or technology

---

# Prohibited Topics

The generated Design System MUST NOT contain any of the following:

| Topic | Why It Is Prohibited |
|-------|---------------------|
| Colors | Belongs to Color Standard (161) |
| Typography | Belongs to Typography Standard (162) |
| Spacing | Belongs to Layout and Spacing Standard |
| Buttons | Belongs to Component Standard |
| Inputs | Belongs to Component Standard |
| Dialogs | Belongs to Component Standard |
| Components | Belongs to Component Standard and Component Contract (docs/10-presentation/04-component-contract.md) |
| Icons | Belongs to Visual Design Standards |
| Motion | Belongs to Motion Standard |
| Design Tokens | Belongs to Design Token Standard |
| CSS | Implementation detail owned by Presentation Documentation |
| HTML | Implementation detail owned by Presentation Documentation |
| Blazor | Framework-specific, owned by Implementation Documentation |
| Implementation | Outside design scope, owned by Implementation Documentation |

---

# Consistency Requirements

The generated Design System must be consistent with:

## Business Foundation

- Customer before system
- Restaurant before software
- Kitchen is the boundary
- Payment is not the end
- Service recovery is a design concern

## Information Hierarchy

Derived from docs/00-standards/ (information hierarchy principles).

The Design System must reflect that information hierarchy drives layout, not the reverse.

## Component Rules

Derived from docs/00-standards/ (component rules).

The Design System must define component philosophy at the design level, not component API.

## AI Constitution

Derived from docs/97-AI-Docs/02-ai-constitution.md.

The Design System must not contradict any constitutional principle.

## Reference Modules

Derived from docs/97-AI-Docs/100-reference-modules.md.

The Design System must align with the architectural patterns established by reference modules.

---

## Output Structure

The generated document must follow the section hierarchy defined in:

docs/160-design/160-design-system-outline.md

Each section must:

- Follow the established canonical writing style
- Read naturally as part of a continuous architectural book
- Introduce new knowledge appropriate to its position
- Build upon previous sections
- Remain within the constitutional scope of the document
- Avoid authoring metadata or instructional content

-----

## Output Target

The generated document must meet the following size target:

- **Target size:** 8,000–12,000 words
- **Estimated pages:** 20–30 pages (printed)
- **Section distribution:** All 9 sections must be present. No section should exceed 30% of the total document length.
- **Minimum section length:** No section should be shorter than 200 words.

This target prevents:
- Excessively short output that omits necessary principles
- Excessively long output that descends into implementation detail
- Unbalanced sections where one topic dominates

---

# Reusability

This prompt is designed to be reusable by future AI models.

A future AI must be able to:

1. Read this prompt
2. Read the Mandatory Reading documents
3. Generate a complete Design System document without additional context

To ensure reusability:

- All document paths are absolute within the repository
- All required concepts are explicitly listed
- No implicit knowledge is assumed
- No conversational context is required

---

# Self-Verification Checklist

After generating the Design System, the AI must verify all of the following.

## General Verification

- [ ] All principles derived from project documentation, not invented
- [ ] No generic design philosophy present
- [ ] WHY explained before HOW in every principle
- [ ] No implementation details
- [ ] No UI specifications
- [ ] No component documentation
- [ ] No framework-specific references
- [ ] No prohibited topics (colors, typography, spacing, buttons, inputs, dialogs, components, icons, motion, design tokens, CSS, HTML, Blazor)
- [ ] Terms consistent with project glossary
- [ ] Cross-references to verified document paths
- [ ] Section hierarchy matches outline (160-design-system-outline.md)
- [ ] Professional Markdown formatting
- [ ] No placeholders, TODOs, or filler text

## Progressive Verification

- [ ] Builds upon previous sections
- [ ] Introduces new knowledge in each section
- [ ] No repeated definitions from earlier sections
- [ ] No repeated examples from earlier sections
- [ ] No forward references to future sections
- [ ] No summaries of previous sections

## Book Verification

- [ ] Reads naturally as a continuous architectural book
- [ ] Smooth transitions between sections
- [ ] No authoring metadata (Objective, Key Messages, Must Mention, Must Avoid, Expected Length)
- [ ] No AI prompt language or instructional tone
- [ ] No generated-text feeling — reads as professionally authored content
- [ ] Content ready for human review

# Related Documents

| Document | Relationship |
|----------|-------------|
| docs/160-design/160-design-system-outline.md | Section hierarchy to follow |
| docs/160-design/160-writing-guide.md | Writing standard to follow |
| docs/01-business-foundation/00-business-foundation.md | Business philosophy source |
| docs/00-standards/ | Information hierarchy and component rules |
| docs/97-AI-Docs/02-ai-constitution.md | Constitutional constraints |
| docs/97-AI-Docs/98-ai-context.md | Project context |
| docs/97-AI-Docs/100-reference-modules.md | Reference module patterns |
| docs/README.md | Project overview |

---

Version: 1.1

Status: Production

---

# Document Ownership

**Owner:** Design Documentation Series

**Review Cycle:** Every major change to the Design Series generation process

**Canonical Location:** docs/160-design/160-generation-prompt.md

---

# Change Log

| Version | Date | Change |
|---------|------|--------|
| 1.0 | 2026-07-23 | Initial generation prompt created |
| 1.1 | 2026-07-23 | Added Book-First authoring model, progressive writing rules, non-repetition policy, canonical documentation ownership, and refined self-verification with General/Progressive/Book categories |
