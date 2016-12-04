# 4.0.0 - 2016-11-28

This is a catch-up release of cumulative changes to NBuilder that have accrued since the last offical release in 2011.
As it's been 5 years since the last official release, it's hard to know everthing that's changed.


## Breaking changes


### Obsolete methods have been removed. 

Any method previously marked with the `Obsolete` attribute has now been removed.

### Silverlight No Longer Supported

As Silverlight is effective a dead technology, we have officially ended support for it. This will allow us to better focus on
a  forthcoming release with .NET Core support.

## New Features

### `Builder` has a non-static implementation.

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

### `With` and `Do` action now supports a signature that receives an index 

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

## Bug Fixes

* The decimal separator was wrong for some cultures.
* Random number generation of decimals was sometimes incorrect.
* Sequences were not created in the correct order.
* Random strings were not always generated between the expected lengths.