# Changelog

## v2.0.0 — 2026-07-19

### Added
- ADR-010 Public API Contract Standard (`docs/98-decisions/ADR-010-public-api-contract.md`)
- Catalog migration report (`docs/97-AI-Docs/112-catalog-migration-report.md`)
- Tables migration report (`docs/97-AI-Docs/113-tables-migration-report.md`)
- Orders migration report (`docs/97-AI-Docs/114-orders-migration-report.md`)
- Kitchen migration report (`docs/97-AI-Docs/115-kitchen-migration-report.md`)
- Payments migration report (`docs/97-AI-Docs/116-payments-migration-report.md`)
- ADR-010 completion report (`docs/97-AI-Docs/117-adr010-completion-report.md`)
- Documentation sync report (`docs/97-AI-Docs/118-documentation-sync-report.md`)

### Changed
- **Catalog DTOs**: `ProductId`, `ProductCategoryId`, `IngredientId` → `Guid`; `ProductStatus`, `ProductCategoryStatus`, `IngredientStatus` → `string`; `Money` → `decimal`; collections → `List<T>`
- **Tables DTO**: `TableId` → `Guid`; `TableStatus` → `string`; `OrderSessionId?` → `Guid?`; `MergedTableIds` → `List<Guid>`
- **Orders DTOs**: `OrderId` → `Guid`; `OrderStatus` → `string`; `Money` (Total, UnitPrice, TotalPrice) → `decimal`; `Quantity` → `int`
- **Kitchen DTOs**: `KitchenTicketId` → `Guid`; `KitchenTicketStatus` → `string`; `KitchenItemId` → `Guid`
- **Payments DTO**: Already compliant — verified

### Refactored
- Public API no longer exposes Domain Value Objects, Domain Enums, Money, or IReadOnlyCollection
- All `FromDomain()` methods updated with `.Value`, `.ToString()`, `.Amount`, `.Select().ToList()`
- 9 DTOs migrated across 5 modules

### Architecture
- Preserved. Domain layer untouched.
- Preserved. No infrastructure changes.
- Preserved. No API route changes.
- Applied ADR-010: Public API Contracts use only `Guid`, `string`, `decimal`, `int`, `List<T>`

### Build
- Application: ✅ 0 Errors, 0 Warnings
- API: ✅ 0 Errors, 0 Warnings
- Web: ✅ 0 Errors, 0 Warnings
- Integration tests: ⏳ Pending (9 enum→string assertion fixes)

### Documentation
- 7 new reports created in `docs/97-AI-Docs/`
- All migration phases documented with ADR compliance tables

---

## v1.0.0 — 2026-07-19

### Added
- UI/UX Architecture Foundation (docs/09-ui/):
  - 00-ui-foundation.md — Vision, principles, architecture layers, workspace definition
  - 01-design-system.md — Colors, typography, spacing, grid, icons, motion, breakpoints
  - 02-navigation.md — Navigation structure, sidebar, top bar, workspace flows, quick actions
  - 03-workspaces.md — Workspace architecture, contracts, dependency diagram, interaction matrix, 6 workspace definitions, state machine mapping
  - 05-cashier.md — Complete Cashier workspace design: purpose, personas, KPIs, user journey, screen flow, layout, 6 panel specifications, component inventory, workflow/state mapping, keyboard/mouse/touch flows, information priority, loading/empty/error/success states, printing flow, offline behavior, permission matrix, responsive behavior, accessibility, performance targets, future extensions
- Runtime verification: all 47 API endpoints verified (HTTP 200/201)
- Production deployment checklist (`docs/97-AI-Docs/92-deployment-checklist.md`)
- Smoke test checklist (`docs/97-AI-Docs/93-smoke-test-checklist.md`)
- Release notes (`docs/releases/v1.0.0.md`)
- Development seed data script (`docs/97-AI-Docs/seed_data.ps1`)
- E2E workflow verification script (`docs/97-AI-Docs/e2e_workflow.ps1`)

### Changed
- **POST /orders now requires `tableId` in request body** (breaking change)
- Updated project status to v1.9 with UI/UX architecture milestone
- Updated session handoff to reflect v1.0.0 release state

### Fixed
- GET /orders returned HTTP 500 — removed 13 invalid test orders with `TableId = Guid.Empty`
- POST /orders generated random `TableId.New()` that never matched any dining table

### Architecture
- Preserved. No architecture changes.
- Preserved. No domain changes.
- Preserved. No infrastructure changes.
- Added UI/UX Architecture Foundation (documentation only, no code)

### Build
- Full solution: ✅ 0 Errors, 0 Warnings (14 projects)

### Runtime Verification
- All 47 API endpoints verified
- Database: migrations applied, schema consistent
- No HTTP 500/404 errors
- No EF Core exceptions
- No DI failures

### Recommendation
- Version: v1.0.0
- Tag: `v1.0.0`

---

## 2026-07-19 — UI Completion Phase 1

### Added
- Home Page: summary cards + quick navigation
- Settings Page: General, Printer, About sections
- Custom CSS: dark theme, responsive, component styles

### Fixed
- Home Page: replaced default Blazor template
- Settings Page: replaced placeholder with full UI

---

## 2026-07-19 — Production Hardening (H1–H3)

### Fixed
- H1: Kitchen polling empty catch → proper logging
- H2: Kitchen polling overlap prevention (`_isPolling` guard)
- H3: OrderPanel TableId/OrderId mismatch (stored OrderId from Create response)

---

## 2026-07-18 — EF Core Runtime Recovery & Database Sync

### Added
- OrderSessionIdConverter, NullableOrderSessionIdConverter
- EF Core Migration: UpdateModelConfiguration

### Fixed
- 8 EF Core Model Validation issues resolved

---

## 2026-07-18 — Governance & Validation

### Added
- FEATURE-REGISTRY.md, TRACEABILITY-MATRIX.md
- PROJECT-GOVERNANCE.md, PROJECT-CONTROL-CENTER.md

---

## 2026-07-17 — All Backend Modules v1

### Added
- Payment Module (4 endpoints, 18 tests)
- Reporting Module (3 endpoints, 24 tests)
- Receipt Module (3 endpoints, 21 tests)
- Printing Infrastructure (57 unit tests)
- Global Exception Handling Middleware

---

## 2026-07-17 — Architecture Baseline v1.0

### Added
- Discovery Phase (P1–P7)
- Presentation Architecture (P8–P14)
- Application Architecture (P15–P16)
- Infrastructure Architecture (P17–P20)