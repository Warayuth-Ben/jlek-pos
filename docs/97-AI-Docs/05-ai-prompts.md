# AI Prompt Library

Version: 1.0

Project: JLek POS

---

# Purpose

This document contains reusable prompts for AI.

All prompts assume that

- AI Engineering Standard
- AI Constitution
- AI Workflow

have already been read.

The AI must continue following those documents.

---

# Analysis Mode

Objective

Understand the current implementation.

Rules

- Read documentation first.
- Verify repository evidence.
- Do not modify files.
- Do not write code.
- Stay within scope.

Output

- Verified Facts
- Findings

No Recommendations unless requested.

---

# Code Review Mode

Objective

Review existing implementation.

Check

- Business Rules
- DDD
- Clean Architecture
- API Design
- Layering
- Error Handling
- Naming

Output

- Verified Facts
- Findings
- Potential Risks

Do not recommend fixes unless requested.

---

# Architecture Review Mode

Objective

Verify architecture compliance.

Check

- Layer dependencies
- Aggregate boundaries
- Repository usage
- CQRS
- DDD

Output

- Verified Facts
- Architecture Findings

If no violations exist

report

"No verified architecture violation found."

---

# Gap Analysis Mode

Objective

Compare implementation with documentation.

Output

A. Implemented

B. Partially Implemented

C. Missing

Every item must include

- Evidence
- Repository Path
- Documentation Reference (Verified Only)

---

# Inventory Mode

Objective

Inventory an existing module.

Report

- Classes
- Interfaces
- Commands
- Queries
- DTOs
- Events
- Repositories
- Business Rules

No recommendations.

---

# Documentation Review Mode

Objective

Review project documentation.

Check

- Consistency
- Duplicates
- Missing sections
- Broken references

Only verified references may be cited.

---

# Business Rule Review Mode

Objective

Verify Business Rules.

Rules

Business Rules may only come from

- Documentation
- Domain Layer

Never infer Business Rules.

Output

- Verified Rules
- Missing Rules
- Conflicts

---

# Repository Inspection Mode

Objective

Inspect repository structure.

Verify

- folders
- files
- namespaces
- project layout

Never infer missing files.

If an item cannot be verified

respond

Not Verified.

---

# DTO Planning Mode

Objective

Plan DTO implementation.

Output

- Existing Domain Model
- Required DTOs
- Mapping Strategy
- Impact

Do not implement.

---

# Implementation Planning Mode

Objective

Prepare implementation.

Explain

- Solution
- Risks
- Impact
- Architecture
- Business Rules

Wait for approval.

Do not write code.

---

# Implementation Mode

Objective

Implement one approved milestone.

Rules

- Read documentation first.
- Verify repository.
- Preserve architecture.
- Preserve Business Rules.
- Reuse existing implementation.
- Stop after one milestone.

Output

- Summary
- Files Modified
- Self Review

---

# Refactoring Mode

Objective

Improve existing code.

Rules

Refactoring is allowed only when

- explicitly approved
- architecture is preserved
- Business Rules are unchanged

Report

- Verified Facts
- Refactoring Plan

Wait for approval.

---

# Self Review Mode

Before finishing

verify

- Build
- Architecture
- Business Rules
- API Contracts
- Documentation

Separate

Verified Facts

Findings

Recommendations

---

# Engineering Report Format

Every report should follow

## Verified Facts

Facts confirmed by evidence.

## Findings

Conclusions derived from verified facts.

## Recommendations

Only when requested.

---

# Standard Response

When evidence is insufficient

respond

Insufficient information.

Include

- Verified information
- Missing information
- Required clarification

Never guess.

---

# Golden Prompt

Read documentation.

Verify evidence.

Understand business.

Respect architecture.

Wait for approval.

Implement one milestone.

Self review.

Recommend documentation updates.

Stop.
--------
Repository Evidence has higher priority than general knowledge.

If repository evidence conflicts with model knowledge,

repository evidence wins.

If repository evidence is missing,

respond "Not Verified."
