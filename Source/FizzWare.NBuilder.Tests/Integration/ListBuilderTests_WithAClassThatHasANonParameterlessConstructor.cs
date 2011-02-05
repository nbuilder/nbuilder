using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class ListBuilderTests_WithAClassThatHasANonParameterlessConstructor
    {
        private const string theString = "TheString";
        private const decimal theDecimal = 10m;

        #pragma warning disable 0618 // (prevent warning for using obsolete method)
        [Test]
        public void should_be_able_to_create_a_list_using_legacy_syntax()
        {
            var list =
                Builder<MyClassWithConstructor>
                    .CreateListOfSize(10)
                    .All()
                        .AreConstructedWith(theString, theDecimal)
                    .Build();

            Assert.That(list.Count, Is.EqualTo(10));
        }
        #pragma warning restore 0618

        [Test]
        public void should_be_able_to_use_AreConstructedUsing()
        {
            var list =
                Builder<MyClassWithConstructor>
                    .CreateListOfSize(10)
                    .All()
                        .AreConstructedUsing(() => new MyClassWithConstructor(1, 2f))
                    .Build();

            Assert.That(list.Count, Is.EqualTo(10));
        }

        [Test]
        public void should_be_able_to_use_IsConstructedWith()
        {
            var list =
                Builder<MyClassWithConstructor>
                    .CreateListOfSize(1)
                    .TheFirst(1)
                        .IsConstructedUsing(() => new MyClassWithConstructor(1, 2f))
                    .Build();

            Assert.That(list.Count, Is.EqualTo(1));
        }

        #pragma warning disable 0618 // (prevent warning for using obsolete method)
        [Test]
        public void should_set_properties_through_constructor_args_using_legacy_syntax()
        {
            var list =
                Builder<MyClassWithConstructor>
                    .CreateListOfSize(10)
                    .All()
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
                    .TheFirst(1)
                        .IsConstructedWith(theString, theDecimal)
                    .Build();

            Assert.That(list[0].String, Is.EqualTo(theString));
            Assert.That(list[0].Decimal, Is.EqualTo(theDecimal));
        }
        #pragma warning restore 0618
    }
}