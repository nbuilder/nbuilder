using System;
using System.Collections.Generic;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder.Implementation
{
    public interface IObjectBuilder<T> : ISingleObjectBuilder<T>
    {
        IObjectBuilder<T> WithConstructorArgs(params object[] args);
        IObjectBuilder<T> With<TFunc>(Func<T, TFunc> func);
        IObjectBuilder<T> Do(Action<T> action);
        IObjectBuilder<T> DoMultiple<U>(Action<T, U> action, IList<U> list);
        IObjectBuilder<T> WithPropertyNamer(IPropertyNamer thePropertyNamer);
        void CallFunctions(T obj);
        T Construct();
        T Name(T obj);
    }
}