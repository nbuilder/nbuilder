using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class SingleObjectBuilderTests_WithAClassThatHasANonParameterlessConstructor
    {
        private const string theString = "string";
        private const decimal theDecimal = 10m;
        private const int theInt = 5;
        private const float theFloat = 15f;

        #pragma warning disable 0618 // (prevent warning for using obsolete method)
        [Test]
        public void ShouldBeAbleToCreateAnObject()
        {
            var builderSetup = new BuilderSetup();
            var obj = new Builder<MyClassWithConstructor>(builderSetup).CreateNew().WithConstructorArgs(theString, theDecimal).Build();

            Assert.That(obj.String, Is.EqualTo(theString));
            Assert.That(obj.Decimal, Is.EqualTo(theDecimal));
        }

        [Test]
        public void ShouldChooseCorrectConstructor()
        {
            var builderSetup = new BuilderSetup();
            var obj = new Builder<MyClassWithConstructor>(builderSetup).CreateNew().WithConstructorArgs(theInt, theFloat).Build();

            Assert.That(obj.Int, Is.EqualTo(theInt));
            Assert.That(obj.Float, Is.EqualTo(theFloat));
        }
        #pragma warning restore 0618
    }
}