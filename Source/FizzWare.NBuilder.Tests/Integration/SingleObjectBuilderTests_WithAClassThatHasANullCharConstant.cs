using NUnit.Framework;
using FizzWare.NBuilder.Tests.TestClasses;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class SingleObjectBuilderTests_WithAClassThatHasANullCharConstant
    {
        [Test]
        public void ShouldBeAbleToCreateAClassThatHasANullCharConstant()
        {
            var builderSetup = new BuilderSetup();
            var mc = new Builder<MyClassWithCharConst>(builderSetup).CreateNew().Build();

            Assert.That(mc.GetNullCharConst(), Is.EqualTo(MyClassWithCharConst.NullCharConst));
        }

        [Test]
        public void ShouldBeAbleToCreateAClassThatHasACharConstant()
        {
            var builderSetup = new BuilderSetup();
            var mc = new Builder<MyClassWithCharConst>(builderSetup).CreateNew().Build();

            Assert.That(mc.GetNonNullCharConst(), Is.EqualTo(MyClassWithCharConst.NonNullCharConst));
        }
    }
}
