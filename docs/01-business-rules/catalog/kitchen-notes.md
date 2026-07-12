\# Kitchen Notes



\---



\## Purpose



Kitchen Notes allow cashiers to communicate special preparation instructions directly to the kitchen.



Kitchen Notes are intended for requests that cannot reasonably be represented by structured business data.



They provide operational flexibility without increasing Product Catalog complexity.



\---



\## Business Rules



\### Free Text



Kitchen Notes are free-text instructions entered by the cashier.



The system stores and prints the note exactly as entered.



The system does not interpret the meaning of Kitchen Notes.



\---



\### Typical Usage



Kitchen Notes should be used only for exceptional or uncommon customer requests.



Examples



\- Less Fat

\- No Skin

\- Large Pieces

\- Separate Sauce

\- Extra Cucumber

\- Not Spicy



\---



\### Structured Data First



If a customer request becomes common and affects pricing, reporting, analytics, or business rules, it should be converted into a structured Option or Modifier.



Kitchen Notes should never replace structured business data.



\---



\### Item Level



Kitchen Notes belong to a specific Order Item.



Different items within the same Order may have different Kitchen Notes.



\---



\### No Business Logic



Kitchen Notes do not



\- change Product definitions

\- change pricing

\- affect reporting

\- participate in business rules



They exist solely to communicate preparation instructions to the kitchen.



\---



\### Historical Integrity



Kitchen Notes recorded in completed Orders must never be modified.



Historical Orders preserve the original customer request.



\---



\## Design Principles



\### Kitchen Notes are Operational



Kitchen Notes exist to assist food preparation.



They are not business rules.



\---



\### Kitchen Notes are Not Structured



The system stores Kitchen Notes without attempting to understand or validate their meaning.



\---



\### Structured Before Free Text



Whenever practical, use



\- Option

\- Modifier



instead of Kitchen Notes.



Kitchen Notes should remain the exception.



\---



\### Frequently Used Requests Become Structured



If the same Kitchen Note is entered frequently, the business should consider converting it into



\- an Option

\- a Modifier



This improves consistency, reporting, and future automation.



\---



\## Examples



Chicken Rice



Kitchen Note



Less Fat



\---



Chicken Rice



Kitchen Note



Separate Sauce



\---



Chicken Rice



Kitchen Note



Large Pieces



\---



Chicken Rice



Kitchen Note



Customer requests White Rice, but the business has not yet created a structured Option.



Temporary Kitchen Note



White Rice



Future Improvement



Convert the request into a structured Option.



\---



\## Future Expansion



Future versions may support



\- Frequently Used Notes

\- Note Templates

\- Voice Input

\- Kitchen Translation

\- AI-assisted Note Suggestions



These capabilities should improve cashier efficiency without changing the role of Kitchen Notes.



\---



End of Document

