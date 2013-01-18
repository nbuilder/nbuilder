using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        [Test]
        public void UniqueRandomPickerShouldBeAbleToPickEntireList()
        {
            var testInput = new[] { 1, 2, 3, 4 };

            var results = Pick<int>.UniqueRandomList(testInput.Length).From(testInput);

            Assert.That(results.Count, Is.EqualTo(testInput.Length));
        }
    }

    [TestFixture]
    public class UniqueRandomPickerMockedTests
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
                uniqueRandomGenerator.Expect(x => x.Next(0, capacity)).Return(randomIndex).Repeat.Times(end);
                list.Expect(x => x[randomIndex]).Return(new MyClass());
            }

            picker.From(list);
        }
    }
}