using System.Collections.Generic;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;
using NUnit.Framework;

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
            randomGenerator = Substitute.For<IRandomGenerator>();
            list = Substitute.For<IList<MyClass>>();
        }

        [Test]
        public void ShouldBeAbleToUseRandomItemPicker()
        {
            const int listCount = 5;
            list.Count.Returns(listCount);
            randomGenerator.Next(0, listCount).Returns(2);

            var picker = new RandomItemPicker<MyClass>(list, randomGenerator);

            // Act
            picker.Pick();

            // Assert
            //http://stackoverflow.com/questions/39610125/how-to-check-received-calls-to-indexer-with-nsubstitute
            var ignored = list.Received()[2];
        }

        [Test]
        public void RandomItemPickerShouldHitRandomGeneratorEveryTimeAnItemIsPicked()
        {
            var zero = new MyClass();
            var one = new MyClass();

            var theList = new List<MyClass> { zero, one };

            int endIndex = theList.Count;


            randomGenerator.Next(0, endIndex).Returns(0, 1);

            var picker = new RandomItemPicker<MyClass>(theList, randomGenerator);

            Assert.That(picker.Pick(), Is.EqualTo(zero));
            Assert.That(picker.Pick(), Is.EqualTo(one));
        }
    }
}