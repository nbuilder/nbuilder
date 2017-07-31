using NUnit.Framework;
using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class WithTests
    {
        [Fact]
        public void ShouldBeAbleToUseWith_Between_LowerAndUpper()
        {
            var constraint = With.Between(1, 5);
            constraint.ShouldBeOfType<BetweenConstraint>();
        }

        [Fact]
        public void ShouldBeAbleToUseWith_Between_LowerOnly()
        {
            var constraint = With.Between(1);
            constraint.ShouldBeOfType<BetweenConstraint>();
        }

        [Fact]
        public void ShouldBeAbleToUseWith_Exactly()
        {
            var constraint = With.Exactly(1);
            constraint.ShouldBeOfType<ExactlyConstraint>();
        }

        [Fact]
        public void ShouldBeAbleToUseWith_UpTo()
        {
            var constraint = With.UpTo(1);
            constraint.ShouldBeOfType<UpToConstraint>();
        }
    }
}