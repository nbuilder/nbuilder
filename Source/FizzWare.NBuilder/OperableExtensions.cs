using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder
{
    public static class OperableExtensions
    {
        private static IDeclaration<T> GetDeclaration<T>(IOperable<T> operable)
        {
            var declaration = operable as IDeclaration<T>;

            if (declaration == null)
                throw new ArgumentException("Must be of type IDeclaration<T>");

            return declaration;
        }

        public static IOperable<T> Have<T, TFunc>(this IOperable<T> operable, Func<T, TFunc> func)
        {
            var declaration = GetDeclaration(operable);

            declaration.ObjectBuilder.With(func);
            return (IOperable<T>)declaration;
        }

        public static IOperable<T> And<T, TFunc>(this IOperable<T> operable, Func<T, TFunc> func)
        {
            return Have(operable, func);
        }

        public static IOperable<T> Has<T, TFunc>(this IOperable<T> operable, Func<T, TFunc> func)
        {
            return Have(operable, func);
        }

        public static IOperable<T> And<T>(this IOperable<T> operable, Action<T> action)
        {
            return HaveDoneToThem(operable, action);
        }
        
        public static IOperable<T> AreConstructedUsing<T>(this IOperable<T> operable, Expression<Func<T>> constructor)
        {
            var declaration = GetDeclaration(operable);
            declaration.ObjectBuilder.WithConstructor(constructor);
            return (IOperable<T>)declaration;
        }

        public static IOperable<T> IsConstructedUsing<T>(this IOperable<T> operable, Expression<Func<T>> constructor)
        {
            return AreConstructedUsing(operable, constructor);
        }

        [Obsolete("Use AreConstructedWith(Expression<Func<T>> constructor) instead")]
        public static IOperable<T> AreConstructedWith<T>(this IOperable<T> operable, params object[] args)
        {
            var declaration = GetDeclaration(operable);
            declaration.ObjectBuilder.WithConstructorArgs(args);
            return (IOperable<T>)declaration;
        }

        [Obsolete("Use IsConstructedWith(Expression<Func<T>> constructor) instead")]
        public static IOperable<T> IsConstructedWith<T>(this IOperable<T> operable, params object[] args)
        {
            return AreConstructedWith(operable, args);
        }

        public static IOperable<T> HaveDoneToThem<T>(this IOperable<T> operable, Action<T> action)
        {
            var declaration = GetDeclaration(operable);
            declaration.ObjectBuilder.Do(action);
            return (IOperable<T>)declaration;
        }

        public static IOperable<T> HasDoneToIt<T>(this IOperable<T> operable, Action<T> action)
        {
            return HaveDoneToThem(operable, action);
        }

        public static IOperable<T> HaveDoneToThemForAll<T, U>(this IOperable<T> operable, Action<T, U> action, IList<U> list)
        {
            var declaration = GetDeclaration(operable);
            declaration.ObjectBuilder.DoMultiple(action, list);
            return (IOperable<T>)declaration;
        }

        public static IOperable<T> HasDoneToItForAll<T, U>(this IOperable<T> operable, Action<T, U> action, IList<U> list)
        {
            return HaveDoneToThemForAll(operable, action, list);
        }
    }
}