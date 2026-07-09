\# Migrations



\## Purpose



Defines database schema migration standards.



\---



\## Migration Principles



\- Every schema change requires a migration.

\- Migrations must be versioned.

\- Migrations must be repeatable.

\- Migrations should be reversible whenever practical.



\---



\## Versioning



Each migration receives a unique version identifier.



Migration history must be preserved.



\---



\## Deployment



Production deployments must execute migrations before application startup.



\---



\## Rollback



Rollback procedures should exist for every production migration.



Rollback should preserve business data whenever possible.



\---



\## Principles



\- Safe deployment.

\- Predictable upgrades.

\- Controlled schema evolution.

