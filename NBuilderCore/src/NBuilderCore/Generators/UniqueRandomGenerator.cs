using System;
using System.Collections.Generic;
using System.Globalization;

namespace NBuilderCore.Generators
{
    public class UniqueRandomGenerator : RandomGenerator, IUniqueRandomGenerator
    {
        private Dictionary<Type, List<object>> trackedValues;

        public UniqueRandomGenerator()
        {
            Reset();
        }

        public override byte Next(byte min, byte max)
        {
            return NextUnique<byte>(min, max, Convert.ToByte(max - min), base.Next);
        }

        public override sbyte Next(sbyte min, sbyte max)
        {
            return NextUnique<sbyte>(min, max, Convert.ToSByte(max - min), base.Next);
        }

        public override char Next(char min, char max)
        {
            return NextUnique<char>(min, max, Convert.ToChar(max - min), base.Next);
        }

        public override decimal Next(decimal min, decimal max)
        {
            return NextUnique(min, max, max - min, base.Next);
        }

        public override double Next(double min, double max)
        {
            return NextUnique(min, max, max - min, base.Next);
        }

        public override float Next(float min, float max)
        {
            return NextUnique(min, max, max - min, base.Next);
        }

        public override int Next(int min, int max)
        {
            return NextUnique(min, max, max - min, base.Next);
        }

        public override long Next(long min, long max)
        {
            return NextUnique(min, max, max - min, base.Next);
        }

        public override short Next(short min, short max)
        {
            return NextUnique<short>(min, max, Convert.ToInt16(max - min), base.Next);
        }

        public override uint Next(uint min, uint max)
        {
            return NextUnique(min, max, max - min, base.Next);
        }

        public override ulong Next(ulong min, ulong max)
        {
            return NextUnique(min, max, max - min, base.Next);
        }

        public override ushort Next(ushort min, ushort max)
        {
            return NextUnique(min, max, Convert.ToUInt16(max - min), base.Next);
        }

        private T NextUnique<T>(T min, T max, T rangeSize, Func<T, T, T> next) where T : struct, IConvertible, IComparable<T>
        {
            if (NoMoreValuesAvailable(rangeSize))
                throw new InvalidOperationException("There are no more unique values available");

            var value = next(min, max);

            while (trackedValues[typeof(T)].Contains(value))
                value = next(min, max);

            trackedValues[typeof(T)].Add(value);

            return value;
        }

        private bool NoMoreValuesAvailable<T>(T rangeSize) where T : struct, IConvertible, IComparable<T>
        {
            T count;

            unchecked
            {
                count = (T)Convert.ChangeType(trackedValues[typeof (T)].Count, typeof(T), CultureInfo.InvariantCulture);
            }

            if (count.CompareTo(rangeSize) == 0 || count.CompareTo(rangeSize) == 1)
            {
                return true;
            }

            return false;
        }

        public void Reset()
        {
            trackedValues = new Dictionary<Type, List<object>>
                                {
                                    {typeof (ushort), new List<object>()},
                                    {typeof (uint), new List<object>()},
                                    {typeof (ulong), new List<object>()},
                                    {typeof (short), new List<object>()},
                                    {typeof (int), new List<object>()},
                                    {typeof (long), new List<object>()},
                                    {typeof (float), new List<object>()},
                                    {typeof (double), new List<object>()},
                                    {typeof (decimal), new List<object>()},
                                    {typeof (byte), new List<object>()},
                                    {typeof (sbyte), new List<object>()},
                                    {typeof (char), new List<object>()}
                                };
        }
    }
}