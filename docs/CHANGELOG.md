# Changelog

## v3.0.0 — 2026-07-19 — Production Readiness Sprint

### Added
- Production Readiness audit (`docs/97-AI-Docs/130-production-readiness-report.md`)
- Documentation sprint: project status v3.0, session handoff update

### Changed
- **Blazor Cashier UI (Phases 13.0-13.8)**: Complete end-to-end cashier workflow implemented
  - TableGrid with keyboard navigation, ARIA roles, status colors
  - OrderPanel with inline quantity display, remove, confirm, cancel
  - MenuModal with search, category tabs, stay-open-after-add behavior
  - PaymentDialog with Cash/PromptPay/Card, change calculation, amount validation
  - ReceiptPreview with Print & Close Table, Close Only, Skip
  - ToastNotification with auto-dismiss, Success/Error/Warning/Info

### Fixed
- **Web build warnings**: 14 warnings eliminated (6 CS1998, 2 CS0649, 6 RZ10012)
  - `CashierWorkspace.razor`: Changed async Task→void for synchronous handlers
  - `CashierWorkspace.razor`: Removed unused `_statusOccupied`/`_statusAvailable`
  - `CashierWorkspace.razor`: Added missing namespace imports for WorkspaceShell
  - `MenuModal.razor`: Changed async Task→void for `SelectCategoryAsync`

### Known Issues (Pre-Release)
| Issue | Status |
|-------|--------|
| `MenuClient.cs` nested JSON parsing (`{ amount: X }`) | ❌ Pending |
| 45 integration test assertions (enum→string after ADR-010) | ❌ Pending |
| Runtime verification against live API | ❌ Pending |

### Build
- Application: ✅ 0 Errors, 0 Warnings
- API: ✅ 0 Errors, 0 Warnings
- Web: ✅ **0 Errors, 0 Warnings**
- Integration Tests: ❌ 45 errors (ADR-010 pending)

---

## v2.0.0 — 2026-07-19 — ADR-010 Public API Contract Migration

### Added
- ADR-010 Public API Contract Standard
- 7 migration reports (112–118)
- Documentation sync report

### Changed
- All 5 modules migrated: Catalog, Tables, Orders, Kitchen, Payments
- 9 DTOs: Domain types replaced with primitives (Guid, string, decimal)

### Architecture
- Preserved. Domain layer untouched.
- Applied ADR-010: Public API uses only primitive types.

---

## v1.0.0 — 2026-07-19 — Initial Production Release

### Added
- UI/UX Architecture Foundation
- Runtime verification: all 47 API endpoints verified
- Production deployment checklist, smoke test checklist
- Release notes, seed data script, E2E workflow script

### Changed
- POST /orders now requires `tableId` in request body (breaking change)

### Fixed
- GET /orders returned HTTP 500 — removed 13 invalid test orders
- POST /orders generated random TableId

### Architecture
- Preserved. No architecture changes.