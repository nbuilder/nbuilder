using System;
using System.Collections.Generic;

namespace FizzWare.NBuilder
{
    public class UniqueRandomPicker<T>
    {
        private readonly IConstraint constraint;
        private readonly IUniqueRandomGenerator uniqueRandomGenerator;

        public UniqueRandomPicker(IConstraint constraint, IUniqueRandomGenerator uniqueRandomGenerator)
        {
            this.constraint = constraint;
            this.uniqueRandomGenerator = uniqueRandomGenerator;
        }

        public IList<T> From(IList<T> listToPickFrom)
        {
            uniqueRandomGenerator.Reset();

            int capacity = listToPickFrom.Count;
            var listToReturn = new List<T>();

            int end = constraint.GetEnd();

            for (int i = 0; i < end; i++)
            {
                int index = uniqueRandomGenerator.Next(0, capacity);

                listToReturn.Add(listToPickFrom[index]);
            }

            return listToReturn;
        }
    }
}