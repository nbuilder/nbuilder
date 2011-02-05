using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class PickerTests
    {
        [Test]
        public void ShouldBeAbleToPickUsingExactlyConstraint()
        {
            var simpleClasses = Builder<SimpleClass>.CreateListOfSize(10).Build();

            var products = Builder<MyClass>
                            .CreateListOfSize(10)
                            .All()
                            .With(x => x.SimpleClasses = Pick<SimpleClass>.UniqueRandomList(With.Exactly(1).Elements).From(simpleClasses)).Build();

            for (int i = 0; i < products.Count; i++)
                Assert.That(products[i].SimpleClasses.Count, Is.EqualTo(1));
        }

        [Test]
        public void ShouldBeAbleToPickUsingBetweenPickerConstraint()
        {
            var simpleClasses = Builder<SimpleClass>.CreateListOfSize(10).Build();

            var products = Builder<MyClass>
                            .CreateListOfSize(10)
                            .All()
                            .With(x => x.SimpleClasses = Pick<SimpleClass>.UniqueRandomList(With.Between(1, 5).Elements).From(simpleClasses)).Build();

            for (int i = 0; i < products.Count; i++)
            {
                Assert.That(products[i].SimpleClasses.Count, Is.AtLeast(1));
                Assert.That(products[i].SimpleClasses.Count, Is.AtMost(5));
            }
        }

        [Test]
        public void PickRandomItemShouldReturnRandomItems()
        {
            var itemList = new List<string>();

            for (int i = 0; i < 100; i++)
                itemList.Add("string"+i);

            var results = new List<string>();

            for (int i = 0; i < 100; i++)
                results.Add(Pick<string>.RandomItemFrom(itemList));

            var distinctItems = results.Distinct();

            Assert.That(distinctItems.Count(), Is.GreaterThan(1));
        }

        [Test]
        public void WhenUsedInContextRandomItemPickerShouldPickDifferentItems()
        {
            var stringList = new List<string>();

            for (int i = 0; i < 100; i++)
                stringList.Add("string"+i);

            var strings = stringList.ToArray();

            var vehicles =
                Builder<MyClass>
                    .CreateListOfSize(10)
                    .All()
                    .With(x => x.StringOne = Pick<string>.RandomItemFrom(strings))
                .Build();

            var list = vehicles.Select(x => x.StringOne);

            var distinctList = list.Distinct();
            Assert.That(distinctList.Count(), Is.GreaterThan(1));
        }

		[Test]
		public void WhenPickingFromSmallListLargeNumberOfTimesShouldPickEachItemAtLeastOnce()
		{
			var fruits = new List<string>() { "apple", "orange", "banana", "pear" };

			var fruitBaskets =
				Builder<MyClass>
					.CreateListOfSize(100)
					.All()
					.With(x => x.StringOne = Pick<string>.RandomItemFrom(fruits))
				.Build();

			var fruitsPicked = fruitBaskets.Select(x => x.StringOne).Distinct();
			Assert.AreEqual(4, fruitsPicked.Count());
		}
    }
}