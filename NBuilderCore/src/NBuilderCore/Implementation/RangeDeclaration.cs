
namespace NBuilderCore.Implementation
{
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