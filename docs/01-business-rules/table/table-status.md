\# Table Status



\## Purpose



Defines the business states of a restaurant table.



\---



\## Business Rules



\### 1. Every table has exactly one current status.



A table cannot exist in multiple statuses simultaneously.



\---



\### 2. A table becomes occupied when the first active order is created.



The table is considered unavailable for new customers until the dining session is completed.



\---



\### 3. A table becomes available only after all associated business activities have finished.



Outstanding orders or unpaid bills prevent the table from becoming available.



\---



\### 4. Table status reflects operational reality.



Status changes must always represent the actual state of restaurant operations.



