\# Coding Standards



\## Purpose



This document defines the coding standards used throughout the JLek POS project.



The purpose of these standards is to ensure that all source code remains consistent, readable, maintainable, and easy to review.



These standards apply to every source file in the project.



\---



\## Objectives



The coding standards aim to:



\- Improve code readability

\- Maintain consistency across the project

\- Reduce unnecessary complexity

\- Encourage clean design

\- Simplify code reviews

\- Support long-term maintenance



\---



\## General Principles



Code should be:



\- Correct

\- Readable

\- Maintainable

\- Testable

\- Predictable



Readable code is preferred over clever code.



\---



\## Design Priorities



When making implementation decisions, always prioritize:



1\. Correctness

2\. Readability

3\. Maintainability

4\. Performance

5\. Cleverness



Performance optimizations should only be introduced when justified.



\---



\## Naming



Use meaningful names.



Good examples:



```csharp

Order

OrderItem

PaymentMethod

KitchenTicket

```



Avoid:



```csharp

Data1

Temp

Obj

Helper

TestClass

```



Names should clearly express intent.



\---



\## Class Design



Each class should have a single responsibility.



A class should represent one concept.



Avoid large classes that manage multiple business concerns.



\---



\## Method Design



Methods should:



\- Perform one task

\- Have descriptive names

\- Be small and focused

\- Avoid unnecessary side effects



Good examples:



```csharp

AddItem()



Cancel()



Complete()



CalculateTotal()

```



Avoid generic names:



```csharp

Run()



Execute()



Do()



Handle()

```



unless the surrounding context makes the purpose obvious.



\---



\## Folder Organization



Every folder should have a clear responsibility.



Avoid generic folders such as:



\- Misc

\- Temp

\- Others

\- Helpers

\- Utils



Folder names should reflect business or architectural concepts.



\---



\## Dependency Rule



Dependencies must always follow the architecture.



```

Web

↓



Infrastructure

↓



Application

↓



Domain

```



Dependencies must never point outward.



\---



\## Business Logic



Business rules belong inside the Domain layer.



Business logic should never be implemented in:



\- UI

\- Infrastructure

\- Database

\- Controllers

\- Components



\---



\## Comments



Write self-explanatory code whenever possible.



Comments should explain:



\- Why



Not:



\- What



Avoid redundant comments.



\---



\## Formatting



Use consistent formatting.



\- One class per file

\- One public type per file

\- Consistent indentation

\- Consistent spacing



Formatting should remain uniform throughout the project.



\---



\## Error Handling



Handle errors explicitly.



Do not silently ignore exceptions.



Meaningful exception messages are preferred.



\---



\## Refactoring



Refactoring should improve readability without changing behavior.



Avoid mixing refactoring with new features in the same commit.



\---



\## Code Reviews



Before committing, verify:



\- The project builds successfully.

\- The implementation follows architecture principles.

\- Naming is consistent.

\- Unused code has been removed.

\- Documentation is updated if required.



\---



\## Final Principle



Code is written for people first and computers second.



The best code is code that another developer can understand quickly and maintain with confidence.

