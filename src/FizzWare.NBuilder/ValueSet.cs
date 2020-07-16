using System;
using System.Collections;
using System.Collections.Generic;

namespace FizzWare.NBuilder.Tests.Unit
{
    public class ValueSet<T>: IEnumerable<T>
    {
        private readonly T[] items;
        private readonly Random random;
        private int currentIndex = 0;

        public ValueSet(params T[] items)
        {
            this.items = items;
            this.random = new Random(DateTime.Now.Millisecond);

        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)items).GetEnumerator();
        }

        public T Next()
        {
            var result = this.items[this.currentIndex];
            this.currentIndex++;
            if (this.currentIndex >= this.items.Length)
            {
                this.currentIndex = 0;
            }

            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public T Random()
        {
            int index = this.random.Next(0, this.items.Length); // creates a number between 1 and 12
            return this.items[index];
        }
    }
}
