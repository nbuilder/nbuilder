using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class SingleObjectBuilderTests_WithAClassThatHasAParameterlessConstructor
    {
        [Test]
        public void PropertiesShouldBeGivenDefaultValues()
        {
            var obj = Builder<MyClass>.CreateNew().Build();

            Assert.That(obj.Int, Is.EqualTo(1));
            Assert.That(obj.StringOne, Is.EqualTo("StringOne1"));
            Assert.That(obj.StringTwo, Is.EqualTo("StringTwo1"));
            Assert.That(obj.EnumProperty, Is.EqualTo(MyEnum.EnumValue1));
        }

        [Test]
        public void ShouldBeAbleToCreateAnObject()
        {
            var obj = Builder<MyClass>.CreateNew();
            Assert.That(obj, Is.Not.Null);
        }

        [Test]
        public void ShouldBeAbleToDisableAutomaticPropertyNaming()
        {
            var builderSetup = new BuilderSettings { AutoNameProperties = false };
            var obj = new Builder(builderSetup).CreateNew<MyClass>().Build();

            Assert.That(obj.Int, Is.EqualTo(0));
        }

        [Test]
        public void ShouldBeAbleToDisableAutomaticPropertyNamingForASpecificFieldOfASpecificType()
        {
            var builderSetup = new BuilderSettings();
            builderSetup.DisablePropertyNamingFor<MyClass, int>(x => x.Int);

            var obj = new Builder(builderSetup).CreateNew<MyClass>().Build();

            Assert.That(obj.Int, Is.EqualTo(0));
            Assert.That(obj.Long, Is.EqualTo(1));
        }

        [Test]
        public void ShouldBeAbleToDisableAutoPropertyNaming()
        {
            var builderSetup = new BuilderSettings { AutoNameProperties = false };

            var obj = new Builder(builderSetup).CreateNew<MyClass>().Build();

            Assert.That(obj.Int, Is.EqualTo(0));
            Assert.That(obj.Int, Is.EqualTo(0));

            Assert.That(obj.StringOne, Is.Null);
            Assert.That(obj.StringOne, Is.Null);
        }

        [Test]
        public void ShouldBeAbleToSpecifyACustomPropertyNamerForASpecificType()
        {
            var builderSetup = new BuilderSettings();
            var propertyNamer = Substitute.For<IPropertyNamer>();

            builderSetup.SetPropertyNamerFor<MyClass>(propertyNamer);

            new Builder(builderSetup).CreateNew<MyClass>().Build();
            new Builder(builderSetup).CreateNew<SimpleClass>().Build();

            propertyNamer.Received().SetValuesOf(Arg.Any<MyClass>());
        }

        [Test]
        public void ShouldBeAbleToSpecifyADefaultCustomPropertyNamer()
        {
            var builderSetup = new BuilderSettings();
            builderSetup.SetDefaultPropertyNamer(new MockPropertyNamerTests());
            new Builder(builderSetup).CreateNew<MyClass>().Build();
            Assert.That(MockPropertyNamerTests.SetValuesOf_obj_CallCount, Is.EqualTo(1));
        }

        [Test]
        public void WithsShouldOverrideDefaultValues()
        {
            var builderSetup = new BuilderSettings();
            var obj = Builder<MyClass>
                .CreateNew()
                .With(x => x.StringTwo = "SpecialDescription")
                .Build();

            Assert.That(obj.StringOne, Is.EqualTo("StringOne1"));
            Assert.That(obj.StringTwo, Is.EqualTo("SpecialDescription"));
        }
    }
}