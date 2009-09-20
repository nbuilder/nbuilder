using System;
using System.Collections.Generic;
using System.Reflection;
using FizzWare.NBuilder.Extensions;
using FizzWare.NBuilder.Generators;

namespace FizzWare.NBuilder.PropertyNaming
{
    public class ExtensibleRandomValuePropertyNamer : IPropertyNamer
    {
        protected IDictionary<Type, Delegate> handlers = new Dictionary<Type, Delegate>();

        public ExtensibleRandomValuePropertyNamer() : this(GetDefaultHandlers()) { }

        public ExtensibleRandomValuePropertyNamer(IEnumerable<Delegate> handlers)
        {
            foreach (var handler in handlers)
            {
                NameWith(handler);
            }
        }

        public ExtensibleRandomValuePropertyNamer NameWith<T>(Func<T> handler)
        {
            NameWith((Delegate)handler);
            return this;
        }

        public ExtensibleRandomValuePropertyNamer DontName<T>()
        {
            return DontName(typeof (T));
        }

        public ExtensibleRandomValuePropertyNamer DontName(Type type)
        {
            if (handlers.ContainsKey(type))
            {
                handlers.Remove(type);
            }
            return this;
        }

        public void SetValuesOfAllIn<T>(IList<T> instances)
        {
            var members = typeof(T).GetPublicInstancePropertiesAndFields();
            foreach (var instance in instances)
            {
                SetMemberValues(members, instance);
            }
        }

        public void SetValuesOf<T>(T instance)
        {
            var members = typeof(T).GetPublicInstancePropertiesAndFields();
            SetMemberValues(members, instance);
        }

        protected void SetMemberValues<T>(IEnumerable<MemberInfo> memberInfos, T instance)
        {
            foreach (var memberInfo in memberInfos)
            {
                SetMemberValue(memberInfo, instance);
            }
        }

        protected void SetMemberValue<T>(MemberInfo memberInfo, T instance)
        {
            if (memberInfo is PropertyInfo && BuilderSetup.ShouldIgnoreProperty((PropertyInfo)memberInfo))
            {
                return;
            }
            var currentValue = memberInfo.GetFieldOrPropertyValue(instance);
            if (!currentValue.IsDefaultValue())
            {
                return;
            }
            var handler = GetTypeHandler(memberInfo);
            if (handler == null)
            {
                return;
            }
            var value = handler.DynamicInvoke();
            memberInfo.SetFieldOrPropertyValue(instance, value);
        }

        protected Delegate GetTypeHandler(MemberInfo memberInfo)
        {
            var type = memberInfo.GetFieldOrPropertyType();
            if (handlers.ContainsKey(type))
            {
                return handlers[type];
            }
            var typeWithoutNullability = type.GetTypeWithoutNullability();
            return handlers.ContainsKey(typeWithoutNullability)
                ? handlers[typeWithoutNullability]
                : type.IsEnum
                    ? GetDefaultEnumHandler(typeWithoutNullability)
                    : null;
        }

        protected Func<Enum> GetDefaultEnumHandler(Type type)
        {
            return () => GetRandom.Enumeration(type);
        }

        protected static IEnumerable<Delegate> GetDefaultHandlers()
        {
            yield return (Func<bool>)GetRandom.Boolean;
            yield return (Func<int>)GetRandom.Int;
            yield return (Func<short>)GetRandom.Short;
            yield return (Func<long>)GetRandom.Long;
            yield return (Func<uint>)GetRandom.UInt;
            yield return (Func<ulong>)GetRandom.ULong;
            yield return (Func<ushort>)GetRandom.UShort;
            yield return (Func<decimal>)GetRandom.Decimal;
            yield return (Func<float>)GetRandom.Float;
            yield return (Func<double>)GetRandom.Double;
            yield return (Func<byte>)GetRandom.Byte;
            yield return (Func<sbyte>)GetRandom.SByte;
            yield return (Func<DateTime>)GetRandom.DateTime;
            yield return (Func<string>)(() => GetRandom.Phrase(50));
            yield return (Func<char>)GetRandom.Char;
            yield return (Func<Guid>)GetRandom.Guid;
        }

        protected void NameWith(Delegate handler)
        {
            var returnType = handler.Method.ReturnType;
            if (handlers.ContainsKey(returnType))
            {
                handlers.Remove(returnType);
            }
            handlers.Add(new KeyValuePair<Type, Delegate>(returnType, handler));
        }
    }
}
