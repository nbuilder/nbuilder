using System;

namespace FizzWare.NBuilder.Implementation
{
    public class DistinctAffectedItemCalculator
    {
        public bool[] Map;

        public DistinctAffectedItemCalculator(int capacity)
        {
            Map = new bool[capacity];
        }

        public void AddRange(int start, int end, int numberOfItems)
        {
            if (( end - start) < (numberOfItems - 1))
                throw new ArgumentException("The number of items cannot be greater than the range");

            int index = start;
            int endIndex = start + numberOfItems;

            for (; index < endIndex; index++)
            {
                Map[index] = true;
            }
        }

        public int GetTotal()
        {
            int total = 0;
            for (int i = 0; i < Map.Length; i++)
            {
                total += Map[i] ? 1 : 0;
            }

            return total;
        }
    }
}