\# Commit Message Guidelines



\## Purpose



This document defines the commit message conventions used throughout the JLek POS project.



The goal is to make the Git history clear, consistent, searchable, and meaningful for both current and future developers.



\---



\## Objectives



Commit messages should:



\- Clearly describe what changed

\- Explain the purpose of the change

\- Improve repository traceability

\- Support long-term maintenance

\- Follow a consistent format



\---



\## General Principles



Every commit should represent one logical change.



One Commit = One Purpose



Avoid combining unrelated changes in a single commit.



\---



\## Commit Format



The project follows the Conventional Commits specification.



```

<type>(<scope>): <description>

```



Scope is optional.



Examples:



```

feat(order): add order aggregate



fix(payment): correct cash rounding



docs(domain): update business rules



refactor(application): simplify order handler



test(domain): add order unit tests



chore: initialize solution

```



\---



\## Commit Types



\### feat



A new feature.



Example:



```

feat(order): implement order cancellation

```



\---



\### fix



A bug fix.



Example:



```

fix(payment): prevent negative balance

```



\---



\### refactor



Code restructuring without changing behavior.



Example:



```

refactor(domain): simplify aggregate logic

```



\---



\### docs



Documentation only.



Example:



```

docs(architecture): update dependency rules

```



\---



\### test



Tests only.



Example:



```

test(order): add aggregate tests

```



\---



\### chore



Maintenance work.



Examples:



\- project initialization

\- dependency updates

\- cleanup



\---



\### build



Changes related to build configuration.



\---



\### ci



Continuous Integration configuration.



\---



\### perf



Performance improvements.



\---



\### style



Formatting only.



No behavior changes.



\---



\## Good Commit Messages



```

feat(order): add kitchen ticket generation



fix(stock): prevent duplicate deduction



docs(workflow): update build process



refactor(domain): split payment service

```



\---



\## Avoid



Avoid messages such as:



```

update



fix



work



test



asdf



แก้ไข



ปรับ



commit

```



These messages provide little historical value.



\---



\## Commit Size



Prefer small commits.



Small commits are:



\- Easier to review

\- Easier to revert

\- Easier to understand



\---



\ ## Before Committing



Verify:



```

dotnet build

```



Review:



```

git status



git diff

```



Then commit.



\---



\## Final Principle



A commit message should answer one simple question:



\*\*"What changed?"\*\*



without requiring someone to read the entire code diff.

