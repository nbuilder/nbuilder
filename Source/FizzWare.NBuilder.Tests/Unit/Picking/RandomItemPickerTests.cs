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
    public class RandomItemPickerTests
    {
        private IRandomGenerator randomGenerator;
        private IList<MyClass> list;

        [SetUp]
        public void SetUp()
        {
            randomGenerator = MockRepository.GenerateStub<IRandomGenerator>();
            list = MockRepository.GenerateMock<IList<MyClass>>();
        }

        [Test]
        public void ShouldBeAbleToUseRandomItemPicker()
        {
            const int listCount = 5;
            list.Stub(x => x.Count).Return(listCount);
            randomGenerator.Stub(x => x.Next(0, listCount)).Return(2);
            
            var picker = new RandomItemPicker<MyClass>(list, randomGenerator);

            // Act
            picker.Pick();

            // Assert
            list.AssertWasCalled(x => x[2]);
        }

        [Test]
        public void RandomItemPickerShouldHitRandomGeneratorEveryTimeAnItemIsPicked()
        {
            var zero = new MyClass();
            var one = new MyClass();

            var theList = new List<MyClass> { zero, one };

            int endIndex = theList.Count;

            randomGenerator.Stub(x => x.Next(0, endIndex)).Return(0).Repeat.Once();
            randomGenerator.Stub(x => x.Next(0, endIndex)).Return(1).Repeat.Once();

            var picker = new RandomItemPicker<MyClass>(theList, randomGenerator);

            Assert.That(picker.Pick(), Is.EqualTo(zero));
            Assert.That(picker.Pick(), Is.EqualTo(one));
        }
    }
}