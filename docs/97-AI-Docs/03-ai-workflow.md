# AI Workflow

Version: 1.3

Project: JLek POS

---

# Purpose

This document defines the standard workflow that every AI must follow when participating in the JLek POS project.

The workflow ensures that every implementation is

- understandable
- reviewable
- traceable
- verifiable
- safe

The AI must never skip workflow steps.

---

# Development Workflow

Documentation

↓

Verification

↓

Existing Implementation Review

↓

Understanding

↓

Analysis

↓

Design

↓

Evidence Audit

↓

Human Review

↓

Approval

↓

Implementation

↓

Build Verification

↓

Runtime Verification

↓

Self Review

↓

Documentation Update

---

# Phase 1 — Documentation

## Objective

Understand the project before making conclusions.

## Context Reuse

If AI onboarding has already been completed in the current conversation,
and the verified context remains available,
the AI must reuse the existing context.

Do not repeat onboarding unnecessarily.

Re-onboarding is required only when:

- Context has been lost
- Conversation restarted
- Repository changed
- Documentation updated
- Human explicitly requests re-onboarding

## Required Actions

If onboarding has not been completed in this conversation:

Read

- 00-start-here.md
- AI Engineering Standard
- AI Constitution
- AI Workflow
- AI Context
- Project Status
- Only the documentation relevant to the requested scope

If onboarding has been completed and context is verified:

- Reuse existing context.
- Read only the documentation relevant to the requested scope.
- Do not re-read previously verified documents unless:
  - the scope changes,
  - documentation changes,
  - repository changes,
  - or context has been lost.

## Documentation Priority

When determining business requirements:

Priority

1. Business Documentation (docs/)
2. AI Documentation (docs/97-AI-Docs/)
3. Source Code
4. Engineering Recommendations

Business documentation is the primary source for business requirements.

Source code is the primary source for implementation verification.

If documentation and implementation differ:

- Report the discrepancy.
- Do not automatically choose either one.

## Output

Verified understanding.

---

# Phase 2 — Verification

## Objective

Verify all information.

## Required Actions

Verify

- Documentation
- Repository
- Source Code
- Architecture

## Verification Scope

Before verification, explicitly define:

- Documentation Scope
- Repository Scope
- Search Scope

Examples

Documentation

- docs/01-business-rules/
- docs/02-domain-model/
- docs/03-system-use-cases/
- docs/04-state-machines/

AI Documentation

- docs/97-AI-Docs/

Repository

- src/
- or the relevant project directory (for example: src/JLek.POS.Domain)

Search Scope

- Exact path
- Immediate directory
- Recursive repository
- Filename
- Content
- Symbol

Never use an undefined scope.
## Previously Verified Evidence

Before concluding that a repository artifact does not exist:

- Review previously verified evidence collected during the current task.
- Previously verified evidence has higher priority than a failed search result.

If a contradiction exists:

- Report the inconsistency.
- Do not conclude non-existence until resolved.

Repository evidence collected during the current task has higher priority than assumptions.

Previously verified evidence must not be discarded because of one failed search.

## Rules

Never verify from memory.

Never verify from assumptions.

Never infer project structure from common software patterns.

Never assume the existence of

- files
- folders
- classes
- interfaces
- methods
- commands
- queries
- handlers
- repositories
- aggregates
- APIs

without repository verification.

## Output

Verified Facts.

---

# Phase 3 — Existing Implementation Review

## Objective

Understand how the current implementation works before proposing changes.

## Required Actions

Review only the implementation related to the requested scope.

Examples

- Endpoint
- Request DTO
- Response DTO
- Command
- Query
- Handler
- Repository
- Aggregate
- Existing Mapping

## Repository Search Strategy

When locating existing implementations, use the following priority:

1. Exact path
2. Directory navigation
3. Filename search
4. Content search
5. Symbol search

If one strategy fails, continue using the next applicable strategy.

Do not conclude non-existence from a single failed search.

## Repository Navigation Rules

When navigating or counting repository artifacts, always specify the intended scope.

Examples:

- Immediate directory only
- Recursive
- Filename search
- Content search
- Exact path access

Never assume recursive traversal unless explicitly requested.

If the scope is ambiguous, ask for clarification or explicitly state the assumed scope before proceeding.

## Tool Usage Guidelines

Before using repository tools, explicitly define:

- Operation
  - Read
  - Search
  - Navigate
  - Inventory

- Scope
  - Exact file
  - Immediate directory
  - Recursive repository

- Search Type
  - Filename
  - Content
  - Symbol

- Verification Method
  - Read file
  - Directory traversal
  - Search tool
  - Build verification

## Rules

Do not redesign before understanding the current implementation.

Reference only verified implementation.

## Output

Verified implementation summary.

---

# Phase 4 — Understanding

## Objective

Understand the problem.

The AI must explain

- Business Problem
- Business Rules
- Existing Design
- Constraints

If these cannot be explained:

STOP.

## Output

Verified understanding.

---

# Phase 5 — Analysis

## Objective

Analyze only.

## Allowed

- Read documentation
- Read source code
- Produce reports
- Ask questions

## Forbidden

- Modify files
- Generate implementation
- Refactor code

## Output

Verified Facts

Findings

---

# Phase 6 — Design

## Objective

Design a solution.

The AI must explain

- Proposed approach
- Impact
- Risks
- Alternatives

No implementation before approval.

