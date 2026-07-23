# 160 — Writing Guide

Version: 1.0

Series: Design (160–199)

Canonical Source: docs/160-design/160-writing-guide.md

---

# Purpose

This document defines the permanent writing standard for the Design Documentation Series.

Every document in the Design Series (160–199) must follow this guide.

This guide applies to both human writers and AI systems generating Design Series content.

---

# Design Series Ownership

The Design Series follows a strict dependency hierarchy.

```
Business
    ↓
Architecture
    ↓
Design Constitution (160)
    ↓
Design Standards (161-199)
    ↓
Implementation
```

Dependencies flow downward only.

Never the reverse.

No Design document may depend on an Implementation document.

No Design Standard (161-199) may contradict the Design Constitution (160).

## Document Owner

Every Design Series document must declare a **Document Owner** — the person, team, or role responsible for maintaining that document.

## Document Responsibility

Every Design Series document must declare its **Document Responsibility** — what it is responsible for deciding, and what it is NOT responsible for deciding.

## Relationship with Other Design Documents

Every Design Series document must declare its relationship with other Design documents. Relationships include:

- **Constitutional parent:** 160-design-system.md owns the design principles that constrain this document
- **Dependent documents:** which documents depend on this one
- **Peer documents:** which documents share the same level and must be consistent

## Document Dependency Direction

Design documents must only reference documents at the same level or higher in the hierarchy.

A Design Standard (161) may reference:
- The Design Constitution (160) — ✅ allowed
- Architecture documents — ✅ allowed
- Business Foundation — ✅ allowed
- Another Design Standard (163) — ✅ allowed if consistent

A Design Standard (161) must NOT reference:
- Implementation documents — ❌ prohibited
- UI component code — ❌ prohibited
- CSS files — ❌ prohibited

---

# Document Naming Convention

## Numbering Rule

Design Series documents use the prefix range **160-199**.

No document outside this range may be placed in the `docs/160-design/` directory.

The numbering reflects the hierarchy and dependency order:

| Range | Purpose | Example |
|-------|---------|---------|
| 160 | Design Constitution (single document) | 160-design-system.md |
| 161-169 | Visual Design Standards | 161-color-system.md, 162-typography.md |
| 170-179 | Component Design Standards | 170-component-philosophy.md |
| 180-189 | Screen and Layout Standards | 180-screen-blueprints.md |
| 190-199 | Interaction and Behavior Standards | 190-interaction-patterns.md |

## Numbering Rules

1. **160 is reserved** for the Design Constitution only. No other document may use 160.
2. Documents 161-199 may be created as needed.
3. Numbers are assigned sequentially within each range.
4. A single number may be reused only if the original document is deprecated.
5. Gaps in numbering are intentional. They allow future documents to be inserted without renumbering.
6. The filename must match the number. Example: `161-color-system.md`, not `161-colors.md` if the title is "Color System".

## File Naming

- Use lowercase hyphenated names.
- The prefix number must match the document's series position.
- Example: `162-typography.md` for document 162 titled "Typography".

---

# Design Series Boundary

## What belongs in the Design Series

- Design principles and philosophy (160)
- Specialized design standards (161-199)
- Design-level specifications derived from the Design Constitution
- Cross-feature design consistency rules

## What does NOT belong in the Design Series

- Business philosophy (owned by docs/01-business-foundation/)
- Architecture decisions (owned by docs/100-architecture/ and ADRs)
- Implementation code (owned by src/)
- CSS specifications (owned by CSS architecture)
- Framework-specific documentation (owned by implementation guides)
- API contracts (owned by ADR-010)
- Testing strategy (owned by docs/10-testing/)

---

# Language Policy

## Canonical documentation language

All Design Series documentation must be written in **English**.

This includes:
- Design principles
- Rationale and explanations
- Rules and guidelines
- Technical identifiers
- File paths
- Document titles
- Terminology

## Allowed exceptions

The following may use Thai:
- Business examples that reference specific restaurant scenarios
- UI labels when documenting real interface text
- Quotes from Business Foundation documents (which are written in Thai)

## Consistency requirement

Terminology must remain consistent regardless of language.

A concept defined in English must use the same English term throughout all documents, including in Thai examples.

---

# Writing Style

## Tone

- Authoritative and precise.
- No marketing language.
- No persuasive language.
- No speculative statements.
- No filler text.

## Sentence Structure

- Use short sentences.
- Use one idea per sentence.
- Use bullet points for lists of related items.
- Use numbered lists for sequential steps.

## Voice

