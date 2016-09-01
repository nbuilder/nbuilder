using System;
using System.Collections.Generic;
using System.Globalization;

namespace NBuilderCore.Generators
{
    public class SequentialGenerator<T> : IGenerator<T> where T : struct, IConvertible
    {
        private const IncrementDate DefaultIncrementDate = IncrementDate.Day;
        private const double DefaultIncrementDateValue = 1;
        private const int DefaultIncrementValue = 1;

        private IDictionary<Type, Action> typeAdvancers = new Dictionary<Type, Action>();

        private IDictionary<IncrementDate, Func<DateTime, DateTime>> dateTimeAdvancerIncrementers = new Dictionary<IncrementDate, Func<DateTime, DateTime>>();
        private IDictionary<IncrementDate, Func<DateTime, DateTime>> dateTimeAdvancerDecrementers = new Dictionary<IncrementDate, Func<DateTime, DateTime>>();

        public SequentialGenerator()
        {

            typeAdvancers = InitializeTypeAdvancers();
            dateTimeAdvancerIncrementers = InitializeDateTimeAdvancerIncrementers();
            dateTimeAdvancerDecrementers = InitializeDateTimeAdvancerDecrementers();

            if (!typeAdvancers.ContainsKey(typeof(T)))
                throw new InvalidOperationException("Sequential generator does not support " + typeof(T).Name);

            if (typeof(T) == typeof(DateTime))
            {
                Increment = (T)Convert.ChangeType(DateTime.MinValue, typeof(T), CultureInfo.InvariantCulture);
                IncrementDateBy = DefaultIncrementDate;
                IncrementDateValueBy = DefaultIncrementDateValue;
            }
            else
            {
                Increment = (T)Convert.ChangeType(DefaultIncrementValue, typeof(T), CultureInfo.InvariantCulture);
            }

            StartingWith(default(T));
        }

        private IDictionary<Type, Action> InitializeTypeAdvancers()
        {
            return new Dictionary<Type, Action>
            {
                { typeof(short), ShortAdvancer },
                { typeof(int), IntAdvancer },
                { typeof(long), LongAdvancer },
                { typeof(decimal), DecimalAdvancer },
                { typeof(float), FloatAdvancer },
                { typeof(double), DoubleAdvancer },
                { typeof(ushort), UShortAdvancer },
                { typeof(uint), UIntAdvancer },
                { typeof(ulong), ULongAdvancer },
                { typeof(byte), ByteAdvancer },
                { typeof(char), CharAdvancer },
                { typeof(bool), BooleanAdvancer },
                { typeof(DateTime), DateTimeAdvancer },
            };
        }

        private IDictionary<IncrementDate, Func<DateTime, DateTime>> InitializeDateTimeAdvancerIncrementers()
        {
            return new Dictionary<IncrementDate, Func<DateTime, DateTime>>
            {
                {IncrementDate.Tick, delegate(DateTime x) { return Convert.ToDateTime(x.AddTicks((long)IncrementDateValueBy)); } },
                {IncrementDate.Millisecond, delegate(DateTime x) { return Convert.ToDateTime(x.AddMilliseconds(IncrementDateValueBy)); } },
                {IncrementDate.Second, delegate(DateTime x) { return Convert.ToDateTime(x.AddSeconds(IncrementDateValueBy)); } },
                {IncrementDate.Minute, delegate(DateTime x) { return Convert.ToDateTime(x.AddMinutes(IncrementDateValueBy)); } },
                {IncrementDate.Hour, delegate(DateTime x) { return Convert.ToDateTime(x.AddHours(IncrementDateValueBy)); } },
                {IncrementDate.Month, delegate(DateTime x) {return Convert.ToDateTime(x.AddMonths((int)IncrementDateValueBy)); } },
                {IncrementDate.Year, delegate(DateTime x) {return Convert.ToDateTime(x.AddYears((int)IncrementDateValueBy)); } },
                {IncrementDate.Day, delegate(DateTime x) {  return Convert.ToDateTime(x.AddDays(IncrementDateValueBy)); } }
            };        
        }

        private IDictionary<IncrementDate, Func<DateTime, DateTime>> InitializeDateTimeAdvancerDecrementers()
        {
            return new Dictionary<IncrementDate, Func<DateTime, DateTime>>
            {
                {IncrementDate.Tick, delegate(DateTime x) { return Convert.ToDateTime(x.AddTicks(-(long)IncrementDateValueBy)); } },
                {IncrementDate.Millisecond, delegate(DateTime x) { return Convert.ToDateTime(x.AddMilliseconds(-IncrementDateValueBy)); } },
                {IncrementDate.Second, delegate(DateTime x) { return Convert.ToDateTime(x.AddSeconds(-IncrementDateValueBy)); } },
                {IncrementDate.Minute, delegate(DateTime x) { return Convert.ToDateTime(x.AddMinutes(-IncrementDateValueBy)); } },
                {IncrementDate.Hour, delegate(DateTime x) { return Convert.ToDateTime(x.AddHours(-IncrementDateValueBy)); } },
                {IncrementDate.Month, delegate(DateTime x) {return Convert.ToDateTime(x.AddMonths(-(int)IncrementDateValueBy)); } },
                {IncrementDate.Year, delegate(DateTime x) {return Convert.ToDateTime(x.AddYears(-(int)IncrementDateValueBy)); } },
                {IncrementDate.Day, delegate(DateTime x) {  return Convert.ToDateTime(x.AddDays(-IncrementDateValueBy)); } }
            };
        }

        private T next;
        private bool hasBeenReset;

