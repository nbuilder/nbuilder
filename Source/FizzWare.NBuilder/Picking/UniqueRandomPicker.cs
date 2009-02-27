using System;
using System.Collections.Generic;

namespace FizzWare.NBuilder
{
    public class UniqueRandomPicker<T>
    {
        readonly Random random = new Random((int)DateTime.Now.Ticks);
        readonly List<int> trackedValues = new List<int>();

        private readonly PickerConstraint constraint;

        public UniqueRandomPicker(PickerConstraint constraint)
        {
            this.constraint = constraint;
        }

        public IList<T> From(IList<T> listToPickFrom)
        {
            List<T> list = new List<T>();

            int start = constraint.GetStart(listToPickFrom.Count);
            int end = constraint.GetEnd(listToPickFrom.Count);

            end = end - start;

            for (int i = 0; i < end; i++)
            {
                int index = random.Next(0, listToPickFrom.Count);

                while (trackedValues.Contains(index))
                    index = random.Next(0, listToPickFrom.Count);

                trackedValues.Add(index);

                list.Add(listToPickFrom[index]);
            }

            return list;
        }
    }
}