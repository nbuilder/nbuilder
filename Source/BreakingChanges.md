# 4.x

## `Builder` is no longer static.

**Old Code**

```csharp
    var results = Builder<MyObject>.CreateListOfSize(10).Build();
```

**New Code**
```csharp
    var results = new Builder<MyObject>(new BuilderSetup()).CreateListOfSize(10).Build();
```


## Obsolete methods have been removed. 