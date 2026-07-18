\# Products



\---



\## Purpose



A Product represents a sellable item offered by the restaurant.



Products are the foundation of the Product Catalog and define what customers can order.



A Product is independent from customer Orders, kitchen workflow, and payment processing.



\---



\## Business Rules



\### Product Definition



A Product represents one logical menu offering.



Examples



\- Chicken Rice

\- Crispy Pork Rice

\- Mixed Chicken Rice

\- Pork Blood Soup

\- Drinking Water



\---



\### Product Identity



Each Product has a unique identity.



The identity must never change, even if



\- Product Name changes

\- Suggested Prices change

\- Category changes



Historical Orders must always reference the same Product.



\---



\### Product Name



The business controls Product names.



Names may be changed without affecting historical Orders.



\---



\### Product Category



Each Product belongs to one Product Category.



Categories organize Products but do not change business behavior.



\---



\### Default Configuration



Every Product defines a default configuration.



Example



Chicken Rice



Default



\- Chicken Rice

\- Boiled Chicken



Customers may modify the default configuration through supported Option Groups, Options, and Modifiers.



\---



\### Product Status



A Product may be



\- Available

\- Unavailable



Unavailable Products cannot be sold.



Historical Orders remain unchanged.



\---



\### Display Order



Products may be displayed in any order chosen by the business.



Display order affects only the user interface.



\---



\### Product Visibility



Products define where they are visible.



Supported visibility



\- Cashier Only

\- Customer Self Ordering

\- Delivery Platform

\- Hidden



Visibility does not change the Product itself.



The business controls visibility for each sales channel.



\---



\### Product Configuration



Products define



\- available Option Groups

\- available Options

\- available Modifiers



Orders record the customer's actual selections.



Changing a Product configuration must never modify historical Orders.



\---



\### Product Customization



Products may support customer customization through



\- Option Groups

\- Options

\- Modifiers

\- Kitchen Notes



The Product defines which customizations are permitted.



\---



\## Design Principles



\### Product is Sellable



A Product represents something the customer can purchase.



Products are never used to represent Ingredients.



\---



\### Product is Stable



Products should remain stable over time.



Business changes should be handled through configuration whenever possible.



\---



\### Product is a Template



A Product defines what may be sold.



An Order Item records what was actually sold.



The Product Catalog defines possibilities.



Orders preserve actual customer transactions.



\---



\### Configuration over Duplication



Customer variations should be represented through configuration instead of creating duplicate Products.



Example



One Product



Chicken Rice



Instead of creating



\- Chicken Rice

\- Chicken Rice with White Rice

\- Chicken Rice Special

\- Chicken Rice Jumbo



\---



\### Product Independence



Products should not contain



\- Payment Rules

\- Kitchen Workflow

\- Order Status



Those responsibilities belong to other business domains.



\---



\## Examples



Chicken Rice



Default



\- Chicken Rice

\- Boiled Chicken



Customer Request



\- White Rice

\- Less Fat



Result



Same Product



Different Configuration



\---



Mixed Chicken Rice



Default



\- Chicken Rice

\- Boiled Chicken

\- Fried Chicken



Customer Request



\- Change to White Rice

\- Extra Crispy Pork



Result



Same Product



Different Configuration



\---



\## Future Expansion



Products are designed to support future capabilities including



\- Promotional Pricing

\- Bundle Products

\- Seasonal Products

\- Product Images

\- Product Tags

\- Barcode Support

\- Multi-language Names

\- Channel-specific Visibility



These capabilities should extend the Product Catalog without redesigning the Product domain.



\---



End of Document

