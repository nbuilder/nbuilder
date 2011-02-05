using System;
using System.Collections.Generic;

namespace FizzWare.NBuilder
{
    public interface IListBuilder<T> : IBuildable<IList<T>>
    {
        IOperable<T> All();

        [Obsolete(Messages.NewSyntax_UseAll)]
        IOperable<T> WhereAll();
    }
}