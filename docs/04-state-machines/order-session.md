\# Order Session State Machine



\## Purpose



Defines the lifecycle of an Order Session from the first customer order until the business transaction is completed.



The Order Session coordinates Orders, Kitchen Tickets and the Bill.



\---



\## Lifecycle Diagram



```text

&#x20;               Close Bill

Active ----------------------------▶ Completed

```



\---



\## States



| State | Allows Business Changes | Final | Description |

|--------|:-----------------------:|:-----:|-------------|

| Active | Yes | No | The customer transaction is in progress. |

| Completed | No | Yes | The customer transaction has finished. |



\---



\## Transition Matrix



| Current State | Actor | Trigger | Guard | Next State |

|---------------|-------|----------|-------|------------|

| Active | Restaurant Staff | Close Bill | Bill is Closed | Completed |



\---



\## Constraints



\- Every Order Session owns one Bill.

\- Every Order belongs to one Order Session.

\- Completed Order Sessions are immutable.

\- Direct transitions not defined in the Transition Matrix are prohibited.



\---



\## Related System Use Cases



\- Create Order

\- Create Add-on Order

\- Process Payment

\- Close Bill



\---



\## Related Domain Objects



\### Aggregate



\- Order Session



\### Related Aggregates



\- Bill

\- Dining Table

\- Kitchen Ticket



\### Entities



\- Order



\---



\## Notes



The Order Session represents the complete customer transaction.



Completion occurs only after the Bill has been successfully closed.

