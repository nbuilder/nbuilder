using FizzWare.NBuilder.Implementation;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class PathTests
    {
        private Path path;

        public PathTests()
        {
            path = new Path();
        }

        [Fact]
        public void ShouldBeAbleToConvertToString()
        {
            path.ToString().ShouldBe("1");
        }

        [Fact]
        public void ShouldBeAbleToIncreaseDepth()
        {
            path.IncreaseDepth();
            path.ToString().ShouldBe("1.1");
        }

        [Fact]
        public void ShouldBeAbleToSetCurrent()
        {
            path.SetCurrent(2);
            path.ToString().ShouldBe("2");
        }

        [Fact]
        public void ShouldBeAbleToDecreaseDepth()
        {
            path.IncreaseDepth();
            path.DecreaseDepth();

            path.ToString().ShouldBe("1");
        }

        [Fact]
        public void SetCurrentShouldSetCurrentIdentifier()
        {
            path.IncreaseDepth();
            path.SetCurrent(2);
            path.ToString().ShouldBe("1.2");
        }
    }
}