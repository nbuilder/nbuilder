using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder
{
    public static class SingleObjectBuilderExtensions
    {
        [Obsolete("Please use WithConstructor( () => new MyClass(arg1, arg2) )")]
        public static ISingleObjectBuilder<T> WithConstructorArgs<T>(this ISingleObjectBuilder<T> objectBuilder, params object[] args)
        {
            ((IObjectBuilder<T>)objectBuilder).WithConstructorArgs(args);
            return objectBuilder;
        }

        /// <summary>
        /// Sets the value of the constructor to be used to build the type
        /// </summary>
        public static ISingleObjectBuilder<T> WithConstructor<T>(this ISingleObjectBuilder<T> objectBuilder, Expression<Func<T>> constructor)
        {
            ((IObjectBuilder<T>)objectBuilder).WithConstructor(constructor);
            return objectBuilder;
        }

        /// <summary>
        /// Sets the value of one of the type's public properties
        /// </summary>
        public static ISingleObjectBuilder<T> With<T, TFunc>(this ISingleObjectBuilder<T> objectBuilder, Func<T, TFunc> func)
        {
            ((IObjectBuilder<T>)objectBuilder).With(func);
            return objectBuilder;
        }

        /// <summary>
        /// Sets the value of one of the type's public properties.
        /// Overloads With to provide a better syntax in some situations.
        /// </summary>
        public static ISingleObjectBuilder<T> And<T, TFunc>(this ISingleObjectBuilder<T> objectBuilder, Func<T, TFunc> func)
        {
            return With(objectBuilder, func);
        }

        /// <summary>
        /// Performs an action on the type.
        /// </summary>
        public static ISingleObjectBuilder<T> Do<T>(this ISingleObjectBuilder<T> objectBuilder, Action<T> func)
        {
            ((IObjectBuilder<T>)objectBuilder).Do(func);
            return objectBuilder;
        }

        /// <summary>
        /// Performs an action for each item in a list.
        /// </summary>
        public static ISingleObjectBuilder<T> DoForAll<T, U>(this ISingleObjectBuilder<T> objectBuilder, Action<T, U> func, IList<U> list)
        {
            ((IObjectBuilder<T>) objectBuilder).DoMultiple(func, list);
            return objectBuilder;
        }

        /// <summary>
        /// Performs an action on the type.
        /// Overloads Do to provide a better syntax in some situations.
        /// </summary>
        public static ISingleObjectBuilder<T> And<T>(this ISingleObjectBuilder<T> objectBuilder, Action<T> func)
        {
            return Do(objectBuilder, func);
        }

        /// <summary>
        /// Sets the value of one of the type's private properties
        /// </summary>
        public static ISingleObjectBuilder<T> With<T, TProperty>(this ISingleObjectBuilder<T> objectBuilder, Expression<Func<T, TProperty>> property, TProperty value)
        {
            var member = ((MemberExpression)property.Body).Member;

            if (member is FieldInfo)
            {
                return objectBuilder.Do(load => ((FieldInfo)member).SetValue(load, value));
            }
            else 
            {
                return objectBuilder.Do(load => ((PropertyInfo)member).SetValue(load, value, null));
            }
        }

        /// <summary>
        /// Sets the value of one of the type's private properties or readonly fields
        /// Overloads With to provide a better syntax in some situations.
        /// </summary>
        public static ISingleObjectBuilder<T> And<T, TProperty>(this ISingleObjectBuilder<T> objectBuilder, Expression<Func<T, TProperty>> property, TProperty value)
        {
            return With(objectBuilder, property, value);
        }
    }
}