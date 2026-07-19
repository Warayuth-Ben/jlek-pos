# Presentation Constitution

## Preamble

This Constitution defines the immutable rules of the Presentation Architecture. Every workspace, store, model, component, and command must follow these rules. Violations are architecture debt.

---

## Article 1 — Business Rules

**Presentation never owns Business Rules.**

Business Rules belong to the Domain Layer. The Presentation Layer may derive visual state from Business Rules (e.g., "this button is disabled because the order is Confirmed"), but the rule definition lives exclusively in the Domain.

---

## Article 2 — API Access

**Components never call APIs directly.**

All API calls are made through the Store or Command Handler. Components dispatch Commands and receive Presentation Models. They never construct HTTP requests, parse responses, or handle API errors directly.

---

## Article 3 — State Ownership

**Stores own Presentation State.**

Presentation Models are created, updated, and destroyed by Stores. Components receive Models as immutable input parameters. Components never hold persistent state. Components never create or modify Models.

---

## Article 4 — Model Immutability

**Presentation Models are immutable.**

Once created, a Presentation Model cannot be modified. State changes create new Model instances. This ensures predictable rendering and eliminates side effects.

---

## Article 5 — DTO Encapsulation

**DTOs never reach Components.**

DTOs are consumed by the Store layer and mapped to Presentation Models. Components have no knowledge of DTO structure, API contracts, or HTTP protocols.

---

## Article 6 — Infrastructure Isolation

**Business Components never know Infrastructure.**

Business Components have no references to HTTP clients, database contexts, configuration files, or any infrastructure concern. All infrastructure is behind the Store and Command Handler boundaries.

---

## Article 7 — Workflow Ownership

**Workspaces own Workflow.**

The Workspace is responsible for orchestrating the business workflow. It creates the Store, manages the skeleton layout, and handles navigation events. The Store manages data. Components render state.

---

## Article 8 — Framework Replaceability

**Framework is replaceable.**

The Presentation Architecture is defined independently of any rendering framework. Blazor, React, Vue, or any future framework are implementations of the architecture, not the architecture itself.

---

## Article 9 — State Machine Alignment

**Every aggregate state has exactly one visual representation.**

State Machines define all valid states. The Presentation Architecture renders exactly those states. No state is invented. No state is hidden without business justification.

---

## Article 10 — Transition Alignment

**Every Transition Matrix entry has exactly one UI affordance.**

Every valid state transition has a corresponding button, shortcut, or gesture. Transitions not in the Transition Matrix are never enabled in the UI.

---

## Article 11 — Loading and Error Handling

**Every data-dependent component handles loading, empty, error, and ready states.**

No component assumes data availability. Every component that consumes data from a Store must handle:
- Loading state (skeleton or spinner)
- Empty state (meaningful message)
- Error state (user-friendly message with recovery action)
- Ready state (normal render)

---

## Article 12 — Consistency

**Every workspace follows the same skeleton.**

All workspaces implement the platform skeleton: Header, Context Bar, Toolbar, Primary Content, Action Area, Status Bar. No workspace invents its own layout.

---

## Enforcement

These rules are enforced through code review and architecture audits. Violations must be corrected before the violating code is merged.