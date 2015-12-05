# PointOfSaleTerminal

## Travis

[![Build Status](https://travis-ci.org/pugachAG/PointOfSaleTerminal.svg?branch=master)](https://travis-ci.org/pugachAG/PointOfSaleTerminal)

## Task 
### Part 1
Consider a grocery market where items have prices per unit but also volume prices. For example, doughnuts may be $1.25 each or 3 for $3 dollars. There could only be a single volume discount per product.

Implement a point-of-sale scanning API that accepts an arbitrary ordering of products (similar to what would happen when actually at a checkout line) then returns the correct total price for an entire shopping cart based on the per unit prices or the volume prices as applicable.

Here are the products listed by code and the prices to use (there is no sales tax):

| Product Code  | Price |
| :-----------: | :---- |
| A | $1.25 each or 3 for $3.00 |
| B | $4.25 |
| C | $1.00 or $5 for a six pack |
| D | $0.75 |


The interface at the top level PointOfSaleTerminal service object should look something like this. You are free to design/implement the rest of the code however you wish, including how you specify the prices in the system:

```
PointOfSaleTerminal terminal = new PointOfSaleTerminal();
terminal.SetPricing(...);
terminal.Scan("A");
terminal.Scan("C");
... etc.
double result = terminal.CalculateTotal();
```

Here are the minimal inputs you should use for your test cases. These test cases must be shown to work in your program:

* Scan these items in this order: ABCDABA; Verify the total price is $13.25.
* Scan these items in this order: CCCCCCC; Verify the total price is $6.00.
* Scan these items in this order: ABCD; Verify the total price is $7.25

### Part 2
Update Part 1 implementation to support discount cards. Discount card can be applied to the Terminal and doesn't effects volume price products. Discount card accumulates total amount of spent money and when it reaches some value, discount rate increases (see table below).

| Total amount  | Discount rate |
| :-----------: | :------------ |
| < 1000 | 1% |
| 1000-9999 | 5% |
| >= 10000 | 10% |

After the purchase card total amount should be increased by the base sum (before discount).
Solution should demonstrate handling series of purchases with discount rate increase.
