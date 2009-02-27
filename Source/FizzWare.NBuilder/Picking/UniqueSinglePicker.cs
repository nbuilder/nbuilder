using System;
using System.Collections.Generic;

namespace FizzWare.NBuilder
{
    public class UniqueSinglePicker<T>
    {
        readonly Random random = new Random((int)DateTime.Now.Ticks);
        readonly List<int> trackedValues = new List<int>();

        public T From(IList<T> listToPickFrom)
        {
            List<T> list = new List<T>();

            int index = random.Next(0, listToPickFrom.Count);

            while (trackedValues.Contains(index))
                index = random.Next(0, listToPickFrom.Count);

            trackedValues.Add(index);

            return listToPickFrom[index];
        }
    }
}