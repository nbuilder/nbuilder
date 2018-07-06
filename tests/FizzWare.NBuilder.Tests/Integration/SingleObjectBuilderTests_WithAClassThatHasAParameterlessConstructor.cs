using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Integration
{
    
    public class SingleObjectBuilderTests_WithAClassThatHasAParameterlessConstructor
    {
        [Fact]
        public void PropertiesShouldBeGivenDefaultValues()
        {
            var obj = new Builder().CreateNew<MyClass>().Build();

            obj.Int.ShouldBe(1);
            obj.StringOne.ShouldBe("StringOne1");
            obj.StringTwo.ShouldBe("StringTwo1");
            obj.EnumProperty.ShouldBe(MyEnum.EnumValue1);
        }

        [Fact]
        public void ShouldBeAbleToCreateAnObject()
        {
            var obj = Builder<MyClass>.CreateNew();
            obj.ShouldNotBeNull();
        }

        [Fact]
        public void ShouldBeAbleToDisableAutomaticPropertyNaming()
        {
            var builderSetup = new BuilderSettings { AutoNameProperties = false };
            var obj = new Builder(builderSetup).CreateNew<MyClass>().Build();

            obj.Int.ShouldBe(0);
        }

        [Fact]
        public void ShouldBeAbleToDisableAutomaticPropertyNamingForASpecificFieldOfASpecificType()
        {
            var builderSetup = new BuilderSettings();
            builderSetup.DisablePropertyNamingFor<MyClass, int>(x => x.Int);

            var obj = new Builder(builderSetup).CreateNew<MyClass>().Build();

            obj.Int.ShouldBe(0);
            obj.Long.ShouldBe(1);
        }

        [Fact]
        public void ShouldBeAbleToDisableAutomaticPropertyNamingForASpecificFieldOfAnInterface()
        {
            var builderSetup = new BuilderSettings();
            builderSetup.DisablePropertyNamingFor<IMyInterfaceWithProperty, int>(x => x.MyIntProperty);

            var obj = new Builder(builderSetup).CreateNew<MyClassWithPropery>().Build();

            obj.MyIntProperty.ShouldBe(0);
        }

        [Fact]
        public void ShouldBeAbleToDisableAutoPropertyNaming()
        {
            var builderSetup = new BuilderSettings { AutoNameProperties = false };

            var obj = new Builder(builderSetup).CreateNew<MyClass>().Build();

            obj.Int.ShouldBe(0);
            obj.Int.ShouldBe(0);

            obj.StringOne.ShouldBeNull();
            obj.StringOne.ShouldBeNull();
        }

        [Fact]
        public void ShouldBeAbleToSpecifyACustomPropertyNamerForASpecificType()
        {
            var builderSetup = new BuilderSettings();
            var propertyNamer = Substitute.For<IPropertyNamer>();

            builderSetup.SetPropertyNamerFor<MyClass>(propertyNamer);

            new Builder(builderSetup).CreateNew<MyClass>().Build();
            new Builder(builderSetup).CreateNew<SimpleClass>().Build();

            propertyNamer.Received().SetValuesOf(Arg.Any<MyClass>());
        }

        [Fact]
        public void ShouldBeAbleToSpecifyADefaultCustomPropertyNamer()
        {
            var builderSetup = new BuilderSettings();
            builderSetup.SetDefaultPropertyNamer(new MockPropertyNamerTests());
            new Builder(builderSetup).CreateNew<MyClass>().Build();
            MockPropertyNamerTests.SetValuesOf_obj_CallCount.ShouldBe(1);
        }

        [Fact]
        public void WithsShouldOverrideDefaultValues()
        {
            var builderSetup = new BuilderSettings();
            var obj = Builder<MyClass>
                .CreateNew()
                .With(x => x.StringTwo = "SpecialDescription")
                .Build();

            obj.StringOne.ShouldBe("StringOne1");
            obj.StringTwo.ShouldBe("SpecialDescription");
        }
    }
}