\# Dependency Rules



\## Purpose



Defines the allowed dependencies between architectural layers.



\---



\## Dependency Principle



Dependencies always point inward.



\---



\## Allowed Dependencies



| Layer | May Depend On |

|---------|---------------|

| Presentation | API, Application |

| API | Application |

| Application | Domain |

| Domain | None |

| Infrastructure | Domain, Application |

| Bootstrap | All Layers |



\---



\## Forbidden Dependencies



\- Domain → Application

\- Domain → Infrastructure

\- Domain → API

\- Domain → Presentation



\- Application → Presentation



\- API → Infrastructure Business Logic



\---



\## Cross Layer Rules



\- Business Rules belong only to the Domain layer.

\- Technical implementation belongs only to Infrastructure.

\- UI never directly accesses the database.

\- API never implements business logic.



\---



\## Dependency Direction



```text

Presentation



↓



API



↓



Application



↓



Domain



Infrastructure ─────▶ Domain



Infrastructure ─────▶ Application

```



\---



\## Principles



\- Low coupling

\- High cohesion

\- Stable dependencies

\- Explicit boundaries

