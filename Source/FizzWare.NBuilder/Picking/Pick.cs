using System.Collections.Generic;
using System.Diagnostics;
using FizzWare.NBuilder.Generators;

namespace FizzWare.NBuilder
{
    public class Pick
    {
        public static T RandomSingle<T>(IList<T> fromList)
        {
            int listSize = fromList.Count;
            int randomIndex = new RandomGenerator<int>(0, listSize).Generate();
            return fromList[randomIndex];
        }

        public static UniqueRandomPicker<T> UniqueRandomListOf<T>(int count)
        {
            return new UniqueRandomPicker<T>(With.Exactly(count).Elements);
        }

        public static UniqueRandomPicker<T> UniqueRandomListOf<T>(PickerConstraint constraint)
        {
            return new UniqueRandomPicker<T>(constraint);
        }

        public static UniqueSinglePicker<T> Unique<T>()
        {
            return new UniqueSinglePicker<T>();
        }
    }
}