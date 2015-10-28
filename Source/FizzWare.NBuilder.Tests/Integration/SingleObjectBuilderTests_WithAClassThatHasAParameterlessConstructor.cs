using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class SingleObjectBuilderTests_WithAClassThatHasAParameterlessConstructor
    {
        private MockRepository mocks;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
        }

      

        [Test]
        public void ShouldBeAbleToCreateAnObject()
        {
            var builderSetup = new BuilderSetup();
            var obj = new Builder<MyClass>(builderSetup).CreateNew();
            Assert.That(obj, Is.Not.Null);
        }

        [Test]
        public void PropertiesShouldBeGivenDefaultValues()
        {
            var builderSetup = new BuilderSetup();
            var obj = new Builder<MyClass>(builderSetup).CreateNew().Build();

            Assert.That(obj.Int, Is.EqualTo(1));
            Assert.That(obj.StringOne, Is.EqualTo("StringOne1"));
            Assert.That(obj.StringTwo, Is.EqualTo("StringTwo1"));
			Assert.That(obj.EnumProperty, Is.EqualTo(MyEnum.EnumValue1));
        }

        [Test]
        public void WithsShouldOverrideDefaultValues()
        {
            var builderSetup = new BuilderSetup();
            var obj = new Builder<MyClass>(builderSetup)
                        .CreateNew()
                        .With(x => x.StringTwo = "SpecialDescription")
                        .Build();

            Assert.That(obj.StringOne, Is.EqualTo("StringOne1"));
            Assert.That(obj.StringTwo, Is.EqualTo("SpecialDescription"));
        }

        [Test]
        public void ShouldBeAbleToDisableAutoPropertyNaming()
        {
            var builderSetup = new BuilderSetup();
            builderSetup.AutoNameProperties = false;

            var obj = new Builder<MyClass>(builderSetup).CreateNew().Build();

            Assert.That(obj.Int, Is.EqualTo(0));
            Assert.That(obj.Int, Is.EqualTo(0));

            Assert.That(obj.StringOne, Is.Null);
            Assert.That(obj.StringOne, Is.Null);
        }

        [Test]
        public void ShouldBeAbleToSpecifyADefaultCustomPropertyNamer()
        {
            var builderSetup = new BuilderSetup();
            builderSetup.SetDefaultPropertyNamer(new MockPropertyNamerTests());
            new Builder<MyClass>(builderSetup).CreateNew().Build();
            Assert.That(MockPropertyNamerTests.SetValuesOf_obj_CallCount, Is.EqualTo(1));
        }

        [Test]
        public void ShouldBeAbleToSpecifyACustomPropertyNamerForASpecificType()
        {
            var builderSetup = new BuilderSetup();
            IPropertyNamer propertyNamer = mocks.DynamicMock<IPropertyNamer>();

            builderSetup.SetPropertyNamerFor<MyClass>(propertyNamer);

            using (mocks.Record())
            {
                propertyNamer.Expect(x => x.SetValuesOf(Arg<MyClass>.Is.TypeOf));
            }

            using (mocks.Playback())
            {
                new Builder<MyClass>(builderSetup).CreateNew().Build();
                new Builder<SimpleClass>(builderSetup).CreateNew().Build();
            }

            mocks.VerifyAll();
        }

        [Test]
        public void ShouldBeAbleToDisableAutomaticPropertyNaming()
        {
            var builderSetup = new BuilderSetup();
            builderSetup.AutoNameProperties = false;
            var obj = new Builder<MyClass>(builderSetup).CreateNew().Build();

            Assert.That(obj.Int, Is.EqualTo(0));
        }

        [Test]
        public void ShouldBeAbleToDisableAutomaticPropertyNamingForASpecificFieldOfASpecificType()
        {
            var builderSetup = new BuilderSetup();
            builderSetup.DisablePropertyNamingFor<MyClass, int>(x => x.Int);

            var obj = new Builder<MyClass>(builderSetup).CreateNew().Build();

            Assert.That(obj.Int, Is.EqualTo(0));
            Assert.That(obj.Long, Is.EqualTo(1));
        }
    }
}