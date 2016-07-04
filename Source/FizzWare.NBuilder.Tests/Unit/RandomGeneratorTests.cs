using System;
using System.Globalization;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;

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
        public void ShouldBeAbleToGeneratePositiveInt16UsingNext()
        {
            randomGenerator.Next((short)0, short.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateInt32UsingNext()
        {
            randomGenerator.Next(int.MinValue, int.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGeneratePositiveInt32UsingNext()
        {
            randomGenerator.Next(0, int.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateInt64UsingNext()
        {
            randomGenerator.Next(long.MinValue, long.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGeneratePositiveInt64UsingNext()
        {
            randomGenerator.Next(0, long.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateSingleUsingNext()
        {
            randomGenerator.Next(float.MinValue, float.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGeneratePositiveSingleUsingNext()
        {
            randomGenerator.Next(0, Single.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateDoubleUsingNext()
        {
            randomGenerator.Next(double.MinValue, double.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGeneratePositiveDoubleUsingNext()
        {
            randomGenerator.Next(0, double.MaxValue);
        }


        [Test]
        public void ShouldBeAbleToGenerateDoubleUsingNext_InPoland()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            try
            {
                randomGenerator.Next(double.MinValue, double.MaxValue);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            }
        }

        [Test]
        public void ShouldBeAbleToGenerateDecimalUsingNext()
        {
            randomGenerator.Next(decimal.MinValue, decimal.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGenerateDecimalUsingNext_InPoland()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            try
            {
                randomGenerator.Next(decimal.MinValue, decimal.MaxValue);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            }
        }

        [Test]
        public void ShouldBeAbleToGeneratePositiveDecimalUsingNext()
        {
            randomGenerator.Next(0, decimal.MaxValue);
        }

        [Test]
        public void ShouldBeAbleToGeneratePositiveFloatUsingNext()
        {
            randomGenerator.Next(0, float.MaxValue);
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
        public void ShouldBeAbleToGenerateShort()
        {
            randomGenerator.Short();
        }

        [Test]
        public void ShouldBeAbleToGenerateFloat()
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

        [Test(Description = "Tests the NextString returns a string that is between the minimum and maximum values specified")]
        public void ShouldBeBetweenMinAndMaxNextString()
        {
            // Arrange
            int minValue = 100;
            int maxValue = 200;

            // Act
            var result = randomGenerator.NextString(minValue, maxValue);

            // Assert
            Assert.That(result.Length, Is.LessThanOrEqualTo(maxValue));
            Assert.That(result.Length, Is.GreaterThanOrEqualTo(minValue));
        }

        // TODO FIX
        #if !SILVERLIGHT
        [Test]
        public void enum_should_throw_if_not_an_enum_type()
        {
            var type = typeof (string);
            Assert.Throws<ArgumentException>(() => randomGenerator.Enumeration(type));
        }
        #endif

        [Test]
        public void RandomGenerator_SeedInitialization_ShouldAllowRandomValuesToBeRepeatable()
        {
            const int seed = 5;

            IRandomGenerator seededRandomGenerator1 = new RandomGenerator(seed);
            IRandomGenerator seededRandomGenerator2 = new RandomGenerator(seed);

            Assert.That(seededRandomGenerator1.Int(), Is.EqualTo(seededRandomGenerator2.Int()));
            Assert.That(seededRandomGenerator1.Int(), Is.EqualTo(seededRandomGenerator2.Int()));
            Assert.That(seededRandomGenerator1.Int(), Is.EqualTo(seededRandomGenerator2.Int()));
            Assert.That(seededRandomGenerator1.Int(), Is.EqualTo(seededRandomGenerator2.Int()));
        }
    }
}