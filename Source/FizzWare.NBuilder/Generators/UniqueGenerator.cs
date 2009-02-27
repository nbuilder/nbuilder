using System;
using System.Collections.Generic;

namespace FizzWare.NBuilder.Generators
{
    public class UniqueRandomGenerator<T> : RandomGenerator<T> where T : IConvertible
    {
        private readonly List<T> trackedValues = new List<T>();

        public UniqueRandomGenerator(T min, T max) 
            : base(min, max)
        {
        }

        public override T Generate()
        {
            T value = base.Generate();

            // loop round until the value is unique
            while (trackedValues.Contains(value))
                value = base.Generate();

            // add it to the list of values that have been provided
            trackedValues.Add(value);

            return value;
        }
    }
}