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

        [Obsolete(Messages.NewSyntax_UseWith)]
        public static IOperable<T> Have<T, TFunc>(this IOperable<T> operable, Func<T, TFunc> func)
        {
            return With(operable, func);
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
        /// Sets the value of one of the type's private properties or readonly fields
        /// </summary>
        [Obsolete(Messages.NewSyntax_UseWith)]
        public static IOperable<T> Have<T, TProperty>(this IOperable<T> operable, Expression<Func<T, TProperty>> property, TProperty value)
        {
            return With(operable, property, value);
        }

        /// <summary>
        /// Sets the value of one of the type's public properties
        /// </summary>
        public static IOperable<T> And<T, TFunc>(this IOperable<T> operable, Func<T, TFunc> func)
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
        /// Sets the value of one of the type's public properties
        /// </summary>
        [Obsolete(Messages.NewSyntax_UseWith)]
        public static IOperable<T> Has<T, TFunc>(this IOperable<T> operable, Func<T, TFunc> func)
        {
            return With(operable, func);
        }

        /// <summary>
        /// Sets the value of one of the type's private properties or readonly fields
        /// </summary>
        [Obsolete(Messages.NewSyntax_UseWith)]
        public static IOperable<T> Has<T, TProperty>(this IOperable<T> operable, Expression<Func<T, TProperty>> property, TProperty value)
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
        /// AreConstructedUsing( () => new MyType(arg1, arg2) )
        /// </summary>
        public static IOperable<T> AreConstructedUsing<T>(this IOperable<T> operable, Expression<Func<T>> constructor)
        {
            var declaration = GetDeclaration(operable);
            declaration.ObjectBuilder.WithConstructor(constructor);
            return (IOperable<T>)declaration;
        }

        /// <summary>
        /// Specify the constructor for the type like this:
        /// 
        /// AreConstructedUsing( () => new MyType(arg1, arg2) )
        /// </summary>
        public static IOperable<T> IsConstructedUsing<T>(this IOperable<T> operable, Expression<Func<T>> constructor)
        {
            return AreConstructedUsing(operable, constructor);
        }

        [Obsolete("Use AreConstructedUsing(Expression<Func<T>> constructor) instead")]
        public static IOperable<T> AreConstructedWith<T>(this IOperable<T> operable, params object[] args)
        {
            var declaration = GetDeclaration(operable);
            declaration.ObjectBuilder.WithConstructorArgs(args);
            return (IOperable<T>)declaration;
        }

        [Obsolete("Use IsConstructedUsing(Expression<Func<T>> constructor) instead")]
        public static IOperable<T> IsConstructedWith<T>(this IOperable<T> operable, params object[] args)
        {
            return AreConstructedWith(operable, args);
        }

        /// <summary>
        /// Performs an action on the type.
        /// </summary>
        public static IOperable<T> Do<T>(this IOperable<T> operable, Action<T> action)
        {
            var declaration = GetDeclaration(operable);
            declaration.ObjectBuilder.Do(action);
            return (IOperable<T>)declaration;
        }

        [Obsolete(Messages.NewSyntax_UseDo)]
        public static IOperable<T> HaveDoneToThem<T>(this IOperable<T> operable, Action<T> action)
        {
            return Do(operable, action);
        }

        /// <summary>
        /// Performs an action on the type.
        /// </summary>
        [Obsolete(Messages.NewSyntax_UseWith)]
        public static IOperable<T> HasDoneToIt<T>(this IOperable<T> operable, Action<T> action)
        {
            return Do(operable, action);
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

        [Obsolete(Messages.NewSyntax_UseDoForEach)]
        public static IOperable<T> HaveDoneToThemForAll<T, U>(this IOperable<T> operable, Action<T, U> action, IList<U> list)
        {
            return DoForEach(operable, action, list);
        }

        /// <summary>
        /// Performs an action for each item in a list.
        /// </summary>
        [Obsolete(Messages.NewSyntax_UseDoForEach)]
        public static IOperable<T> HasDoneToItForAll<T, U>(this IOperable<T> operable, Action<T, U> action, IList<U> list)
        {
            return DoForEach(operable, action, list);
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