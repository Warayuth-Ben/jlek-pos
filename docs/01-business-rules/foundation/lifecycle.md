\# Order Lifecycle



\## Purpose



Defines the standard lifecycle of an order.



\---



\## Business Rules



\### 1. Every order has a lifecycle.



An order always progresses through defined business states.



\---



\### 2. Lifecycle order



The normal sequence is:



Draft



↓



Confirmed



↓



Preparing



↓



Ready



↓



Served



↓



Completed



\---



\### 3. Cancellation



An order may be cancelled before completion.



Cancelled orders remain in history.



They are never physically deleted.



\---



\### 4. Completion



A completed order is immutable.



Business information must no longer be modified.



Administrative corrections must create separate business records.



