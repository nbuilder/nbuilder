using System;
using System.Globalization;
using FizzWare.NBuilder.Tests.TestClasses;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class RandomGeneratorTests
    {
        readonly IRandomGenerator randomGenerator = new RandomGenerator();


        [Fact]
        public void ShouldBeAbleToGenerateUInt16UsingNext()
        {
            randomGenerator.Next(ushort.MinValue, ushort.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGenerateUInt32UsingNext()
        {
            randomGenerator.Next(uint.MinValue, uint.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGenerateUInt64UsingNext()
        {
            randomGenerator.Next(ulong.MinValue, ulong.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGenerateInt16UsingNext()
        {
            randomGenerator.Next(short.MinValue, short.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGeneratePositiveInt16UsingNext()
        {
            randomGenerator.Next((short)0, short.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGenerateInt32UsingNext()
        {
            randomGenerator.Next(int.MinValue, int.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGeneratePositiveInt32UsingNext()
        {
            randomGenerator.Next(0, int.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGenerateInt64UsingNext()
        {
            randomGenerator.Next(long.MinValue, long.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGeneratePositiveInt64UsingNext()
        {
            randomGenerator.Next(0, long.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGenerateSingleUsingNext()
        {
            randomGenerator.Next(float.MinValue, float.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGeneratePositiveSingleUsingNext()
        {
            randomGenerator.Next(0, Single.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGenerateDoubleUsingNext()
        {
            randomGenerator.Next(double.MinValue, double.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGeneratePositiveDoubleUsingNext()
        {
            randomGenerator.Next(0, double.MaxValue);
        }


        [Fact]
        public void ShouldBeAbleToGenerateDoubleUsingNext_InPoland()
        {
            SetCulture("pl-PL");
            try
            {
                randomGenerator.Next(double.MinValue, double.MaxValue);
            }
            finally
            {
                SetCulture("en-US");
            }
        }

        private static void SetCulture(string cultureIdentifier)
        {
#if NETSTANDARD1_6 || NETCORE
            CultureInfo.CurrentCulture = new CultureInfo(cultureIdentifier);
#else
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureIdentifier);
#endif
        }

        [Fact]
        public void ShouldBeAbleToGenerateDecimalUsingNext()
        {
            randomGenerator.Next(decimal.MinValue, decimal.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGenerateDecimalUsingNext_InPoland()
        {
            SetCulture("pl-PL");
            try
            {
                randomGenerator.Next(decimal.MinValue, decimal.MaxValue);
            }
            finally
            {
                SetCulture("en-US");
            }
        }

        [Fact]
        public void ShouldBeAbleToGeneratePositiveDecimalUsingNext()
        {
            randomGenerator.Next(0, decimal.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGeneratePositiveFloatUsingNext()
        {
            randomGenerator.Next(0, float.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGenerateCharUsingNext()
        {
            randomGenerator.Next(char.MinValue, char.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGenerateByteUsingNext()
        {
            randomGenerator.Next(byte.MinValue, byte.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGenerateSByteUsingNext()
        {
            randomGenerator.Next(sbyte.MinValue, sbyte.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGenerateGuid()
        {
            var value = randomGenerator.Guid();

            value.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public void ShouldBeAbleToGenerateDateTimeUsingNext()
        {
            randomGenerator.Next(DateTime.MinValue, DateTime.MaxValue);
        }

        [Fact]
        public void ShouldBeAbleToGenerateBooleanUsingNext()
        {
            randomGenerator.Next();
        }

        [Fact]
        public void ShouldBeAbleToGenerateBoolean()
        {
            randomGenerator.Boolean();
        }

        [Fact]
        public void ShouldBeAbleToGenerateDateTime()
        {
            randomGenerator.DateTime();
        }

        [Fact]
        public void ShouldBeAbleToGenerateUInt16()
        {
            randomGenerator.UShort();
        }

        [Fact]
        public void ShouldBeAbleToGenerateUInt32()
        {
            randomGenerator.UInt();
        }

        [Fact]
        public void ShouldBeAbleToGenerateUInt64()
        {
            randomGenerator.ULong();
        }

        [Fact]
        public void ShouldBeAbleToGenerateInt16()
        {
            randomGenerator.Short();
        }

        [Fact]
        public void ShouldBeAbleToGenerateInt32()
        {
            randomGenerator.Int();
        }

        [Fact]
        public void ShouldBeAbleToGenerateInt64()
        {
            randomGenerator.Long();
        }

        [Fact]
        public void ShouldBeAbleToGenerateShort()
        {
            randomGenerator.Short();
        }

        [Fact]
        public void ShouldBeAbleToGenerateFloat()
        {
            randomGenerator.Float();
        }

        [Fact]
        public void ShouldBeAbleToGenerateDouble()
        {
            randomGenerator.Double();
        }

        [Fact]
        public void ShouldBeAbleToGenerateDecimal()
        {
            randomGenerator.Decimal();
        }

        [Fact]
        public void ShouldBeAbleToGenerateChar()
        {
            randomGenerator.Char();
        }

        [Fact]
        public void ShouldBeAbleToGenerateByte()
        {
            randomGenerator.Byte();
        }

        [Fact]
        public void ShouldBeAbleToGenerateSByte()
        {
            randomGenerator.SByte();
        }

        [Fact]
        public void should_be_able_to_generate_enum_using_type_param()
        {
            randomGenerator.Enumeration<MyEnum>();
        }

        [Fact]
        public void should_be_able_to_generate_enum()
        {
            randomGenerator.Enumeration(typeof(MyEnum));
        }

        [Fact]
        public void should_be_able_to_generate_a_phrase()
        {
            var phrase = randomGenerator.Phrase(50);

            phrase.Length.ShouldBeLessThanOrEqualTo(50);
        }


        [Theory]
        [InlineData(4, 5)]
        [InlineData(16, 20)]
        [InlineData(100, 200)]
        public void ShouldBeBetweenMinAndMaxNextString(int minLength, int maxLength)
        {
            // Arrange
            for (int i = 0; i < 100; i++)
            {
                // Act
                var result = randomGenerator.NextString(minLength, maxLength);

                // Assert
                result.Length.ShouldBeLessThanOrEqualTo(maxLength);
                result.Length.ShouldBeGreaterThanOrEqualTo(minLength);
            }

        }


        [Fact]
        public void enum_should_throw_if_not_an_enum_type()
        {
            var type = typeof(string);
            Should.Throw<ArgumentException>(() => randomGenerator.Enumeration(type));
        }

        [Fact]
        public void RandomGenerator_SeedInitialization_ShouldAllowRandomValuesToBeRepeatable()
        {
            const int seed = 5;

            IRandomGenerator seededRandomGenerator1 = new RandomGenerator(seed);
            IRandomGenerator seededRandomGenerator2 = new RandomGenerator(seed);

            seededRandomGenerator1.Int().ShouldBe(seededRandomGenerator2.Int());
            seededRandomGenerator1.Int().ShouldBe(seededRandomGenerator2.Int());
            seededRandomGenerator1.Int().ShouldBe(seededRandomGenerator2.Int());
            seededRandomGenerator1.Int().ShouldBe(seededRandomGenerator2.Int());
        }
    }
}