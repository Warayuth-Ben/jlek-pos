\# API Error Handling



\## Purpose



Defines standard API error responses.



\---



\## Error Categories



\- Validation Error

\- Authentication Error

\- Authorization Error

\- Business Error

\- System Error



\---



\## Error Structure



Every error should include:



\- Error Code

\- Error Message



Optional:



\- Error Details

\- Correlation Identifier



\---



\## Principles



\- Never expose internal exceptions.

\- Return meaningful error messages.

\- Use appropriate HTTP status codes.

