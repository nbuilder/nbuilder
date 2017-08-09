using NSubstitute;
using Shouldly;
using Xunit;
using Assert = Xunit.Assert;

namespace FizzWare.NBuilder.Tests.Unit.Picking
{
    public class BetweenConstraintTests
    {
        private IUniqueRandomGenerator uniqueRandomGenerator;
        private int lower;
        private int upper;

        public BetweenConstraintTests()
        {
            uniqueRandomGenerator = Substitute.For<IUniqueRandomGenerator>();
        }


        [Fact]
        public void ShouldBeAbleToUseBetweenPickerConstraint()
        {
            lower = 1;
            upper = 5;
            uniqueRandomGenerator.Next(lower, upper).Returns(2);
            var constraint = new BetweenConstraint(uniqueRandomGenerator, lower, upper);

            int end = constraint.GetEnd();

            end.ShouldBe(2);
        }

        [Fact]
        public void ShouldBeAbleToAddUpperUsingAnd()
        {
            uniqueRandomGenerator.Next(lower, upper).Returns(2);

            var constraint = new BetweenConstraint(uniqueRandomGenerator, lower);
            constraint.And(upper);

            constraint.GetEnd();
        }
    }
}