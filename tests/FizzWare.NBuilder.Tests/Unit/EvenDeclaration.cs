using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder.Tests.Unit.Extensibility
{
    public class EvenDeclaration<T> : Declaration<T>
    {
        public EvenDeclaration(IListBuilderImpl<T> listBuilderImpl, IObjectBuilder<T> objectBuilder) 
            : base(listBuilderImpl, objectBuilder)
        {
        }

        public override int NumberOfAffectedItems
        {
            get
            {
                return listBuilderImpl.Capacity/2;
            }
        }

        public override int Start
        {
            get { return 0; }
        }

        public override int End
        {
            get { return listBuilderImpl.Capacity - 1; }
        }

        public override void Construct()
        {
            for (int i = 0; i < NumberOfAffectedItems; i++)
                myList.Add(objectBuilder.Construct(i));
        }

        public override void AddToMaster(T[] masterList)
        {
            for (int i = 0, j = 0; i < myList.Count; i++, j+=2)
            {
                this.MasterListAffectedIndexes.Add(j);
                masterList[j] = myList[i];
            }
        }
    }
}