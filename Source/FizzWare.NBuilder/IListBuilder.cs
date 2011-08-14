using System;
using System.Collections.Generic;

namespace FizzWare.NBuilder
{
    public interface IListBuilder<T> : IBuildable<IList<T>>
    {
        IOperable<T> All();

        #if DO_NOT_OBSOLETE_OLD_SYNTAX
        [Obsolete(Messages.NewSyntax_UseAll)]
        #endif
        IOperable<T> WhereAll();
    }
}