using System.Collections.Generic;

namespace FizzWare.NBuilder.Implementation
{
    class DeclarationComparer<T> : IComparer<IDeclaration<T>>
    {
        public int Compare(IDeclaration<T> x, IDeclaration<T> y)
        {
            if (y is IGlobalDeclaration<T>)
                return 1;

            if (x is IGlobalDeclaration<T>)
                return -1;

            if (x is RangeDeclaration<T> && y is RangeDeclaration<T>)
            {
                return x.Start - y.Start;
            }

            return 0; // (both are global declarations)
        }
    }
}