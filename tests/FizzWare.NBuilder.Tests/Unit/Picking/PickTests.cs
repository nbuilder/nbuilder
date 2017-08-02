using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Tests.TestClasses;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit.Picking
{
    public class PickTests
    {
        [Fact]
        public void ShouldBeAbleToPickUniqueRandomListGivingExactCount()
        {
            var picker = Pick<MyClass>.UniqueRandomList(10);
            picker.ShouldBeOfType<UniqueRandomPicker<MyClass>>();
        }

        [Fact]
        public void ShouldBeAbleToPickUsingConstraint()
        {
            var picker = Pick<MyClass>.UniqueRandomList(new ExactlyConstraint(10));
            picker.ShouldBeOfType<UniqueRandomPicker<MyClass>>();
        }

        [Fact]
        public void ShouldBeAbleToUseRandomItemFrom()
        {
            var list = new List<MyClass> { new MyClass() };
            var item = Pick<MyClass>.RandomItemFrom(list);
            item.ShouldBe(list[0]);
        }
    }
}