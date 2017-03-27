using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit.Picking
{
    [TestFixture]
    public class ExactlyConstraintTests
    {

        [Test]
        public void ShouldBeAbleToUseBetweenPickerConstraint()
        {
            var constraint = new ExactlyConstraint(5);

            int end = constraint.GetEnd();

            Assert.That(end, Is.EqualTo(5));
        }
    }
}