- Active voice preferred.
- Passive voice allowed only when the actor is unknown or irrelevant.

---

# Terminology Rules

## Consistency

- Every concept must have exactly one term.
- Never use synonyms for the same concept within the Design Series.
- If a term is defined in docs/04-ai-glossary.md or docs/00-standards/glossary.md, use that definition.

## Prohibited Practices

- Do not invent new terms for existing concepts.
- Do not rename existing concepts.
- Do not use generic design terminology that contradicts project-specific definitions.

## Cross-Reference Terminology

- When referencing a concept owned by another document, use the exact term from that document.
- Include a cross-reference to the source document.

---

# Document Structure

## Required Metadata

Every Design Series document MUST include at the top:

- Title (with series number, e.g., "160 — Writing Guide")
- Version number
- Series identifier (Design, 160–199)
- Canonical source path

## Required Sections

Every Design Series document MUST include the following sections in order:

1. **Purpose** — Why the document exists (one paragraph)
2. **Scope** — What the document covers and does not cover
3. **Audience** — Who should read this document
4. **Content** — The document body
5. **Related Documents** — Cross-references to other canonical documents
6. **Versioning** — Version history
7. **Change Log** — Table of changes

## Section Template

Each major content section must follow this template:

### Section Title

**Objective**

One sentence describing what this section achieves.

**Key Messages**

Bullet list of the most important points in this section.

**Must Mention**

Topics that MUST be covered in this section.

**Must Avoid**

Topics that MUST NOT be covered in this section.

**Reference Documents**

List of documents that inform this section.

**Expected Length**

Estimated word count or page count for this section.

---

# Information Hierarchy Rules

## Priority

1. Business principles
2. Design principles
3. Architecture constraints
4. Implementation guidance

Content at a lower level must never contradict content at a higher level.

## Separation

- One concept must have exactly one source of truth.
- Do not duplicate information from other documents.
- If information from another document is needed, cross-reference it instead of copying it.

## Depth

- Design Series documents operate at the design level.
- Do not descend into implementation details.
- Do not ascend into business principles already covered by Business Foundation.

---

# Canonical Documentation Rules

- Every Design Series document must exist at exactly one path.
- No two documents may own the same concept.
- If a concept must be referenced in multiple documents, one document owns it and the others cross-reference it.

## Document Ownership

Each Design Series document must declare:

- **Owner:** The person or team responsible for the document
- **Review Cycle:** How often the document must be reviewed
- **Canonical Location:** The file path

---

# Cross Reference Rules

## Purpose

Cross-references prevent duplication and maintain canonical ownership.

Every major topic referenced in a Design document must identify:

- **Canonical Owner** — the document that owns this topic
- **Related Documents** — other documents that reference the same topic
- **Topics that must NOT be duplicated** — specific concepts that must not be copied into this document

## Format

Use the following format for cross-references:

[Document Title](path/to/document.md) — brief description of relationship

## Requirements

- Every cross-reference must be a verified path.
- Every cross-reference must include a brief description of why the reference exists.
- Do not cross-reference documents that do not exist.
- Every topic that appears in more than one document must have exactly one Canonical Owner.
- If a topic is owned by another document, this document must NOT duplicate the topic's content. Only reference it.

## Canonical Owner Identification

Every Design Series document section that covers a topic with an external owner MUST include:

```
**Canonical Owner:** [path/to/owning-document.md]
**Must Not Duplicate:** [list of specific concepts that must not be duplicated]
```

## Cross-Reference Inventory

Each Design Series document should maintain a Related Documents table listing:

- Document path
- Relationship (e.g. "Constitutional parent", "Source of principles", "Peer document")
- Dependency direction (upward or lateral)

---

# Contributor Guidance

## When to create a new document

Create a new Design Series document when:
- A new design domain needs to be standardized (e.g., color system, typography)
- A concept has grown complex enough that documenting it within an existing document would violate single responsibility
- A new design principle or rule requires its own canonical source of truth

## When to extend an existing document

Extend an existing document when:
- The new content is a natural expansion of the document's existing responsibility
- The new content does not create duplication with another document
- The document's scope explicitly includes the new topic
- The document is at the same level of abstraction as the new content

## When NOT to create a new document

Do NOT create a new document when:
- The topic is already owned by another document — extend that document instead
- The topic belongs to a different series (Business, Architecture, Implementation) — place it there
- The topic is a specific implementation detail — it belongs in source code or implementation documentation
- The topic can be a single section in an existing document — prefer a section over a new document

## One Concept, One Owner, One Source of Truth

