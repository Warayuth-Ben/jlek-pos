\# Dining Table State Machine



\## Purpose



Defines the lifecycle of a Dining Table while serving dine-in customers.



The Dining Table State Machine governs table availability throughout a customer session.



\---



\## Lifecycle Diagram



```text

&#x20;               Assign Table

Available ----------------------------▶ Occupied

&#x20;   ▲                                     │

&#x20;   │                                     │ Release Table

&#x20;   └─────────────────────────────────────┘

```



\---



\## States



| State | Allows Business Changes | Final | Description |

|--------|:-----------------------:|:-----:|-------------|

| Available | Yes | No | The table is available for a new Order Session. |

| Occupied | Yes | No | The table is assigned to an active Order Session. |



\---



\## Transition Matrix



| Current State | Actor | Trigger | Guard | Next State |

|---------------|-------|----------|-------|------------|

| Available | Restaurant Staff | Assign Table | Table available | Occupied |

| Occupied | POS System | Release Table | Order Session completed | Available |



\---



\## Constraints



\- A Dining Table can belong to only one active Order Session.

\- Available tables cannot contain an active Order Session.

\- Occupied tables cannot be assigned to another Order Session.

\- Direct transitions not defined in the Transition Matrix are prohibited.



\---



\## Related System Use Cases



\- Assign Table

\- Transfer Table

\- Merge Tables

\- Split Tables

\- Release Table



\---



\## Related Domain Objects



\### Aggregate



\- Dining Table



\### Related Aggregates



\- Order Session



\---



\## Notes



Table availability is independent of payment.



A table becomes Available only after the associated Order Session has been completed.

