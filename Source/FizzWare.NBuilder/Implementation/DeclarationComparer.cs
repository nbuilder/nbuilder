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

            if (!(x is RangeDeclaration<T>) || !(y is RangeDeclaration<T>)) return 0; // (both are global declarations)
            if (x.Start != y.Start) return x.Start - y.Start;

            /*
             * Adding the Created property was necessary because of an change of implementation of the
             * Sort() function at a framework level.
             * Given 2 declarations x = TheFirst(0, 5), y = TheFirst(0, 5), x should execute before y even
             * though they are identical according to their range properties.
             * To facilitate this, I added a Created property to track the moment the RangeDeclaration was created.
             * This property gives me a way to determine the original order of operations.
             */
            var xr = (RangeDeclaration<T>) x;
            var yr = (RangeDeclaration<T>) y;

            return System.DateTime.Compare(xr.Created, yr.Created);
        }
    }
}