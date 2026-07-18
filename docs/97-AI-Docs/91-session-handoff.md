# AI Handoff

Current Version

v1.0.0-rc1

Current Branch

main (tagged: v1.0.0-rc1)

Status

Release Candidate — Awaiting validation

---

## Repository Status

| Item | Value |
|------|-------|
| Current branch | `main` |
| Git tag | `v1.0.0-rc1` |
| Previous tag | `v1.0.0`, `architecture-baseline-v1.0`, `governance-v1` |
| Remote | `origin` (https://github.com/Warayuth-Ben/jlek-pos.git) |
| Working tree | Clean |
| Build | ✅ 0 Errors, 0 Warnings |

---

## Completed Backend Modules (All Frozen)

| Module | Commands | Queries | APIs | Tests |
|--------|----------|---------|------|-------|
| Order v1 | 6 | 2 | 7 | — |
| Menu/Catalog v1 | 15 | 3 | 27 | 54 |
| Table v1 | 7 | 3 | 10 | 17 |
| Kitchen v1 | 5 | 3 | 8 | 21 |
| Payment v1 | 2 | 2 | 4 | 18 |
| Reporting v1 | 0 | 3 | 3 | 24 |
| Receipt v1 | 3 | 0 | 3 | 21 |
| Printing Infrastructure v1 | — | — | — | 57 unit tests |

**Totals: 20 Commands, 12 Queries, 47 API Endpoints, 155 Integration Tests, 57 Unit Tests**

---

## Completed UI Modules

| Page | Status | Backend Dependencies |
|------|--------|---------------------|
| Cashier | ✅ Complete | Order, Menu, Table, Payment, Receipt |
| Kitchen | ✅ Complete (polling 15s) | Kitchen |
| Dashboard | ✅ Complete | Reports, Tables, Kitchen, Orders |
| Reports | ✅ Complete | Reports |
| Settings | ⚠️ Placeholder | None |
| Home | ⚠️ Default template | None |

---

## Production Hardening Completed (H1–H3)

- H1: Kitchen polling empty catch → proper logging
- H2: Kitchen polling overlap prevention (`_isPolling` guard)
- H3: OrderPanel TableId/OrderId mismatch (stored OrderId from Create response)

---

## Known Limitations

1. **No SignalR** — Kitchen uses 15s polling instead of real-time updates
2. **No TableName in KitchenTicket** — Domain frozen (Snapshot Aggregate pattern)
3. **No CreatedAt in KitchenTicket** — Domain frozen (no timestamp property)
4. **No CancellationToken propagation** in some UI components
5. **Settings page** is placeholder only
6. **Home page** shows default Blazor template

---

## Deferred Items (v1.1+)

- Settings page (Restaurant, Printer, Payment, Tax, User, Backup)
- SignalR for real-time Kitchen updates
- Monthly Sales report
- Export functionality
- Home page customization (restaurant dashboard)
- Notification auto-dismiss timer
- CancellationToken propagation

---

## Architecture Constraints

All decisions in `docs/98-decisions/` are frozen:

| ADR | Decision |
|-----|----------|
| ADR-001 | Transaction Strategy (non-atomic for v1) |
| ADR-002 | Aggregate Communication (by ID only) |
| ADR-003 | Event Strategy (in-process dispatcher) |
| ADR-004 | Outbox Strategy (deferred) |
| ADR-005 | Event Strategy (MediatR pattern) |
| ADR-006 | Event Handler Rule (one handler per event) |
| ADR-007 | Kitchen Integration (snapshot pattern) |
| ADR-008 | Payment Integration (cross-aggregate rules) |
| ADR-009 | Presentation Architecture (Blazor WebAssembly) |

### Do NOT modify:
- Domain Layer (7 aggregates all frozen)
- Application Layer (CQRS pattern frozen)
- Infrastructure Layer (7 repositories frozen)
- API Contracts (47 endpoints frozen)
- Integration Tests (155 tests frozen)

### Allowed changes:
- UI-only improvements
- Bug fixes
- Documentation updates

---

## Next Recommended Task

After RC validation:
1. Settings page implementation
2. Home page customization
3. SignalR for Kitchen real-time updates
4. v1.1 planning

---

## Development Rules

1. Follow AI Engineering Standard (`docs/97-AI-Docs/01-ai-engineering-standard.md`)
2. Follow AI Constitution (`docs/97-AI-Docs/02-ai-constitution.md`)
3. Follow AI Workflow (`docs/97-AI-Docs/03-ai-workflow.md`)
4. Documentation first. Evidence based. Never guess.
5. Human approval required before implementation.
6. Build after every change. Run existing tests.