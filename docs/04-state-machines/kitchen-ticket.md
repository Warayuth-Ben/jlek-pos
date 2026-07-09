\# Kitchen Ticket State Machine



\## Purpose



Defines the lifecycle of a Kitchen Ticket from creation until all food has been served.



The Kitchen Ticket State Machine governs all kitchen preparation activities.



\---



\## Lifecycle Diagram



```text

&#x20;                    Start Preparation

Pending --------------------------------▶ Preparing

&#x20;                                           │

&#x20;                                           │ Complete Preparation

&#x20;                                           ▼

&#x20;                                        Ready

&#x20;                                           │

&#x20;                                           │ Serve Items

&#x20;                                           ▼

&#x20;                                         Served

```



\---



\## States



| State | Editable | Final | Description |

|--------|:--------:|:-----:|-------------|

| Pending | No | No | The Kitchen Ticket has been created and is waiting for preparation. |

| Preparing | No | No | Kitchen staff are preparing the food. |

| Ready | No | No | Food preparation is complete and ready to be served. |

| Served | No | Yes | All items have been served to the customer. |



\---



\## Transition Matrix



| Current State | Actor | Trigger | Guard | Next State |

|---------------|-------|----------|-------|------------|

| Pending | Kitchen Staff | Start Preparation | Always | Preparing |

| Preparing | Kitchen Staff | Complete Preparation | All Kitchen Items prepared | Ready |

| Ready | Kitchen Staff | Serve Items | All prepared items served | Served |



\---



\## Constraints



\- Every Kitchen Ticket belongs to exactly one Order.

\- Every Kitchen Ticket contains at least one Kitchen Item.

\- Preparation cannot begin before the associated Order is confirmed.

\- Served Kitchen Tickets are immutable.

\- Direct transitions not defined in the Transition Matrix are prohibited.



\---



\## Related System Use Cases



\- Create Kitchen Ticket

\- Start Preparation

\- Complete Preparation

\- Serve Items



\---



\## Related Domain Objects



\### Aggregate



\- Kitchen Ticket



\### Related Aggregates



\- Order Session



\### Entities



\- Kitchen Ticket

\- Kitchen Item



\---



\## Notes



Each confirmed Order creates one Kitchen Ticket.



Every confirmed Add-on Order creates an additional Kitchen Ticket.



Kitchen Ticket lifecycle is independent of payment.

