\# Pricing



\---



\## Purpose



Pricing defines how Products, Options, and Modifiers determine the recommended selling price of a Product.



The Product Catalog stores suggested prices.



The actual selling price is determined when an Order Item is created.



\---



\## Business Rules



\### Suggested Prices



A Product may define one or more suggested selling prices.



Suggested prices assist cashiers during ordering.



Examples



Chicken Rice



\- 40

\- 50

\- 60



The number of suggested prices is not limited.



Suggested prices do not need to be consecutive.



Examples



\- 40

\- 50

\- 80

\- 120



\---



\### Actual Selling Price



The selling price recorded in an Order Item represents the actual selling price.



The actual selling price may differ from any suggested price.



Examples



Suggested



40



Actual



80



Business Reason



Special customer request.



\---



\### Price Override



Authorized staff may override the suggested selling price.



The override applies only to the current Order Item.



The Product definition remains unchanged.



When possible, the business should record the reason for the override for future auditing.



\---



\### Option Price Adjustment



Options may define a price adjustment.



Examples



Regular



+0



Special



+10



Jumbo



+20



\---



\### Modifier Price Adjustment



Modifiers may



\- Increase price

\- Decrease price

\- Leave price unchanged



Examples



Add Egg



+10



Change to White Rice



+0



\---



\### Combined Price Adjustment



The final selling price may include adjustments from both



\- Options

\- Modifiers



The Product Catalog defines the available adjustments.



The Order records the final calculated selling price.



\---



\### Historical Orders



Changing suggested prices must never modify historical Orders.



Orders always preserve the actual selling price recorded during the transaction.



\---



\## Design Principles



\### Suggested Price is Recommendation



Suggested prices improve cashier speed.



They are recommendations, not mandatory selling prices.



\---



\### Actual Selling Price is Source of Truth



Reports, receipts, and accounting always use the actual selling price recorded in the Order.



\---



\### Pricing belongs to the Order



Products suggest prices.



Orders record completed transactions.



\---



\### Business Flexibility



Restaurants may adjust selling prices at any time without modifying Product definitions.



\---



\### Pricing Hierarchy



Pricing follows this conceptual order.



Suggested Product Price



↓



Option Adjustments



↓



Modifier Adjustments



↓



Manual Price Override (if any)



↓



Actual Selling Price



\---



\## Examples



Suggested Prices



40



50



60



Customer Request



Large Portion



Actual Selling Price



80



\---



\## Future Expansion



Future versions may support



\- Promotions

\- Time-based Pricing

\- Happy Hour

\- Channel-specific Pricing

\- Customer-specific Pricing

\- Membership Pricing

\- Dynamic Pricing



These capabilities should extend the Product Catalog without redesigning the Product domain.



\---



End of Document

