using System.Collections.Generic;

namespace FizzWare.NBuilder.PropertyValueNaming
{
    public interface IPropertyValueNamingStategy<T>
    {
        void SetValues(IList<T> obj);
        void SetValue(T obj);
    }
}