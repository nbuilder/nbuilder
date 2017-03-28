using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FizzWare.NBuilder.Extensions
{
    public static class TypeExtensions
    {
        public static Type GetTypeWithoutNullability(this Type t)
        {
            return t.GetInfo().IsGenericType &&
                   t.GetGenericTypeDefinition() == typeof(Nullable<>)
                       ? t.GetInfo().GetGenericArguments().Single()
                       : t;
        }

        public static IList<MemberInfo> GetPublicInstancePropertiesAndFields(this Type t)
        {
            var memberInfos = new List<MemberInfo>();
            memberInfos.AddRange(t.GetInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance));
            memberInfos.AddRange(t.GetInfo().GetFields());
            return memberInfos;
        }
        
#if NETCORE
        public static TypeInfo GetInfo(this Type type)
        {
            return type.GetTypeInfo();
        }

        public static PropertyInfo[] GetProperties(this Type type, BindingFlags flags)
        {
            return type.GetTypeInfo().GetProperties(flags);
        }

        public static FieldInfo[] GetFields(this Type type)
        {
            return type.GetTypeInfo().GetFields();
        }
#else
        public static Type GetInfo(this Type type)
        {
            return type;
        }
#endif

    }
}
