using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit.Picking
{
    [TestFixture]
    public class UpToConstraintTests
    {
        private MockRepository mocks;
        private IUniqueRandomGenerator uniqueRandomGenerator;
        private const int count = 5;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            uniqueRandomGenerator = mocks.StrictMock<IUniqueRandomGenerator>();
        }

        [Test]
        public void ShouldBeAbleToUseBetweenPickerConstraint()
        {
            var constraint = new UpToConstraint(uniqueRandomGenerator, count);

            using (mocks.Record())
            {
                uniqueRandomGenerator.Expect(x => x.Next(0, count)).Return(1);
            }

            using (mocks.Ordered())
            {
                int end = constraint.GetEnd();

                Assert.That(end, Is.EqualTo(1));
            }
        }
    }
}
