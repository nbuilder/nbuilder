using System;
using System.Collections.Generic;
using System.Text;
using FizzWare.NBuilder.Implementation;

namespace FizzWare.NBuilder.Implementation
{
    public class ReflectionUtil : IReflectionUtil
    {
        public T CreateInstanceOf<T>()
        {
            try
            {
                return Activator.CreateInstance<T>();
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

            var constructors = type.GetConstructors();

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