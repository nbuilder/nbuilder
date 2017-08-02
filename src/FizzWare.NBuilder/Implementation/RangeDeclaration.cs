
using System;
using System.Diagnostics;

namespace FizzWare.NBuilder.Implementation
{
    [DebuggerDisplay("Range: Start={Start}; End={End}; ({NumberOfAffectedItems} Affected Items)")]
    public class RangeDeclaration<T> : Declaration<T>
    {
        private readonly int start;
        private readonly int end;

        public RangeDeclaration(IListBuilderImpl<T> listBuilderImpl, IObjectBuilder<T> objectBuilder, int start, int end)
            : base(listBuilderImpl, objectBuilder)
        {
            this.start = start;
            this.end = end;
        }

        internal DateTime Created { get; } = DateTime.Now;

        public override int Start
        {
            get { return start; }
        }

        public override int End
        {
            get { return end; }
        }

        public override void Construct()
        {
            for (int i = Start; i < End + 1; i++)
                myList.Add(objectBuilder.Construct(i));
        }

        public override void AddToMaster(T[] masterList)
        {
            for (int i = Start; i < End + 1; i++)
            {
                AddItemToMaster(myList[i - Start], masterList, i);
            }
        }

        public override int NumberOfAffectedItems
        {
            get { return (end + 1) - start; }
        }
    }
}