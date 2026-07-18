# Project Control Center

Version: 1.0
Last Updated: 2026-07-18

**Entry Point for every AI session.** Read this first, then follow cross-references.

---

## Current Sprint

| Sprint | Goal | Status |
|--------|------|--------|
| **Sprint 1** | Foundation + Cashier UI | ✅ **Active** |

Sprint 1 Goal (from `110-master-implementation-plan.md` §6):
Working Cashier UI with Order management, Menu selection, Table management, and Payment.

Current focus: **Cashier UI completion** (F8.1–F8.4), **Integration Test fixes**.

---

## Current Approved Work

| Work Item | Status | Started | Owner |
|-----------|--------|---------|-------|
| Typed API Clients (5 clients) | ✅ Complete | Sprint 1 | AI |
| CashierPage skeleton | ✅ Complete | Sprint 1 | AI |
| TableGrid integration | ✅ Complete | Sprint 1 | AI |
| OrderPanel (Create, Add, Confirm, Pay, Release) | ✅ Complete | Sprint 1 | AI |
| MenuModal (browse categories + products, add item) | ✅ Complete | Sprint 1 | AI |
| NavMenu integration | ✅ Complete | Sprint 1 | AI |
| Receipt integration | ✅ Complete | Sprint 1 | AI |
| Integration test fixes (Order.Create signature) | ✅ Complete | Sprint 1 | AI |
| Governance documents | ✅ Complete | Sprint 1 | AI |

---

## Frozen Areas

The following are frozen — NO code changes allowed without Architecture + Product Owner approval:

| Module | Frozen Since | Includes |
|--------|-------------|----------|
| Architecture Baseline | v1.0 | All 20 architecture phases |
| Order Module | v1 | Domain, API, Business Rules, Contracts |
| Menu/Catalog Module | v1 | Product, Category, Ingredient aggregates |
| Table Module | v1 | DiningTable aggregate, all operations |
| Kitchen Module | v1 | KitchenTicket aggregate, state machine |
| Payment Module | v1 | Payment aggregate, 4 rules, state machine |
| Reporting Module | v1 | Query-only, read model |
| Receipt Module | v1 | Output module, formatter, printer abstractions |
| Printing Infrastructure | v1 | ESC/POS, USB, LAN, Null printers |
| Integration Testing Infrastructure | v1 | Testcontainers, WebApplicationFactory |

See `FEATURE-REGISTRY.md` for exact feature freeze status per feature.

---

## Deferred Features

| Feature | Reason | Dependencies |
|---------|--------|--------------|
| F1.3 Remove Item (UI) | Not implemented in UI — skip MVP | API exists, client missing |
| F1.5 Cancel Order (UI) | Not implemented in UI — skip MVP | API exists, client missing |
| F1.6 Complete Order (UI) | Not needed — auto-completed via Confirm → Pay flow | API exists, client missing |
| F8.5 Kitchen Display UI | Skipped — no UI component | Kitchen API exists |
| F8.6 Manager Dashboard | Skipped — out of MVP scope | Reports API exists |
| F8.7 Login Screen | Skipped — Auth not implemented | F9.1 not started |
| F9.x Authentication | Skipped — out of MVP scope | None |
| F10.x Dashboard | Skipped — out of MVP scope | Reports API exists |

---

## High Priority Features (Next)

| Priority | Feature | Effort | Dependency |
|----------|---------|--------|------------|
| P0 | F1.3 Remove Item — add to `IOrderClient` + `OrderPanel` | Small | API ready |
| P0 | F3.3–F3.5 Transfer/Merge/Split — add to `ITableClient` + `TableGrid` | Medium | API ready |
| P0 | F4.2–F4.5 Kitchen operations — add `IKitchenClient` + `KitchenPage` | Large | API ready |
| P1 | F5.3 Refund — add to `IPaymentClient` + `OrderPanel` | Medium | API ready |
| P1 | F7.1–F7.3 Reports — add clients + dashboard | Medium | API ready |

---

## Current Risks

