using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder.Implementation
{
    public class RandomDeclaration<T> : RangeDeclaration<T>
    {
        private readonly IUniqueRandomGenerator<int> uniqueRandomGenerator;
        private readonly int amount;

        public RandomDeclaration(IListBuilderImpl<T> listBuilderImpl, IObjectBuilder<T> objectBuilder, IUniqueRandomGenerator<int> uniqueRandomGenerator, int amount, int start, int end) 
            : base(listBuilderImpl, objectBuilder, start, end)
        {
            this.uniqueRandomGenerator = uniqueRandomGenerator;
            this.amount = amount;
        }

        public RandomDeclaration(IListBuilderImpl<T> listBuilderImpl, IObjectBuilder<T> objectBuilder, IUniqueRandomGenerator<int> uniqueRandomGenerator, int amount)
            : this(listBuilderImpl, objectBuilder, uniqueRandomGenerator, amount, 0, listBuilderImpl.Capacity)
        {
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
                int index = uniqueRandomGenerator.Generate(Start, End - 1);
                AddItemToMaster(myList[i], masterList, index);
            }
        }

        public override int NumberOfAffectedItems
        {
            get { return amount; }
        }
    }
}