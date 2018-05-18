using System;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class SequentialGeneratorMultiTypeTests
    {
        [Fact]
        public void ShouldBeAbleToUseAShort()
        {
            var generator = new SequentialGenerator<short>();
            generator.Generate().ShouldBe((short)0);
            generator.Generate().ShouldBe((short)1);

            generator.Direction = GeneratorDirection.Descending;

            generator.Generate().ShouldBe((short)0);
            generator.Generate().ShouldBe((short)-1);
        }

        [Fact]
        public void ShouldBeAbleToUseALong()
        {
            var generator = new SequentialGenerator<long>();
            generator.Generate().ShouldBe(0);
            generator.Generate().ShouldBe(1);

            generator.Direction = GeneratorDirection.Descending;

            generator.Generate().ShouldBe(0);
            generator.Generate().ShouldBe(-1);
        }

        [Fact]
        public void ShouldBeAbleToUseADecimal()
        {
            var generator = new SequentialGenerator<decimal> { Increment = .5m };

            generator.Generate().ShouldBe(0m);
            generator.Generate().ShouldBe(0.5m);
            generator.Generate().ShouldBe(1m);

            generator.Direction = GeneratorDirection.Descending;

            generator.Generate().ShouldBe(0.5m);
            generator.Generate().ShouldBe(0m);
        }

        [Fact]
        public void ShouldBeAbleToUseAFloat()
        {
            var generator = new SequentialGenerator<float>();
            generator.Generate().ShouldBe(0f);
            generator.Generate().ShouldBe(1f);
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate().ShouldBe(0f);
            generator.Generate().ShouldBe(-1f);
        }

        [Fact]
        public void ShouldBeAbleToUseADouble()
        {
            var generator = new SequentialGenerator<double>();
            generator.Generate().ShouldBe(0d);
            generator.Generate().ShouldBe(1d);
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate().ShouldBe(0d);
            generator.Generate().ShouldBe(-1d);
        }

        [Fact]
        public void ShouldBeAbleToUseAnUnsignedShort()
        {
            var generator = new SequentialGenerator<ushort>();
            generator.Generate().ShouldBe((ushort)0);
            generator.Generate().ShouldBe((ushort)1);

            generator.Direction = GeneratorDirection.Descending;
            generator.Generate().ShouldBe((ushort)0);
            generator.Generate().ShouldBe((ushort)0);
        }

        [Fact]
        public void ShouldBeAbleToUseAnUnsignedInt()
        {
            var generator = new SequentialGenerator<uint>();
            generator.Generate().ShouldBe((uint)0);
            generator.Generate().ShouldBe((uint)1);

            generator.Direction = GeneratorDirection.Descending;
            generator.Generate().ShouldBe((uint)0);
            generator.Generate().ShouldBe((uint)0);
        }

        [Fact]
        public void ShouldBeAbleToUseAnUnsignedLong()
        {
            var generator = new SequentialGenerator<ulong>();
            generator.Generate().ShouldBe((ulong)0);
            generator.Generate().ShouldBe((ulong)1);

            generator.Direction = GeneratorDirection.Descending;
            generator.Generate().ShouldBe((ulong)0);
            generator.Generate().ShouldBe((ulong)0);
        }

        [Fact]
        public void ShouldBeAbleToUseAByte()
        {
            var generator = new SequentialGenerator<byte>();
            generator.Generate().ShouldBe((byte)0);
            generator.Generate().ShouldBe((byte)1);

            generator.Direction = GeneratorDirection.Descending;

            generator.Generate().ShouldBe((byte)0);
            generator.Generate().ShouldBe((byte)0);
        }

        [Fact]
        public void ShouldBeAbleToUseAChar()
        {
            var generator = new SequentialGenerator<char>();
            generator.StartingWith('A');
            generator.Generate().ShouldBe('A');
            generator.Generate().ShouldBe('B');
            generator.Generate().ShouldBe('C');

            generator.Direction = GeneratorDirection.Descending;

            generator.Generate().ShouldBe('B');
            generator.Generate().ShouldBe('A');
        }

        [Fact]
        public void ShouldBeAbleToUseABoolean()
        {
            var generator = new SequentialGenerator<bool>();
            generator.Generate().ShouldBe(false);
            generator.Generate().ShouldBe(true);
            generator.Generate().ShouldBe(false);
        }

        [Fact]
        public void ShouldBeAbleToUseADateTime()
        {
            var generator = new SequentialGenerator<DateTime>();
            generator.Generate().ShouldBe(DateTime.MinValue);
            generator.Generate().ShouldBe(DateTime.MinValue.AddDays(1));
        }

        [Fact]
        public void Generate_UnsupportedStruct_ThrowsInvalidOperationException()
        {
           Should.Throw<InvalidOperationException>(() => new SequentialGenerator<MyDummyStruct>());
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