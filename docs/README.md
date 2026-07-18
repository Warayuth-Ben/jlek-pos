# Documentation Portal

Welcome to the JLek POS documentation.

---

# Project Status

**Release Candidate v1.0.0-rc1 — Tagged and Pushed**

| Area | Status |
|------|--------|
| Architecture Baseline | 🧊 Frozen |
| Domain (7 Aggregates) | 🧊 Frozen |
| Application (20 Commands + 12 Queries) | 🧊 Frozen |
| Infrastructure (7 Repositories, PostgreSQL) | 🧊 Frozen |
| API (47 Endpoints) | 🧊 Frozen |
| Integration Testing (155 tests) | 🧊 Frozen |
| Printing Infrastructure (57 unit tests) | 🧊 Frozen |
| CI/CD (GitHub Actions) | 🧊 Frozen |
| **Web UI (4 of 6 pages)** | ✅ **95% Complete** |
| Authentication / Authorization | ⏸️ Deferred |
| Settings Page | ⏸️ Deferred |

Next Phase: **v1.1 Development** (Settings, SignalR, Monthly Reports, Export)

---

# Reading Order

1. `PROJECT-CHARTER.md`
2. `PROJECT-ROADMAP.md`
3. `ARCHITECTURE.md`
4. `00-standards/` — Engineering standards, conventions
5. `01-business-foundation/` — Restaurant business knowledge
6. `01-business-rules/` — Business rules per module
7. `02-domain-model/` — Domain concepts and bounded contexts
8. `03-system-use-cases/` — System interactions
9. `04-state-machines/` — Business state machines
10. `05-application/` — Application layer design
11. `06-technical/` — Technical architecture
12. `07-database/` — Database schema
13. `08-api/` — API design
14. `09-ui/` — UI architecture
15. `10-testing/` — Testing strategy
16. `11-development/` — Development workflow
17. `97-AI-Docs/` — AI onboarding and project status (start here)
18. `20-thai/` — Thai language documentation

---

# Key Architecture Decisions

| Decision | Choice |
|----------|--------|
| Architecture | Clean Architecture + DDD + CQRS |
| Language | C# (.NET 8) |
| Database | PostgreSQL 17 |
| ORM | EF Core |
| API | ASP.NET Core Minimal API |
| UI | Blazor WebAssembly |
| Testing | xUnit + Testcontainers + FluentAssertions |
| IDs | Strongly Typed IDs (GUID, Domain-generated) |
| Repository | One per aggregate, 4-method contract |
| Adapters | Interface in Application, Implementation in Infrastructure |

---

# Documentation Index

| Directory | Purpose |
|-----------|---------|
| `01-business-foundation/` | 14 business principles, 146 business scenarios |
| `01-business-rules/` | Business rules for 7 modules |
| `02-domain-model/` | Domain model, bounded contexts, ubiquitous language |
| `03-system-use-cases/` | Use cases per module |
| `04-state-machines/` | State machines (Order, Kitchen, Table, Bill, OrderSession) |
| `05-application/` | Application layer design |
| `06-technical/` | Technical architecture decisions |
| `07-database/` | Database schema, migration, indexing |
| `08-api/` | API design, versioning, error handling |
| `09-ui/` | UI architecture, design system, components |
| `10-testing/` | Testing strategy, unit/integration/e2e |
| `11-development/` | Development workflow, conventions, CI/CD |
| `20-thai/` | Thai language documentation |
| `21-knowledge-book/` | Long-term restaurant knowledge preservation |
| `30-analysis/` | Design analysis, methodology, domain blueprint |
| `96-architecture/` | Module-specific architecture |
| `97-AI-Docs/` | AI onboarding and project status (start here) |
| `98-decisions/` | Architecture Decision Records |
| `99-references/` | External references |