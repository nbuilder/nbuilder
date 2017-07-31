using System.Collections.Generic;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Unit.Picking
{
    
    public class RandomItemPickerTests
    {
        private IRandomGenerator randomGenerator;
        private IList<MyClass> list;

        public RandomItemPickerTests()
        {
            randomGenerator = Substitute.For<IRandomGenerator>();
            list = Substitute.For<IList<MyClass>>();
        }

        [Fact]
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

        [Fact]
        public void RandomItemPickerShouldHitRandomGeneratorEveryTimeAnItemIsPicked()
        {
            var zero = new MyClass();
            var one = new MyClass();

            var theList = new List<MyClass> { zero, one };

            int endIndex = theList.Count;


            randomGenerator.Next(0, endIndex).Returns(0, 1);

            var picker = new RandomItemPicker<MyClass>(theList, randomGenerator);

            picker.Pick().ShouldBe(zero);
            picker.Pick().ShouldBe(one);
        }
    }
}