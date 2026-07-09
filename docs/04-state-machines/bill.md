\# Bill State Machine



\## Purpose



Defines the lifecycle of a Bill from creation until final closure.



The Bill State Machine governs all payment-related business states.



\---



\## Lifecycle Diagram



```text

&#x20;                 Partial Payment

Open --------------------------------▶ Partially Paid

&#x20;│                                         │

&#x20;│ Full Payment                            │ Remaining Payment

&#x20;▼                                         ▼

Paid --------------------------------------------┘

&#x20;│

&#x20;│ Close Bill

&#x20;▼

Closed

```



\---



\## States



| State | Editable | Final | Description |

|--------|:--------:|:-----:|-------------|

| Open | Yes | No | The Bill has been created and is awaiting payment. |

| Partially Paid | Yes | No | One or more Payments have been recorded, but an outstanding balance remains. |

| Paid | No | No | The outstanding balance is zero. |

| Closed | No | Yes | The Bill has been finalized and can no longer be modified. |



\---



\## Transition Matrix



| Current State | Actor | Trigger | Guard | Next State |

|---------------|-------|----------|-------|------------|

| Open | Restaurant Staff | Process Payment | Payment is less than outstanding balance | Partially Paid |

| Open | Restaurant Staff | Process Payment | Outstanding balance becomes zero | Paid |

| Partially Paid | Restaurant Staff | Process Payment | Outstanding balance becomes zero | Paid |

| Paid | Restaurant Staff | Close Bill | Always | Closed |



\---



\## Constraints



\- Every Bill belongs to exactly one Order Session.

\- Every Payment belongs to exactly one Bill.

\- A Bill may contain multiple Payments.

\- A Bill cannot be Closed until it reaches the Paid state.

\- Closed Bills are immutable.

\- Direct transitions not defined in the Transition Matrix are prohibited.



\---



\## Related System Use Cases



\- Request Payment

\- Process Payment

\- Refund Payment

\- Close Bill



\---



\## Related Domain Objects



\### Aggregate



\- Bill



\### Related Aggregates



\- Order Session



\### Entities



\- Bill

\- Payment

\- Refund



\### Value Objects



\- Money



\---



\## Notes



Payment completion and Bill closure are separate business concepts.



A Paid Bill indicates that the customer has settled the balance.



A Closed Bill indicates that the business transaction has been finalized.

