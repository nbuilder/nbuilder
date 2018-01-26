using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FizzWare.NBuilder.Extensions
{
    public static class TypeExtensions
    {
#if !NETSTANDARD1_6
        // added for api compatibility with .net standard

        //public static MethodInfo GetMethodInfo(this MulticastDelegate d)
        //{
        //    return d.Method;
        //}

        public static MethodInfo GetMethodInfo(this Delegate d)
        {
            return d.Method;
        }

        public static Type GetTypeInfo(this Type t)
        {
            return t; 
        }
#else
#endif

        public static Type GetTypeWithoutNullability(this Type t)
        {
            return t.GetTypeInfo().IsGenericType &&
                   t.GetGenericTypeDefinition() == typeof(Nullable<>)
                       ? t.GetTypeInfo().GetGenericArguments().Single()
                       : t;
        }

        public static IList<MemberInfo> GetPublicInstancePropertiesAndFields(this Type t)
        {
            var memberInfos = new List<MemberInfo>();
            memberInfos.AddRange(t.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance));
            memberInfos.AddRange(t.GetTypeInfo().GetFields());
            return memberInfos;
        }

        public static bool IsAbstract(this Type self)
        {
            return self.GetTypeInfo().IsAbstract;
        }

        public static bool IsGenericType(this Type self)
        {
            return self.GetTypeInfo().IsGenericType;
        }

        public static bool IsInterface(this Type self)
        {
            return self.GetTypeInfo().IsInterface;
        }

        public static bool IsEnum(this Type self)
        {
            return self.GetTypeInfo().IsEnum;
        }

        public static bool IsValueType(this Type self)
        {
            return self.GetTypeInfo().IsValueType;
        }

        public static ConstructorInfo[] GetConstructors(this Type self)
        {
            return self.GetTypeInfo().GetConstructors();
        }

        public static ConstructorInfo[] GetConstructors(this Type self, BindingFlags flags)
        {
            return self.GetTypeInfo().GetConstructors(flags);
        }

        public static FieldInfo[] GetFields(this Type self)
        {
            return self.GetTypeInfo().GetFields();
        }


        public static FieldInfo[] GetFields(this Type self, BindingFlags flags)
        {
            return self.GetTypeInfo().GetFields(flags);
        }


        public static PropertyInfo[] GetProperties(this Type self)
        {
            return self.GetTypeInfo().GetProperties();
        }

        public static PropertyInfo[] GetProperties(this Type self, BindingFlags flags)
        {
            return self.GetTypeInfo().GetProperties(flags);
        }

        public static Type BaseType(this Type self)
        {
            return self.GetTypeInfo().BaseType;
        }

        public static Type[] GenericTypeArguments(this Type self)
        {
            Type[] argsTypes = null;
            
#if NET35
            argsTypes =self.GenericTypeArguments;
#else
            argsTypes = self.GetTypeInfo().GetGenericArguments();
#endif
            return argsTypes ?? new Type[0];
        }

        public static bool IsValueTuple(this Type self)
        {
            return self.IsValueType() && self.GenericTypeArguments().Any() &&
                   (self.FullName?.Contains($"{nameof(System)}.Value{nameof(Tuple)}") ?? false);
        }
    }
}
