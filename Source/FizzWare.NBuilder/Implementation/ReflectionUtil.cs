using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FizzWare.NBuilder.Implementation;
using System.Runtime.Serialization;

namespace FizzWare.NBuilder.Implementation
{
    public class ReflectionUtil : IReflectionUtil
    {
        public T CreateInstanceOf<T>()
        {
            try
            {
                return (T)Activator.CreateInstance(typeof(T), true);
            }
            catch (MissingMethodException e)
            {
                throw new TypeCreationException(typeof(T).Name + " does not have a default parameterless constructor", e);
            }
        }

        public T CreateInstanceOf<T>(params object[] args)
        {
            try
            {
                var obj = Activator.CreateInstance(typeof(T), args);
                return (T)obj;
            }
            catch (MissingMethodException e)
            {
                var list = new List<string>();
                foreach (var o in args)
                    list.Add(o.GetType().Name);

                var argList = string.Join(", ", list.ToArray());

                throw new TypeCreationException("Constructor with args " + argList, e);
            }
        }

        public bool RequiresConstructorArgs(Type type)
        {
            if (type.IsValueType)
                return false;

            var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var constructorInfo in constructors)
            {
                if  (constructorInfo.GetParameters().Length == 0)
                    return false;
            }

            return true;
        }

        public bool IsDefaultValue(object value)
        {
            if (value is ValueType)
            {
                var type = value.GetType();

                var defaultValue = Activator.CreateInstance(type);

                if (value.Equals(defaultValue))
                    return true;
                else
                    return false;
            }
            else
            {
                return value == null;
            }
        }
    }
}