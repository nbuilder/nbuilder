using System;
using System.Diagnostics;
using System.Reflection;

namespace NBuilderCore.Extensions
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

        public static object GetFieldOrPropertyValue<T>(this MemberInfo memberInfo, T instance)
        {
            object currentValue = null;

            if (memberInfo is FieldInfo)
            {
                currentValue = ((FieldInfo)memberInfo).GetValue(instance);
            }

            if (memberInfo is PropertyInfo)
            {
                try
                {
                    if (((PropertyInfo)memberInfo).GetGetMethod() != null)
                    {
                        currentValue = ((PropertyInfo)memberInfo).GetValue(instance, null);
                    }
                }
                catch (Exception)
                {
                    
                    #if !SILVERLIGHT
                    Trace.WriteLine(string.Format("NBuilder warning: {0} threw an exception when attempting to read its current value", memberInfo.Name));
                    #endif
                }
            }

            return currentValue;
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
