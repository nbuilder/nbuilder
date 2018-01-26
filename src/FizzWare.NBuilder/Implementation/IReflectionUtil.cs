using System;
using System.Reflection;

namespace FizzWare.NBuilder.Implementation
{
    public interface IReflectionUtil
    {
        T CreateInstanceOf<T>();
        T CreateInstanceOf<T>(params object[] args);
        T CreateInstanceOfValueTuple<T>();
        bool RequiresConstructorArgs(Type type);
        bool IsDefaultValue(object value);
    }
}