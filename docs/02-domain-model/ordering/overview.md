\# Ordering Domain Overview



\## Purpose



Defines the domain model responsible for customer ordering.



The Ordering domain manages customer orders from creation until they are completed, cancelled, or handed over to other business contexts.



It owns the business rules related to order composition, order modifications, service channels, and customer requests.



\---



\## Responsibilities



The Ordering domain is responsible for:



\* Creating orders

\* Managing order items

\* Managing add-on orders

\* Recording customer notes

\* Managing service channels

\* Maintaining order lifecycle

\* Publishing business events



\---



\## Collaborates With



\* Table Domain

\* Kitchen Domain

\* Payment Domain



The Ordering domain does not perform kitchen preparation or payment processing directly.



