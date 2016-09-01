using System.Collections.Generic;

namespace NBuilderCore
{
    public interface IListBuilder<T> : IBuildable<IList<T>>
    {
        IOperable<T> All();
    }
}