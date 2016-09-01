using System.Collections.Generic;
using NBuilderCore.Generators;

namespace NBuilderCore.Picking
{
    public class RandomItemPicker<T>
    {
        private readonly IList<T> from;
        private readonly IRandomGenerator randomGenerator;
        private readonly int max;

        public RandomItemPicker(IList<T> from, IRandomGenerator randomGenerator)
        {
            this.from = from;
            this.randomGenerator = randomGenerator;
            max = from.Count;
        }

        public T Pick()
        {
            int index = randomGenerator.Next(0, max);
            return from[index];
        }
    }
}