using NSubstitute;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit.Picking
{
    [TestFixture]
    public class BetweenConstraintTests
    {
        private IUniqueRandomGenerator uniqueRandomGenerator;
        private int lower;
        private int upper;

        [SetUp]
        public void SetUp()
        {
            uniqueRandomGenerator = Substitute.For<IUniqueRandomGenerator>();
        }

        [Test]
        public void ShouldBeAbleToUseBetweenPickerConstraint()
        {
            lower = 1;
            upper = 5;
            uniqueRandomGenerator.Next(lower, upper).Returns(2);
            var constraint = new BetweenConstraint(uniqueRandomGenerator, lower, upper);

            int end = constraint.GetEnd();

            Assert.That(end, Is.EqualTo(2));
        }

        [Test]
        public void ShouldBeAbleToAddUpperUsingAnd()
        {
            uniqueRandomGenerator.Next(lower, upper).Returns(2);

            var constraint = new BetweenConstraint(uniqueRandomGenerator, lower);
            constraint.And(upper);

            constraint.GetEnd();
        }
    }
}