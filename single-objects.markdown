---
layout: normal
title: NBuilder - building single objects
---

# Building Single Objects

## Creating a single object

This will build an object with default values for all the properties that NBuilder is able to set.

<pre class="brush: csharp">
    
var product = Builder&lt;Product&gt;.CreateNew().Build();

</pre>


## Setting the value of a property

This will assign default values to everything apart from the description to which it will assign "A custom description here"


<pre class="brush: csharp">
```
var product = Builder<Product>
    .CreateNew()
        .With(x => x.Description = "A custom description here")
    .Build();
```
</pre>

## Setting more than one property

You can set any number of properties on the object. And() internally is in fact exactly the same as With(). It is provided as an option for improved readability.

```
var product = Builder<Product>
    .CreateNew()
        .With(x => x.Title = "Special title")
        .And(x => x.Description = "Special description")
        .And(x => x.Id = 2)
    .Build();
```

## Supplying constructor parameters

Given you have a type that has a constructor:

```
public BasketItem(ShoppingBasket basket, Product product, int quantity)
    : this (basket)
{
    Product = product;
    Quantity = quantity;
}
```

You can supply constructor args using WithConstructor() method:

```
var basketItem = Builder<BasketItem>
    .CreateNew()
        .WithConstructor(() => new BasketItem(basket, product, quantity))
    .Build();
```

## Calling a method on an object
You can use Do() to call a method on an object

```
var category = Builder<Category>
    .CreateNew()
        .Do(x => x.AddChild(child))
    .Build();
```
## Do(), And()

```
var category = Builder<Category>
    .CreateNew()
        .Do(x => x.AddChild(child))
        .And(x => x.AddChild(anotherChild))
    .Build();
```

## Using "multi functions"

If you want to call the same method for each item of a list, you can use `DoForAll()`

```
var categories = Builder<Category>.CreateListOfSize(5).Build();
 
var product = Builder<Product>
    .CreateNew()
        .DoForAll((prod, cat) => prod.AddToCategory(cat), categories)
    .Build();
```