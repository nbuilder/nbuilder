using System;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class SequentialGeneratorMultiTypeTests
    {
        [Test]
        public void ShouldBeAbleToUseAShort()
        {
            var generator = new SequentialGenerator<short>();
            Assert.That(generator.Generate(), Is.EqualTo(0));
            Assert.That(generator.Generate(), Is.EqualTo(1));

            generator.Direction = GeneratorDirection.Descending;

            Assert.That(generator.Generate(), Is.EqualTo(0));
            Assert.That(generator.Generate(), Is.EqualTo(-1));
        }

        [Test]
        public void ShouldBeAbleToUseALong()
        {
            var generator = new SequentialGenerator<long>();
            Assert.That(generator.Generate(), Is.EqualTo(0));
            Assert.That(generator.Generate(), Is.EqualTo(1));

            generator.Direction = GeneratorDirection.Descending;

            Assert.That(generator.Generate(), Is.EqualTo(0));
            Assert.That(generator.Generate(), Is.EqualTo(-1));
        }

        [Test]
        public void ShouldBeAbleToUseADecimal()
        {
            var generator = new SequentialGenerator<decimal> { Increment = .5m };

            Assert.That(generator.Generate(), Is.EqualTo(0m));
            Assert.That(generator.Generate(), Is.EqualTo(0.5m));
            Assert.That(generator.Generate(), Is.EqualTo(1m));

            generator.Direction = GeneratorDirection.Descending;

            Assert.That(generator.Generate(), Is.EqualTo(0.5m));
            Assert.That(generator.Generate(), Is.EqualTo(0m));
        }

        [Test]
        public void ShouldBeAbleToUseAFloat()
        {
            var generator = new SequentialGenerator<float>();
            Assert.That(generator.Generate(), Is.EqualTo(0f));
            Assert.That(generator.Generate(), Is.EqualTo(1f));
            generator.Direction = GeneratorDirection.Descending;
            Assert.That(generator.Generate(), Is.EqualTo(0f));
            Assert.That(generator.Generate(), Is.EqualTo(-1f));
        }

        [Test]
        public void ShouldBeAbleToUseADouble()
        {
            var generator = new SequentialGenerator<double>();
            Assert.That(generator.Generate(), Is.EqualTo(0d));
            Assert.That(generator.Generate(), Is.EqualTo(1d));
            generator.Direction = GeneratorDirection.Descending;
            Assert.That(generator.Generate(), Is.EqualTo(0d));
            Assert.That(generator.Generate(), Is.EqualTo(-1d));
        }

        [Test]
        public void ShouldBeAbleToUseAnUnsignedShort()
        {
            var generator = new SequentialGenerator<ushort>();
            Assert.That(generator.Generate(), Is.EqualTo(0));
            Assert.That(generator.Generate(), Is.EqualTo(1));

            generator.Direction = GeneratorDirection.Descending;
            Assert.That(generator.Generate(), Is.EqualTo(0));
            Assert.That(generator.Generate(), Is.EqualTo(0));
        }

        [Test]
        public void ShouldBeAbleToUseAnUnsignedInt()
        {
            var generator = new SequentialGenerator<uint>();
            Assert.That(generator.Generate(), Is.EqualTo(0));
            Assert.That(generator.Generate(), Is.EqualTo(1));

            generator.Direction = GeneratorDirection.Descending;
            Assert.That(generator.Generate(), Is.EqualTo(0));
            Assert.That(generator.Generate(), Is.EqualTo(0));
        }

        [Test]
        public void ShouldBeAbleToUseAnUnsignedLong()
        {
            var generator = new SequentialGenerator<ulong>();
            Assert.That(generator.Generate(), Is.EqualTo(0));
            Assert.That(generator.Generate(), Is.EqualTo(1));

            generator.Direction = GeneratorDirection.Descending;
            Assert.That(generator.Generate(), Is.EqualTo(0));
            Assert.That(generator.Generate(), Is.EqualTo(0));
        }

        [Test]
        public void ShouldBeAbleToUseAByte()
        {
            var generator = new SequentialGenerator<byte>();
            Assert.That(generator.Generate(), Is.EqualTo(0));
            Assert.That(generator.Generate(), Is.EqualTo(1));

            generator.Direction = GeneratorDirection.Descending;

            Assert.That(generator.Generate(), Is.EqualTo(0));
            Assert.That(generator.Generate(), Is.EqualTo(0));
        }

        [Test]
        public void ShouldBeAbleToUseAChar()
        {
            var generator = new SequentialGenerator<char>();
            generator.StartingWith('A');
            Assert.That(generator.Generate(), Is.EqualTo('A'));
            Assert.That(generator.Generate(), Is.EqualTo('B'));
            Assert.That(generator.Generate(), Is.EqualTo('C'));

            generator.Direction = GeneratorDirection.Descending;

            Assert.That(generator.Generate(), Is.EqualTo('B'));
            Assert.That(generator.Generate(), Is.EqualTo('A'));
        }

        [Test]
        public void ShouldBeAbleToUseABoolean()
        {
            var generator = new SequentialGenerator<bool>();
            Assert.That(generator.Generate(), Is.EqualTo(false));
            Assert.That(generator.Generate(), Is.EqualTo(true));
            Assert.That(generator.Generate(), Is.EqualTo(false));
        }

        [Test]
        public void ShouldBeAbleToUseADateTime()
        {
            var generator = new SequentialGenerator<DateTime>();
            Assert.That(generator.Generate(), Is.EqualTo(DateTime.MinValue));
            Assert.That(generator.Generate(), Is.EqualTo(DateTime.MinValue.AddDays(1)));
        }

        [Test]
        public void Generate_UnsupportedStruct_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => new SequentialGenerator<MyDummyStruct>());
        }

        private struct MyDummyStruct : IConvertible
        {

            public TypeCode GetTypeCode()
            {
                return new TypeCode();
            }

            public bool ToBoolean(IFormatProvider provider)
            {
                return default(bool);
            }

            public byte ToByte(IFormatProvider provider)
            {
                return default(byte);
            }

            public char ToChar(IFormatProvider provider)
            {
                return default(char);
            }

            public DateTime ToDateTime(IFormatProvider provider)
            {
                return default(DateTime);
            }

            public decimal ToDecimal(IFormatProvider provider)
            {
                return default(decimal);
            }

            public double ToDouble(IFormatProvider provider)
            {
                return default(double);
            }

            public short ToInt16(IFormatProvider provider)
            {
                return default(short);
            }

            public int ToInt32(IFormatProvider provider)
            {
                return default(int);
            }

            public long ToInt64(IFormatProvider provider)
            {
                return default(long);
            }

            public sbyte ToSByte(IFormatProvider provider)
            {
                return default(sbyte);
            }

            public float ToSingle(IFormatProvider provider)
            {
                return default(float);
            }

            public string ToString(IFormatProvider provider)
            {
                return default(string);
            }

            public object ToType(Type conversionType, IFormatProvider provider)
            {
                return default(object);
            }

            public ushort ToUInt16(IFormatProvider provider)
            {
                return default(ushort);
            }

            public uint ToUInt32(IFormatProvider provider)
            {
                return default(uint);
            }

            public ulong ToUInt64(IFormatProvider provider)
            {
                return default(ulong);
            }
        }
    }
}