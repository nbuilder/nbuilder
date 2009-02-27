using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzWare.NBuilder.Generators
{
    public class RandomGenerator<T> : IGenerator<T>  where T : IConvertible
    {
        private readonly T min;
        private readonly T max;
        private readonly Random random;

        public RandomGenerator(T min, T max)
        {
            this.min = min;
            this.max = max;
            random = new Random((int)DateTime.Now.Ticks);
        }

        public virtual T Generate()
        {
            int imin = Convert.ToInt32(min);
            int imax = Convert.ToInt32(max);

            return (T)Convert.ChangeType(random.Next(imin, imax), typeof(T));
        }
    }
}