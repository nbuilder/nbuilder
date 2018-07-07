
using System;
using System.Diagnostics;

namespace FizzWare.NBuilder.Implementation
{
    [DebuggerDisplay("Range: Start={Start}; End={End}; ({NumberOfAffectedItems} Affected Items)")]
    public class RangeDeclaration<T> : Declaration<T>
    {
        private readonly int _start;
        private readonly int _end;

        public RangeDeclaration(IListBuilderImpl<T> listBuilderImpl, IObjectBuilder<T> objectBuilder, int start, int end)
            : base(listBuilderImpl, objectBuilder)
        {
            this._start = start;
            this._end = end;
        }

        internal DateTime Created { get; } = DateTime.Now;

        public override int Start => _start;

        public override int End => _end;

        public override void Construct()
        {
            for (var i = Start; i < End + 1; i++)
                myList.Add(objectBuilder.Construct(i));
        }

        public override void AddToMaster(T[] masterList)
        {
            for (var i = Start; i < End + 1; i++)
            {
                AddItemToMaster(myList[i - Start], masterList, i);
            }
        }

        public override int NumberOfAffectedItems => (_end + 1) - _start;
    }
}