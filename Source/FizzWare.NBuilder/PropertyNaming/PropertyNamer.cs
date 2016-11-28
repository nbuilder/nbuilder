using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using FizzWare.NBuilder.Implementation;
using System.Linq;

namespace FizzWare.NBuilder.PropertyNaming
{
    public abstract class PropertyNamer : IPropertyNamer
    {
        protected readonly IReflectionUtil ReflectionUtil;
        protected const BindingFlags FLAGS = (BindingFlags.Public | BindingFlags.Instance);
        private BuilderSettings BuilderSettings;
        protected PropertyNamer(IReflectionUtil reflectionUtil, BuilderSettings builderSettings)
        {
            this.ReflectionUtil = reflectionUtil;
            BuilderSettings = builderSettings;
        }

        public abstract void SetValuesOfAllIn<T>(IList<T> objects);

        public virtual void SetValuesOf<T>(T obj)
        {
            var type = typeof(T);

            foreach (var propertyInfo in type.GetProperties(FLAGS).Where(p => p.CanWrite))
                SetMemberValue(propertyInfo, obj);

            foreach (var propertyInfo in type.GetFields().Where(f => !f.IsLiteral))
                SetMemberValue(propertyInfo, obj);
        }

        protected static object GetCurrentValue<T>(MemberInfo memberInfo, T obj)
        {
            object currentValue = null;

            if (memberInfo is FieldInfo)
            {
                currentValue = ((FieldInfo)memberInfo).GetValue(obj);
            }

            if (memberInfo is PropertyInfo)
            {
                try
                {
                    if (((PropertyInfo)memberInfo).GetGetMethod() != null)
                    {
                        currentValue = ((PropertyInfo) memberInfo).GetValue(obj, null);
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

        protected static Type GetMemberType(MemberInfo memberInfo)
        {
            Type type = null;

            if (memberInfo is FieldInfo)
            {
                type = ((FieldInfo)memberInfo).FieldType;
            }

            if (memberInfo is PropertyInfo)
            {
                type = ((PropertyInfo)memberInfo).PropertyType;
            }

			if (type != null && IsNullableType(type))
			{
				type = Nullable.GetUnderlyingType(type);
			}

            return type;
        }

		private static bool IsNullableType(Type type)
		{
			return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>));
		}

    	protected virtual void SetValue<T>(MemberInfo memberInfo, T obj, object value)
        {
            if (value != null)
            {
                if (memberInfo is FieldInfo)
                    ((FieldInfo)memberInfo).SetValue(obj, value);

                if (memberInfo is PropertyInfo)
                {
                    if (((PropertyInfo)memberInfo).CanWrite)
                        ((PropertyInfo)memberInfo).SetValue(obj, value, null);
                }
            }
        }

        protected virtual void HandleUnknownType<T>(Type memberType, MemberInfo memberInfo, T obj)
        {
        }

        protected abstract short GetInt16(MemberInfo memberInfo);
        protected abstract int GetInt32(MemberInfo memberInfo);
        protected abstract long GetInt64(MemberInfo memberInfo);
        protected abstract decimal GetDecimal(MemberInfo memberInfo);
        protected abstract float GetSingle(MemberInfo memberInfo);
        protected abstract double GetDouble(MemberInfo memberInfo);
        protected abstract ushort GetUInt16(MemberInfo memberInfo);
        protected abstract uint GetUInt32(MemberInfo memberInfo);
        protected abstract ulong GetUInt64(MemberInfo memberInfo);
        protected abstract sbyte GetSByte(MemberInfo memberInfo);
        protected abstract byte GetByte(MemberInfo memberInfo);
        protected abstract DateTime GetDateTime(MemberInfo memberInfo);
        protected abstract string GetString(MemberInfo memberInfo);
        protected abstract bool GetBoolean(MemberInfo memberInfo);
        protected abstract char GetChar(MemberInfo memberInfo);
        protected abstract Enum GetEnum(MemberInfo memberInfo);
        protected abstract Guid GetGuid(MemberInfo memberInfo);
        protected abstract TimeSpan GetTimeSpan(MemberInfo memberInfo);

        protected virtual bool ShouldIgnore(MemberInfo memberInfo)
        {
            if (memberInfo is PropertyInfo)
                if (BuilderSettings.ShouldIgnoreProperty(((PropertyInfo) memberInfo)))
                    return true;

            return false;
        }

        protected virtual void SetMemberValue<T>(MemberInfo memberInfo, T obj)
        {
            Type type = GetMemberType(memberInfo);

            if (BuilderSettings.HasDisabledAutoNameProperties && ShouldIgnore(memberInfo))
                return;

            object currentValue = GetCurrentValue(memberInfo, obj);

            if (!ReflectionUtil.IsDefaultValue(currentValue))
                return;

            object value = null;

            if (type == typeof(short))
            {
                value = GetInt16(memberInfo);
            }

            else if (type == typeof(int))
            {
                value = GetInt32(memberInfo);
            }

            else if (type == typeof(long))
            {
                value = GetInt64(memberInfo);
            }

            else if (type == typeof(decimal))
            {
                value = GetDecimal(memberInfo);
            }

            else if (type == typeof(float))
            {
                value = GetSingle(memberInfo);
            }

            else if (type == typeof(double))
            {
                value = GetDouble(memberInfo);
            }

            else if (type == typeof(ushort))
            {
                value = GetUInt16(memberInfo);
            }

            else if (type == typeof(uint))
            {
                value = GetUInt32(memberInfo);
            }

            else if (type == typeof(ulong))
            {
                value = GetUInt64(memberInfo);
            }

            else if (type == typeof(char))
            {
                value = GetChar(memberInfo);
            }

            else if (type == typeof(byte))
            {
                value = GetByte(memberInfo);
            }

            else if (type == typeof(sbyte))
            {
                value = GetSByte(memberInfo);
            }

            else if (type == typeof(DateTime))
            {
                value = GetDateTime(memberInfo);
            }

            else if (type == typeof(string))
            {
                value = GetString(memberInfo);
            }

            else if (type == typeof(bool))
            {
                value = GetBoolean(memberInfo);
            }

			else if (type.BaseType == typeof(Enum))
			{
				value = GetEnum(memberInfo);
			}

            else if (type == typeof(Guid))
            {
                value = GetGuid(memberInfo);
            }

            else if (type == typeof(TimeSpan))
            {
                value = GetTimeSpan(memberInfo);
            }

            else
            {
                HandleUnknownType(type, memberInfo, obj);
            }

            SetValue(memberInfo, obj, value);
        }

        protected static Array GetEnumValues(Type enumType)
        {
            var enumArray = EnumHelper.GetValues(enumType);
            return enumArray;
        }
    }
}