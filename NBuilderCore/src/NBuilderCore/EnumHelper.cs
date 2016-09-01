using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NBuilderCore
{
    public static class EnumHelper
    {
        // NB: This can't use Enum.GetValues() because it's not available in Silverlight
        public static T[] GetValues<T>()
        {
            Type enumType = typeof(T);

            if (!enumType.GetTypeInfo().IsEnum)
            {
                throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
            }

            List<T> values = new List<T>();

            var fields = from field in enumType.GetFields()
                         where field.IsLiteral
                         select field;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(enumType);
                values.Add((T)value);
            }

            return values.ToArray();
        }

        public static object[] GetValues(Type enumType)
        {
            if (!enumType.GetTypeInfo().IsEnum)
            {
                throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
            }

            List<object> values = new List<object>();

            var fields = from field in enumType.GetFields()
                         where field.IsLiteral
                         select field;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(enumType);
                values.Add(value);
            }

            return values.ToArray();
        }
    }
}