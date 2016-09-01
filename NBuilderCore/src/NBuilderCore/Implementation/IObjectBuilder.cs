using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NBuilderCore.PropertyNaming;

namespace NBuilderCore.Implementation
{
    public interface IObjectBuilder<T> : ISingleObjectBuilder<T>
    {
        /// <summary>
        /// Specify a constructor in the form WithConstructor( () => new MyClass(arg1, arg2) )
        /// </summary>
        /// <param name="constructor"></param>
        /// <returns>An object builder</returns>
        IObjectBuilder<T> WithConstructor(Expression<Func<T>> constructor);
        IObjectBuilder<T> WithConstructor(Expression<Func<int, T>> constructor);

        // This has been obsolete for a while, so don't allow this one to be hidden
        [Obsolete("Use WithConstructor() instead")]
        IObjectBuilder<T> WithConstructorArgs(params object[] args);

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