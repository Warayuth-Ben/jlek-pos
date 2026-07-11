\# AI Engineering Standard

Version: 1.1

Project: JLek POS



\---



\# Purpose



This document defines the engineering standards that every AI participating in the JLek POS project must follow.



These standards apply to all AI systems, regardless of provider (ChatGPT, Cline, Codex, Claude, Gemini, or others).



If these standards conflict with the AI's general knowledge, the project standards always take precedence.



\---

\# Core Engineering Principles



The AI must always follow these principles.



1\. Documentation before Code



2\. Verification before Conclusion



3\. Understanding before Implementation



4\. Reuse before Creation



5\. Business before Technology



6\. Architecture before Code



7\. Human Approval before Implementation



8\. Facts before Opinions



9\. Small Steps before Large Changes



10\. Never Guess

###### \----------



\# Standard 1 — Documentation First



Before analyzing, designing, or writing code,



the AI must read the relevant project documentation.



Never rely solely on model memory.



\---



\# Standard 2 — Never Guess



If sufficient information is unavailable,



the AI must stop and respond:



> "Insufficient information."



The response must include:



\- Missing information

\- Documentation already reviewed

\- Questions that require clarification



The AI must never invent Business Rules.



\---



\# Standard 3 — Business Before Code



Before implementation,



the AI must understand:



\- Business Problem

\- Business Rules

\- Business Workflow



If these cannot be explained,



implementation must not begin.



\---



\# Standard 4 — Explain Before Implement



Before writing code,



the AI must explain:



\- Proposed solution

\- Reasoning

\- Impact

\- Risks

\- Documentation references



Implementation requires approval.



\---



\# Standard 5 — Architecture First



Architecture is more important than code.



Business is more important than technology.



Code is the result of design.



\---



\# Standard 6 — Documentation is the Source of Truth



Project documentation is the authoritative source.



If documentation conflicts with general AI knowledge,



documentation always wins.



\---



\# Standard 7 — Human Review



The AI must never make final decisions regarding:



\- Business Rules

\- Architecture

\- Aggregate Design

\- State Machines



The AI may recommend.



Humans decide.



\---



\# Standard 8 — Self Review



Before submitting work,



the AI must verify:



\- Business Rules

\- DDD compliance

\- Architecture

\- API contracts

\- Documentation consistency



\---



\# Standard 9 — Documentation Update



If implementation changes:



\- Business Rules

\- Workflow

\- Architecture



the AI must recommend updating the documentation.



\---



\# Standard 10 — Reviewability



Every conclusion must be reviewable.



Every recommendation should be supported by evidence.



\---



\# Standard 11 — Separate Facts from Conclusions



Every report must clearly separate:



\## Verified Facts



Information confirmed from:



\- Documentation

\- Source Code

\- Explicit User Instructions



\## Findings



Conclusions derived from verified facts.



\## Recommendations



Suggestions proposed by the AI.



Never mix these three categories.



\---



\# Standard 12 — Documentation Verification



Documentation references must always be verified.



A documentation reference is considered VERIFIED only if:



\- the file exists

\- the AI has opened and read the file

\- the referenced section or heading actually exists



If verification is impossible,



the AI must respond:



> Documentation reference not verified.



The AI must never invent:



\- filenames

\- folder names

\- document titles

\- headings

\- document structure



Similarity is NOT verification.



Memory is NOT verification.



Inference is NOT verification.



Only verified documentation may be cited.



\---



\# Standard 13 — Repository Verification



Repository structure must always be verified.



The AI must never assume:



\- folders

\- filenames

\- namespaces

\- project structure



If an item cannot be verified,



respond:



> Not verified.



\---



\# Standard 14 — Evidence Based Reasoning



Every factual statement must be traceable to one of the following:



1\. Verified Documentation

2\. Verified Source Code

3\. Explicit User Instruction



If a statement cannot be traced to one of these sources,



it must not be presented as fact.



\---



\# Standard 15 — Missing Documentation



If documentation cannot be found,



the AI must never replace it with an assumed document.



Instead, report:



\- Documentation not found

\- Documentation not verified



and stop.



\---



\# Standard 16 — Zero Assumption Principle



When evidence is missing,



the AI must reduce confidence,



never increase assumptions.



Unknown is preferable to incorrect.



Always prefer:



> Not verified.



over:



\- probably

\- likely

\- should be

\- assumed

\- inferred



Never fill knowledge gaps with speculation.



\---



\# Engineering Principle



The AI must always follow this order:



Documentation



↓



Verification



↓



Understanding



↓



Analysis



↓



Design



↓



Human Approval



↓



Implementation



↓



Self Review



↓



Documentation Update



###### \-------------------

\# Standard 17 — Reuse Before Create



Before creating any new code, the AI must inspect the existing codebase.



The AI must first determine whether an existing implementation can be reused or extended.



The AI must never create a new:



\- Aggregate

\- Entity

\- Value Object

\- Domain Event

\- Repository

\- Command

\- Query

\- DTO

\- Service

\- Endpoint



without first verifying that an equivalent implementation does not already exist.



If an existing implementation can be extended,



reuse is preferred over replacement.



Creating new code without verification is considered an architecture violation.

\-----------------------

\# Standard 18 — Analysis Mode



During Analysis Mode the AI must never modify the repository.



Analysis Mode allows only:



\- Reading documentation

\- Reading source code

\- Inspecting project structure

\- Producing reports

\- Asking questions



Analysis Mode strictly prohibits:



\- Creating files

\- Modifying files

\- Renaming files

\- Deleting files

\- Refactoring

\- Writing code



Implementation may begin only after explicit human approval.

###### \--------------------

\# Standard 19 — Small Increment Principle



The AI must implement only one approved milestone at a time.



Each milestone must be:



\- independently reviewable

\- independently compilable

\- independently testable

\- independently reversible



The AI must stop after completing the approved milestone.



The AI must never continue to the next milestone without approval.

\----------------------

\# Standard 20 — Preserve Existing Architecture



The AI must preserve the existing project architecture.



The AI must never redesign:



\- Domain Model

\- Aggregate Boundaries

\- Business Rules

\- Folder Structure

\- Layer Responsibilities

\- Project Architecture



unless explicitly requested by a human.



When extending the system,



the AI must follow the existing architecture instead of introducing a new one.

\---------------------------

\# AI Compliance Checklist



Before completing any task, the AI should verify:



###### □ Documentation has been read

###### 

###### □ Business Rules are understood

###### 

###### □ Architecture has been verified

###### 

###### □ Repository structure has been verified

###### 

###### □ Existing implementation has been inspected

###### 

###### □ No assumptions were made

###### 

###### □ Documentation references are verified

###### 

###### □ Source code references are verified

###### 

###### □ Facts are separated from recommendations

###### 

###### □ Human approval has been obtained before implementation

###### 

###### □ Self review has been completed

\---------------------------
Verified means the information has been directly confirmed by evidence.

Believed means the information is assumed without verified evidence.


The AI must never mark information as VERIFIED
unless verification has actually been performed.

The word

VERIFIED

is reserved exclusively for evidence that has been directly confirmed.

False verification is considered a critical engineering violation.
-------------------------------
Every VERIFIED statement must include enough evidence
for a human reviewer to independently confirm it.

Examples

✓ Repository path

✓ Exact filename

✓ Exact heading

✓ Exact class

✓ Exact method

✓ Exact namespace

Without evidence,

VERIFIED must not be used.
--------------------------------
The AI must explicitly distinguish between

Verified

Observed

Assumed

Unknown

The AI must never upgrade

Unknown

to

VERIFIED BY

Repository Path

Heading

Evidence.
------------------------------