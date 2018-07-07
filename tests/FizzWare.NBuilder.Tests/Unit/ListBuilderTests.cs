using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;

using NSubstitute;
using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class ListBuilderTests
    {
        private IReflectionUtil reflectionUtil;
        private IPropertyNamer propertyNamer;
        private readonly MyClass myClass = new MyClass();
        private const int listSize = 10;

        public ListBuilderTests()
        {
            reflectionUtil = Substitute.For<IReflectionUtil>();
            propertyNamer = Substitute.For<IPropertyNamer>();
        }

        [Fact]
        public void ShouldConstructDeclarations()
        {
            IGlobalDeclaration<MyClass> declaration = Substitute.For<IGlobalDeclaration<MyClass>>();

            var builder = new ListBuilder<MyClass>(listSize, propertyNamer, reflectionUtil, new BuilderSettings());

            builder.AddDeclaration(declaration);

            declaration.Construct();

            builder.Construct();
        }

        [Fact]
        public void ConstructShouldComplainIfTypeNotParameterlessNoAllAndSumOfItemsInDeclarationsDoNotEqualCapacity()
        {
            IDeclaration<MyClassWithConstructor> declaration1 = Substitute.For<IDeclaration<MyClassWithConstructor>>();
            IDeclaration<MyClassWithConstructor> declaration2 = Substitute.For<IDeclaration<MyClassWithConstructor>>();

            declaration1.NumberOfAffectedItems.Returns(2);
            declaration2.NumberOfAffectedItems.Returns(2);
            reflectionUtil.RequiresConstructorArgs(typeof(MyClass)).Returns(true);
            var builder = new ListBuilder<MyClass>(10, propertyNamer, reflectionUtil, new BuilderSettings());

            Should.Throw<BuilderException>(() => builder.Construct());
        }

        [Fact]
        public void Constructing_AssignsValuesToProperties()
        {
            propertyNamer.SetValuesOfAllIn(Arg.Any<IList<MyClass>>());

            var list = new List<MyClass>();

            var builder = new ListBuilder<MyClass>(listSize, propertyNamer, reflectionUtil, new BuilderSettings());

            builder.Name(list);
        }

        [Fact]
        public void ShouldBeAbleToBuildAList()
        {
            IDeclaration<MyClass> declaration = Substitute.For<IDeclaration<MyClass>>();

            var builder = new ListBuilder<MyClass>(listSize, propertyNamer, reflectionUtil, new BuilderSettings());

            reflectionUtil.RequiresConstructorArgs(typeof(MyClass)).Returns(false);
            reflectionUtil.CreateInstanceOf<MyClass>().Returns(myClass);
            declaration.Construct();
            declaration.AddToMaster(Arg.Any<MyClass[]>());
            propertyNamer.SetValuesOfAllIn(Arg.Any<IList<MyClass>>());
            declaration.CallFunctions(Arg.Any<IList<MyClass>>());

            builder.Build();
        }

        [Fact]
        public void IfNoAllExistsAndSumOfAffectedItemsInDeclarationsIsLessThanCapacity_ShouldAddADefaultAll()
        {
            var builder = new ListBuilder<MyClass>(30, propertyNamer, reflectionUtil, new BuilderSettings());
            builder.TheFirst(10);

            reflectionUtil.RequiresConstructorArgs(typeof(MyClass)).Returns(false);

            // Even though a declaration of 10 has been added, we expect the list builder to add
            // a default GlobalDeclaration (All). Therefore we expect CreateInstanceOf to be called 40 times
            reflectionUtil.CreateInstanceOf<MyClass>().Returns(myClass);
            builder.Construct();
        }

        private class Faker
        {
            private readonly string[] _names;
            private int indexer = 0;

            public string Next()
            {
                if (indexer > _names.Length - 1)
                    indexer = 0;

                return _names[indexer++];
            }

            public Faker(params string[] names)
            {
                _names = names;
            }
        }

        [Fact]
        public void WithFactory_ShouldCreateMultipleInstances()
        {

            var faker = new Faker("value1", "value2", "value3");
            var results = new Builder()
                    .CreateListOfSize<SimpleClass>(2)
                    .All()
                    .WithFactory(() => new SimpleClass(faker.Next()))
                    .Build()
                ;

            results[0].ShouldNotBe(results[1]);
        }


        [Fact]
        public void WithFactory_InstancesShouldReevaluateExpressionEachTime()
        {

            var faker = new Faker("value1", "value2", "value3");
            var results = new Builder()
                    .CreateListOfSize<SimpleClass>(2)
                    .All()
                    .WithFactory(() => new SimpleClass(faker.Next()))
                    .Build()
                ;

            results[0].String1.ShouldNotBe(results[1].String1);
        }

        [Fact]
        public void TheRest_OperatesOnRemainingItems()
        {
            var results = new Builder()
                    .CreateListOfSize<SimpleClass>(10)
                    .TheFirst(2)
                    .Do(row => row.String1 = "One")
                    .TheRest()
                    .Do(row => row.String1 = "Ten")
                    .Build()
                ;

            results.Take(2).ToList().ForEach(e => e.String1.ShouldBe("One"));
            results.Skip(2).ToList().ForEach(e => e.String1.ShouldBe("Ten"));
        }
    }
}