using System;
using System.Collections.Generic;

namespace FizzWare.NBuilder
{
    public interface IListBuilder<T> : IBuildable<IList<T>>
    {
        int Length { get; }

        IOperable<T> All();
    }
}