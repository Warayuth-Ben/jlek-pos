# AI Workflow

Version: 1.1

Project: JLek POS

---

# Purpose

This document defines the standard workflow that every AI must follow when participating in the JLek POS project.

The workflow ensures that every implementation is

- understandable
- reviewable
- traceable
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

## Required Actions

Read

- 00-start-here.md
- AI Engineering Standard
- AI Constitution
- AI Workflow
- AI Context
- Project Status
- Only the documentation relevant to the requested scope

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

## Rules

Never verify from memory.

Never verify from assumptions.

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

## Rules

Do not redesign before understanding the current implementation.

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

If these cannot be explained,

STOP.

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

## Output

Design Proposal.

---

# Phase 7 — Human Review

## Objective

Receive feedback.

The AI must wait for

- Questions
- Corrections
- Approval

The AI must not continue automatically.

---

# Phase 8 — Approval

Implementation begins only after explicit approval.

Examples

- Approved
- Continue
- Proceed
- Implement

Without approval,

STOP.

---

# Phase 9 — Implementation

## Rules

Implement only

the approved milestone.

## Requirements

- Preserve architecture
- Preserve Business Rules
- Reuse existing code
- Keep changes minimal

The AI should recommend creating a Git checkpoint before implementation whenever practical.

The AI must stop immediately after implementation.

---

# Phase 10 — Build Verification

## Objective

Verify that the implementation compiles successfully.

## Requirements

- Build succeeds
- No unexpected compile errors

If the build fails,

stop and resolve the issue before continuing.

---

# Phase 11 — Runtime Verification

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

# Phase 12 — Self Review

Before completion,

verify

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

# Phase 13 — Documentation Update

If implementation changes

- Business Rules
- Workflow
- Architecture
- Public APIs

the AI must recommend documentation updates.

Implementation is not complete

until documentation impact has been considered.

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

# Engineering Workflow Summary

Read

↓

Verify

↓

Review Existing Implementation

↓

Understand

↓

Analyze

↓

Design

↓

Human Review

↓

Approval

↓

Implement

↓

Build

↓

Runtime Verification

↓

Self Review

↓

Update Documentation

---

# Stop Conditions

The AI must stop immediately when

- Documentation is insufficient
- Repository cannot verify information
- Business Rules are unclear
- Architecture is unclear
- Human approval has not been received

## Response

Insufficient information.

State

- What is verified
- What is missing
- What requires clarification

Never continue by guessing.

---

# Deliverable Format

Every report should follow this structure.

## Verified Facts

Only verified information.

## Findings

Conclusions derived from verified facts.

## Recommendations

Optional.

Provide recommendations only after approval or when explicitly requested.

-----
## Verification Checklist

Before referring to any project artifact:

- [ ] File exists
- [ ] Folder exists
- [ ] Class exists
- [ ] Method exists
- [ ] Business Rule exists
- [ ] Documentation exists

If any item cannot be verified:

Report

> Not yet verified.