        public T Generate()
        {
            if (!hasBeenReset)
                Advance();

            hasBeenReset = false;

            T val = (T)Convert.ChangeType(next, typeof (T), CultureInfo.InvariantCulture);
            return val;
        }

        public virtual void StartingWith(T nextValueToGenerate)
        {
            next = nextValueToGenerate;
            hasBeenReset = true;
        }

        protected virtual void Advance()
        {
            typeAdvancers[typeof(T)].Invoke();
        }

        private void DateTimeAdvancer()
        {
            if (Direction == GeneratorDirection.Ascending)
                PerformAdvance(x => Convert.ToDateTime(x), (x, y) => { return dateTimeAdvancerIncrementers[IncrementDateBy].Invoke(x); });
            else
                PerformAdvance(x => Convert.ToDateTime(x), (x, y) => { return dateTimeAdvancerDecrementers[IncrementDateBy].Invoke(x); });
        }

        private void BooleanAdvancer()
        {
            next = (T)Convert.ChangeType(Convert.ToBoolean(next) == false ? true : false, typeof(bool), CultureInfo.InvariantCulture);
        }

        private void CharAdvancer()
        {
            if (Direction == GeneratorDirection.Ascending)
                PerformAdvance(x => Convert.ToChar(x), (x, y) => Convert.ToChar(x + y));
            else
                PerformAdvance(x => Convert.ToChar(x), (x, y) => Convert.ToChar(x - y));
        }

        private void ByteAdvancer()
        {
            if (Direction == GeneratorDirection.Ascending)
            {
                // TODO: Add this check in for all types
                //if (Convert.ToByte(next) < byte.MaxValue)
                PerformAdvance(x => Convert.ToByte(x), (x, y) => Convert.ToByte(x + y));
            }
            else
            {
                if (Convert.ToByte(next) > 0)
                    PerformAdvance(x => Convert.ToByte(x), (x, y) => Convert.ToByte(x - y));
            }
        }

        private void ULongAdvancer()
        {
            if (Direction == GeneratorDirection.Ascending)
                PerformAdvance(x => Convert.ToUInt64(x), (x, y) => x + y);
            else
            {
                if (Convert.ToUInt64(next) > 0)
                    PerformAdvance(x => Convert.ToUInt64(x), (x, y) => x - y);
            }
        }

        private void UIntAdvancer()
        {
            if (Direction == GeneratorDirection.Ascending)
                PerformAdvance(x => Convert.ToUInt32(x), (x, y) => x + y);
            else
            {
                if (Convert.ToUInt32(next) > 0)
                    PerformAdvance(x => Convert.ToUInt32(x), (x, y) => x - y);
            }
        }

        private void UShortAdvancer()
        {
            if (Direction == GeneratorDirection.Ascending)
                PerformAdvance(x => Convert.ToUInt16(x), (x, y) => Convert.ToUInt16(x + y));
            else
            {
                if (Convert.ToUInt16(next) > 0)
                    PerformAdvance(x => Convert.ToUInt16(x), (x, y) => Convert.ToUInt16(x - y));
            }
        }

        private void DoubleAdvancer()
        {
            if (Direction == GeneratorDirection.Ascending)
                PerformAdvance(x => Convert.ToDouble(x), (x, y) => x + y);
            else
                PerformAdvance(x => Convert.ToDouble(x), (x, y) => x - y);
        }

        private void FloatAdvancer()
        {
            if (Direction == GeneratorDirection.Ascending)
                PerformAdvance(x => Convert.ToSingle(x), (x, y) => x + y);
            else
                PerformAdvance(x => Convert.ToSingle(x), (x, y) => x - y);
        }

        private void DecimalAdvancer()
        {
            if (Direction == GeneratorDirection.Ascending)
                PerformAdvance(x => Convert.ToDecimal(x), (x, y) => x + y);
            else
                PerformAdvance(x => Convert.ToDecimal(x), (x, y) => x - y);
        }

        private void LongAdvancer()
        {
            if (Direction == GeneratorDirection.Ascending)
                PerformAdvance(x => Convert.ToInt64(x), (x, y) => x + y);
            else
                PerformAdvance(x => Convert.ToInt64(x), (x, y) => x - y);
        }

        private void IntAdvancer()
        {
            if (Direction == GeneratorDirection.Ascending)
                PerformAdvance(x => Convert.ToInt32(x), (x, y) => x + y);
            else
                PerformAdvance(x => Convert.ToInt32(x), (x, y) => x - y);
        }

        private void ShortAdvancer()
        {
            if (Direction == GeneratorDirection.Ascending)
                PerformAdvance(x => Convert.ToInt16(x), (x, y) => Convert.ToInt16(x + y));
            else
                PerformAdvance(x => Convert.ToInt16(x), (x, y) => Convert.ToInt16(x - y));
        }

        private void PerformAdvance<TTo>(Func<T, TTo> convert, Func<TTo, TTo, TTo> advance)
        {
            next = (T)Convert.ChangeType(advance(convert(next), convert(Increment)), typeof (T), CultureInfo.InvariantCulture);
        }
        
        public T Increment { get; set; }

        public IncrementDate IncrementDateBy { get; set; }

        public double IncrementDateValueBy { get; set; }

        public GeneratorDirection Direction { get; set; }
    }

    public enum GeneratorDirection
    {
        Ascending,
        Descending
    }

    public enum IncrementDate
    {
        Tick,
        Millisecond,
        Second,
        Minute,
        Hour,
        Day,
        Month,
        Year
    }
}