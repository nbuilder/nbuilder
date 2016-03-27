# Nbuilder - A rapid test object generator.

[![Build status](https://ci.appveyor.com/api/projects/status/9y5vf1su1edysn1h?svg=true)](https://ci.appveyor.com/project/crmckenzie/nbuilder)
[![Join the chat at https://gitter.im/garethdown44/nbuilder](https://badges.gitter.im/garethdown44/nbuilder.svg)](https://gitter.im/garethdown44/nbuilder?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

Through a fluent, extensible interface, NBuilder allows you to rapidly create test data, automatically assigning values to properties and public fields that are one of the built in .NET data types (e.g. ints and strings). NBuilder allows you to override for properties you are interested in using lambda expressions.

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

For example, out of the box the list builder has seven 'declarations' `WhereAll()`, `WhereRandom(n)`, `WhereRandom(n, start, end)`, `WhereTheFirst(n)`, `WhereTheLast(n)`, `AndTheNext(n)`, `AndThePrevious(n)`. However if you wanted to add your own,

e.g. to return all the even or odd items, all you need to do is write a new extension method -` WhereAllEven()`

##### "Operable" extensions

If, for example, you find yourself repeating yourself when creating test data and you want to wrap something up in a method, you can do this by extending IOperable<T>. You can do this generically or per-type.

For example say if rather than saying:

```c#
Builder<Product>.CreateListOfSize(10).WhereAll().Have(x => x.Title = "12345....[LongString].....12345").Build();
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
    .WhereAll()
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

For more examples, [please check the functional tests](https://github.com/garethdown44/nbuilder/tree/master/Source/FizzWare.NBuilder.FunctionalTests)

Until the full documentation is available please have a look at the functional tests in the source code. These explain how to do everything that's currently possible in NBuilder.

### Contributing

##### To run the functional tests

Create an SQL Database named `NBuilderTests`. 

Update the connection string Data Source inside the `App.config` and make sure it points to an instance of SQL Server on your development machine.

```xml
<add name="Default" connectionString="Data Source=[.\SQLExpress ];Initial Catalog=NBuilderTests;Integrated Security=SSPI"/>
```

##### Development guidelines

- The project has two outputs - a CLR version and a Silverlight version. All code must compile for both.
- Every patch must have unit tests and those tests must provide 100% coverage.
- Every new class must have a 'unit' test fixture at least.
- Add integration tests when necessary to do so.
- For new features or changes to existing features use the functional tests project for high level real world tests and to serve as simple documentation for users.
- Any new tests must follow this naming convention: `MethodName_Scenario_Expectation()`
- Every class must have an interface and must be injected through constructor arguments.
- Every class must have a single responsibility. (SOLID Principles)
- Every new test must be in Arrange Act Assert form. If touching an existing test in record/replay, convert it to AAA syntax unless it is too time consuming.
- The "Foo Bar" convention is not permitted anywhere
- American English spellings should be used not British English

#####  Tests are divided into three categories:

Unit - The class under test is completely isolated by use of stubs or mocks.    
Integration - Classes tested together.    
Functional - 'Real life' tests and documentation.

##### Continuous Integration

NBuilder uses [TeamCity](http://teamcity.codebetter.com/project.html?projectId=NBuilder) hosted by [CodeBetter](http://codebetter.com/) for continuous integration.
