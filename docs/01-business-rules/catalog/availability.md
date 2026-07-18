\# Availability



\---



\## Purpose



Availability determines whether a Product, Ingredient, Option, or Modifier may currently be used or sold.



Availability reflects the current business operating condition.



It is not intended to represent inventory quantity.



\---



\## Business Rules



\### Product Availability



A Product may be



\- Available

\- Unavailable



Unavailable Products cannot be ordered.



Historical Orders remain unchanged.



\---



\### Ingredient Availability



Ingredients may be



\- Available

\- Unavailable



The business controls Ingredient availability manually.



Examples



\- Crispy Pork Sold Out

\- Chicken Finished

\- Rice Not Ready



\---



\### Option Availability



Options may be



\- Available

\- Unavailable



Unavailable Options cannot be selected.



\---



\### Modifier Availability



Modifiers may be



\- Available

\- Unavailable



Unavailable Modifiers cannot be applied.



\---



\### Temporary Changes



Availability may change many times during a business day.



Examples



Morning



Crispy Pork



Available



Afternoon



Sold Out



Evening



Cooked Again



Available



\---



\### Ingredient Substitution



Ingredient availability does not automatically determine whether substitute ingredients may be used.



Examples



\- Chicken Rice may be changed to White Rice.

\- Crispy Pork may become available again after additional preparation.



Ingredient substitution is always a business decision.



\---



\### Business Decision



Availability changes are business decisions.



The system must never automatically assume whether a Product, Ingredient, Option, or Modifier should become unavailable.



The business always decides whether an item should be sold, hidden, substituted, or restored.



\---



\## Design Principles



\### Availability is Operational



Availability reflects the restaurant's current operating condition.



It does not represent accounting or inventory.



\---



\### Manual Control



Restaurant staff control availability.



Business decisions always override automation.



\---



\### Historical Integrity



Availability changes never modify historical Orders.



\---



\### Availability Does Not Replace Business Decisions



Availability only represents the current operational status.



It does not decide



\- whether substitute ingredients may be used

\- whether a Product should remain sellable

\- whether additional preparation will make an item available again



These decisions always belong to the business.



\---



\### Simple Before Complex



Current version supports only



\- Available

\- Unavailable



Inventory quantities are outside the current project scope.



\---



\## Examples



Ingredient



Crispy Pork



Unavailable



Possible Business Result



\- Crispy Pork Rice unavailable

\- Mixed Rice without Crispy Pork still available

\- Crispy Pork becomes available again after additional preparation



The appropriate action is always determined by the business.



\---



\## Future Expansion



Future versions may support



\- Automatic Availability

\- Inventory Quantity

\- Low Stock Warning

\- Supplier Integration

\- Estimated Recovery Time



without redesigning the Product Catalog.



\---



End of Document

