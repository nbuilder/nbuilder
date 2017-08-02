using System;
using System.Linq.Expressions;
using System.Reflection;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder
{
    public static class BuilderSetup
    {
        internal static readonly BuilderSettings Instance =new BuilderSettings();
        public static bool AutoNameProperties
        {
            get { return Instance.AutoNameProperties; }
            set { Instance.AutoNameProperties = value; }
        }

        public static bool HasDisabledAutoNameProperties
        {
            get { return Instance.HasDisabledAutoNameProperties;  }
            set { Instance.HasDisabledAutoNameProperties = value; }
        }

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

        static BuilderSetup()
        {

        }
    }
}