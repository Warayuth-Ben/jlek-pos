\# Item Quantity



\## Purpose



Defines business rules for the quantity of each order item.



\---



\## Business Rules



\### 1. Every order item has a quantity.



Quantity represents the number of units requested by the customer.



\---



\### 2. Quantity must be greater than zero.



Zero or negative quantities are not valid business values.



\---



\### 3. Quantity may be modified while the order remains editable.



All quantity changes become part of the business history.



\---



\### 4. Quantity directly affects pricing and kitchen preparation.



Business calculations use the current approved quantity.



