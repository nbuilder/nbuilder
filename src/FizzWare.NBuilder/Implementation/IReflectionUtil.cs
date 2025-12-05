using System;
using System.Reflection;

namespace FizzWare.NBuilder.Implementation
{
    public interface IReflectionUtil
    {
        T CreateInstanceOf<T>();
        T CreateInstanceOf<T>(params object[] args);
        /// <summary>
        /// Creates an instance of the specified type at runtime.
        /// Use this method when the type to instantiate is not known at compile time.
        /// </summary>
        /// <param name="t">The <see cref="Type"/> to instantiate.</param>
        /// <returns>An instance of the specified type.</returns>
        object CreateInstanceOf(Type t);

        /// <summary>
        /// Creates an instance of the specified type at runtime, using the provided constructor arguments.
        /// Use this method when the type to instantiate is not known at compile time and constructor arguments are required.
        /// </summary>
        /// <param name="t">The <see cref="Type"/> to instantiate.</param>
        /// <param name="args">Constructor arguments to pass to the type's constructor.</param>
        /// <returns>An instance of the specified type.</returns>
        object CreateInstanceOf(Type t, params object[] args);

        bool RequiresConstructorArgs(Type type);
        bool IsDefaultValue(object value);
    }
}