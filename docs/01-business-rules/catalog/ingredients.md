\# Ingredients



\---



\## Purpose



Ingredients represent the physical materials used to prepare Products.



Ingredients are business resources used during food preparation.



Ingredients are not sellable products.



The current Product Catalog defines Ingredients only as business resources. Product recipes are outside the current implementation scope.



\---



\## Business Rules



\### Ingredient Definition



An Ingredient represents a physical food component used by one or more Products.



Examples



\- Boiled Chicken

\- Fried Chicken

\- Crispy Pork

\- Red Pork

\- Chicken Rice

\- White Rice

\- Egg

\- Cucumber



\---



\### Ingredient Identity



Each Ingredient has a unique identity.



Changing an Ingredient name must not affect historical Orders.



\---



\### Ingredient Status



Each Ingredient may be



\- Available

\- Unavailable



Availability is controlled by the business.



\---



\### Ingredient Usage



One Ingredient may be used by multiple Products.



Examples



Boiled Chicken



Used by



\- Chicken Rice

\- Chicken Rice (White Rice)

\- Mixed Chicken Rice



\---



\### Product Dependency



Products may depend on one or more Ingredients.



If a required Ingredient becomes unavailable, the business may choose to



\- stop selling affected Products

\- hide affected Products

\- replace the Ingredient if permitted



The appropriate action is always determined by the business.



\---



\### Temporary Availability



Ingredient availability represents temporary operational status.



Examples



\- Crispy Pork sold out

\- Chicken finished

\- Rice cooking

\- Egg unavailable



Availability may change multiple times during a single business day.



\---



\### Inventory Quantity



The current version does not track inventory quantities.



Only business availability is maintained.



Examples



\- Available

\- Unavailable



\---



\## Design Principles



\### Ingredients are Operational Resources



Ingredients support food preparation.



They are not customer-facing Products.



\---



\### Ingredient Availability is Simple



The current Product Catalog records only whether an Ingredient is available.



Detailed inventory management belongs to future milestones.



\---



\### Ingredients are Independent from Recipes



The Product Catalog identifies Ingredients only.



Recipe definitions, quantities, and preparation rules are intentionally outside the current implementation scope.



\---



\### Products Reference Ingredients



Products may reference Ingredients.



Ingredients never reference Products or customer Orders.



\---



\### Availability Does Not Replace Business Decisions



Ingredient availability represents operational status only.



The business always decides whether



\- a Product should remain sellable

\- an Ingredient may be substituted

\- additional preparation should restore availability



\---



\### Business Controls Availability



Staff members may manually update Ingredient availability at any time.



The system should immediately reflect the current business state.



\---



\## Examples



Ingredient



Crispy Pork



Status



Unavailable



Possible Business Result



\- Crispy Pork Rice unavailable

\- Mixed Rice without Crispy Pork still available

\- Crispy Pork becomes available again after additional preparation



The appropriate action is always determined by the business.



\---



\## Future Expansion



Future versions may support



\- Inventory Quantity

\- Low Stock Warning

\- Automatic Availability

\- Purchase Orders

\- Supplier Management

\- Cost Calculation

\- Waste Tracking

\- Recipe Management



These capabilities should extend the Ingredient domain without redesigning the Product Catalog.



\---



End of Document

