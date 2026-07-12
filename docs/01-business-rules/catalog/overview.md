\# Product Catalog Overview



\---



\## Purpose



The Product Catalog is the authoritative source describing every product that can be sold by the restaurant.



It defines the business rules governing products, categories, ingredients, pricing, configuration, availability, and customer customization.



The Product Catalog does not describe customer orders, kitchen workflow, or payment processing.



Its responsibility is to define \*\*what can be sold\*\*.



\---



\## Scope



This section defines business rules for:



\- Product

\- Product Category

\- Ingredient

\- Option Group

\- Option

\- Modifier

\- Suggested Pricing

\- Availability

\- Kitchen Note



These rules apply regardless of service channel, including:



\- Dine-in

\- Takeaway

\- Delivery

\- Future Self Ordering



\---



\## Business Motivation



Restaurant menus constantly evolve.



Prices change.



New products are introduced.



Ingredients become temporarily unavailable.



Customers frequently request modifications.



The Product Catalog exists to support these business changes without requiring redesign of the ordering system.



\---



\## Core Concepts



\### Product



A sellable product offered by the restaurant.



Examples



\- Chicken Rice

\- Crispy Pork Rice

\- Boiled Pork Blood Soup

\- Drinking Water



\---



\### Product Category



A logical grouping of products.



Categories are created and maintained by the business.



Examples



\- Rice

\- Drinks

\- Soup



\---



\### Ingredient



A physical ingredient used to prepare one or more products.



Ingredients are not products.



Examples



\- Boiled Chicken

\- Crispy Pork

\- Rice

\- Cucumber



\---



\### Option Group



A logical collection of customer choices.



Examples



\- Size

\- Rice Type

\- Protein



\---



\### Option



A structured customer selection understood by the system.



Examples



\- Regular

\- Special

\- Jumbo

\- White Rice

\- Chicken Rice



\---



\### Modifier



A business-defined adjustment applied to a product.



A modifier may



\- change configuration

\- increase price

\- decrease price

\- have no price effect



Examples



\- Add Egg

\- Change to White Rice

\- Extra Crispy Pork



\---



\### Suggested Price



Recommended selling prices configured by the business.



Suggested prices are references only.



\---



\### Actual Selling Price



The actual selling price recorded in an order.



The business may override the suggested price when authorized.



\---



\### Availability



Availability determines whether a product, option, modifier, or ingredient may currently be sold.



Availability is controlled by the business.



\---



\### Kitchen Note



A free-text instruction sent directly to the kitchen.



Kitchen Notes are not interpreted by the system.



Examples



\- Less Fat

\- No Skin

\- Separate Sauce

\- Large Pieces



\---

\## Business Glossary



| Concept | Description |

|----------|-------------|

| Product | A sellable item offered by the restaurant. |

| Product Category | A logical grouping of Products. |

| Ingredient | A physical component used to prepare Products. |

| Option Group | A group defining how customers make structured selections. |

| Option | A predefined structured selection. |

| Modifier | A structured adjustment applied to a Product. |

| Kitchen Note | Free-text instructions sent directly to the kitchen. |

| Suggested Price | Recommended selling prices configured by the business. |

| Actual Selling Price | The selling price recorded in a completed Order. |



\---

\## Design Principles



The Product Catalog follows these principles.



\### Product is not Order Item



A Product describes what may be sold.



An Order Item records what was actually sold.



\---



\### Product is not Ingredient



Products are sold.



Ingredients are used to prepare products.



\---



\### Configuration over Duplication



Products should support business variation through configuration whenever possible.



Avoid creating duplicate products solely to represent customer modifications.



\---



\### Structured Data before Free Text



Business information that affects pricing, reporting, or business rules should be represented as structured data.



Kitchen Notes should only be used for information that cannot reasonably be structured.



\---



\### Suggested Price is not Actual Selling Price



Suggested prices assist cashiers.



Actual Selling Price represents the completed transaction.



\---



\### Customer View is not Cashier View



Customers and cashiers may see different configuration options.



The business controls option visibility.



\---



\### Business before Technology



Business rules define the Product Catalog.



Technology implements those rules.



\---



\## Relationship to Other Domains



The Product Catalog supplies information to other business domains.



Product Catalog



↓



Order



↓



Kitchen



↓



Payment



↓



Reporting



The Product Catalog does not contain business rules belonging to those domains.



\---



\## Future Expansion



The Product Catalog is designed to support future capabilities without redesign.



Future capabilities may include:



\- Promotions

\- Bundle Products

\- Cost Tracking

\- Inventory Management

\- Multiple Price Lists

\- Customer Self Ordering

\- Delivery Platform Integration



\---



End of Document

