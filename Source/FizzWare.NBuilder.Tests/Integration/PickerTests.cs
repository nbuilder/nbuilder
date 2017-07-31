using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder.Tests.TestClasses;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Integration
{
    
    public class PickerTests
    {
        [Fact]
        public void ShouldBeAbleToPickUsingExactlyConstraint()
        {
            var builderSetup = new BuilderSettings();
            var simpleClasses =Builder<SimpleClass>.CreateListOfSize(10).Build();

            var products = Builder<MyClass>
                            .CreateListOfSize(10)
                            .All()
                            .With(x => x.SimpleClasses = Pick<SimpleClass>.UniqueRandomList(With.Exactly(1).Elements).From(simpleClasses)).Build();

            for (int i = 0; i < products.Count; i++)
                products[i].SimpleClasses.Count.ShouldBe(1);
        }

        [Fact]
        public void ShouldBeAbleToPickUsingBetweenPickerConstraint()
        {
            var builderSetup = new BuilderSettings();
            var simpleClasses =Builder<SimpleClass>.CreateListOfSize(10).Build();

            var products = Builder<MyClass>
                            .CreateListOfSize(10)
                            .All()
                            .With(x => x.SimpleClasses = Pick<SimpleClass>.UniqueRandomList(With.Between(1, 5).Elements).From(simpleClasses)).Build();

            for (int i = 0; i < products.Count; i++)
            {
                products[i].SimpleClasses.Count.ShouldBeGreaterThanOrEqualTo(1);
                products[i].SimpleClasses.Count.ShouldBeLessThanOrEqualTo(5);
            }
        }

        [Fact]
        public void PickRandomItemShouldReturnRandomItems()
        {
            var itemList = new List<string>();

            for (int i = 0; i < 100; i++)
                itemList.Add("string"+i);

            var results = new List<string>();

            for (int i = 0; i < 100; i++)
                results.Add(Pick<string>.RandomItemFrom(itemList));

            var distinctItems = results.Distinct();

            distinctItems.Count().ShouldBeGreaterThan(1);
        }

        [Fact]
        public void WhenUsedInContextRandomItemPickerShouldPickDifferentItems()
        {
            var builderSetup = new BuilderSettings();
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
            distinctList.Count().ShouldBeGreaterThan(1);
        }

        [Fact]
        public void WhenPickingFromSmallListLargeNumberOfTimesShouldPickEachItemAtLeastOnce()
        {
            var builderSetup = new BuilderSettings();
            var fruits = new List<string>() { "apple", "orange", "banana", "pear" };

            var fruitBaskets =
                Builder<MyClass>
                    .CreateListOfSize(100)
                    .All()
                    .With(x => x.StringOne = Pick<string>.RandomItemFrom(fruits))
                .Build();

            var fruitsPicked = fruitBaskets.Select(x => x.StringOne).Distinct();
            fruitsPicked.Count().ShouldBe(4);
        }
    }
}