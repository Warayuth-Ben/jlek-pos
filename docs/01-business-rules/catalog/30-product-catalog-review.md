\# Product Catalog Review



Status: In Progress



Date: 2026-07-12



\---



\# Summary



Product Catalog Business Freeze v1 has been drafted.



All major business concepts have been documented.



The documents have completed the first review pass.



Several files have already been improved during review.



The catalog is now very close to Freeze.



\---



\# Completed



\## Product Catalog



\- overview.md

\- categories.md

\- products.md

\- ingredients.md

\- option-groups.md

\- options.md

\- modifiers.md

\- pricing.md

\- availability.md

\- kitchen-notes.md

\- future.md



\---



\# Major Business Decisions



\## Product



Product is a sellable template.



Order Item records the actual transaction.



\---



\## Suggested Price



Products define Suggested Prices.



Orders record Actual Selling Prices.



Suggested Price ≠ Actual Selling Price.



\---



\## Product Configuration



Configuration is preferred over duplicate Products.



Examples



\- White Rice

\- Special

\- Jumbo



\---



\## Option



Structured customer selection.



Belongs to an Option Group.



\---



\## Modifier



Structured adjustment to a Product.



May affect pricing.



\---



\## Kitchen Note



Free text.



Used only when the request cannot reasonably become structured.



\---



\## Ingredient



Operational business resource.



Inventory management is intentionally out of scope.



\---



\## Availability



Availability represents operational status.



Business always decides whether



\- substitute ingredients may be used

\- Products remain sellable

\- Products return to Available



The system must never make these decisions automatically.



\---



\# Review Notes



The following improvements have been identified.



\## overview.md



\- Add Business Glossary

\- Add Product Catalog relationship diagram



\---



\## products.md



\- Product is Template

\- Product Configuration

\- Unified Visibility



\---



\## categories.md



\- Category Identity

\- Category Status



\---



\## ingredients.md



\- Clarify Ingredient vs Recipe

\- Add Recipe Management to Future



\---



\## option-groups.md



\- Option Group Identity

\- Unified Visibility



\---



\## options.md



\- Option Identity

\- Clarify Option vs Modifier

\- Unified Visibility



\---



\## modifiers.md



\- Multiple Modifiers

\- Unified Visibility

\- Clarify Option vs Modifier



\---



\## pricing.md



\- Pricing Hierarchy

\- Combined Price Adjustment

\- Price Override Reason



\---



\## availability.md



\- Ingredient Substitution

\- Business Decision

\- Operational Status



\---



\## kitchen-notes.md



\- Frequently Used Requests Become Structured

\- Kitchen Notes never affect pricing



\---



\## future.md



\- Recipe Management

\- Catalog Versioning

\- Channel-specific Visibility

\- Backward Compatibility



\---



\# Architecture Decisions



Current Product Catalog structure



Product



├── Category



├── Suggested Prices



├── Ingredients



├── Option Groups



│      └── Options



├── Modifiers



└── Availability



↓



Order



↓



Order Item



├── Actual Selling Price



├── Selected Options



├── Selected Modifiers



└── Kitchen Note



\---



\# Next Session



Priority 1



Review remaining Product Catalog files.



Apply all approved improvements.



\---



Priority 2



Freeze Product Catalog v1.



Update CHANGELOG.



Commit Product Catalog Freeze.



Merge into feature/menu-module.



\---



Priority 3



Begin Menu Module implementation.



Expected first milestone



\- Product Aggregate

\- Category Aggregate

\- Option Group

\- Option

\- Modifier



\---



End of Document

