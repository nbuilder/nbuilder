# Release Notes

## 6.1.0 - 2019-12-17

[Doug Murphy](https://github.com/Doug-Murphy) has added a couple of PR's to nbuilder.

* **Bug:** [Random generation of enums never includes the last enum value](https://github.com/nbuilder/nbuilder/issues/79).

* **Feature:** Add support for IPv6 addresses and MAC addresses to the `GetRandom` static class which is used to generate random, properly formatted values for specific property types.

## 6.0.1 - 2018-08-19

* **Bug** `Guid.Empty` being incremented. [Solution](https://github.com/nbuilder/nbuilder/pull/93) was to disable NBuilder's property name for static or read-only fields. Thanks to [Dominic Hemken](https://github.com/DHemken97) for the PR.
* **Bug** `CreateListOfSize` had undefined behavior when called from the static `Builder` and executed on multiple threads at the same time. While the future of NBulider will be to remove the static builder, it's a defect in the current implementation. Thanks to [Ovidiu Rădoi](https://github.com/oviradoi) for the PR.

## 6.0.0 - 2018-07-07

* **Breaking Change:** `WithConstructor` 
  * No longer takes an `Expression<Func<T>>`. 
  * Takes a `Func<T>`.
  * Marked `[Obsolete]` in favor of `WithFactory`
  * This change was to address an [issue](https://github.com/nbuilder/nbuilder/issues/42) in which the constructor expression was not being reevaluated for each item in a list. 
* **Feature:** [@AdemCatamak](https://github.com/AdemCatamak) Added support for `IndexOf` as part of the `ListBuilder` implementation.
```csharp
var products = new Builder()
    .CreateListOfSize<Product>(10)
    .IndexOf(0, 2, 5)
    .With(x => x.Title = "A special title")
    .Build();
```
* **Feature:** [@PureKrome](https://github.com/PureKrome) Added support for `DateTimeKind` to `RandomGenerator`
```csharp
var result = randomGenerator.Next(DateTime.MinValue, DateTime.MaxValue, DateTimeKind.Utc);
```
* **Feature:** Added `DisablePropertyNamingFor(PropertyInfo)` overload to `BuilderSettings`.
* **Feature:** Added `TheRest` as an extension to the `ListBuilder`.
```csharp
var results = new Builder()
        .CreateListOfSize<SimpleClass>(10)
        .TheFirst(2)
        .Do(row => row.String1 = "One")
        .TheRest()
        .Do(row => row.String1 = "Ten")
        .Build()
    ;
```

* **Bug:** Last item in enum is never generated when generating property values randomly.
* **Bug:** Lost strong name when porting to .NET Standard.
* **Bug:** Non-deterministic behavior when calling `TheLast` multiple times for the same range.

## 5.1.0 - 2018-05-15

* Added support for disabling property naming via an interface:

In a multi tenant scenario, the db entities usually have a property that denotes that they are for that specific tenant:

```
public interface ITenantEntity
{
    int TenantId {get; set;}
}

public class User: ITenantEntity
{
    public int TenantId {get; set;}
    ...
}

public class Car: ITenantEntity
{
    public int TenantId {get; set;}
    ...
}
```

When writing tests for the db layer, it is useful to disable the autoassignment for this property by just specifying an interface in BuilderSetup.DisablePropertyNamingFor.



## 5.0.0 - 2017-08-08

* Dropped support for .NET 3.5
* Added support for .NET Standard 1.6


## 4.0.0 - 2016-11-28

This is a catch-up release of cumulative changes to NBuilder that have accrued since the last offical release in 2011.
As it's been 5 years since the last official release, it's hard to know everthing that's changed.


### Breaking changes


#### Obsolete methods have been removed. 

Any method previously marked with the `Obsolete` attribute has now been removed.

#### Silverlight No Longer Supported

As Silverlight is effective a dead technology, we have officially ended support for it. This will allow us to better focus on
a  forthcoming release with .NET Core support.

### New Features

#### `Builder` has a non-static implementation.

This will allow you to create customized `BuilderSettings` for different testing scenarios.

**Old Code**

```csharp
    var results = Builder<MyObject>.CreateListOfSize(10).Build();
```

**New Code**
```csharp
    var settings = new BuilderSettings()
    var results = new Builder(settings).CreateListOfSize<MyObject>(10).Build();
```

#### `With` and `Do` action now supports a signature that receives an index 

***Example***

```csharp
    var builderSettings = new BuilderSettings();
    var list = new Builder(builderSettings)
            .CreateListOfSize<MyClassWithConstructor>(10)
            .All()
            .Do((row, index) => row.Int = index*2)
            .WithConstructor(() => new MyClassWithConstructor(1, 2f))
            .Build();

```

### Bug Fixes

* The decimal separator was wrong for some cultures.
* Random number generation of decimals was sometimes incorrect.
* Sequences were not created in the correct order.
* Random strings were not always generated between the expected lengths.