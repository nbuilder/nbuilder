using System;
using System.Collections.Generic;

namespace FizzWare.NBuilder
{
    public interface IListBuilder<T> : IBuildable<IList<T>>
    {
        IOperable<T> All();
    }
}