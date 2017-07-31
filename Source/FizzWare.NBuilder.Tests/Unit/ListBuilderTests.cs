using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NSubstitute;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class ListBuilderTests
    {
        private IReflectionUtil reflectionUtil;
        private IPropertyNamer propertyNamer;
        private readonly MyClass myClass = new MyClass();
        private const int listSize = 10;

        [SetUp]
        public void SetUp()
        {
            reflectionUtil = Substitute.For<IReflectionUtil>();
            propertyNamer = Substitute.For<IPropertyNamer>();
        }

        [Test]
        public void ShouldConstructDeclarations()
        {
            IGlobalDeclaration<MyClass> declaration = Substitute.For<IGlobalDeclaration<MyClass>>();

            var builder = new ListBuilder<MyClass>(listSize, propertyNamer, reflectionUtil, new BuilderSettings());

            builder.AddDeclaration(declaration);

            declaration.Construct();

            builder.Construct();
        }

        [Test]
        public void ConstructShouldComplainIfTypeNotParameterlessNoAllAndSumOfItemsInDeclarationsDoNotEqualCapacity()
        {
            IDeclaration<MyClassWithConstructor> declaration1 = Substitute.For<IDeclaration<MyClassWithConstructor>>();
            IDeclaration<MyClassWithConstructor> declaration2 = Substitute.For<IDeclaration<MyClassWithConstructor>>();

            declaration1.NumberOfAffectedItems.Returns(2);
            declaration2.NumberOfAffectedItems.Returns(2);
            reflectionUtil.RequiresConstructorArgs(typeof(MyClass)).Returns(true);
            var builder = new ListBuilder<MyClass>(10, propertyNamer, reflectionUtil, new BuilderSettings());

            Assert.Throws<BuilderException>(() => builder.Construct());
        }

        [Test]
        public void Constructing_AssignsValuesToProperties()
        {
            propertyNamer.SetValuesOfAllIn(Arg.Any<IList<MyClass>>());

            var list = new List<MyClass>();

            var builder = new ListBuilder<MyClass>(listSize, propertyNamer, reflectionUtil, new BuilderSettings());

            builder.Name(list);
        }

        [Test]
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

        [Test]
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
    }
}