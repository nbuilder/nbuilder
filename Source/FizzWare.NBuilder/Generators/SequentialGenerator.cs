using System;

namespace FizzWare.NBuilder.Generators
{
    public class SequentialGenerator<T> : IGenerator<T> where T : IConvertible
    {
        private int next;

        public T Generate()
        {
            return (T) Convert.ChangeType(next++, typeof (T));
        }
    }
}