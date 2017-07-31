using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Unit.Picking
{
    public class ExactlyConstraintTests
    {

        [Fact]
        public void ShouldBeAbleToUseBetweenPickerConstraint()
        {
            var constraint = new ExactlyConstraint(5);

            int end = constraint.GetEnd();

            end.ShouldBe(5);
        }
    }
}