Every concept in the Design Series must follow this rule:

- **One Concept** — one distinct idea, principle, rule, or standard
- **One Owner** — exactly one document is responsible for it
- **One Source of Truth** — that document is the only authoritative source

If a concept appears in multiple documents, only one document owns it. All other documents must cross-reference the owner.

## Scalability

This section is designed for the full 160-199 range. The rules above remain stable regardless of how many Design documents exist.

Adding a new document (e.g., 165-button-philosophy.md) requires:
1. A new file in docs/160-design/ following the Document Naming Convention
2. A cross-reference from the Design Constitution (160) to the new document
3. No changes to this Writing Guide
4. No changes to the Generation Prompt (unless creating a new prompt for the new document)
5. No changes to the Outline (unless the new document represents a new section category)

---

# Review Process

## Review Stages

1. **Self Review** — Writer verifies compliance with this Writing Guide
2. **Peer Review** — Another contributor reviews for consistency and accuracy
3. **Editorial Review** — Editor checks style, terminology, and structure
4. **Approval** — Document owner approves for publication

## Review Checklist

Every Design Series document MUST pass the following checks before publication:

- [ ] Purpose clearly stated
- [ ] Scope clearly defined
- [ ] No duplicated responsibilities with other documents
- [ ] Consistent terminology with project glossary
- [ ] All cross-references verified
- [ ] No implementation details
- [ ] No placeholders
- [ ] No TODO items
- [ ] Professional Markdown formatting
- [ ] Version number present
- [ ] Change log populated

---

# Definition of Done

A Design Series document is complete only when:

- All required sections are present
- Content is production-ready (no drafts, no TODOs)
- No duplicated responsibilities exist
- All cross-references are verified
- Document passes the Review Checklist
- Document is committed to Git
- Version number is set

---

# Versioning Rules

## Version Number Scheme

- Major version: Breaking structural changes (sections added or removed)
- Minor version: Content updates without structural changes
- Patch: Fixes, corrections, formatting

## Version Location

Version number must appear at the top of the document, below the title.

## Change Log

Every Design Series document must maintain a Change Log table.

| Version | Date | Change |
|---------|------|--------|
| 1.0 | YYYY-MM-DD | Initial version |

---

# Writing Checklist

Before submitting a Design Series document, verify:

- [ ] Title matches the series number and file name
- [ ] Purpose section present
- [ ] Scope section present
- [ ] Audience section present
- [ ] All Required Sections present
- [ ] Each section uses the Section Template
- [ ] No section contains implementation details
- [ ] No section duplicates content from other documents
- [ ] All cross-references use verified paths
- [ ] Terminology matches project glossary
- [ ] No invented terms
- [ ] No placeholders or TODOs
- [ ] Professional Markdown
- [ ] Version number set
- [ ] Change Log populated

---

# Review Checklist

## Editorial Checklist

- [ ] Grammar and spelling correct
- [ ] Consistent formatting throughout
- [ ] Bullet lists use consistent punctuation
- [ ] Headings follow consistent hierarchy
- [ ] No broken cross-references
- [ ] No orphaned sections

## AI Checklist

- [ ] All facts derived from project documentation
- [ ] No invented principles
- [ ] No generic design philosophy not specific to this project
- [ ] Cross-references verified against repository
- [ ] No implementation specifications
- [ ] No UI component specifications
- [ ] No framework-specific references (Blazor, CSS, HTML, etc.)

## Contributor Checklist

- [ ] Document purpose is clearly stated
- [ ] Document scope is clearly bounded
- [ ] Document does not conflict with existing documentation
- [ ] All Related Documents are listed
- [ ] Document is ready for Git commit
- [ ] Long-term maintainability is ensured

---

# Related Documents

| Document | Relationship |
|----------|-------------|
| docs/00-standards/writing-style.md | Project-wide writing style |
| docs/00-standards/document-templates.md | Document template standards |
| docs/04-ai-glossary.md | Project glossary |
| docs/97-AI-Docs/01-ai-engineering-standard.md | AI engineering standards |
| docs/160-design/160-design-system-outline.md | Design System outline |
| docs/160-design/160-generation-prompt.md | AI generation prompt |

---

# Versioning

Version: 1.0

Status: Draft

---

# Document Ownership

**Owner:** Design Documentation Series

**Review Cycle:** Every major change to the Design Series

**Canonical Location:** docs/160-design/160-writing-guide.md

---

# Change Log

| Version | Date | Change |
|---------|------|--------|
| 1.0 | 2026-07-23 | Initial writing guide created |