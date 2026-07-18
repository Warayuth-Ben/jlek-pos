# Project Governance

Version: 1.0
Last Updated: 2026-07-18

---

## Feature Lifecycle

```
Designed → Modeled → Implemented → API Ready → Client Ready → UI Ready → Verified → UAT → Frozen
```

| Stage | Gate | Owner | Exit Criteria |
|-------|------|-------|---------------|
| **Designed** | Architecture Review | Architect | Business scenario + architecture approved |
| **Modeled** | Domain Review | Domain Expert | Domain model, events, rules complete and reviewed |
| **Implemented** | Code Review | Developer | All layers coded per architecture |
| **API Ready** | API Review | Backend | Endpoints deployed, responses conform to contracts |
| **Client Ready** | Client Review | Frontend | Typed API Client exists and tested |
| **UI Ready** | UI Review | Designer | UI component exists and works |
| **Verified** | QA Review | QA | Integration tests pass, no regressions |
| **UAT** | User Review | Product Owner | User acceptance test passed |
| **Frozen** | Release Review | Architect | No changes except security/bug fixes per Freeze Rules |

---

## Definition of Done

A feature is complete when ALL of the following are satisfied:

| Criteria | Description |
|----------|-------------|
| ✅ Build Passes | `dotnet build` succeeds with 0 errors, 0 warnings |
| ✅ Tests Pass | All existing + new tests pass |
| ✅ Architecture Preserved | No architecture drift from frozen decisions |
| ✅ Business Rules Preserved | All business rules remain in Domain layer |
| ✅ Naming Correct | Follows project naming conventions |
| ✅ No Analyzer Violations | No warnings or analyzer errors |
| ✅ Self Review Complete | AI has reviewed its own work |
| ✅ Human Review Complete | Human has approved the implementation |
| ✅ Documentation Updated | AI Context, Feature Registry, Project Status updated |
| ✅ Commit Ready | Changes are committed with meaningful message |

---

## Change Boundary

| Area | Change Allowed? | Approval Required |
|------|----------------|-------------------|
| Domain layer | ❌ Frozen — no changes | Architect + Product Owner |
| Application handlers | ❌ Frozen — no changes | Architect |
| Infrastructure | ❌ Frozen — no changes | Architect |
| API contracts | ❌ Frozen — no changes | Architect |
| Business rules | ❌ Frozen — no changes | Domain Expert |
| Aggregate boundaries | ❌ Frozen — no changes | Architect |
| Presentation (Web UI) | ✅ New code only | Tech Lead |
| Typed API Clients | ✅ New clients only | Tech Lead |
| UI components | ✅ New components only | Tech Lead |
| Integration tests | ✅ Bug fixes only | QA |
| Documentation | ✅ Always allowed | Self-review |
| Bug fixes | ✅ Limited to Frozen areas | Architect |
| Security fixes | ✅ Always allowed | Security Lead |

---

## Impact Analysis

Before any change, assess:

1. **Architecture Impact**: Does this change affect any frozen architecture decision?
2. **Business Rule Impact**: Does this change affect any business rule?
3. **API Contract Impact**: Does this change affect any API endpoint or response DTO?
4. **Database Impact**: Does this change affect any EF Core configuration or migration?
5. **Test Impact**: How many existing tests need updating?
6. **Documentation Impact**: Which docs need updating?

If any impact is identified, approval from the component owner is required.

---

## Approval Gates

| Gate | Required Approvers | Escalation |
|------|-------------------|------------|
| Business Scenario | Product Owner | Stakeholder |
| Architecture Design | Architect | CTO |
| Implementation | Tech Lead | Architect |
| UI Design | Designer | Product Owner |
| Test Plan | QA Lead | Tech Lead |
| Release | All above | Stakeholder |

---

## Freeze Rules

1. **No behavioral changes** to frozen components except security/bug fixes
2. **Bug fixes** must be verified by integration tests before merge
3. **Security fixes** bypass freeze but must be documented
4. **Architecture changes** require a new ADR and milestone
5. **Frozen modules** may be read as reference but never modified

---

## Deferred Feature Rules

1. Deferred features must be documented in `FEATURE-REGISTRY.md` with stage "❌ Not Started"
2. Deferred features must have a valid reason (e.g., dependency not met, out of scope)
3. Deferred features may be promoted to active by Product Owner approval
4. Promoting a deferred feature requires re-reading all relevant documentation

---

## Documentation Update Rules

1. Every feature completion must update:
   - `FEATURE-REGISTRY.md` — stage, UI file, test count
   - `TRACEABILITY-MATRIX.md` — all columns
   - `PROJECT-CONTROL-CENTER.md` — MVP progress, sprint status
   - `99-project-status.md` — overall progress
   - `CHANGELOG.md` — summary of changes

2. Documentation must be updated BEFORE marking a feature complete
3. Documentation changes must be reviewed by human
4. Outdated documentation blocks feature promotion

---

## Cross-Reference

| Document | Purpose |
|----------|---------|
| `FEATURE-REGISTRY.md` | Feature lifecycle tracking |
| `TRACEABILITY-MATRIX.md` | End-to-end traceability |
| `PROJECT-CONTROL-CENTER.md` | Entry point for AI sessions |
| `110-master-implementation-plan.md` | Implementation plan |
| `99-project-status.md` | Overall project status |
| `98-ai-context.md` | AI context |
| `91-session-handoff.md` | Session handoff |

Governance Baseline

Version

1.0

Status

Approved

Date

2026-07-18