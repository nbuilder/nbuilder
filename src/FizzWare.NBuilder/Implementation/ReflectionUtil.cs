using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FizzWare.NBuilder.Extensions;

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

        public object CreateInstanceOf(Type t)
        {
            var requiresArgs = RequiresConstructorArgs(t);
            if (!requiresArgs)
            return CreateInstanceOf(t, null);

            return CreateInstanceOf(t,BuildConstructorArgs(t));
        }

        public object CreateInstanceOf(Type t, params object[] args)
        {
            try
            {
                var obj = Activator.CreateInstance(t, args);
                return obj;
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
        public T CreateInstanceOf<T>(params object[] args)
        {
            var obj = CreateInstanceOf(typeof(T),args);
            return (T)obj;
        }
        private object[] BuildConstructorArgs(Type t)
        {
            ConstructorInfo constructor = t.GetConstructors().FirstOrDefault();
            if (constructor == null)
            {
                throw new TypeCreationException($"No public constructors found for type {t.FullName}");
            }

            ParameterInfo[] parameters = constructor.GetParameters();
            object[] args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                Type paramType = parameters[i].ParameterType;
                args[i] = GetDefaultValue(paramType);
            }

            return args;
        }

        // Method to get default value for a type
        private object GetDefaultValue(Type t)
        {
            if (t.IsValueType())
            {
                return Activator.CreateInstance(t);
            }
            else
            {
                return null; // Default value for reference types
            }
        }
        public bool RequiresConstructorArgs(Type type)
        {
            if (type.IsValueType())
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