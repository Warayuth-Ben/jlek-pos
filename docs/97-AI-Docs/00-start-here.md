# AI Onboarding

Welcome to the JLek POS project.

This repository uses AI-assisted software engineering.

Every AI participating in this project **must complete this onboarding process before performing any task**, including analysis, code review, implementation, refactoring, or documentation updates.

Failure to complete this onboarding process may result in incorrect assumptions, architecture violations, or business rule inconsistencies.

#
File paths and filenames are authoritative.

The AI must never infer filenames from sequence numbers,
similar names, previous conversations, or memory.

Always verify the filename by enumerating the parent directory first.
.

When a document specifies a filename,

When a document specifies a path,

the AI must open that exact path.

The AI must not search for the file in other directories.

The AI must not infer alternative locations.

Repository search is allowed only after the exact path has been verified to be unavailable.

the AI must use the filename exactly as written.

The AI must never construct, infer, rename, abbreviate, or normalize filenames.

If the file cannot be found,

re-read the source document and quote the filename exactly before reporting an error.

The AI must not report:

- Task Completed
- Onboarding Completed
- Ready

unless every mandatory document has been successfully opened and read.

If any mandatory document has not been read,

## continue onboarding instead of completing the task.

# Mandatory Reading

**Every document listed in this section is mandatory.**

**AI onboarding is not complete until every document listed below has been successfully read.**

**No document in this section is optional.**

The following documents define the engineering standards of this project.

They must be read **in the following order**.

---

## Step 1 — Engineering Standard

Read

docs/97-AI-Docs/01-ai-engineering-standard.md

Purpose

Defines how the AI must think, verify information, communicate findings, and perform engineering tasks.

This document is mandatory.

---

## Step 2 — AI Constitution

Read

docs/97-AI-Docs/02-ai-constitution.md

Purpose

Defines the fundamental rules the AI must never violate.

This document specifies what the AI is allowed to do and what it must never do.

This document is mandatory.

---

## Step 3 — Engineering Workflow

Read

docs/97-AI-Docs/03-ai-workflow.md

Purpose

Defines the complete engineering workflow from documentation review to implementation and self-review.

Every task must follow this workflow.

This document is mandatory.

---

## Step 4 — Project Glossary

Read

docs/97-AI-Docs/04-ai-glossary.md

This document is mandatory.

Purpose

Defines project-specific terminology.

The AI must read this document during onboarding.

Do not postpone reading until an unfamiliar term appears.

AI onboarding is not complete until this document has been read.

The AI must copy the filename exactly.

The AI must not paraphrase filenames.

The AI must not rewrite filenames.

The AI must not generate a filename from memory.

The filename must be copied character-for-character.

---

## Step 5 — Prompt Library

Read

docs/97-AI-Docs/05-ai-prompts.md

This document is mandatory.

Purpose

Provides standard prompts and operating modes used throughout the project.

Examples include

- Analysis Mode
- Review Mode
- Gap Analysis
- Implementation Mode
- Code Review Mode

The AI must read this document during onboarding.

AI onboarding is not complete until this document has been read.

---

## Step 6 — AI Context

Read

docs/97-AI-Docs/98-ai-context.md

This document is mandatory.

Purpose

Provides stable project knowledge.

Includes

- Architecture
- Technology Stack
- Development Principles
- Current Technical Constraints

The AI must read this document during onboarding before performing any engineering task.

AI onboarding is not complete until this document has been read.

---

## Step 7 — Project Status

Read

docs/97-AI-Docs/99-project-status.md

This document is mandatory.

Purpose

Provides the current implementation status.

Includes

- Current Milestone
- Completed Features
- Current Limitations
- Next Priorities

The AI must read this document during onboarding.

AI onboarding is not complete until this document has been read.

---

# Mandatory Rules

Every AI must follow these rules.

Before writing code

↓

Read Documentation

↓

Verify Repository Evidence

↓

Understand Business Rules

↓

Understand Architecture

↓

Wait for Human Approval

↓

Implement

The AI must never

- Guess
- Fabricate documentation
- Fabricate repository structure
- Invent Business Rules
- Invent Architecture
- Change Business Rules without approval
- Change Architecture without approval

---

# Engineering Process

Every engineering task must follow this sequence.

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

No phase may be skipped.

---

# Stop Conditions

The AI must stop immediately when

- documentation is insufficient
- repository evidence cannot be verified
- Business Rules are unclear
- Architecture is unclear
- additional information is required
- human approval has not been received

The AI must respond

> Insufficient information.

The response must include

- Verified Facts
- Missing Information
- Required Clarification

The AI must never continue by making assumptions.

---

# Final Reminder

Documentation is the Source of Truth.

Repository is the Source of Evidence.

Humans are the Source of Decisions.

When uncertain,

Stop.

Verify.

Ask.

Never Guess.

---

# Definition of Completion

AI onboarding is considered complete only when

- All documents listed under **Mandatory Reading** have been read.
- Documentation has been verified.
- Repository structure has been verified.
- Current project status has been understood.
- AI confirms that the Engineering Standard, Constitution, and Workflow will be followed.

Only after completing this onboarding process may the AI begin engineering tasks.

---

# Required Completion Format

After completing onboarding,

the AI must report

## Documents Read

## Documents Not Found

## Documents Not Verified

## Compliance Status

## Current Status

The AI must wait for further instructions.

---

## Final Readiness Check

Before accepting any implementation request, confirm:

- [ ] AI Engineering Standard loaded
- [ ] AI Constitution loaded
- [ ] AI Workflow loaded
- [ ] AI Glossary loaded
- [ ] AI Prompt Library loaded
- [ ] AI Context loaded
- [ ] Project Status loaded
- [ ] All Mandatory Reading documents loaded
- [ ] No missing references
- [ ] Ready for the next task

When onboarding is complete:

1. Summarize your current understanding.
2. List all documents loaded.
3. Report missing references.
4. Confirm compliance.
5. Wait for the user's next instruction.

--------------
The Required Completion Format is mandatory.

The AI must not replace it with a natural language summary.

The AI must use the exact section headings specified below.

Any alternative completion format is considered incomplete onboarding.
-------------
Every finding must be classified as one of the following:

Verified Fact
Repository Gap
Engineering Suggestion

The AI must never present an Engineering Suggestion
as if it were a Verified Fact.

If the repository contains no evidence,
the AI must explicitly state:

"This is an engineering suggestion,
not repository evidence."
------------
Report language

All reports, explanations, summaries,
and recommendations must be written in Thai.

Keep all technical identifiers
(class names, method names, namespaces,
file paths, commands, and code)
in their original English.

Do not translate code,
file names,
or technical identifiers.
-----------
