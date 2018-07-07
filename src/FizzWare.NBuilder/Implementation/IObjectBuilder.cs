using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder.Implementation
{
    public interface IObjectBuilder<T> : ISingleObjectBuilder<T>
    {
        [Obsolete("Use WithFactory instead.")]
        IObjectBuilder<T> WithConstructor(Func<T> constructor);

        [Obsolete("Use WithFactory instead")]
        IObjectBuilder<T> WithConstructor(Func<int, T> constructor);

        IObjectBuilder<T> WithFactory(Func<T> constructor);

        IObjectBuilder<T> WithFactory(Func<int, T> constructor);


        IObjectBuilder<T> With<TFunc>(Func<T, TFunc> func);
        IObjectBuilder<T> With(Action<T, int> action);
        
        IObjectBuilder<T> Do(Action<T> action);
        IObjectBuilder<T> Do(Action<T, int> action);

        IObjectBuilder<T> DoMultiple<TAction>(Action<T, TAction> action, IList<TAction> list);
        IObjectBuilder<T> WithPropertyNamer(IPropertyNamer propertyNamer);
        void CallFunctions(T obj);
        void CallFunctions(T obj, int objIndex);
        T Construct(int index);
        T Name(T obj);
    }
}