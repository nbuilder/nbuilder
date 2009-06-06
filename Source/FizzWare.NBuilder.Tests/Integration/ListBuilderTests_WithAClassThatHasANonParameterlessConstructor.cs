using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class ListBuilderTests_WithAClassThatHasANonParameterlessConstructor
    {
        private const string theString = "TheString";
        private const decimal theDecimal = 10m;

        [Test]
        public void ShouldBeAbleToCreateAList()
        {
            var list =
                Builder<MyClassWithConstructor>
                    .CreateListOfSize(10)
                    .WhereAll()
                        .AreConstructedWith(theString, theDecimal)
                    .Build();

            Assert.That(list, Has.Count(10));
        }

        [Test]
        public void ShouldSetPropertiesThroughConstructorArgs()
        {
            var list =
                Builder<MyClassWithConstructor>
                    .CreateListOfSize(10)
                    .WhereAll()
                        .AreConstructedWith(theString, theDecimal)
                    .Build();

            Assert.That(list[0].String, Is.EqualTo(theString));
            Assert.That(list[0].Decimal, Is.EqualTo(theDecimal));
        }

        [Test]
        public void ShouldBeAbleToUseSingularSyntax()
        {
            var list =
                Builder<MyClassWithConstructor>
                    .CreateListOfSize(1)
                    .WhereTheFirst(1)
                        .IsConstructedWith(theString, theDecimal)
                    .Build();

            Assert.That(list[0].String, Is.EqualTo(theString));
            Assert.That(list[0].Decimal, Is.EqualTo(theDecimal));
        }
    }
}