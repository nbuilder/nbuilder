using System;

namespace FizzWare.NBuilder
{
    public class ReflectionUtil
    {
        public static T CreateInstanceOf<T>()
        {
            return Activator.CreateInstance<T>();
        }

        public static T CreateInstanceOf<T>(params object[] args)
        {
            var obj = Activator.CreateInstance(typeof (T), args);
            return (T) obj;
        }
    }
}