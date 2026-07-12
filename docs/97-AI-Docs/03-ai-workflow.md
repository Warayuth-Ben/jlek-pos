# AI Workflow

Version: 1.0

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

Self Review

↓

Documentation Update

---

# Phase 1 — Documentation

Objective

Understand the project before making conclusions.

Required Actions

- Read 00-start-here.md
- Read AI Engineering Standard
- Read AI Constitution
- Read AI Context
- Read Project Status
- Read only relevant project documentation

Output

Verified understanding.

---

# Phase 2 — Verification

Objective

Verify all information.

Required Actions

Verify

- Documentation
- Repository
- Source Code
- Architecture

Rules

Never verify from memory.

Never verify from assumptions.

Output

Verified Facts.

---

# Phase 3 — Understanding

Objective

Understand the problem.

The AI must explain

- Business Problem
- Business Rules
- Existing Design
- Constraints

If these cannot be explained,

STOP.

---

# Phase 4 — Analysis

Objective

Analyze only.

Allowed

- Read documentation
- Read source code
- Produce reports
- Ask questions

Forbidden

- Modify files
- Generate implementation
- Refactor code

Output

Verified Facts

Findings

---

# Phase 5 — Design

Objective

Design a solution.

The AI must explain

- Proposed approach
- Impact
- Risks
- Alternatives

No code.

Output

Design Proposal.

---

# Phase 6 — Human Review

Objective

Receive feedback.

The AI must wait for

- Questions
- Corrections
- Approval

The AI must not continue automatically.

---

# Phase 7 — Approval

Implementation begins only after explicit approval.

Examples

Approved

Continue

Implement

Proceed

Without approval,

STOP.

---

# Phase 8 — Implementation

Rules

Implement only

the approved milestone.

Requirements

- Preserve architecture
- Preserve Business Rules
- Reuse existing code
- Keep changes minimal

The AI must stop immediately after implementation.

---

# Phase 9 — Self Review

Before completion,

verify

- Build consistency
- Architecture
- Business Rules
- DDD
- API Contracts
- Documentation

Separate

Verified Facts

Findings

Recommendations

---

# Phase 10 — Documentation Update

If implementation changes

- Business Rules
- Workflow
- Architecture
- Public APIs

the AI must recommend documentation updates.

Implementation is not complete

until documentation impact has been considered.

---

# Engineering Workflow Summary

Read

↓

Verify

↓

Understand

↓

Analyze

↓

Design

↓

Review

↓

Approve

↓

Implement

↓

Review

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

Response

Insufficient information.

State

- What is verified
- What is missing
- What requires clarification

Never continue by guessing.

---

# Deliverable Format

Every report should follow this structure

## Verified Facts

Only verified information.

## Findings

Conclusions derived from verified facts.

## Recommendations

Optional.

Only after approval or when explicitly requested.
