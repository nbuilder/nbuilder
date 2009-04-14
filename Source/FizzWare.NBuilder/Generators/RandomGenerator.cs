using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzWare.NBuilder
{
    public class RandomGenerator<T> : IRandomGenerator<T> where T : IConvertible
    {
        private readonly Random random;

        public RandomGenerator()
        {
            random = new Random(DateTime.Now.Millisecond);
        }

        public virtual T Generate(int lower, int upper)
        {
            return (T) Convert.ChangeType(random.Next(lower, upper + 1), typeof(T));
        }
    }
}