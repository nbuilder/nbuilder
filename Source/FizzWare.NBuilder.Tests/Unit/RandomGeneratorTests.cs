using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class RandomGeneratorTests
    {
        readonly IRandomGenerator randomGenerator = new RandomGenerator();

        [Test]
        public void ShouldBeAbleToGenerateUInt16()
        {
            randomGenerator.Next(ushort.MinValue, ushort.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateUInt32()
        {
            randomGenerator.Next(uint.MinValue, uint.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateUInt64()
        {
            randomGenerator.Next(ulong.MinValue, ulong.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateInt16()
        {
            randomGenerator.Next(short.MinValue, short.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateInt32()
        {
            randomGenerator.Next(int.MinValue, int.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateInt64()
        {
            randomGenerator.Next(long.MinValue, long.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateSingle()
        {
            randomGenerator.Next(float.MinValue, float.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateDouble()
        {
            randomGenerator.Next(double.MinValue, double.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateDecimal()
        {
            randomGenerator.Next(decimal.MinValue, decimal.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateChar()
        {
            randomGenerator.Next(char.MinValue, char.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateByte()
        {
            randomGenerator.Next(byte.MinValue, byte.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateDateTime()
        {
            randomGenerator.Next(DateTime.MinValue, DateTime.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateBoolean()
        {
            randomGenerator.Next();
        }
    }
}