The proposal must be based only on verified facts.

## Output

Design Proposal.

---

# Phase 7 — Evidence Audit

## Objective

Verify that every statement in the proposal is supported by evidence.

## Required Actions

Review the complete proposal.

Verify every reference to

- Documentation
- Source Code
- Files
- Folders
- Classes
- Interfaces
- Methods
- Commands
- Queries
- Handlers
- APIs
- Business Rules
- Architecture

## Rules

Never reference an unverified project artifact.

If any referenced artifact cannot be verified:

replace it with

> Not yet verified.

Confidence is not evidence.

Unknown information must never be presented as fact.

## Output

### Verified Facts

### Unverified Items

### Corrections

---

# Phase 8 — Human Review

## Objective

Receive feedback.

The AI must wait for

- Questions
- Corrections
- Approval

The AI must not continue automatically.

---

# Phase 9 — Approval

## Objective

Implementation begins only after explicit approval.

Examples

- Approved
- Continue
- Proceed
- Implement

Without approval:

STOP.

---

# Phase 10 — Implementation

## Rules

Implement only the approved milestone.

## Requirements

- Preserve architecture
- Preserve Business Rules
- Reuse existing code
- Keep changes minimal

The AI should recommend creating a Git checkpoint before implementation whenever practical.

The AI must stop immediately after implementation.

After completing the approved feature, the AI must not automatically recommend implementation of future milestones.

Recommendations must remain within the currently approved milestone unless explicitly requested by the human.

---

# Phase 11 — Build Verification

## Objective

Verify that the implementation compiles successfully.

## Requirements

- Build succeeds
- No unexpected compile errors

If the build fails:

stop and resolve the issue before continuing.

---

# Phase 12 — Runtime Verification

## Objective

Verify the implementation at runtime.

Examples

- Swagger
- HTTP Status Codes
- Request DTO
- Response DTO
- Route Verification

Implementation is not complete until runtime verification succeeds.

---

# Phase 13 — Self Review

## Objective

Before completion, verify

- Build consistency
- Architecture
- Business Rules
- DDD
- API Contracts
- Documentation

Separate

## Verified Facts

## Findings

## Recommendations

---

# Phase 14 — Documentation Update

## Objective

If implementation changes

- Business Rules
- Workflow
- Architecture
- Public APIs

the AI must recommend documentation updates.

Implementation is not complete until documentation impact has been considered.

---

# Milestone Rule

Each implementation should complete only one small milestone.

Preferred examples

- One endpoint
- One business capability
- One aggregate behavior

Avoid implementing multiple features in a single milestone.

Small milestones improve

- reviewability
- traceability
- rollback safety

---

# Stop Conditions

The AI must stop immediately when

- Documentation is insufficient
- Repository cannot verify information
- Business Rules are unclear
- Architecture is unclear
- Human approval has not been received

## Response

State

## Verified Facts

## Missing Information

## Clarification Required

Never continue by guessing.

---

# Deliverable Format

Every report should follow this structure.

## Verified Facts

Only information supported by verified evidence.

## Findings

Derived only from verified facts.

## Unverified Items

List anything that could not be verified.

Use

> Not yet verified.

## Recommendations

Optional.

Provide recommendations only after approval or when explicitly requested.

---

# Verification Checklist

Before referring to any project artifact:

verify

- [ ] File exists
- [ ] Folder exists
- [ ] Class exists
- [ ] Interface exists
- [ ] Method exists
- [ ] Business Rule exists
- [ ] Documentation exists

If any item cannot be verified:

- Do not reference it.
- Do not infer it.
- Do not generate implementation from it.

Instead report

> Not yet verified.

AI confidence is never considered evidence.

---------
หาก Business Scenario Q001–Q146
ไม่มีการเปลี่ยนแปลง

AI ต้องไม่เสนอ

- Feature ใหม่
- Module ใหม่
- Bounded Context ใหม่
- Workflow ใหม่

AI สามารถเสนอได้เฉพาะ

- การทำให้ Scenario เดิมสมบูรณ์
- การลดความซ้ำซ้อน
- การเพิ่มคุณภาพ
- การเพิ่มความครอบคลุมของการทดสอบ
- การปรับปรุง UX โดยไม่เปลี่ยน Business Workflow
--------

# AI Restrictions

Never

- redesign approved architecture
- redesign approved UI
- rename existing concepts
- create new abstractions without evidence
- refactor unrelated code
- fix unrelated issues
- optimize outside the current scope

If an issue is found outside the approved scope

Report it as

Out of Scope

Do not implement it.

---

If documentation and implementation differ

Never guess.

Report the discrepancy.

Wait for Human Decision.

---

If repository evidence contradicts previous assumptions

Repository evidence wins.

Update the report.

Do not continue until consistency is restored.
--------

# Scope Lock
Current Task Scope is authoritative.

Never expand the implementation scope.

If additional work is discovered,

report it as

Future Work

Do not implement it.
----------
# Evidence First

Every implementation decision

must be supported by evidence from

- Business Documentation
- Approved ADR
- Repository

Never rely on assumptions.
----------
# Stop on Uncertainty

If multiple valid implementations exist

and no approved documentation specifies one,

STOP.

Present the alternatives.

Wait for Human Decision.

----------
# No Silent Changes
Never change

- naming
- folder structure
- architecture
- API contract
- workflow

without explicitly reporting the reason.

Every intentional change must be documented.
--------



