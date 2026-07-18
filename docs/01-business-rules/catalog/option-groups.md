\# Option Groups



\---



\## Purpose



Option Groups define structured groups of customer selections available for a Product.



An Option Group organizes related Options and defines how customers may select them.



Option Groups improve consistency, pricing, reporting, and user experience.



\---



\## Business Rules



\### Option Group Definition



An Option Group represents one category of customer choices.



Examples



\- Size

\- Rice Type

\- Protein

\- Drink

\- Spice Level



\---



\### Option Group Identity



Each Option Group has a unique identity.



Changing the name of an Option Group must not affect historical Orders.



\---



\### Product Ownership



A Product may contain zero or more Option Groups.



Different Products may use different Option Groups.



\---



\### Selection Rules



Each Option Group defines



\- Minimum Selection

\- Maximum Selection



Examples



Size



Minimum



1



Maximum



1



Protein



Minimum



1



Maximum



4



Drink



Minimum



0



Maximum



1



Unlimited selection is represented by leaving Maximum undefined.



Selection Rules are business rules defined by the Product Catalog.



\---



\### Display Order



The business controls the display order of Option Groups.



Display order affects only the user interface.



\---



\### Availability



Option Groups may be



\- Available

\- Unavailable



Unavailable Option Groups cannot be selected.



Historical Orders remain unchanged.



\---



\### Visibility



Option Groups define where they are visible.



Supported visibility



\- Cashier Only

\- Customer Self Ordering

\- Delivery Platform

\- Hidden



Examples



Size



Customer Self Ordering



Rice Type



Cashier Only



Internal Preparation



Hidden



The business controls visibility for each sales channel.



\---



\## Design Principles



\### Group Before Option



Options should always belong to an Option Group.



Avoid creating standalone Options.



\---



\### Business Controlled



Businesses decide



\- which groups exist

\- which Products use them

\- selection rules

\- visibility



without changing application logic.



\---



\### Independent from Pricing



Option Groups organize customer choices.



Pricing belongs to



\- Options

\- Modifiers



not to the Option Group itself.



\---



\### Option Groups Define Structure



Option Groups define how customers make selections.



The selected values are recorded as Options in the Order.



\---



\## Examples



Chicken Rice



Option Groups



\- Size

\- Rice Type

\- Protein



Example



Size



\- Regular

\- Special

\- Jumbo



Rice Type



\- Chicken Rice

\- White Rice



Protein



\- Boiled Chicken

\- Fried Chicken

\- Crispy Pork

\- Red Pork



\---



Drinking Water



Option Groups



None



\---



\## Future Expansion



Future versions may support



\- Conditional Option Groups

\- Dynamic Visibility

\- Channel-specific Rules

\- Time-based Availability

\- Customer Profile Rules



These capabilities should extend the Product Catalog without redesigning the Product domain.



\---



End of Document

