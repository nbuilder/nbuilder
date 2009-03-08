using System;

namespace FizzWare.NBuilder
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