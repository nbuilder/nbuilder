using System;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Implementation;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class DistinctAffectedItemCalculatorTests
    {
        private DistinctAffectedItemCalculator sut;

        [SetUp]
        public void SetUp()
        {
            sut = new DistinctAffectedItemCalculator(5);
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void ShouldCreateTheMap()
        {
            Assert.That(sut.Map.Length, Is.EqualTo(5));
        }

        [Test]
        public void ShouldThrowIfAmountIsGreaterThanStartAndEnd()
        {
            Assert.Throws<ArgumentException>(() => sut.AddRange(0, 1, 3));
        }

        [Test]
        public void ShouldBeAbleToAddARange()
        {
            sut.AddRange(0, 4, 2);

            Assert.IsTrue(sut.Map[0]);
            Assert.IsTrue(sut.Map[1]);
            Assert.IsFalse(sut.Map[2]);
            Assert.IsFalse(sut.Map[3]);
            Assert.IsFalse(sut.Map[4]);
        }

        [Test]
        public void ShouldBeAbleTwoContiguousRanges()
        {
            sut.AddRange(0, 1, 2);
            sut.AddRange(2, 3, 2);

            Assert.IsTrue(sut.Map[0]);
            Assert.IsTrue(sut.Map[1]);
            Assert.IsTrue(sut.Map[2]);
            Assert.IsTrue(sut.Map[3]);
            Assert.IsFalse(sut.Map[4]);
        }

        [Test]
        public void ShouldBeAbleToAddTwoOverlappingRanges()
        {
            sut.AddRange(0, 1, 2);
            sut.AddRange(1, 3, 2);

            Assert.IsTrue(sut.Map[0]);
            Assert.IsTrue(sut.Map[1]);
            Assert.IsTrue(sut.Map[2]);
            Assert.IsFalse(sut.Map[3]);
            Assert.IsFalse(sut.Map[4]);
        }

        [Test]
        public void ShouldBeAbleToGetTotal()
        {
            sut.AddRange(0, 1, 2);
            sut.AddRange(1, 3, 2);

            Assert.That(sut.GetTotal(), Is.EqualTo(3));
        }
    }
}