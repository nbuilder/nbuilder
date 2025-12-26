# Nbuilder - A rapid test object generator.

Through a fluent, extensible interface, NBuilder allows you to rapidly create test data, automatically assigning values to properties and public fields that are one of the built in .NET data types (e.g. ints and strings). NBuilder allows you to override for properties you are interested in using lambda expressions.

| Metric      | Status      |
| ----- | ----- |
| Production Build  | [![Build status](https://ci.appveyor.com/api/projects/status/av4lwhtejk94wgiy/branch/master?svg=true)](https://ci.appveyor.com/project/NBuilder/nbuilder-1m5q4) |
| Development Build | [![Build status](https://ci.appveyor.com/api/projects/status/av4lwhtejk94wgiy/branch/develop?svg=true)](https://ci.appveyor.com/project/NBuilder/nbuilder-1m5q4/branch/develop) |
| Nuget             | [![NuGet Badge](https://buildstats.info/nuget/nbuilder)](https://www.nuget.org/packages/nbuilder/) |
   
[![Join the chat at https://gitter.im/garethdown44/nbuilder](https://badges.gitter.im/garethdown44/nbuilder.svg)](https://gitter.im/garethdown44/nbuilder?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)


### How can NBuilder help?

This test data has a variety of uses. For example:

- For automated functional and acceptance tests.
- Returning the data from a stubbed service.
- Creating test data for use when developing or testing an application.
- Performance tuning with large amounts of data.

### Major features

#### Persistence

Easily persist generated objects using `Persist()`

NBuilder also allows you to easily set up persistence. You do this by telling NBuilder how to persist your objects. The most convenient place to do this would be in an NUnit SetUpFixture class.

```c#
var repository = new ProductRepository();
BuilderSetup.SetPersistenceCreateMethod<IList<Product>>(repository.CreateAll);
```

Once you have done this, it's simply a case of calling `Persist()` instead of `Build()`:

```c#
Builder<Product>.CreateListOfSize(100).Persist();
```

#### Hierarchy generation

Easily create hierarchies of objects by telling NBuilder how to add children to your object. You can even persist the hierarchies just by giving NBuilder create and update methods.

You can easily create a random hierarchy by first creating an initial list and then calling `BuildHierarchy()`, and passing in a specification.

```c#
var hierarchySpec = Builder<HierarchySpec<Category>>.CreateNew()
                .With(x => x.AddMethod = (parent, child) => parent.AddChild(child))
                .With(x => x.Depth = 5)
                .With(x => x.MaximumChildren = 10)
                .With(x => x.MinimumChildren = 5)
                .With(x => x.NamingMethod = (cat, title) => cat.Title = "Category " + title)
                .With(x => x.NumberOfRoots = 10).Build();

            Builder<Category>.CreateListOfSize(2500).BuildHierarchy(hierarchySpec);
```

This will create a category tree and by supplying a naming method, will even name your categories with their path in the tree. For example:

```c#
 Category - Title = "1"
      Category - Title = "1.1"
      Category - Title = "1.2"
  Category - Title = "2"
      Category - Title = "2.1"
          Category - Title = "2.1.1"
      Category - Title = "2.2"
      Category - Title = "2.3"
```

#### Configurability

NBuilder is highly configurable. Through the BuilderSetup class you can control how NBuilder names objects and disable naming for certain properties of certain types.

##### Nullable Value Types

NBuilder provides flexible control over how nullable value types are handled during test data generation. Understanding and controlling this behavior is crucial for creating realistic test scenarios.

###### Default Behavior (Nullable Properties Get Values)

By default, NBuilder will populate nullable value types with actual values (not null), using the same generators as their non-nullable counterparts.

```c#
public class Product
{
    public int Id { get; set; }
    public int? Rating { get; set; }        // Will be set to 1, 2, 3, etc.
    public Guid? SessionId { get; set; }    // Will be set to a generated Guid
    public decimal? Discount { get; set; }  // Will be set to 1, 2, 3, etc.
}

// Default behavior - all properties get values
var product = Builder<Product>.CreateNew().Build();
// Result: Rating = 1, SessionId = [some-guid], Discount = 1
```

**Why this is useful:**
- Most of the time, you want populated objects for testing
- Avoids null reference exceptions in your tests
- Creates more realistic test data

###### Setting All Nullable Types to Null

Sometimes you need to test null-handling scenarios. You can configure NBuilder to set all nullable properties to null:

```c#
public class UserProfile
{
    public string Username { get; set; }     // Always gets a value
    public int? Age { get; set; }            // Will be null
    public DateTime? LastLogin { get; set; } // Will be null
    public decimal? Balance { get; set; }    // Will be null
}

// Set all nullable value types to null
var settings = new BuilderSettings();
settings.UseNullAsDefaultValueForAllNullableTypes();
var builder = new Builder(settings);

var profile = builder.CreateNew<UserProfile>().Build();
// Result: Age = null, LastLogin = null, Balance = null
```

**When to use this:**
- Testing null-handling logic in your application
- Verifying default values are applied correctly
- Testing optional fields in forms or APIs
- Simulating incomplete data scenarios

###### Setting Specific Nullable Types to Null

For more granular control, you can specify which nullable types should be null while others get values:

```c#
public class Order
{
    public int OrderId { get; set; }
    public Guid? TrackingId { get; set; }      // Will be null
    public int? DiscountPercent { get; set; }  // Will be null
    public decimal? ShippingCost { get; set; } // Will get a value
    public DateTime? DeliveredAt { get; set; } // Will get a value
}

// Only specific nullable types are set to null
var settings = new BuilderSettings();
settings.UseNullAsDefaultValueForNullableType(typeof(Guid?), typeof(int?));
var builder = new Builder(settings);

var order = builder.CreateNew<Order>().Build();
// Result: TrackingId = null, DiscountPercent = null, ShippingCost = 1, DeliveredAt = [some-date]
```

**When to use this:**
- Testing mixed scenarios (some nulls, some values)
- Simulating optional vs. required fields
- Testing business rules that depend on specific nullable fields

###### Practical Example: Testing Null-Safe Code

```c#
public class PricingService
{
    public decimal CalculateTotal(Order order)
    {
        var subtotal = order.Subtotal;
        var shipping = order.ShippingCost ?? 0m;  // Null-coalescing
        var discount = order.DiscountPercent.HasValue 
            ? subtotal * (order.DiscountPercent.Value / 100m) 
            : 0m;
        
        return subtotal + shipping - discount;
    }
}

// Test with nulls to ensure null-safe logic works
var settings = new BuilderSettings();
settings.UseNullAsDefaultValueForNullableType(typeof(decimal?), typeof(int?));
var builder = new Builder(settings);

var orders = builder.CreateListOfSize<Order>(100).Build();
// All orders have ShippingCost = null and DiscountPercent = null

var service = new PricingService();
foreach (var order in orders)
{
    var total = service.CalculateTotal(order); // Should not throw NullReferenceException
    Assert.IsTrue(total >= 0);
}
```

###### Combining with Property-Specific Overrides

You can still override individual properties even when using global nullable settings:

```c#
var settings = new BuilderSettings();
settings.UseNullAsDefaultValueForAllNullableTypes();
var builder = new Builder(settings);

// Most nullable properties will be null, but we override specific ones
var premiumUser = builder.CreateNew<UserProfile>
    .With(x => x.Age = 25)              // Override to set a value
    .With(x => x.Balance = 1000.00m)    // Override to set a value
    .Build();
// Result: Age = 25, Balance = 1000.00, LastLogin = null (still null)
```

###### Disabling Null Behavior for Specific Properties

If you want most nullable properties to be null but specific ones to get values, you can disable auto-naming for those properties:

```c#
var settings = new BuilderSettings();
settings.UseNullAsDefaultValueForAllNullableTypes();

// Disable the null behavior for a specific property
settings.DisablePropertyNamingFor<Order, decimal?>(x => x.ShippingCost);

var builder = new Builder(settings);
var order = builder.CreateNew<Order>().Build();
// Result: TrackingId = null, DiscountPercent = null, ShippingCost = default(decimal) [0]
```

###### Summary: When to Use Each Approach

| Scenario | Approach |
|----------|----------|
| General testing with realistic data | Default behavior (nullable properties get values) |
| Testing null-handling logic | `UseNullAsDefaultValueForAllNullableTypes()` |
| Testing optional fields in business logic | `UseNullAsDefaultValueForNullableType(...)` for specific types |
| Testing incomplete/partial data | Mix of global and property-specific overrides |
| Performance testing with sparse data | `UseNullAsDefaultValueForAllNullableTypes()` |

##### Custom persistence service

Easily add your own custom persistence service, allowing you to use any `ORM`.

```c#
BuilderSetup.SetPersistenceService(new MyCustomPersistenceService());
Builder<Product>.CreateNew().Persist();
```

##### Turning off automatic property naming

If you don't want properties to be automatically given values, you can simply turn it off.

```c#
BuilderSetup.AutoNameProperties = false;
```

##### Changing the default property namer

You can change the default property namer to use the random value property namer, or you can create your own either from scratch implementing the IPropertyNamer interface, or by extending one of the classes, for example to add support

```c#
BuilderSetup.SetDefaultPropertyNamer(new RandomValuePropertyNamer());
```

##### Adding a property namer for a specific type

If, for example, you have a class that has a custom struct, NBuilder will ignore this property because it doesn't know how to set it. You could overcome this by adding a special property namer, just for Products.

```c#
BuilderSetup.SetPropertyNamerFor<Product>(new CustomProductPropertyNamer(new ReflectionUtil()));
```

##### Disabling automatic property naming for a specific property of a specific type

If you don't want values to automatically be assigned to certain properties, you can disable it like this:

```c#
BuilderSetup.DisablePropertyNamingFor<Product, int>(x => x.Id);
```

#### Extensibility

Through extension methods you can extend NBuilder's fluent interface to add custom building functionality. You can also create custom property namers globally or for specific types.

##### Custom declarations

In NBuilder nearly all of the public interface is implemented with extension methods. This of course means it's possible to add your own.

For example, out of the box the list builder has seven 'declarations' `All()`, `WhereRandom(n)`, `WhereRandom(n, start, end)`, `WhereTheFirst(n)`, `WhereTheLast(n)`, `AndTheNext(n)`, `AndThePrevious(n)`. However if you wanted to add your own,

e.g. to return all the even or odd items, all you need to do is write a new extension method -` WhereAllEven()`

##### "Operable" extensions

If, for example, you find yourself repeating yourself when creating test data and you want to wrap something up in a method, you can do this by extending IOperable<T>. You can do this generically or per-type.

For example say if rather than saying:

```c#
Builder<Product>
	.CreateListOfSize(10)
	.All()
	.Have(x => x.Title = "12345....[LongString].....12345")
	.Build();
```

You could instead create an extension method:

```c#
public static IOperable<Product> HaveLongTitles(this IOperable<Product> operable)
{
    ((IDeclaration<Product>) operable).ObjectBuilder.With(x => x.Title = "12345....[LongString].....12345");
    return operable;
}
```

Giving you the ability to say:

```c#
Builder<Product>
    .CreateListOfSize(10)
    .All()
        .HaveLongTitles()
    .Build();
```

You could of course make it even more succinct by adding an extension method to IListBuilder<Product>

```c#
public static IListBuilder<Product> WhereAllHaveLongTitles(this IListBuilder<Product> listBuilder)
{
    var listBuilderImpl = (IListBuilderImpl<Product>) listBuilder;
    var declaration = new GlobalDeclaration<Product>(listBuilderImpl, listBuilderImpl.CreateObjectBuilder());
    declaration.Have(x => x.Title = "12345....[LongString].....12345");

    return declaration;
}
```

This would allow you to say:

```c#
Builder<Product>.CreateListOfSize(10).WhereAllHaveLongTitles();
```

For more examples, [please check the functional tests](https://github.com/garethdown44/nbuilder/tree/master/Source/FizzWare.NBuilder.Tests/Integration)

Until the full documentation is available please have a look at the functional tests in the source code. These explain how to do everything that's currently possible in NBuilder.

##### Continuous Integration

NBuilder uses [AppVeyor](https://ci.appveyor.com/project/NBuilder) for continuous integration.
