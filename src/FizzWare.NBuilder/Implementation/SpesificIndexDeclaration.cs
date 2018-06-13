using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder.Implementation
{
    public class SpesificIndexDeclaration<T> : Declaration<T>
    {
        private readonly int[] _indexes;

        public SpesificIndexDeclaration(IListBuilderImpl<T> listBuilderImpl, IObjectBuilder<T> objectBuilder, params int[] indexes) 
            : base(listBuilderImpl, objectBuilder)
        {
            _indexes = indexes ?? new int[0];
        }

        public override void Construct()
        {
            for (int i = 0; i < _indexes.Length; i++)
            {
                myList.Add(objectBuilder.Construct(i));
            }
        }

        public override void AddToMaster(T[] masterList)
        {
            for (int i = 0; i < _indexes.Length; i++)
            {
                AddItemToMaster(myList[i], masterList, _indexes[i]);
            }
        }

        /// <summary>
        /// Return length of given index array
        /// </summary>
        public override int NumberOfAffectedItems => _indexes.Length;

        /// <summary>
        /// Return minimum value of given indexes
        /// </summary>
        public override int Start => _indexes.Min();
        /// <summary>
        /// Return maximum value of given indexes
        /// </summary>
        public override int End => _indexes.Max();
    }
}