\# Constraints



\## Purpose



Defines database constraints that protect data integrity.



\---



\## Constraint Types



\- Primary Key

\- Foreign Key

\- Unique Constraint

\- Check Constraint

\- Not Null Constraint



\---



\## Primary Key



Every table must contain exactly one Primary Key.



Primary Keys are immutable.



\---



\## Foreign Key



Foreign Keys preserve relationships between business entities.



Every Foreign Key must reference an existing record.



\---



\## Unique Constraint



Use Unique Constraints to prevent duplicate business data.



Examples:



\- Bill Number

\- Order Number



\---



\## Check Constraint



Check Constraints enforce simple database rules.



Examples:



\- Quantity > 0

\- Amount >= 0



Business Rules belong in the Domain Layer.



\---



\## Not Null



Required business data should use NOT NULL whenever appropriate.



\---



\## Principles



\- Protect data integrity.

\- Prevent invalid data.

\- Keep constraints simple.

