\# Naming Conventions



\## Purpose



Defines naming rules used throughout the project.



\---



\## Aggregates



Singular PascalCase



Examples:



\- Order

\- Bill

\- Payment



\---



\## Entities



Singular PascalCase



Examples:



\- OrderItem

\- PaymentRecord



\---



\## Value Objects



Singular PascalCase



Examples:



\- Money

\- Quantity

\- CustomerName



\---



\## Domain Events



Past tense.



Examples:



\- OrderCreated

\- OrderConfirmed

\- PaymentCompleted



\---



\## Application Services



Verb + Noun



Examples:



\- CreateOrder

\- ConfirmOrder

\- ProcessPayment



\---



\## API



REST resources use plural nouns.



Examples:



```

/orders

/payments

/tables

```



\---



\## Files



Use lowercase kebab-case.



Example:



```

create-order.md

```

