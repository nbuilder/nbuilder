using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FizzWare.NBuilder
{
    public class Pick<T>
    {
        public static UniqueRandomPicker<T> UniqueRandomList(int count)
        {
            return new UniqueRandomPicker<T>(With.Exactly(count).Elements, new UniqueRandomGenerator<int>());
        }

        public static UniqueRandomPicker<T> UniqueRandomList(Constraint constraint)
        {
            return new UniqueRandomPicker<T>(constraint, new UniqueRandomGenerator<int>());
        }

        public static T RandomItemFrom(IList<T> list)
        {
            return new RandomItemPicker<T>(list, new RandomGenerator<int>()).Pick();
        }
    }
}