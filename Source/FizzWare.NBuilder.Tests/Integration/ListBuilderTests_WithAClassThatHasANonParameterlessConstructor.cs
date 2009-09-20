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
        public void should_be_able_to_create_a_list_using_legacy_syntax()
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
        public void should_be_able_to_use_AreConstructedUsing()
        {
            var list =
                Builder<MyClassWithConstructor>
                    .CreateListOfSize(10)
                    .WhereAll()
                        .AreConstructedUsing(() => new MyClassWithConstructor(1, 2f))
                    .Build();

            Assert.That(list, Has.Count(10));
        }

        [Test]
        public void should_be_able_to_use_IsConstructedWith()
        {
            var list =
                Builder<MyClassWithConstructor>
                    .CreateListOfSize(1)
                    .WhereTheFirst(1)
                        .IsConstructedUsing(() => new MyClassWithConstructor(1, 2f))
                    .Build();

            Assert.That(list, Has.Count(1));
        }

        [Test]
        public void should_set_properties_through_constructor_args_using_legacy_syntax()
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
        public void should_be_able_to_use_legacy_singular_syntax()
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