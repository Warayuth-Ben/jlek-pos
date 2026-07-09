\# API Design



\## Purpose



Defines REST API design standards.



\---



\## API Style



The system uses RESTful APIs.



\---



\## Resource Naming



\- Use plural resource names.

\- Use lowercase URLs.

\- Use hyphen-separated words.



Examples



/orders



/bills



/payments



\---



\## HTTP Methods



GET



Retrieve resources.



POST



Create resources.



PUT



Replace resources.



PATCH



Partially update resources.



DELETE



Remove resources when permitted.



\---



\## HTTP Status Codes



200 OK



201 Created



204 No Content



400 Bad Request



401 Unauthorized



403 Forbidden



404 Not Found



409 Conflict



422 Unprocessable Entity



500 Internal Server Error



\---



\## Idempotency



PUT



DELETE



Should be idempotent.



POST



May not be idempotent unless explicitly designed.



\---



\## Pagination



Large collections should support pagination.



\---



\## Filtering



Collections may support filtering.



\---



\## Sorting



Collections may support sorting.



\---



\## Design Principles



\- Predictable URLs.

\- Consistent HTTP semantics.

\- Resource-oriented design.

