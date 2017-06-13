using System;
using System.Collections.Generic;
using System.Reflection;
using FizzWare.NBuilder.Extensions;

namespace FizzWare.NBuilder.PropertyNaming
{
    public class ExtensibleRandomValuePropertyNamer : IPropertyNamer
    {
        private readonly IRandomGenerator randomGenerator;
        protected IDictionary<Type, Delegate> Handlers = new Dictionary<Type, Delegate>();
        private BuilderSettings BuilderSettings;
        public ExtensibleRandomValuePropertyNamer(BuilderSettings builderSettings)
            : this (new RandomGenerator(),builderSettings)
        {
        }

        public ExtensibleRandomValuePropertyNamer(IRandomGenerator randomGenerator, BuilderSettings builderSettings)
        {
            BuilderSettings = builderSettings;
            this.randomGenerator = randomGenerator;
            var handlers = GetDefaultHandlers();

            foreach (var handler in handlers)
                NameWith(handler);
        }

        public ExtensibleRandomValuePropertyNamer(IEnumerable<Delegate> handlers)
        {
            foreach (var handler in handlers)
                NameWith(handler);
        }

        public ExtensibleRandomValuePropertyNamer NameWith<T>(Func<T> handler)
        {
            NameWith((Delegate)handler);
            return this;
        }

        public ExtensibleRandomValuePropertyNamer NameWith<T>(Func<MemberInfo, T> handler)
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
            if (Handlers.ContainsKey(type))
            {
                Handlers.Remove(type);
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
            if (memberInfo is PropertyInfo && BuilderSettings.ShouldIgnoreProperty((PropertyInfo)memberInfo))
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
            var value = handler.GetMethodInfo().GetParameters().Length == 1 ? 
                handler.DynamicInvoke(memberInfo) : 
                handler.DynamicInvoke();
            memberInfo.SetFieldOrPropertyValue(instance, value);
        }

        protected Delegate GetTypeHandler(MemberInfo memberInfo)
        {
            var type = memberInfo.GetFieldOrPropertyType();
            if (Handlers.ContainsKey(type))
            {
                return Handlers[type];
            }
            var typeWithoutNullability = type.GetTypeWithoutNullability();
            return Handlers.ContainsKey(typeWithoutNullability)
                ? Handlers[typeWithoutNullability]
                : type.IsEnum()
                    ? GetDefaultEnumHandler(typeWithoutNullability)
                    : null;
        }

        protected Func<Enum> GetDefaultEnumHandler(Type type)
        {
            return () => randomGenerator.Enumeration(type);
        }

        protected IEnumerable<Delegate> GetDefaultHandlers()
        {
            yield return (Func<bool>)randomGenerator.Boolean;
            yield return (Func<int>)randomGenerator.Int;
            yield return (Func<short>)randomGenerator.Short;
            yield return (Func<long>)randomGenerator.Long;
            yield return (Func<uint>)randomGenerator.UInt;
            yield return (Func<ulong>)randomGenerator.ULong;
            yield return (Func<ushort>)randomGenerator.UShort;
            yield return (Func<decimal>)randomGenerator.Decimal;
            yield return (Func<float>)randomGenerator.Float;
            yield return (Func<double>)randomGenerator.Double;
            yield return (Func<byte>)randomGenerator.Byte;
            yield return (Func<sbyte>)randomGenerator.SByte;
            yield return (Func<DateTime>)randomGenerator.DateTime;
            yield return (Func<string>)(() => randomGenerator.Phrase(50));
            yield return (Func<char>)randomGenerator.Char;
            yield return (Func<Guid>)randomGenerator.Guid;
        }

        protected void NameWith(Delegate handler)
        {
            var returnType = handler.GetMethodInfo().ReturnType;
            if (Handlers.ContainsKey(returnType))
            {
                Handlers.Remove(returnType);
            }
            Handlers.Add(new KeyValuePair<Type, Delegate>(returnType, handler));
        }
    }
}