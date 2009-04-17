using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

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
                            .WhereAll()
                            .Have(x => x.SimpleClasses = Pick<SimpleClass>.UniqueRandomList(With.Exactly(1).Elements).From(simpleClasses)).Build();

            for (int i = 0; i < products.Count; i++)
                Assert.That(products[i].SimpleClasses.Count, Is.EqualTo(1));
        }

        [Test]
        public void ShouldBeAbleToPickUsingBetweenPickerConstraint()
        {
            var simpleClasses = Builder<SimpleClass>.CreateListOfSize(10).Build();

            var products = Builder<MyClass>
                            .CreateListOfSize(10)
                            .WhereAll()
                            .Have(x => x.SimpleClasses = Pick<SimpleClass>.UniqueRandomList(With.Between(1, 5).Elements).From(simpleClasses)).Build();

            for (int i = 0; i < products.Count; i++)
            {
                Assert.That(products[i].SimpleClasses.Count, Is.AtLeast(1));
                Assert.That(products[i].SimpleClasses.Count, Is.AtMost(5));
            }
        }
    }
}