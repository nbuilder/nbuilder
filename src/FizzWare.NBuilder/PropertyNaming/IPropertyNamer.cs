using System.Collections.Generic;

namespace FizzWare.NBuilder.PropertyNaming
{
    public interface IPropertyNamer
    {
        void SetValuesOfAllIn<T>(IList<T> obj);
        void SetValuesOf<T>(T obj);
        //void SetValuesOf<T>(T obj, int sequenceNumber, string sequenceIdentifier);
    }
}