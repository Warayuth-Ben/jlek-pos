\# Error Handling



\## Purpose



Defines how errors are classified, propagated, and presented throughout the system.



\---



\## Error Categories



\- Validation Errors

\- Business Errors

\- Application Errors

\- Infrastructure Errors

\- Unexpected Errors



\---



\## Validation Errors



Examples



\- Missing required field.

\- Invalid data format.

\- Invalid quantity.



\---



\## Business Errors



Examples



\- Order already confirmed.

\- Bill already closed.

\- Dining Table unavailable.



\---



\## Application Errors



Examples



\- Requested Aggregate not found.

\- Invalid application workflow.



\---



\## Infrastructure Errors



Examples



\- Database unavailable.

\- Network timeout.

\- External service failure.



\---



\## Unexpected Errors



Examples



\- Programming error.

\- Unhandled exception.

\- Unknown system failure.



\---



\## Error Principles



\- Return meaningful error messages.

\- Never expose internal implementation details.

\- Preserve business consistency.

\- Log unexpected errors.

\- Fail safely.



\---



\## Error Response



Every error should include:



\- Error Code

\- Error Category

\- Human-readable Message

\- Timestamp

\- Correlation Identifier (when available)



\---



\## Recovery Strategy



Whenever possible:



\- Allow retry for transient failures.

\- Rollback failed transactions.

\- Preserve data integrity.

