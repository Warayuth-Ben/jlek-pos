\# Git Workflow



\## Purpose



This document defines the Git workflow used throughout the JLek POS project.



The purpose is to maintain a clean repository history, reduce integration problems, and ensure that every change is traceable.



\---



\## Objectives



The Git workflow aims to:



\- Keep commit history clean

\- Make every change traceable

\- Reduce merge conflicts

\- Support long-term maintenance

\- Improve collaboration



\---



\## Repository Structure



The project uses a single Git repository.



Source code, documentation, and configuration files are maintained together.



\---



\## Branch Strategy



Current strategy:



\- main



Development is currently performed directly on the `main` branch.



If team development becomes necessary, feature branches may be introduced.



\---



\## Standard Workflow



Every change should follow this sequence:



Planning



↓



Implementation



↓



Build



↓



Review



↓



Commit



↓



Push



\---



\## Build Rule



Before every commit:



```bash

dotnet build

```



The build must succeed.



Never commit code that does not build successfully.



\---



\## Review Rule



Before committing, review the repository.



```bash

git status

```



Review the actual changes.



```bash

git diff

```



Verify commit history if necessary.



```bash

git log --oneline

```



\---



\## Commit Rule



Each commit should represent one logical change.



Good examples:



\- One feature

\- One bug fix

\- One refactoring

\- One documentation update



Avoid combining unrelated changes.



\---



\## Push Rule



Push only after:



\- Build succeeds

\- Changes are reviewed

\- Commit message is complete



```bash

git push

```



\---



\## Tags



Major milestones should be tagged.



Example:



```bash

git tag v0.1.0



git push origin v0.1.0

```



Suggested milestones:



\- v0.1.0 Solution Initialization

\- v0.2.0 Domain Foundation

\- v0.3.0 Order Aggregate

\- v0.4.0 Application Layer

\- v0.5.0 Infrastructure

\- v1.0.0 Production Ready



\---



\## Force Push



Avoid force push whenever possible.



```bash

git push --force

```



should only be used in exceptional situations where the consequences are fully understood.



\---



\## Repository Cleanliness



The repository should never contain generated files.



Examples:



\- bin/

\- obj/

\- .vs/



These should be excluded using `.gitignore`.



\---



\## Documentation



Documentation should be committed together with implementation whenever both are modified.



Documentation should accurately reflect the current system.



\---



\## Final Principle



Git is not only a backup tool.



Git is the complete history of how the software evolves.



Every commit should help future developers understand what changed and why.

