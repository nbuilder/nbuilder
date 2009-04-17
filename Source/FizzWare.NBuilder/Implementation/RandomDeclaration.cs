using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder.Implementation
{
    public class RandomDeclaration<T> : RangeDeclaration<T>
    {
        private readonly IUniqueRandomGenerator uniqueRandomGenerator;
        private readonly int amount;

        public RandomDeclaration(IListBuilderImpl<T> listBuilderImpl, IObjectBuilder<T> objectBuilder, IUniqueRandomGenerator uniqueRandomGenerator, int amount, int start, int end) 
            : base(listBuilderImpl, objectBuilder, start, end)
        {
            this.uniqueRandomGenerator = uniqueRandomGenerator;
            this.amount = amount;
        }

        public override void Construct()
        {
            for (int i = 0; i < amount; i++)
            {
                myList.Add(objectBuilder.Construct());
            }
        }

        public override void AddToMaster(T[] masterList)
        {
            for (int i = 0; i < amount; i++)
            {
                int index = uniqueRandomGenerator.Next(Start, End); // was End - 1

                AddItemToMaster(myList[i], masterList, index);
            }
        }

        public override int NumberOfAffectedItems
        {
            get { return amount; }
        }
    }
}