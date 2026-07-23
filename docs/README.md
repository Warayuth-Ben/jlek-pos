# JLek POS — Documentation Portal

**Version:** v2.0.0

**Status:** Production Release — ADR-010 Complete

**Build:** ✅ 0 Errors, 0 Warnings

---

# Project Status

| Area | Status |
|------|--------|
| Architecture (Clean Architecture + DDD + CQRS) | ✅ Production |
| Domain (7 Aggregates) | ✅ Production |
| Application (20 Commands + 12 Queries) | ✅ Production |
| Infrastructure (7 Repositories, PostgreSQL) | ✅ Production |
| API (47 Endpoints) | ✅ Production |
| **ADR-010 Public API Contract** | ✅ **Complete — 5 modules migrated** |
| DTOs: Catalog, Tables, Orders, Kitchen, Payments | ✅ All primitives (Guid, string, decimal) |
| Integration Testing (155 tests) | ✅ Production (CI-verified) |
| Printing Infrastructure (57 unit tests) | ✅ Production |
| CI/CD (GitHub Actions) | ✅ Production |
| UI (Blazor WebAssembly, 6 pages) | ✅ Production |
| Runtime Verification | ✅ Verified |
| CSS Architecture | ✅ Complete — 4 layers, 16 files, 0 build errors |
| Current Focus | **Frontend Component Architecture** |

---

## Documentation Architecture

```
Business Foundation (000)
    ↓
Architecture (100)
    ↓
Design Constitution (160) ✅ Frozen v1.0
    ↓
Design Standards (161–199) 🚧
    ↓
Implementation (200)
```

The Design Constitution (160) is the canonical design authority.
Every Design Standard (161–199) derives its authority from this document.

---

## Frontend Architecture

### CSS Layering

| Layer | Directory | Purpose |
|-------|-----------|---------|
| Foundation | `css/foundation/` | Variables, reset, typography |
| Components | `css/components/` | Reusable UI (buttons, cards, badges) |
| Layout | `css/layout/` | Application shell structure |
| Features | `css/features/` | Business UI (cashier, kitchen, dashboard, reports, settings) |

**Dependency Direction:** Foundation → Components → Layout → Features

**Key Rule:** Feature ownership > Cross-feature coupling. Duplication is acceptable.

**Documentation:** `docs/97-AI-Docs/101-css-architecture.md`
