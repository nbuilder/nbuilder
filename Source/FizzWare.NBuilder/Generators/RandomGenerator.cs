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

        public RandomGenerator()
        {
            rnd = new Random(Guid.NewGuid().GetHashCode());
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
            return (float) Next((int) min, (int)max);
        }

        public virtual double Next(double min, double max)
        {
            return rnd.NextDouble();
        }

        public virtual decimal Next(decimal min, decimal max)
        {
            if (min < int.MinValue)
                min = (decimal) int.MinValue;

            if (max > int.MaxValue)
                max = (decimal) int.MaxValue;

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

        public Guid NextGuid()
        {
            return Guid.NewGuid();
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

        // TODO: Implement NextString()
        //public virtual string NextString(int minLength, int maxLength)
        //{

        //}
    }
    // ReSharper restore RedundantCast
}