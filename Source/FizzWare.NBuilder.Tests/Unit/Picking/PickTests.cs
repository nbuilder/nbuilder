using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Unit.Picking
{
    [TestFixture]
    public class PickTests
    {
        [Test]
        public void ShouldBeAbleToPickUniqueRandomListGivingExactCount()
        {
            var picker = Pick<MyClass>.UniqueRandomList(10);
            Assert.That(picker, Is.TypeOf(typeof(UniqueRandomPicker<MyClass>)));
        }

        [Test]
        public void ShouldBeAbleToPickUsingConstraint()
        {
            var picker = Pick<MyClass>.UniqueRandomList(new ExactlyConstraint(10));
            Assert.That(picker, Is.TypeOf(typeof(UniqueRandomPicker<MyClass>)));
        }

        [Test]
        public void ShouldBeAbleToUseRandomItemFrom()
        {
            var list = new List<MyClass> { new MyClass() };
            var item = Pick<MyClass>.RandomItemFrom(list);
            Assert.That(item, Is.EqualTo(list[0]));
        }
    }
}