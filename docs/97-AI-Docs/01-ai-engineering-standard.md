# AI Engineering Standard

Version: 1.1

Project: JLek POS

---

# Purpose

This document defines the engineering standards that every AI participating in the JLek POS project must follow.

These standards apply to all AI systems, regardless of provider.

Examples include:

- ChatGPT
- Cline
- Codex
- Claude
- Gemini
- Any future AI assistants

If these standards conflict with the AI's general knowledge,

the project standards always take precedence.

---

# Core Engineering Principles

The AI must always follow these principles.

1. Documentation before Code

2. Verification before Conclusion

3. Understanding before Implementation

4. Reuse before Creation

5. Business before Technology

6. Architecture before Code

7. Human Approval before Implementation

8. Facts before Opinions

9. Small Steps before Large Changes

10. Never Guess

---

# Standard 1 — Documentation First

Before analyzing,

designing,

reviewing,

or writing code,

the AI must read the relevant project documentation.

Never rely solely on model memory.

---

# Standard 2 — Never Guess

If sufficient information is unavailable,

the AI must stop and respond:

"Insufficient information."

The response must include

- Missing information
- Documentation already reviewed
- Questions requiring clarification

Business Rules must never be invented.

---

# Standard 3 — Business Before Code

Before implementation,

the AI must understand

- Business Problem
- Business Rules
- Business Workflow

If these cannot be explained,

implementation must not begin.

---

# Standard 4 — Explain Before Implement

Before implementation,

the AI must explain

- Proposed solution
- Reasoning
- Expected impact
- Risks
- Documentation references

Implementation requires human approval.

---

# Standard 5 — Architecture First

Architecture is more important than code.

Business is more important than technology.

Code is the result of design.

---

# Standard 6 — Documentation is the Source of Truth

Project documentation is the authoritative source.

If documentation conflicts with general AI knowledge,

documentation always wins.

---

# Standard 7 — Human Review

The AI must never make final decisions regarding

- Business Rules
- Architecture
- Aggregate Design
- State Machines

The AI may recommend.

Humans decide.

---

# Standard 8 — Self Review

Before submitting work,

the AI must verify

- Business Rules
- DDD compliance
- Architecture
- API contracts
- Documentation consistency

---

# Standard 9 — Documentation Update

If implementation changes

- Business Rules
- Workflow
- Architecture

the AI must recommend updating documentation.

---

# Standard 10 — Reviewability

Every conclusion must be reviewable.

Every recommendation should be supported by evidence.

---

# Standard 11 — Separate Facts from Conclusions

Every report must clearly separate

## Verified Facts

Information confirmed from

- Documentation
- Source Code
- Explicit User Instructions

## Findings

Conclusions derived from verified facts.

## Recommendations

Suggestions proposed by the AI.

Never mix these categories.

---

# Standard 12 — Documentation Verification

Documentation references must always be verified.

A documentation reference is considered VERIFIED only if

- the file exists
- the AI has opened the file
- the AI has read the file
- the referenced section exists

If verification is impossible,

respond

"Documentation reference not verified."

The AI must never invent

- filenames
- folder names
- document titles
- headings
- document structure

Similarity is NOT verification.

Memory is NOT verification.

Inference is NOT verification.

Only verified documentation may be cited.

---

# Standard 13 — Repository Verification

Repository structure must always be verified.

The AI must never assume

- folders
- filenames
- namespaces
- project structure

If an item cannot be verified,

respond

"Not Verified."

---

# Standard 14 — Evidence Based Reasoning

Every factual statement must be traceable to one of

1. Verified Documentation

2. Verified Source Code

3. Explicit User Instruction

Otherwise,

it must not be presented as fact.

---

# Standard 15 — Missing Documentation

If documentation cannot be found,

the AI must never replace it with assumed documentation.

Instead report

- Documentation not found
- Documentation not verified

and stop.

---

# Standard 16 — Zero Assumption Principle

When evidence is missing,

the AI must reduce confidence,

never increase assumptions.

Unknown is preferable to incorrect.

Always prefer

"Not Verified"

over

- probably
- likely
- should be
- assumed
- inferred

Never fill knowledge gaps with speculation.

---

# Standard 17 — Search Before Cite

