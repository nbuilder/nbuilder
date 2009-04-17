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
                randomGenerator.Expect(x => x.Next(0, listCount - 1)).Return(2);
                list.Expect(x => x[2]).Return(new MyClass());
            }

            using (mocks.Ordered())
            {
                RandomItemPicker<MyClass> picker = new RandomItemPicker<MyClass>(list, randomGenerator);
                picker.Pick();
            }
        }
    }
}