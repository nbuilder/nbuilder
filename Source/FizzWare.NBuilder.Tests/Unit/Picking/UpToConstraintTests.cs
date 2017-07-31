using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Unit.Picking
{
    
    public class UpToConstraintTests
    {
        private IUniqueRandomGenerator uniqueRandomGenerator;
        private const int count = 5;

        public  UpToConstraintTests()
        {
            uniqueRandomGenerator = Substitute.For<IUniqueRandomGenerator>();
        }

        [Fact]
        public void ShouldBeAbleToUseBetweenPickerConstraint()
        {
            var constraint = new UpToConstraint(uniqueRandomGenerator, count);

            uniqueRandomGenerator.Next(0, count).Returns(1);
            int end = constraint.GetEnd();

            end.ShouldBe(1);
        }
    }
}
