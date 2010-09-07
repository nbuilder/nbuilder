using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit.Picking
{
    [TestFixture]
    public class UniqueRandomPickerTests
    {
        private MockRepository mocks;
        private IConstraint constraint;
        private IUniqueRandomGenerator uniqueRandomGenerator;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            constraint = mocks.DynamicMock<IConstraint>();
            uniqueRandomGenerator = mocks.DynamicMock<IUniqueRandomGenerator>();
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void UniqueRandomPickerShouldBeAbleToPickFromList()
        {
            var list = mocks.DynamicMock<IList<MyClass>>();
            var picker = new UniqueRandomPicker<MyClass>(constraint, uniqueRandomGenerator);
            

            using (mocks.Record())
            {
                var capacity = 10;
                var randomIndex = 3;
                var end = 2;

                uniqueRandomGenerator.Expect(x => x.Reset());
                list.Expect(x => x.Count).Return(capacity);
                constraint.Expect(x => x.GetEnd()).Return(end);
                uniqueRandomGenerator.Expect(x => x.Next(0, capacity - 1)).Return(randomIndex).Repeat.Times(end);
                list.Expect(x => x[randomIndex]).Return(new MyClass());
            }

            picker.From(list);
        }
    }
}