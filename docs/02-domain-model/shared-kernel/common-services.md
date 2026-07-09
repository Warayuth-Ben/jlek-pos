\# Common Domain Services



\## Purpose



Defines reusable domain services shared across multiple bounded contexts.



A Common Domain Service contains business logic that does not belong to a single Entity or Aggregate.



\---



\# Business Number Service



Responsibilities:



\* Generate Order Numbers

\* Generate Bill Numbers

\* Generate Kitchen Ticket Numbers



\---



\# Business Clock Service



Responsibilities:



\* Provide business timestamps

\* Ensure consistent time references



\---



\# Pricing Service



Responsibilities:



\* Calculate item totals

\* Calculate order totals

\* Apply pricing rules



\---



\# Validation Service



Responsibilities:



\* Execute shared business validations

\* Enforce common business constraints



\---



\## Principles



\* Services are stateless.

\* Services do not own business data.

\* Services operate on domain objects.

\* Services must not replace Entity responsibilities.



