using System;
using System.Reflection;

namespace FizzWare.NBuilder.Extensions
{
    public static class MemberInfoExtensions
    {
        public static Type GetFieldOrPropertyType(this MemberInfo m)
        {
            if (m is FieldInfo)
            {
                return ((FieldInfo)m).FieldType;
            }

            return ((PropertyInfo)m).PropertyType;
        }

        public static object GetFieldOrPropertyValue<T>(this MemberInfo m, T instance)
        {
            if (m is FieldInfo)
            {
                return ((FieldInfo)m).GetValue(instance);
            }

            return ((PropertyInfo)m).GetValue(instance, null);
        }

        public static void SetFieldOrPropertyValue<T>(this MemberInfo m, T instance, object value)
        {
            if (m is FieldInfo)
            {
                ((FieldInfo)m).SetValue(instance, value);
            }
            else if (m is PropertyInfo)
            {
                if (((PropertyInfo)m).CanWrite)
                {
                    ((PropertyInfo)m).SetValue(instance, value, null);
                }
            }
        }
    }
}
