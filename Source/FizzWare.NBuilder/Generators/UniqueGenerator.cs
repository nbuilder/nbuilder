using System;
using System.Collections.Generic;

namespace FizzWare.NBuilder
{
    public class UniqueRandomGenerator<T> : RandomGenerator<T>, IUniqueRandomGenerator<T> where T : IConvertible
    {
        private readonly List<T> trackedValues = new List<T>();

        public override T Generate(int lower, int upper)
        {
            T value = base.Generate(lower, upper);

            // loop round until the value is unique
            while (trackedValues.Contains(value))
                value = base.Generate(lower, upper);

            // add it to the list of values that have been provided
            trackedValues.Add(value);

            return value;
        }

        public void Reset()
        {
            trackedValues.Clear();
        }
    }
}