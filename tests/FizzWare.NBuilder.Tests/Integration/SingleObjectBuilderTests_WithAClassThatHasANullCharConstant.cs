
using FizzWare.NBuilder.Tests.TestClasses;
using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Integration
{
    
    public class SingleObjectBuilderTests_WithAClassThatHasANullCharConstant
    {
        [Fact]
        public void ShouldBeAbleToCreateAClassThatHasANullCharConstant()
        {
            var builderSetup = new BuilderSettings();
            var mc = Builder<MyClassWithCharConst>.CreateNew().Build();

            mc.GetNullCharConst().ShouldBe(MyClassWithCharConst.NullCharConst);
        }

        [Fact]
        public void ShouldBeAbleToCreateAClassThatHasACharConstant()
        {
            var builderSetup = new BuilderSettings();
            var mc = Builder<MyClassWithCharConst>.CreateNew().Build();

            mc.GetNonNullCharConst().ShouldBe(MyClassWithCharConst.NonNullCharConst);
        }
    }
}
