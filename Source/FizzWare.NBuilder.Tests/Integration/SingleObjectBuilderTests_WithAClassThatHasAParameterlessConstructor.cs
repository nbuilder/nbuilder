using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using FizzWare.NBuilder.PropertyNaming;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
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
            var obj = Builder<MyClass>.CreateNew();
            Assert.That(obj, Is.Not.Null);
        }

        [Test]
        public void PropertiesShouldBeGivenDefaultValues()
        {
            var obj = Builder<MyClass>.CreateNew().Build();

            Assert.That(obj.Int, Is.EqualTo(1));
            Assert.That(obj.StringOne, Is.EqualTo("StringOne1"));
            Assert.That(obj.StringTwo, Is.EqualTo("StringTwo1"));
        }

        [Test]
        public void WithsShouldOverrideDefaultValues()
        {
            var obj = Builder<MyClass>
                        .CreateNew()
                        .With(x => x.StringTwo = "SpecialDescription")
                        .Build();

            Assert.That(obj.StringOne, Is.EqualTo("StringOne1"));
            Assert.That(obj.StringTwo, Is.EqualTo("SpecialDescription"));
        }

        [Test]
        public void ShouldBeAbleToDisableAutoPropertyNaming()
        {
            try
            {
                BuilderSetup.AutoNameProperties = false;

                var obj = Builder<MyClass>.CreateNew().Build();

                Assert.That(obj.Int, Is.EqualTo(0));
                Assert.That(obj.Int, Is.EqualTo(0));

                Assert.That(obj.StringOne, Is.Null);
                Assert.That(obj.StringOne, Is.Null);
            }
            finally
            {
                BuilderSetup.AutoNameProperties = true;
            }
        }

        [Test]
        public void ShouldBeAbleToSpecifyADefaultCustomPropertyNamer()
        {
            try
            {
                BuilderSetup.SetDefaultPropertyNamer(new MockPropertyNamerTests());
                Builder<MyClass>.CreateNew().Build();
                Assert.That(MockPropertyNamerTests.SetValuesOf_obj_CallCount, Is.EqualTo(1));
            }
            finally
            {
                BuilderSetup.ResetToDefaults();
            }
        }

        [Test]
        public void ShouldBeAbleToSpecifyACustomPropertyNamerForASpecificType()
        {
            try
            {
                IPropertyNamer propertyNamer = mocks.DynamicMock<IPropertyNamer>();

                BuilderSetup.SetPropertyNamerFor<MyClass>(propertyNamer);

                using (mocks.Record())
                {
                    propertyNamer.Expect(x => x.SetValuesOf(Arg<MyClass>.Is.TypeOf));
                }

                using (mocks.Playback())
                {
                    Builder<MyClass>.CreateNew().Build();
                    Builder<SimpleClass>.CreateNew().Build();
                }

                mocks.VerifyAll();
            }
            finally 
            {
                BuilderSetup.ResetToDefaults();
            }
        }

        [Test]
        public void ShouldBeAbleToDisableAutomaticPropertyNaming()
        {
            BuilderSetup.AutoNameProperties = false;
            var obj = Builder<MyClass>.CreateNew().Build();

            Assert.That(obj.Int, Is.EqualTo(0));
            BuilderSetup.AutoNameProperties = true;
        }

        [Test]
        public void ShouldBeAbleToDisableAutomaticPropertyNamingForASpecificFieldOfASpecificType()
        {
            try
            {
                BuilderSetup.DisablePropertyNamingFor<MyClass, int>(x => x.Int);

                var obj = Builder<MyClass>.CreateNew().Build();

                Assert.That(obj.Int, Is.EqualTo(0));
                Assert.That(obj.Long, Is.EqualTo(1));
            }
            finally
            {
                BuilderSetup.ResetToDefaults();
            }
        }
    }
}