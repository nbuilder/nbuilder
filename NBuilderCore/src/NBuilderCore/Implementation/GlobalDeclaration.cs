


namespace NBuilderCore.Implementation
{
    public class GlobalDeclaration<T> : Declaration<T>, IGlobalDeclaration<T>
    {
        public GlobalDeclaration(IListBuilderImpl<T> listBuilderImpl, IObjectBuilder<T> objectBuilder)
            : base(listBuilderImpl, objectBuilder)
        {
        }

        public override void Construct()
        {
            for (int i = 0; i < listBuilderImpl.Capacity; i++)
            {
                myList.Add(objectBuilder.Construct(i));
            }
        }

        public override void AddToMaster(T[] masterList)
        {
            for (int i = 0; i < listBuilderImpl.Capacity; i++)
            {
                AddItemToMaster(myList[i], masterList, i);
            }
        }

        public override int NumberOfAffectedItems
        {
            get { return listBuilderImpl.Capacity; }
        }

        public override int Start
        {
            get { return 0; }
        }

        public override int End
        {
            get { return listBuilderImpl.Capacity - 1; }
        }
    }
}