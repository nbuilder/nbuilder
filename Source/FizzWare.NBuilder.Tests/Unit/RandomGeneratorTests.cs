using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class RandomGeneratorTests
    {
        readonly IRandomGenerator randomGenerator = new RandomGenerator();

        [Test]
        public void ShouldBeAbleToGenerateUInt16UsingNext()
        {
            randomGenerator.Next(ushort.MinValue, ushort.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateUInt32UsingNext()
        {
            randomGenerator.Next(uint.MinValue, uint.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateUInt64UsingNext()
        {
            randomGenerator.Next(ulong.MinValue, ulong.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateInt16UsingNext()
        {
            randomGenerator.Next(short.MinValue, short.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateInt32UsingNext()
        {
            randomGenerator.Next(int.MinValue, int.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateInt64UsingNext()
        {
            randomGenerator.Next(long.MinValue, long.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateSingleUsingNext()
        {
            randomGenerator.Next(float.MinValue, float.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateDoubleUsingNext()
        {
            randomGenerator.Next(double.MinValue, double.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateDecimalUsingNext()
        {
            randomGenerator.Next(decimal.MinValue, decimal.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateCharUsingNext()
        {
            randomGenerator.Next(char.MinValue, char.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateByteUsingNext()
        {
            randomGenerator.Next(byte.MinValue, byte.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateSByteUsingNext()
        {
            randomGenerator.Next(sbyte.MinValue, sbyte.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateGuid()
        {
            var value = randomGenerator.Guid();

            Assert.That(value, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void ShouldBeAbleToGenerateDateTimeUsingNext()
        {
            randomGenerator.Next(DateTime.MinValue, DateTime.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateBooleanUsingNext()
        {
            randomGenerator.Next();
        }

        [Test]
        public void ShouldBeAbleToGenerateBoolean()
        {
            randomGenerator.Boolean();
        }

        [Test]
        public void ShouldBeAbleToGenerateDateTime()
        {
            randomGenerator.DateTime();
        }

        [Test]
        public void ShouldBeAbleToGenerateUInt16()
        {
            randomGenerator.UShort();
        }

        [Test]
        public void ShouldBeAbleToGenerateUInt32()
        {
            randomGenerator.UInt();
        }

        [Test]
        public void ShouldBeAbleToGenerateUInt64()
        {
            randomGenerator.ULong();
        }

        [Test]
        public void ShouldBeAbleToGenerateInt16()
        {
            randomGenerator.Short();
        }

        [Test]
        public void ShouldBeAbleToGenerateInt32()
        {
            randomGenerator.Int();
        }

        [Test]
        public void ShouldBeAbleToGenerateInt64()
        {
            randomGenerator.Long();
        }

        [Test]
        public void ShouldBeAbleToGenerateSingle()
        {
            randomGenerator.Float();
        }

        [Test]
        public void ShouldBeAbleToGenerateDouble()
        {
            randomGenerator.Double();
        }

        [Test]
        public void ShouldBeAbleToGenerateDecimal()
        {
            randomGenerator.Decimal();
        }

        [Test]
        public void ShouldBeAbleToGenerateChar()
        {
            randomGenerator.Char();
        }

        [Test]
        public void ShouldBeAbleToGenerateByte()
        {
            randomGenerator.Byte();
        }

        [Test]
        public void ShouldBeAbleToGenerateSByte()
        {
            randomGenerator.SByte();
        }

        [Test]
        public void should_be_able_to_generate_enum_using_type_param()
        {
            randomGenerator.Enumeration<MyEnum>();
        }

        [Test]
        public void should_be_able_to_generate_enum()
        {
            randomGenerator.Enumeration(typeof(MyEnum));
        }

        [Test]
        public void should_be_able_to_generate_a_phrase()
        {
            var phrase = randomGenerator.Phrase(50);

            Assert.That(phrase.Length, Is.LessThanOrEqualTo(50));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void enum_should_throw_if_not_an_enum_type()
        {
            var type = typeof (string);

            randomGenerator.Enumeration(type);
        }
    }
}