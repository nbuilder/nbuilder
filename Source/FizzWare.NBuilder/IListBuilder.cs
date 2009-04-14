using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder
{
    public interface IListBuilder<T> : IBuildable<IList<T>>
    {
        IOperable<T> WhereAll();
    }
}