# Development Workflow

## Purpose

This document defines the standard development workflow for JLEK POS.

Every change to the project should follow this workflow to keep the repository stable, maintainable, and easy to understand.

---

# Standard Workflow

## Phase 1 — Planning

- Review requirements
- Update documentation if necessary
- Confirm architecture
- Define implementation scope

Do not write code during this phase.

---

## Phase 2 — Implementation

- Implement one feature at a time
- Avoid unrelated refactoring
- Keep each commit focused on a single purpose

---

## Phase 3 — Health Check

Run:

```bash
dotnet build
```

The project must build successfully before moving forward.

If Build fails:

- Fix errors
- Build again
- Do not commit until Build succeeds

---

## Phase 4 — Review

Check repository status:

```bash
git status
```

Review changes:

```bash
git diff
```

Verify no unexpected files are included.

Examples:

- bin/
- obj/
- .vs/

---

## Phase 5 — Commit

```bash
git add .

git commit -m "feat(order): add order aggregate"
```

One Commit = One Purpose

---

## Phase 6 — Push

```bash
git push
```

---

# Build Rule

Every completed phase must pass

```bash
dotnet build
```

before committing.

---

# Documentation Rule

If architecture or business rules change:

Update documentation before implementing code.

Documentation and code should always stay synchronized.

---

# Refactoring Rule

Refactoring should be performed separately from feature development whenever possible.

Avoid mixing:

- New Feature
- Folder Restructuring
- Namespace Changes
- Large Refactoring

in the same commit.

---

# Git Rules

Commit messages should follow Conventional Commits.

Examples:

- feat:
- fix:
- refactor:
- docs:
- test:
- chore:
- build:

Avoid generic commit messages such as:

- update
- fix
- test
- work

---

# Design Principles

Priority:

1. Correctness
2. Readability
3. Maintainability
4. Performance
5. Cleverness

Readable code is preferred over clever code.

---

# Naming

Use meaningful names.

Good:

- Order
- OrderItem
- PaymentMethod

Avoid:

- temp
- data2
- obj
- helper1

---

# Folder Principles

Every folder must have a clear responsibility.

Avoid generic folders:

- Misc
- Temp
- Utils
- Others

---

# Class Principles

One class should have one responsibility.

---

# Domain First

Always think in this order:

Business

↓

Model

↓

Code

Never the opposite.

---

# Dependency Rule

Outer layers may depend on inner layers.

Inner layers must never depend on outer layers.

Web
↓

Infrastructure
↓

Application
↓

Domain

Dependencies always point inward.

---

Documentation Freeze Rule

Once a documentation phase has been reviewed and approved,
it should be considered frozen.

Changes should only be made if:

- Business requirements change
- Architecture changes
- A factual error is discovered

Avoid revisiting completed documentation for stylistic improvements alone.

---

# Milestones

Recommended milestone tags:

- v0.1.0 Solution Initialization
- v0.2.0 Domain Foundation
- v0.3.0 Order Aggregate
- v0.4.0 Application Layer
- v0.5.0 Infrastructure
- v0.6.0 First Working POS
- v0.7.0 Kitchen
- v0.8.0 Reports
- v1.0.0 Production Ready

---

# Final Goal

The goal of JLEK POS is not only to build a working POS system.

The project should also serve as a long-term reference implementation demonstrating:

- Clean Architecture
- Domain-Driven Design
- Professional Git history
- Maintainable codebase
- Comprehensive documentation
- Production-quality project structure