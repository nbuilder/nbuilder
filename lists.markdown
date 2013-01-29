---
layout: normal
title: NBuilder: Building Lists
---

# Lists

One of NBuilder's most useful features is its ability to create lists.

NBuilder has two different property namers, a sequential namer and a random namer. The default namer is the sequential one. This is probably the most useful for most scenarios because you know exactly what it is going to produce.

It always starts from one and gives ascending values to the properties for each object it builds.

## Creating a basic list

```
var products = Builder<Product>.CreateListOfSize(10).Build();
```

NBuilder will name all public properties and fields but it won't touch private, protected or internal ones.

It will create a list whose properties are named like this:

## Using All()

You can specify that different parts of the list have certain property values. These are called 'declarations'. The most basic declaration is All(). When you declare a set of objects, you always follow it with what you want to do to that set of objects. In this case With() is being used.

```
var products = Builder<Product>
    .CreateListOfSize(10)
    .All()
        .With(x => x.Title = "A special title")
    .Build();
```

One common use for All() is if you want to insert a load of objects into the database. Most ORMs will treat an object with an ID of 0 as a new object so to do this you would say
`All().With(x => x.Id = 0)`

## TheFirst(), TheLast()

There are some methods provided to quickly declare the first or last x objects.

```
var products = Builder<Product>
    .CreateListOfSize(10)
    .TheFirst(2)
        .With(x => x.Title = "A special title")
    .Build();
```

All these methods (apart from All()) are in fact extension methods. You can add your own methods to NBuilder, simply by writing an extension method.

## TheNext(), ThePrevious()

When you use TheFirst() you can follow it with multiple calls to TheNext()

```
var list = Builder<Product>
    .CreateListOfSize(30)
    .TheFirst(10)
        .With(x => x.Title = "Special Title 1")
    .TheNext(10)
        .With(x => x.Title = "Special Title 2")
    .TheNext(10)
        .With(x => x.Title = "Special Title 3")
    .Build();    
```

As you might expect there's also a TheLast() and ThePrevious() as well

```
var list = Builder<Product>
    .CreateListOfSize(30)
    .TheLast(10)
        .With(x => x.Title = "Special Title 1")
    .ThePrevious(10)
        .With(x => x.Title = "Special Title 2")
    .Build();
```
	
## Section(x, y)

You can use Section() to apply rules to segments of the list

```
var list = Builder<Product>
    .CreateListOfSize(30)
    .All()
        .With(x => x.Title = "Special Title 1")
    .Section(12, 14)
        .With(x => x.Title = "Special Title 2")
    .Section(16, 18)
        .With(x => x.Title = "Special Title 3")
    .Build();
```

## Calling a method on the objects of a declaration

If you want to call a method on a set of objects, just use the With() method

```
var children = Builder<Category>.CreateListOfSize(3).Build();
 
var list = Builder<Category>
    .CreateListOfSize(10)
    .TheFirst(2)
        .Do(x => x.AddChild(children[0]))
        .And(x => x.AddChild(children[1]))
    .TheNext(2)
        .Do(x => x.AddChild(children[2]))
    .Build();
```
	
## Picking random items

If you want to pick an item or some items at random, and you don't care which they are, you can use the Pick class.

```
var children = Builder<Category>.CreateListOfSize(10).Build();
 
var categories = Builder<Category>
    .CreateListOfSize(10)
    .TheFirst(2)
        .Do(x => x.AddChild(Pick<Category>.RandomItemFrom(children)))
    .Build();
```

You can also pick a unique random list using the Pick class

```
Pick<Product>.UniqueRandomList(With.Between(5).And(10).Elements).From(products);
```