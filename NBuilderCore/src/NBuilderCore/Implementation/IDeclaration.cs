using System.Collections.Generic;

namespace NBuilderCore.Implementation
{
    public interface IDeclaration<T>
    {
        void Construct();
        void CallFunctions(IList<T> masterList);
        void AddToMaster(T[] masterList);
        int NumberOfAffectedItems { get; }
        IList<int> MasterListAffectedIndexes { get; }

        int Start { get; }
        int End { get; }
        IListBuilderImpl<T> ListBuilderImpl { get; }

        IObjectBuilder<T> ObjectBuilder { get; }
    }
}