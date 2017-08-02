using System;
using FizzWare.NBuilder.Implementation;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class DistinctAffectedItemCalculatorTests
    {
        private DistinctAffectedItemCalculator sut;

        public DistinctAffectedItemCalculatorTests()
        {
            sut = new DistinctAffectedItemCalculator(5);
        }

        [Fact]
        public void ShouldCreateTheMap()
        {
            sut.Map.Length.ShouldBe(5);
        }

        [Fact]
        public void ShouldThrowIfAmountIsGreaterThanStartAndEnd()
        {
            Should.Throw<ArgumentException>(() => sut.AddRange(0, 1, 3));
        }

        [Fact]
        public void ShouldBeAbleToAddARange()
        {
            sut.AddRange(0, 4, 2);

            sut.Map[0].ShouldBeTrue();
            sut.Map[1].ShouldBeTrue();
            sut.Map[2].ShouldBeFalse();
            sut.Map[3].ShouldBeFalse();
            sut.Map[4].ShouldBeFalse();
        }

        [Fact]
        public void ShouldBeAbleTwoContiguousRanges()
        {
            sut.AddRange(0, 1, 2);
            sut.AddRange(2, 3, 2);

            sut.Map[0].ShouldBeTrue();
            sut.Map[1].ShouldBeTrue();
            sut.Map[2].ShouldBeTrue();
            sut.Map[3].ShouldBeTrue();
            sut.Map[4].ShouldBeFalse();
        }

        [Fact]
        public void ShouldBeAbleToAddTwoOverlappingRanges()
        {
            sut.AddRange(0, 1, 2);
            sut.AddRange(1, 3, 2);

            sut.Map[0].ShouldBeTrue();
            sut.Map[1].ShouldBeTrue();
            sut.Map[2].ShouldBeTrue();
            sut.Map[3].ShouldBeFalse();
            sut.Map[4].ShouldBeFalse();
        }

        [Fact]
        public void ShouldBeAbleToGetTotal()
        {
            sut.AddRange(0, 1, 2);
            sut.AddRange(1, 3, 2);

            sut.GetTotal().ShouldBe(3);
        }
    }
}