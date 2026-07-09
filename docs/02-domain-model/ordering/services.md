\# Ordering Domain Services



\## Purpose



Defines business operations that do not naturally belong to a single Entity.



\---



\# Order Creation Service



Creates a valid Order inside an Order Session.



\---



\# Order Confirmation Service



Confirms an Order and publishes business events.



\---



\# Add-on Order Service



Creates an additional Order within an existing Order Session.



\---



\# Order Cancellation Service



Cancels an Order according to business policy.



\---



\## Principles



\* Services contain business operations.

\* Services do not own business state.

\* Services operate on Aggregates.



