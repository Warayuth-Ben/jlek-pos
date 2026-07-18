\# Future Expansion



\---



\## Purpose



The Product Catalog is designed to support future business growth without requiring fundamental changes to the domain model.



This document records planned capabilities that are intentionally outside the current implementation scope.



These items are future design considerations only.



\---



\## Product Catalog Evolution



The Product Catalog should continue to evolve through configuration rather than redesign.



Future enhancements should extend the existing model whenever possible.



Backward compatibility should always be preserved.



\---



\## Planned Capabilities



\### Promotions



Support promotional pricing such as



\- Percentage Discounts

\- Fixed Amount Discounts

\- Buy One Get One

\- Bundle Promotions

\- Happy Hour

\- Campaign Pricing



Promotions should remain independent from Product definitions.



\---



\### Bundle Products



Support product bundles such as



\- Meal Sets

\- Family Sets

\- Seasonal Sets



Bundles should reference existing Products instead of duplicating them.



\---



\### Customer Self Ordering



Support customer ordering through



\- Mobile Devices

\- QR Code Ordering

\- Kiosk

\- Tablet Ordering



Customer interfaces may expose fewer configuration options than cashier interfaces.



\---



\### Delivery Platforms



Support external ordering channels



Examples



\- Grab

\- LINE MAN

\- Food Panda



Channel-specific configuration should not require changes to Product definitions.



\---



\### Recipe Management



Future versions may define the relationship between Products and their Ingredients.



Recipe definitions may support



\- Default Ingredients

\- Optional Ingredients

\- Preparation Rules

\- Portion Definitions



Recipe Management should extend the Product Catalog without affecting historical Orders.



\---



\### Cost Tracking



Products may optionally maintain cost information.



Cost data should support



\- Profit Analysis

\- Margin Reports

\- Future Inventory



Cost should remain independent from selling prices.



\---



\### Inventory Management



Future versions may support



\- Quantity Tracking

\- Low Stock Alerts

\- Automatic Availability

\- Supplier Management

\- Purchase Orders



Inventory should extend Ingredient management without redesigning the Product Catalog.



\---



\### Multiple Price Lists



Support different pricing models



Examples



\- Dine-in

\- Takeaway

\- Delivery

\- Member Pricing

\- Seasonal Pricing



Actual Selling Price remains the source of truth for completed Orders.



\---



\### Channel-specific Visibility



Future versions may allow Products, Options, and Modifiers to have different visibility for each sales channel.



Examples



\- POS Cashier

\- Customer Self Ordering

\- Grab

\- LINE MAN



Visibility rules should be configurable without duplicating Products.



\---



\### Reporting



Future reporting may analyze



\- Product Popularity

\- Modifier Usage

\- Option Usage

\- Ingredient Availability

\- Pricing Trends

\- Customer Preferences



Structured business data should always be preferred over free-text whenever possible.



\---



\### Catalog Versioning



Future versions may support Product Catalog versioning to improve auditing and long-term historical tracking.



Versioning must never modify completed Orders.



\---



\## Design Principles



Future capabilities should



\- preserve existing Product identities

\- preserve historical Orders

\- preserve business flexibility

\- preserve backward compatibility

\- extend existing models instead of replacing them



Configuration should always be preferred over duplication.



\---



\## Out of Scope



The following capabilities are intentionally excluded from the current milestone



\- Promotion Engine

\- Inventory Quantity

\- Supplier Management

\- Accounting Integration

\- Loyalty Programs

\- AI Recommendations



These capabilities may be introduced in future milestones.



\---



End of Document

