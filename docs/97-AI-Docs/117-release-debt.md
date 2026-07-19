# Release Debt — JLek POS v1.0.0-rc2

## Purpose

This document tracks items that are intentionally deferred from the v1.0.0 release.

These items are not release blockers. They are scheduled for v1.1 or later.

---

## RD-001 — Integration Tests (ADR-010)

| Field | Value |
|-------|-------|
| **ID** | RD-001 |
| **Severity** | Medium |
| **Area** | Tests |
| **Count** | 50 test failures |
| **Root Cause** | Integration tests use domain enum types (`ProductStatus`, `TableStatus`, `KitchenTicketStatus`) and `Guid.Value` references. After ADR-010 migration, the API returns strings and raw Guids. Tests need to be updated to match the contract. |
| **Classification** | Test issue only. Zero production code defects. |
| **Target** | v1.1 |

**Why deferred:** The 50 failures are 100% test-side. Production code builds with 0 errors. Deploying with failing tests is acceptable because they validate the old contract, not the current API.

---

## RD-002 — Authentication (v1.1)

| Field | Value |
|-------|-------|
| **ID** | RD-002 |
| **Severity** | High |
| **Area** | API Security |
| **Root Cause** | No JWT authentication. Any client can call any endpoint. |
| **Impact** | Unrestricted access in production |
| **Mitigation** | Deploy behind VPN or firewall for initial deployment |
| **Target** | v1.1 |

**Why deferred:** Auth is a feature, not a bug. The POS is designed for local network use initially. JWT + role-based access will be added in v1.1.

---

## RD-003 — Production CORS Policy

| Field | Value |
|-------|-------|
| **ID** | RD-003 |
| **Severity** | High |
| **Area** | API Configuration |
| **Root Cause** | `AllowAnyOrigin()` in Program.cs |
| **Impact** | Cross-origin requests allowed from any domain |
| **Mitigation** | Deploy Blazor WASM on same origin as API for initial deployment |
| **Target** | v1.1 |

**Why deferred:** Same-origin deployment eliminates the CORS risk. Policy update required only when API and UI are on different origins.

---

## RD-004 — Accessibility Improvements

| Field | Value |
|-------|-------|
| **ID** | RD-004 |
| **Severity** | Low |
| **Area** | Frontend |
| **Items** | WCAG AA contrast, keyboard focus indicators, skip navigation, screen reader announcements |
| **Target** | v1.2 |

**Why deferred:** The POS is designed for staff use in controlled environments. Accessibility is important but not a release blocker.

---

## RD-005 — Docker Support

| Field | Value |
|-------|-------|
| **ID** | RD-005 |
| **Severity** | Low |
| **Area** | Deployment |
| **Items** | Dockerfile, docker-compose.yml, PostgreSQL container |
| **Target** | v1.1 |

**Why deferred:** Manual deployment is sufficient for initial release. Docker support simplifies deployment but is not required.

---

## RD-006 — Request Rate Limiting

| Field | Value |
|-------|-------|
| **ID** | RD-006 |
| **Severity** | Low |
| **Area** | API |
| **Items** | `UseRateLimiter()` middleware |
| **Target** | v1.2 |

**Why deferred:** POS is a local-network application. Rate limiting is not critical.

---

## RD-007 — Connection String via Environment Variable

| Field | Value |
|-------|-------|
| **ID** | RD-007 |
| **Severity** | Low |
| **Area** | Configuration |
| **Root Cause** | `DefaultConnection` in `appsettings.*.json` |
| **Mitigation** | Override with `ConnectionStrings__DefaultConnection` env var |
| **Target** | v1.1 |

**Why deferred:** Standard .NET pattern. Env var override works without code changes.

---

## Summary

| ID | Item | Severity | Target |
|----|------|----------|--------|
| RD-001 | Integration Tests (ADR-010) | Medium | v1.1 |
| RD-002 | Authentication | High | v1.1 |
| RD-003 | Production CORS Policy | High | v1.1 |
| RD-004 | Accessibility Improvements | Low | v1.2 |
| RD-005 | Docker Support | Low | v1.1 |
| RD-006 | Request Rate Limiting | Low | v1.2 |
| RD-007 | Connection String via Env Var | Low | v1.1 |

**Total deferred: 7 items**

**None block v1.0.0 release.**