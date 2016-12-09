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
            return ReflectionHelper.GetTypeInfo(t).IsGenericType &&
                   ReflectionHelper.GetTypeInfo(t).GetGenericTypeDefinition() == typeof(Nullable<>)
                       ? ReflectionHelper.GetTypeInfo(t).GetGenericArguments().Single()
                       : t;
        }

        public static IList<MemberInfo> GetPublicInstancePropertiesAndFields(this Type t)
        {
            var memberInfos = new List<MemberInfo>();
            memberInfos.AddRange(ReflectionHelper.GetTypeInfo(t).GetProperties(BindingFlags.Public | BindingFlags.Instance));
            memberInfos.AddRange(ReflectionHelper.GetTypeInfo(t).GetFields());
            return memberInfos;
        }
    }
}
