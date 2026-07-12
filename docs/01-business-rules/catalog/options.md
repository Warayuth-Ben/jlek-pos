\# Options



\---



\## Purpose



Options represent structured customer selections within an Option Group.



Options are understood by the system and may affect product configuration, pricing, reporting, and business rules.



Options should represent common customer choices that occur frequently during ordering.



\---



\## Business Rules



\### Option Definition



An Option represents one selectable value within an Option Group.



Examples



Option Group



Size



Options



\- Regular

\- Special

\- Jumbo



\---



Option Group



Rice Type



Options



\- Chicken Rice

\- White Rice



\---



Option Group



Protein



Options



\- Boiled Chicken

\- Fried Chicken

\- Crispy Pork

\- Red Pork



\---



\### Option Identity



Each Option has a unique identity.



Changing the display name of an Option must not affect historical Orders.



\---



\### Business Meaning



Options represent structured business information.



The system understands every Option and may use it for



\- Pricing

\- Kitchen Printing

\- Reporting

\- Analytics

\- Future Promotions



\---



\### Display Order



The business controls the display order of Options.



Display order affects only the user interface.



\---



\### Availability



Each Option may be



\- Available

\- Unavailable



Unavailable Options cannot be selected.



Historical Orders remain unchanged.



\---



\### Visibility



Each Option defines where it is visible.



Supported visibility



\- Cashier Only

\- Customer Self Ordering

\- Delivery Platform

\- Hidden



Examples



White Rice



Cashier Only



Special



Customer Self Ordering



Internal Option



Hidden



The business controls visibility for each sales channel.



\---



\### Price Adjustment



An Option may define a price adjustment.



Examples



Regular



+0



Special



+10



Jumbo



+20



White Rice



+0



Price adjustment is optional.



\---



\## Design Principles



\### Options are Structured



Every Option has a predefined business meaning.



The system should never guess the meaning of an Option.



\---



\### Options Represent Choices



Options represent predefined customer selections within an Option Group.



They do not modify the Product after configuration.



Product adjustments belong to Modifiers.



\---



\### Frequent Requests Become Options



Frequently requested customer choices should become structured Options.



Examples



\- White Rice

\- Special

\- Jumbo



Less frequent or exceptional requests should remain Kitchen Notes.



\---



\### Business Controls Options



Businesses may



\- Create

\- Rename

\- Reorder

\- Hide

\- Disable



Options without changing application logic.



\---



\## Examples



Chicken Rice



Option Groups



Size



Selected Option



Special



Rice Type



Selected Option



White Rice



\---



Mixed Chicken Rice



Option Group



Protein



Selected Options



\- Boiled Chicken

\- Fried Chicken



\---



\## Future Expansion



Future versions may support



\- Option Images

\- Multi-language Labels

\- Channel-specific Visibility

\- Time-based Availability

\- Customer Preferences



These capabilities should extend the Product Catalog without redesigning the Product domain.



\---



End of Document

