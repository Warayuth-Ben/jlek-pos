\# Error Handling



\## Purpose



Define consistent error handling across the system.



\---



\## Error Categories



Business Errors



Examples



\- Order already completed

\- Bill already closed

\- Table unavailable



\---



Validation Errors



Examples



\- Invalid input

\- Missing required fields



\---



Infrastructure Errors



Examples



\- Database unavailable

\- Printer offline



\---



Unexpected Errors



Examples



\- Null reference

\- Unknown exception



\---



\## Principles



\- Fail fast.

\- Return meaningful errors.

\- Never expose internal implementation.

\- Log unexpected failures.

