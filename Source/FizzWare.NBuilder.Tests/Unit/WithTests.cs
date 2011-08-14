using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class WithTests
    {
        [Test]
        public void ShouldBeAbleToUseWith_Between_LowerAndUpper()
        {
            var constraint = With.Between(1, 5);
            Assert.That(constraint, Is.TypeOf(typeof(BetweenConstraint)));
        }

        [Test]
        public void ShouldBeAbleToUseWith_Between_LowerOnly()
        {
            var constraint = With.Between(1);
            Assert.That(constraint, Is.TypeOf(typeof(BetweenConstraint)));
        }

        [Test]
        public void ShouldBeAbleToUseWith_Exactly()
        {
            var constraint = With.Exactly(1);
            Assert.That(constraint, Is.TypeOf(typeof(ExactlyConstraint)));
        }

        [Test]
        public void ShouldBeAbleToUseWith_UpTo()
        {
            var constraint = With.UpTo(1);
            Assert.That(constraint, Is.TypeOf(typeof(UpToConstraint)));
        }
    }
}