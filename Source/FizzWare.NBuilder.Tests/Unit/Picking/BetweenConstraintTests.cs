using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit.Picking
{
    [TestFixture]
    public class BetweenConstraintTests
    {
        private MockRepository mocks;
        private IUniqueRandomGenerator<int> uniqueRandomGenerator;
        private int lower;
        private int upper;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            uniqueRandomGenerator = mocks.StrictMock<IUniqueRandomGenerator<int>>();
        }

        [Test]
        public void ShouldBeAbleToUseBetweenPickerConstraint()
        {
            lower = 1;
            upper = 5;
            var constraint = new BetweenConstraint(uniqueRandomGenerator, lower, upper);

            using (mocks.Record())
            {
                uniqueRandomGenerator.Expect(x => x.Generate(lower, upper)).Return(2);
            }

            using (mocks.Ordered())
            {
                int end = constraint.GetEnd();

                Assert.That(end, Is.EqualTo(2));
            }
        }
    }
}
