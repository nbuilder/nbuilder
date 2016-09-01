using System.Collections.Generic;

namespace NBuilderCore.PropertyNaming
{
    public interface IPropertyNamer
    {
        void SetValuesOfAllIn<T>(IList<T> obj);
        void SetValuesOf<T>(T obj);
        //void SetValuesOf<T>(T obj, int sequenceNumber, string sequenceIdentifier);
    }
}