using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder
{
    public static class SingleObjectBuilderExtensions
    {
        public static ISingleObjectBuilder<T> WithConstructorArgs<T>(this ISingleObjectBuilder<T> objectBuilder, params object[] args)
        {
            ((IObjectBuilder<T>)objectBuilder).WithConstructorArgs(args);
            return objectBuilder;
        }

        public static ISingleObjectBuilder<T> With<T, TFunc>(this ISingleObjectBuilder<T> objectBuilder, Func<T, TFunc> func)
        {
            ((IObjectBuilder<T>)objectBuilder).With(func);
            return objectBuilder;
        }

        public static ISingleObjectBuilder<T> And<T, TFunc>(this ISingleObjectBuilder<T> objectBuilder, Func<T, TFunc> func)
        {
            return With(objectBuilder, func);
        }

        public static ISingleObjectBuilder<T> Do<T>(this ISingleObjectBuilder<T> objectBuilder, Action<T> func)
        {
            ((IObjectBuilder<T>)objectBuilder).Do(func);
            return objectBuilder;
        }

        //public static IOperable<T> HaveDoneToThemForAll<T, U>(this IOperable<T> operable, Action<T, U> action, IList<U> list)
        public static ISingleObjectBuilder<T> DoForAll<T, U>(this ISingleObjectBuilder<T> objectBuilder, Action<T, U> func, IList<U> list)
        {
            ((IObjectBuilder<T>) objectBuilder).DoMultiple(func, list);
            return objectBuilder;
        }

        public static ISingleObjectBuilder<T> And<T>(this ISingleObjectBuilder<T> objectBuilder, Action<T> func)
        {
            return Do(objectBuilder, func);
        }
    }
}