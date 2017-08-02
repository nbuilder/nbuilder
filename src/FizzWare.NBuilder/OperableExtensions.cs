using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder
{
    public static class OperableExtensions
    {
        /// <summary>
        /// Sets the value of one of the type's public properties
        /// </summary>
        public static IOperable<T> With<T, TFunc>(this IOperable<T> operable, Func<T, TFunc> func)
        {
            var declaration = GetDeclaration(operable);

            declaration.ObjectBuilder.With(func);
            return (IOperable<T>)declaration;
        }

        /// <summary>
        /// Sets the value of one of the type's public properties and provides the index of the object being set
        /// </summary>
        public static IOperable<T> With<T>(this IOperable<T> operable, Action<T, int> func)
        {
            var declaration = GetDeclaration(operable);

            declaration.ObjectBuilder.With(func);
            return (IOperable<T>)declaration;
        }

        /// <summary>
        /// Sets the value of one of the type's private properties or readonly fields
        /// </summary>
        public static IOperable<T> With<T, TProperty>(this IOperable<T> operable, Expression<Func<T, TProperty>> property, TProperty value)
        {
            var declaration = GetDeclaration(operable);
            
            declaration.ObjectBuilder.With(property, value);
            return (IOperable<T>)declaration;
        }

        /// <summary>
        /// Sets the value of one of the type's public properties
        /// </summary>
        public static IOperable<T> And<T, TFunc>(this IOperable<T> operable, Func<T, TFunc> func)
        {
            return With(operable, func);
        }

        /// <summary>
        /// Sets the value of one of the type's public properties and provides the index of the object being set
        /// </summary>
        public static IOperable<T> And<T>(this IOperable<T> operable, Action<T, int> func)
        {
            return With(operable, func);
        }

        /// <summary>
        /// Sets the value of one of the type's private properties or readonly fields
        /// </summary>
        public static IOperable<T> And<T, TProperty>(this IOperable<T> operable, Expression<Func<T, TProperty>> property, TProperty value)
        {
            return With(operable, property, value);
        }
        

        /// <summary>
        /// Performs an action on the type.
        /// </summary>
        public static IOperable<T> And<T>(this IOperable<T> operable, Action<T> action)
        {
            return Do(operable, action);
        }

        /// <summary>
        /// Specify the constructor for the type like this:
        /// 
        /// WithConstructor( () => new MyType(arg1, arg2) )
        /// </summary>
        public static IOperable<T> WithConstructor<T>(this IOperable<T> operable, Expression<Func<T>> constructor)
        {
            var declaration = GetDeclaration(operable);
            declaration.ObjectBuilder.WithConstructor(constructor);
            return (IOperable<T>)declaration;
        }

        /// <summary>
        /// Specify the constructor for the type like this:
        /// 
        /// WithConstructor( () => new MyType(arg1, arg2) )
        /// </summary>
        public static IOperable<T> WithConstructor<T>(this IOperable<T> operable, Expression<Func<int, T>> constructor)
        {
            var declaration = GetDeclaration(operable);
            declaration.ObjectBuilder.WithConstructor(constructor);
            return (IOperable<T>)declaration;
        }


        /// <summary>
        /// Performs an action on the object.
        /// </summary>
        public static IOperable<T> Do<T>(this IOperable<T> operable, Action<T> action)
        {
            var declaration = GetDeclaration(operable);
            declaration.ObjectBuilder.Do(action);
            return (IOperable<T>)declaration;
        }

        /// <summary>
        /// Performs an action on the object.
        /// </summary>
        public static IOperable<T> Do<T>(this IOperable<T> operable, Action<T, int> action)
        {
            var declaration = GetDeclaration(operable);
            declaration.ObjectBuilder.Do(action);
            return (IOperable<T>)declaration;
        }


        /// <summary>
        /// Performs an action for each item in a list.
        /// </summary>
        public static IOperable<T> DoForEach<T, U>(this IOperable<T> operable, Action<T, U> action, IList<U> list)
        {
            var declaration = GetDeclaration(operable);
            declaration.ObjectBuilder.DoMultiple(action, list);
            return (IOperable<T>)declaration;
        }


        private static IDeclaration<T> GetDeclaration<T>(IOperable<T> operable)
        {
            var declaration = operable as IDeclaration<T>;

            if (declaration == null)
                throw new ArgumentException("Must be of type IDeclaration<T>");

            return declaration;
        }
    }
}