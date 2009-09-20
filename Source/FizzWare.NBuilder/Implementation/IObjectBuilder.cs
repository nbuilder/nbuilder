using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder.Implementation
{
    public interface IObjectBuilder<T> : ISingleObjectBuilder<T>
    {
        /// <summary>
        /// Specify a constructor in the form WithConstructor( () => new MyClass(arg1, arg2) )
        /// </summary>
        /// <param name="constructor"></param>
        /// <returns>An object builder</returns>
        IObjectBuilder<T> WithConstructor(Expression<Func<T>> constructor);

        [Obsolete("Use WithConstructor() instead")]
        IObjectBuilder<T> WithConstructorArgs(params object[] args);

        IObjectBuilder<T> With<TFunc>(Func<T, TFunc> func);
        IObjectBuilder<T> Do(Action<T> action);
        IObjectBuilder<T> DoMultiple<TAction>(Action<T, TAction> action, IList<TAction> list);
        IObjectBuilder<T> WithPropertyNamer(IPropertyNamer propertyNamer);
        void CallFunctions(T obj);
        T Construct();
        T Name(T obj);
    }
}