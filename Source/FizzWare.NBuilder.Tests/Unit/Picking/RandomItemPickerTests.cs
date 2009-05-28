using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Unit.Picking
{
    [TestFixture]
    public class RandomItemPickerTests
    {
        private MockRepository mocks;
        private IRandomGenerator randomGenerator;
        private IList<MyClass> list;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            randomGenerator = mocks.DynamicMock<IRandomGenerator>();
            list = mocks.DynamicMock<IList<MyClass>>();
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void ShouldBeAbleToUseRandomItemPicker()
        {
            const int listCount = 5;

            using (mocks.Record())
            {
                list.Expect(x => x.Count).Return(listCount);
                randomGenerator.Expect(x => x.Next(0, listCount)).Return(2);
                list.Expect(x => x[2]).Return(new MyClass());
            }

            using (mocks.Ordered())
            {
                var picker = new RandomItemPicker<MyClass>(list, randomGenerator);
                picker.Pick();
            }
        }

        [Test]
        public void RandomItemPickerShouldHitRandomGeneratorEveryTimeAnItemIsPicked()
        {
            var zero = new MyClass();
            var one = new MyClass();
            var two = new MyClass();
            var three = new MyClass();

            var theList = new List<MyClass> {zero, one, two, three};

            int endIndex = theList.Count;

            using (mocks.Record())
            {
                randomGenerator.Expect(x => x.Next(0, endIndex)).Return(0);
                randomGenerator.Expect(x => x.Next(0, endIndex)).Return(1);
                randomGenerator.Expect(x => x.Next(0, endIndex)).Return(2);
                randomGenerator.Expect(x => x.Next(0, endIndex)).Return(3);
            }

            using (mocks.Ordered())
            {
                var picker = new RandomItemPicker<MyClass>(theList, randomGenerator);
                Assert.That(picker.Pick(), Is.EqualTo(zero));
                Assert.That(picker.Pick(), Is.EqualTo(one));
                Assert.That(picker.Pick(), Is.EqualTo(two));
                Assert.That(picker.Pick(), Is.EqualTo(three));
            }
        }
    }
}