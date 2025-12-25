using System;
using System.Linq.Expressions;
using System.Reflection;
using FizzWare.NBuilder.Extensions;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder
{
    [Obsolete("Support for the static BuilderSetup will be removed in a future major version. Please migrate to instance-based builders.")]
    public static class BuilderSetup
    {
        internal static readonly BuilderSettings Instance =new BuilderSettings();
        public static bool IsBuildingAllNullablePropertiesAsNull => Instance.IsBuildingAllNullablePropertiesAsNull;
        public static bool AutoNameProperties => Instance.AutoNameProperties;

        public static bool HasDisabledAutoNameProperties => Instance.HasDisabledAutoNameProperties;

        public static bool ShouldIgnoreProperty(PropertyInfo info)
        {
            return Instance.ShouldIgnoreProperty(info);
        }

        public static void ResetToDefaults()
        {
            Instance.ResetToDefaults();
        }

        public static IPropertyNamer GetPropertyNamerFor<T>()
        {
            return Instance.GetPropertyNamerFor<T>();
        }

        public static void SetPropertyNamerFor<T>(IPropertyNamer propertyNamer)
        {
            Instance.SetPropertyNamerFor<T>(propertyNamer);
        }

        public static void SetDefaultPropertyName(IPropertyNamer propertyNamer)
        {
            Instance.SetDefaultPropertyNamer(propertyNamer);
        }

        public static void SetPersistenceService(IPersistenceService service)
        {
            Instance.SetPersistenceService(service);
        }

        public static IPersistenceService GetPersistenceService()
        {
            return Instance.GetPersistenceService();
        }

        public static void SetCreatePersistenceMethod<T>(Action<T> saveMethod)
        {
            Instance.SetCreatePersistenceMethod(saveMethod);
        }

        public static void SetUpdatePersistenceMethod<T>(Action<T> saveMethod)
        {
            Instance.SetUpdatePersistenceMethod(saveMethod);
        }

        public static void DisablePropertyNamingFor<T, TFunc>(Expression<Func<T,TFunc >> func)
        {
            Instance.DisablePropertyNamingFor(func);
        }

        /// <summary>
        /// Set the builder to build all properties that are nullable value types as null instead of the non-null equivalent type's default value.
        /// </summary>
        public static void UseNullAsDefaultValueForAllNullableTypes()
        {
            Instance.UseNullAsDefaultValueForAllNullableTypes();
        }

        /// <summary>
        /// Specify any nullable value types that should be set to null when building instead of the non-null equivalent's default value.
        /// </summary>
        /// <param name="types">The nullable value types that you wish to leave as null when building.</param>
        public static void UseNullAsDefaultValueForNullableType(params Type[] types)
        {
            foreach (Type type in types)
            {
                if (!type.IsGenericType() || type.GetGenericTypeDefinition() != typeof(Nullable<>))
                {
                    throw new ArgumentException($"{type} is not a nullable type.");
                }
                Instance.UseNullAsDefaultValueForNullableType(type);
            }
        }

        static BuilderSetup()
        {

        }
    }
}