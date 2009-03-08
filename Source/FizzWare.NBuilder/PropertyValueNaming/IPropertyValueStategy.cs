using System.Collections.Generic;

namespace FizzWare.NBuilder.PropertyValueNaming
{
    public interface IPropertyValueNamingStategy<T>
    {
        void SetValuesOfAll(IList<T> obj);
        void SetValuesOf(T obj);
    }
}