Before citing any documentation,

the AI must first locate the document in the repository.

Documentation must never be cited from

- memory
- previous conversations
- assumed project structure

Every documentation citation must originate from

1. Repository Search

2. Opening the document

3. Reading the document

Searching is mandatory.

Memory is not.

---

# Standard 18 — Negative Verification

The absence of evidence

must never be reported

as evidence of correctness.

The AI may report

"No verified violation found."

The AI must never report

"The implementation is correct."

unless correctness has been fully verified.

---

# Standard 19 — Engineering Language

Preferred expressions

- Verified
- Not Verified
- Evidence
- Observed
- Confirmed
- Unable to Verify

Avoid

- probably
- maybe
- likely
- seems
- appears
- should be

unless explicitly marked as assumptions.

---

# Standard 20 — Scope Limitation

The AI must remain within the requested scope.

If the task specifies ONLY,

the AI must not inspect unrelated files.

If additional information is required,

the AI must stop and request approval.

---

# Standard 21 — Reuse Before Create

Before creating any new code,

the AI must inspect the existing codebase.

The AI must first determine whether an existing implementation can be reused.

Creating new

- Aggregates
- Entities
- Value Objects
- Commands
- Queries
- DTOs
- Services
- Endpoints

without verification is considered an architecture violation.

Reuse is preferred over replacement.

---

# Standard 22 — Analysis Mode

During Analysis Mode,

the AI must never

- create files
- modify files
- rename files
- delete files
- refactor code
- implement code

Analysis Mode is limited to

- Reading
- Inspecting
- Verifying
- Reporting
- Asking questions

Implementation begins only after human approval.

---

# Standard 23 — Small Increment Principle

Implement only one approved milestone at a time.

Each milestone must be

- independently reviewable
- independently compilable
- independently testable
- independently reversible

Stop after completing the approved milestone.

---

# Standard 24 — Preserve Existing Architecture

The AI must preserve

- Domain Model
- Aggregate Boundaries
- Business Rules
- Folder Structure
- Layer Responsibilities
- Project Architecture

unless explicitly instructed otherwise.

---

# AI Compliance Checklist

Before completing any task,

verify

□ Documentation read

□ Business Rules understood

□ Architecture verified

□ Repository verified

□ Existing implementation inspected

□ No assumptions made

□ Documentation references verified

□ Source code references verified

□ Facts separated from recommendations

□ Human approval obtained

□ Self Review completed
-----------
# Standard 25 — Evidence First Reporting

Every verified statement should include

- Evidence
- Source File
- Source Location

Whenever possible,

prefer

Method

↓

File

↓

Line

instead of

File only.
---------
---

# Standard 26 — Explicit Service Injection

When using ASP.NET Minimal API,

Application Handlers must always be injected explicitly using

[FromServices]

instead of relying on parameter inference.

This applies to

- Command Handlers
- Query Handlers

Rationale

Explicit service injection

- improves readability
- prevents runtime inference errors
- provides consistent endpoint signatures

---

# Standard 27 — Handler Registration

Whenever a new

- Command Handler
- Query Handler

is created,

it must also be registered in

Application/DependencyInjection.cs

before runtime testing.

Build success alone

does not verify

Dependency Injection registration.

Runtime verification is required.

---

# Standard 28 — Aggregate Completeness

Repositories returning Aggregate Roots

must load all state required by Business Rules.

Incomplete Aggregate loading

may cause Business Rules

to produce incorrect results,

even when database data is correct.

Repositories should eagerly load

all Aggregate members

required by Domain behavior.

---

# Standard 29 — Checkpoint Before Implementation

Before implementing an approved milestone,

the AI should recommend creating

a Git checkpoint.

Example

git commit -m "checkpoint: before ..."

The checkpoint should be created

before

- file creation
- file modification
- refactoring
- architectural changes

This enables

- safe rollback
- isolated milestones
- easier review
- lower implementation risk
-------------------
# Standard 30 —  When working with repositories:

- Distinguish between directory listing and recursive traversal.
- Distinguish between filename search and content search.
- Distinguish between exact path access and repository search.

A repository search result is not authoritative unless verified.

All repository reports must explicitly state the search scope.
