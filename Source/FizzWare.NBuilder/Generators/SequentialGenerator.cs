using System;
using System.Collections.Generic;
using System.Globalization;

namespace FizzWare.NBuilder
{
    public class SequentialGenerator<T> : IGenerator<T> where T : struct, IConvertible
    {
        public SequentialGenerator()
        {
            var validTypes = new List<Type>
                                 {
                                     typeof(short),
                                     typeof(int),
                                     typeof(long),
                                     typeof(decimal),
                                     typeof(float),
                                     typeof(double),
                                     typeof(ushort),
                                     typeof(uint),
                                     typeof(ulong),
                                     typeof(byte),
                                     typeof(char),
                                     typeof(bool)
                                 };

            if (!validTypes.Contains(typeof(T)))
                throw new InvalidOperationException("Sequential generator does not support " + typeof(T).Name);

            Increment = (T)Convert.ChangeType(1, typeof(T));
        }

        private T next;

        public T Generate()
        {
            Advance();
            T val = (T)Convert.ChangeType(next, typeof (T));
            return val;
        }

        public void ResetTo(T resetTo)
        {
            next = resetTo;
        }

        private void Advance()
        {
            if (typeof(T) == typeof(short))
            {
                if (Direction == GeneratorDirection.Ascending)
                    PerformAdvance(x => Convert.ToInt16(x), (x, y) => Convert.ToInt16(x + y));
                else
                    PerformAdvance(x => Convert.ToInt16(x), (x, y) => Convert.ToInt16(x - y));
            }

            if (typeof(T) == typeof(int))
            {
                if (Direction == GeneratorDirection.Ascending)
                    PerformAdvance(x => Convert.ToInt32(x), (x,y) => x + y);
                else
                    PerformAdvance(x => Convert.ToInt32(x), (x, y) => x - y);
            }

            if (typeof(T) == typeof(long))
            {
                if (Direction == GeneratorDirection.Ascending)
                    PerformAdvance(x => Convert.ToInt64(x), (x, y) => x + y);
                else
                    PerformAdvance(x => Convert.ToInt64(x), (x, y) => x - y);
            }

            if (typeof(T) == typeof(decimal))
            {
                if (Direction == GeneratorDirection.Ascending)
                    PerformAdvance(x => Convert.ToDecimal(x), (x, y) => x + y);
                else
                    PerformAdvance(x => Convert.ToDecimal(x), (x, y) => x - y);
            }

            if (typeof(T) == typeof(float))
            {
                if (Direction == GeneratorDirection.Ascending)
                    PerformAdvance(x => Convert.ToSingle(x), (x, y) => x + y);
                else
                    PerformAdvance(x => Convert.ToSingle(x), (x, y) => x - y);
            }

            if (typeof(T) == typeof(double))
            {
                if (Direction == GeneratorDirection.Ascending)
                    PerformAdvance(x => Convert.ToDouble(x), (x, y) => x + y);
                else
                    PerformAdvance(x => Convert.ToDouble(x), (x, y) => x - y);
            }

            if (typeof(T) == typeof(ushort))
            {
                if (Direction == GeneratorDirection.Ascending)
                    PerformAdvance(x => Convert.ToUInt16(x), (x, y) => Convert.ToUInt16(x + y));
                else
                {
                    if (Convert.ToUInt16(next) > 0)
                        PerformAdvance(x => Convert.ToUInt16(x), (x, y) => Convert.ToUInt16(x - y));
                }
            }

            if (typeof(T) == typeof(uint))
            {
                if (Direction == GeneratorDirection.Ascending)
                    PerformAdvance(x => Convert.ToUInt32(x), (x, y) => x + y);
                else
                {
                    if (Convert.ToUInt32(next) > 0)
                        PerformAdvance(x => Convert.ToUInt32(x), (x, y) => x - y);
                }
            }

            if (typeof(T) == typeof(ulong))
            {
                if (Direction == GeneratorDirection.Ascending)
                    PerformAdvance(x => Convert.ToUInt64(x), (x, y) => x + y);
                else
                {
                    if (Convert.ToUInt64(next) > 0)
                        PerformAdvance(x => Convert.ToUInt64(x), (x, y) => x - y);
                }
            }

            if (typeof(T) == typeof(byte))
            {
                if (Direction == GeneratorDirection.Ascending)
                    PerformAdvance(x => Convert.ToByte(x), (x, y) => Convert.ToByte(x + y));
                else
                    PerformAdvance(x => Convert.ToByte(x), (x, y) => Convert.ToByte(x - y));
            }

            if (typeof(T) == typeof(char))
            {
                if (Direction == GeneratorDirection.Ascending)
                    PerformAdvance(x => Convert.ToChar(x), (x, y) => Convert.ToChar(x + y));
                else
                    PerformAdvance(x => Convert.ToChar(x), (x, y) => Convert.ToChar(x - y));
            }

            if (typeof(T) == typeof(bool))
            {
                next = (T) Convert.ChangeType(Convert.ToBoolean(next) == false ? true : false, typeof (bool));
            }
        }

        private void PerformAdvance<TTo>(Func<T, TTo> convert, Func<TTo, TTo, TTo> advance)
        {
            next = (T)Convert.ChangeType(advance(convert(next), convert(Increment)), typeof (T));
        }

        public T Increment { get; set; }

        public GeneratorDirection Direction { get; set; }
    }

    public enum GeneratorDirection
    {
        Ascending,
        Descending
    }
}