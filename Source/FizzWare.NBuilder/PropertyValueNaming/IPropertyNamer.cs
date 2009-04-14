using System.Collections.Generic;

namespace FizzWare.NBuilder.PropertyNaming
{
    public interface IPropertyNamer<T>
    {
        void SetValuesOfAllIn(IList<T> obj);
        void SetValuesOf(T obj);
        void SetValuesOf(T obj, int sequenceNumber, string sequenceIdentifier);
    }
}