\# AI Project Context



\## Purpose



This document provides context for future AI sessions.



It summarizes the current state of the JLek POS project so future AI assistants can continue development without re-discovering previous decisions.



This is not user documentation.



This is AI context.



\---



\# Current Project Status



Current Phase



Project Initialization Completed



Documentation Foundation Completed



Ready to begin Domain Foundation.



\---



\# Repository Status



Completed



\- Git Repository initialized

\- GitHub connected

\- Multi-project solution created

\- Project references configured

\- Solution builds successfully



Projects



\- Domain

\- Application

\- Infrastructure

\- Shared

\- Web



Project structure follows standard .NET practices.



\---



\# Documentation Status



The following documentation has been completed and reviewed.



✅ 00-standards



Purpose



Engineering standards and development workflow.



Status



Frozen.



\---



✅ 01-business-rules



Purpose



Restaurant business knowledge.



Status



Frozen.



\---



✅ 02-domain-model



Purpose



DDD model.



Status



Frozen.



Notes



Removed duplicated document:



domain-decisions.md



Reason



Duplicated ubiquitous-language.md



\---



✅ 03-system-use-cases



Purpose



Application behavior.



Status



Frozen.



\---



✅ 04-state-machines



Purpose



Business lifecycle.



Status



Frozen.



\---



\# Important Decisions



Repository is NOT a source code repository.



Repository is a project knowledge base.



It contains:



\- Business

\- Architecture

\- Design

\- Documentation

\- Source Code

\- Decisions



Repository is considered the project's memory.



\---



Documentation is considered complete.



Avoid unnecessary modifications.



Only modify documentation when:



\- Business changes

\- Architecture changes

\- Factual mistakes are found



Avoid documentation churn.



\---



\# Development Philosophy



Business



↓



Domain



↓



Use Cases



↓



State Machines



↓



Implementation



Implementation must always follow documentation.



Never reverse this dependency.



\---



AI should never invent business rules.



Always follow documentation.



If documentation and implementation differ,



documentation wins until intentionally updated.



\---



\# Current Build Status



Solution



PASS



Build



PASS



Project References



PASS



Architecture



Stable



Documentation



Stable



\---



\# Lessons Learned During Initialization



Several issues occurred during the first initialization attempt.



The project workflow has been updated.



Lessons learned:



\- Never create internal project folders before project initialization is complete.

\- Build after every milestone.

\- Follow standard .NET project structure.

\- Do not use force operations unnecessarily.

\- Keep documentation frozen after review.

\- Health Check after every phase.



\---



\# Development Workflow



For every implementation:



Review Documentation



↓



Design



↓



Implement



↓



Build



↓



Health Check



↓



Commit



Never skip Build.



Never skip Health Check.



\---



\# Next Phase



Phase 2



Domain Foundation



Implementation order:



1\. Domain Project Skeleton



2\. Shared Kernel



3\. Entity



4\. Aggregate Root



5\. Value Object



6\. Domain Event



7\. Business Rule



8\. Result



9\. Money



10\. Quantity



11\. Build



12\. Commit



Ordering Aggregate starts only after Domain Foundation is complete.



\---



\# Coding Guidelines



Prefer clarity over cleverness.



Keep architecture consistent.



Avoid premature optimization.



Avoid unnecessary abstractions.



Always preserve Clean Architecture dependency rules.



\---



\# User Preferences



The project owner prefers:



\- Build stability over implementation speed.

\- Long-term maintainability.

\- Clean Architecture.

\- Domain Driven Design.

\- Documentation-first development.

\- Small incremental commits.

\- Health Check after every phase.

\- Minimal refactoring after documentation freeze.



The user prefers understanding concepts before implementation.



Explain architectural reasoning before writing code.



\---



\# AI Instructions



Do not redesign completed documentation.



Do not rename folders unless necessary.



Do not introduce new architectural patterns without justification.



Prefer extending existing standards over creating new ones.



Treat documentation as the project's Single Source of Truth.



Always explain WHY before HOW.



The user values architectural understanding more than rapid code generation.



\---



\# Current Milestone



Foundation Complete



Ready for Domain Implementation.

