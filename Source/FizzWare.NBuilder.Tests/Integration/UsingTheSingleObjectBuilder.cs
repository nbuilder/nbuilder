using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class UsingTheSingleObjectBuilderWithAClassThatHasAParameterlessConstructor
    {
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
    }
}