using System;
using System.Collections.Generic;
using System.Reflection;

namespace NBuilderCore.Implementation
{
    public class ReflectionUtil : IReflectionUtil
    {
        public T CreateInstanceOf<T>()
        {
            try
            {
                // nb: Silverlight only has two overloads, one without params, one with.
                #if SILVERLIGHT
                return (T)Activator.CreateInstance(typeof(T));
                #endif

                // nb: The non-silverlight version allows creation of a class without a public constructor.
                //     Think someone submitted this as a patch. I didn't like it then, and don't like it now.
                //     It was always in the back of my mind that NBuilder wouldn't mess around with your classes in ways that
                //     weren't possible doing things without reflection.
                //
                //     Plus, now the SL and .NET versions have differences.
                #if !SILVERLIGHT
                return (T)Activator.CreateInstance(typeof(T), true);
                #endif

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
            if (type.GetTypeInfo().IsValueType)
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