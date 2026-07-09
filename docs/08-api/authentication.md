\# Authentication



\## Purpose



Defines how clients authenticate with the system.



\---



\## Authentication Principles



\- Every protected endpoint requires authentication.

\- Authentication verifies identity.

\- Authentication occurs before authorization.



\---



\## Supported Mechanisms



The implementation may support:



\- Session Authentication

\- Token Authentication



\---



\## Token Rules



Authentication tokens should:



\- Be securely generated.

\- Have expiration.

\- Be revocable.



\---



\## Session Rules



Sessions should:



\- Expire after inactivity.

\- Be securely stored.

\- Be invalidated after logout.



\---



\## Security Principles



\- Never expose credentials.

\- Never transmit passwords in plain text.

\- Always validate authentication before processing requests.