| Risk | Impact | Mitigation | Status |
|------|--------|------------|--------|
| **No Authentication** | All endpoints publicly accessible | UAT in isolated environment | 🔴 Open |
| **No Logging** | Cannot debug production issues | Add ILogger to handlers | 🔴 Open |
| **Integration tests have pre-existing issues** | CI may fail | Fixed: Order.Create signature | ✅ Resolved |
| **UI has no CSS styling** | Poor user experience | Accept for MVP | 🟡 Accepted |

---

## Next Approved Transition

The next work item requires Product Owner approval.

Candidate work items (ordered by business value):

1. **Kitchen Display UI** (F8.5) — `IKitchenClient` + `KitchenPage.razor` — needed for kitchen staff
2. **Authentication** (F9.1–F9.3) — JWT + Identity — required before production
3. **Remove Item UI** (F1.3) — add to client + OrderPanel — small improvement
4. **CSS Styling** — improve Cashier UI appearance
5. **Refund UI** (F5.3) — add to client + OrderPanel

---

## MVP Progress

| Epic | Progress | Done | Total |
|------|----------|------|-------|
| Epic 1: Order Management | 100% backend / 60% UI | 6 | 6 |
| Epic 2: Menu/Catalog | 100% backend / 30% UI | 5 | 5 |
| Epic 3: Table Management | 100% backend / 30% UI | 7 | 7 |
| Epic 4: Kitchen Display | 100% backend / 0% UI | 5 | 5 |
| Epic 5: Payment Processing | 100% backend / 50% UI | 4 | 4 |
| Epic 6: Receipt Printing | 100% backend / 25% UI | 4 | 4 |
| Epic 7: Reporting | 100% backend / 0% UI | 3 | 3 |
| Epic 8: Web UI | 35% | — | 7 components |
| Epic 9: Auth | 0% | — | 3 |
| Epic 10: Dashboard | 0% | — | 4 |
| **Overall** | **≈ 95% backend / 20% UI** | **34 features** | **~44** |

---

## Release Readiness

| Check | Status | Detail |
|-------|--------|--------|
| Full solution build | ✅ PASS | 0 errors, 0 warnings |
| Integration tests | ✅ PASS (compilation) | 212+ tests |
| CI/CD pipeline | ✅ Operational | GitHub Actions |
| Security review | ❌ Not done | No authentication |
| Performance review | ❌ Not done | No benchmarks |
| UAT | ❌ Not started | Requires isolated environment |
| Documentation | 🟡 Updated | 4 governance docs created |

**Blockers to release:**
1. Authentication (F9.1) — required before production deployment
2. Logging infrastructure — required for production support

---

## AI Session Entry Protocol

1. Read this document (`PROJECT-CONTROL-CENTER.md`)
2. Read current AI Context (`98-ai-context.md`)
3. Read Feature Registry (`FEATURE-REGISTRY.md`) for work area
4. Read Traceability Matrix (`TRACEABILITY-MATRIX.md`) for dependencies
5. Read Project Governance (`PROJECT-GOVERNANCE.md`) for rules
6. Verify build status before implementation
7. Confirm scope with Product Owner

---

## Cross-Reference

| Document | Purpose | Location |
|----------|---------|----------|
| Feature Registry | Feature lifecycle tracking | `docs/97-AI-Docs/FEATURE-REGISTRY.md` |
| Traceability Matrix | End-to-end traceability | `docs/97-AI-Docs/TRACEABILITY-MATRIX.md` |
| Project Governance | Rules, gates, lifecycle | `docs/97-AI-Docs/PROJECT-GOVERNANCE.md` |
| Master Implementation Plan | Full plan | `docs/97-AI-Docs/110-master-implementation-plan.md` |
| Project Status | Overall progress | `docs/97-AI-Docs/99-project-status.md` |
| AI Context | AI guidance | `docs/97-AI-Docs/98-ai-context.md` |
| Session Handoff | Session state | `docs/97-AI-Docs/91-session-handoff.md` |
| Change Log | Change history | `docs/CHANGELOG.md` |
| Business Scenarios | Use cases | `docs/03-system-use-cases/` |
| Architecture Docs | Architecture | `docs/96-architecture/` |
| ADR Documents | Decisions | `docs/98-decisions/` |

Governance Status

✅ Baseline v1 Approved

Next Phase

UAT Preparation

Current Priority

1. Documentation Synchronization
2. UAT
3. Bug Fix Sprint
4. Deferred Features