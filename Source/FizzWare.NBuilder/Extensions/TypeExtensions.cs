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
            return t.IsGenericType &&
                   t.GetGenericTypeDefinition() == typeof(Nullable<>)
                       ? t.GetGenericArguments().Single()
                       : t;
        }

        public static IList<MemberInfo> GetPublicInstancePropertiesAndFields(this Type t)
        {
            var memberInfos = new List<MemberInfo>();
            memberInfos.AddRange(t.GetProperties(BindingFlags.Public | BindingFlags.Instance));
            memberInfos.AddRange(t.GetFields());
            return memberInfos;
        }
    }
}
