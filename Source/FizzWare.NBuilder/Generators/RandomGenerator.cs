using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzWare.NBuilder
{
    // Resharper incorrectly advises that the typecasts are redundant

    // ReSharper disable RedundantCast
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random rnd;

        private static DateTime minSqlServerDate = new DateTime(1753, 1, 1);
        private static DateTime maxSqlServerDate = new DateTime(9999, 12, 31);

        private static readonly string[] latinWords =
            {
                "lorem", "ipsum", "dolor", "sit", "amet", "consectetur",
                "adipisicing", "elit", "sed", "do", "eiusmod", "tempor",
                "incididunt", "ut", "labore", "et", "dolore", "magna", "aliqua"
            };

        public RandomGenerator() : this(System.Guid.NewGuid().GetHashCode()) { }

        public RandomGenerator(int seed) : this(new Random(seed)) { }

        public RandomGenerator(Random random)
        {
            rnd = random;
        }

        public virtual short Next(short min, short max)
        {
            return (short)Next((int)min, max);
        }

        public virtual int Next(int min, int max)
        {
            return rnd.Next(min, max);
        }

        public virtual long Next(long min, long max)
        {
            double rn = (max * 1.0 - min * 1.0) * rnd.NextDouble() + min * 1.0;
            return Convert.ToInt64(rn);
        }

        public virtual float Next(float min, float max)
        {
            return (float)Next((int)min, (int)max);
        }

        public virtual double Next(double min, double max)
        {
            return min + rnd.NextDouble() * (max - min);
        }

        public virtual decimal Next(decimal min, decimal max)
        {
            if (min < int.MinValue)
                min = (decimal)int.MinValue;

            if (max > int.MaxValue)
                max = (decimal)int.MaxValue;

            int iMin = (int)min;
            int iMax = (int)max;

            int integer = rnd.Next(iMin, iMax);
            int fraction = rnd.Next(0, 4000);

            return (decimal)Convert.ToDecimal(string.Format("{0}.{1}", integer, fraction));
        }

        public virtual char Next(char min, char max)
        {
            return (char)Next((int)min, (int)max);
        }

        public virtual byte Next(byte min, byte max)
        {
            return (byte)Next((int)min, (int)max);
        }

        public virtual sbyte Next(sbyte min, sbyte max)
        {
            return (sbyte)Next((int)min, (int)max);
        }

        public DateTime Next(DateTime min, DateTime max)
        {
            long minTicks = min.Ticks;
            long maxTicks = max.Ticks;
            double rn = (Convert.ToDouble(maxTicks)
               - Convert.ToDouble(minTicks)) * rnd.NextDouble()
               + Convert.ToDouble(minTicks);
            return new DateTime(Convert.ToInt64(rn));

        }

        public virtual bool Next()
        {
            return rnd.Next(2) == 1;
        }

        public Guid Guid()
        {
            return System.Guid.NewGuid();
        }

        public virtual bool Boolean()
        {
            return Next(0, 2) != 0;
        }

        public virtual int Int()
        {
            return Next(int.MinValue, int.MaxValue);
        }

        public virtual short Short()
        {
            return Next(short.MinValue, short.MaxValue);
        }

        public virtual long Long()
        {
            return Next(long.MinValue, long.MaxValue);
        }

        public virtual uint UInt()
        {
            return Next(uint.MinValue, uint.MaxValue);
        }

        public virtual ulong ULong()
        {
            return Next(ulong.MinValue, ulong.MaxValue);
        }

        public virtual ushort UShort()
        {
            return Next(ushort.MinValue, ushort.MaxValue);
        }

        public virtual decimal Decimal()
        {
            return Next(decimal.MinValue, decimal.MaxValue);
        }

        public virtual float Float()
        {
            return Next(float.MinValue, float.MaxValue);
        }

        public virtual double Double()
        {
            return Next(double.MinValue, double.MaxValue);
        }

        public virtual byte Byte()
        {
            return Next(byte.MinValue, byte.MaxValue);
        }

        public virtual sbyte SByte()
        {
            return Next(sbyte.MinValue, sbyte.MaxValue);
        }

        public virtual DateTime DateTime()
        {
            return Next(minSqlServerDate, maxSqlServerDate);
        }

        public virtual string Phrase(int length)
        {
            var count = latinWords.Length;
            var result = string.Empty;
            var done = false;
            while (!done)
            {
                var word = latinWords[Next(0, count - 1)];
                if (result.Length + word.Length + 1 > length)
                {
                    done = true;
                }
                else
                {
                    result += word + " ";
                }
            }
            return result.Trim();
        }

        public virtual char Char()
        {
            return Next(char.MinValue, char.MaxValue);
        }

        public virtual ushort Next(ushort min, ushort max)
        {
            return (ushort)Next((int)min, (int)max);
        }

        public virtual uint Next(uint min, uint max)
        {
            byte[] buffer = new byte[sizeof(uint)];
            rnd.NextBytes(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }

        public virtual ulong Next(ulong min, ulong max)
        {
            byte[] buffer = new byte[sizeof(ulong)];
            rnd.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        public T Enumeration<T>() where T : struct
        {
            var values = EnumHelper.GetValues(typeof(T));
            var index = Next(0, values.Length);
            return (T)values.GetValue(index);
        }

        public Enum Enumeration(Type type)
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException(string.Format("{0} is not an enum type.", type.FullName), "type");
            }
            var values = EnumHelper.GetValues(type);
            var index = Next(0, values.Length);
            return (Enum)values.GetValue(index);
        }

        public virtual string NextString(int minLength, int maxLength)
        {
            bool takeLower = this.Boolean();
            bool takeAverage = !takeLower && this.Boolean();
            bool takeHigher = !takeAverage && this.Boolean();

            takeAverage = !takeLower && !takeHigher;
            var average = (maxLength + minLength) / rnd.Next(2, 4);

            var count = latinWords.Length;
            var maxwordLength = latinWords.OrderBy(o => o.Length).First().Length;
            var result = new StringBuilder(string.Empty);
            var done = false;

            while (!done)
            {
                var word = latinWords[Next(0, count - 1)];
                var potential = result.Length + word.Length + 1;

                if (takeHigher && potential + maxwordLength < maxLength - 1)
                {
                    result.Append(word).Append(" ");
                }
                else
                {
                    result.Append(word).Append(" ");
                }

                if (takeHigher && latinWords.Any(w => w.Length + result.Length >= maxLength))
                {
                    done = true;
                }
                else if (takeAverage && result.Length >= average)
                {
                    done = true;
                }
                else if (takeLower && result.Length > minLength)
                {
                    done = true;
                }
            }

            return result.ToString().Trim();
        }
    }
    // ReSharper restore RedundantCast
}