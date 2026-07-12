\# Modifiers



\---



\## Purpose



Modifiers represent business-defined changes applied to a Product after the customer selects its standard configuration.



Unlike Options, Modifiers represent adjustments to a Product rather than selections within predefined groups.



Modifiers provide structured customization without creating duplicate Products.



\---



\## Business Rules



\### Modifier Definition



A Modifier changes some aspect of a Product.



Examples



\- Add Egg

\- Extra Crispy Pork

\- Extra Chicken

\- Change to White Rice

\- No Skin



\---



\### Price Adjustment



A Modifier may



\- Increase Price

\- Decrease Price

\- Leave Price Unchanged



Examples



Add Egg



+10



Extra Crispy Pork



+20



Change to White Rice



+0



Price adjustment is optional.



\---



\### Multiple Modifiers



A Product may have zero or more Modifiers.



Examples



Chicken Rice



Modifiers



\- Add Egg

\- Extra Crispy Pork

\- No Skin



The business determines which Modifiers may be combined.



\---



\### Availability



Modifiers may be



\- Available

\- Unavailable



Unavailable Modifiers cannot be selected.



\---



\### Visibility



Modifiers define where they are visible.



Supported visibility



\- Cashier Only

\- Customer Self Ordering

\- Delivery Platform

\- Hidden



The business controls visibility.



\---



\## Design Principles



\### Modifier Changes the Product



Modifiers adjust an existing Product.



They do not create new Products.



\---



\### Modifier is Structured



Every Modifier has predefined business meaning.



The system understands every Modifier.



\---



\### Modifier is Different from Option



Options represent predefined customer selections within an Option Group.



Modifiers represent adjustments applied after the Product has been configured.



Both are structured business data.



\---



\### Modifier may affect Pricing



Unlike Kitchen Notes,



Modifiers may participate in



\- Pricing

\- Reporting

\- Analytics



\---



\### Modifier is Different from Kitchen Note



Kitchen Notes are free text.



Modifiers are structured business data understood by the system.



\---



\## Examples



Chicken Rice



Modifier



\- Add Egg



\---



Chicken Rice



Modifier



\- Change to White Rice



\---



Chicken Rice



Modifier



\- Extra Crispy Pork



\---



Chicken Rice



Modifiers



\- Change to White Rice

\- Extra Crispy Pork



Kitchen Note



Less Fat



\---



\## Future Expansion



Future versions may support



\- Modifier Groups

\- Conditional Modifiers

\- Channel-specific Modifiers

\- Promotion-aware Modifiers

\- Time-based Modifiers



These capabilities should extend the Product Catalog without redesigning the Product domain.



\---



End of Document

