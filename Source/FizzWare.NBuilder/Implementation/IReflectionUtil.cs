using System;

namespace FizzWare.NBuilder.Implementation
{
    public interface IReflectionUtil
    {
        T CreateInstanceOf<T>();
        T CreateInstanceOf<T>(params object[] args);
        bool RequiresConstructorArgs(Type type);
        bool IsDefaultValue(object value);
    }
}