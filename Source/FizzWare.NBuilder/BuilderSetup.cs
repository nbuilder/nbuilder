using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder
{
    public sealed class BuilderSetup
    {
        private static IPersistenceService persistenceService;
        public static bool AutoNameProperties;
        private static Dictionary<Type, IPropertyNamer> propertyNamers;
        private static IPropertyNamer defaultPropertyNamer;

        private static List<PropertyInfo> disabledAutoNameProperties;

        internal static bool HasDisabledAutoNameProperties;

        static BuilderSetup()
        {
            ResetToDefaults();
        }

        public static void ResetToDefaults()
        {
            SetDefaultPropertyNamer(new SequentialPropertyNamer(new ReflectionUtil()));
            persistenceService = new PersistenceService();
            AutoNameProperties = true;
            propertyNamers = new Dictionary<Type, IPropertyNamer>();
            HasDisabledAutoNameProperties = false;
            disabledAutoNameProperties = new List<PropertyInfo>();
        }

        public static void SetDefaultPropertyNamer(IPropertyNamer propertyNamer)
        {
            defaultPropertyNamer = propertyNamer;
        }

        public static void RegisterPersistenceService(IPersistenceService service)
        {
            persistenceService = service;
        }

        public static IPersistenceService GetPersistenceService()
        {
            return persistenceService;
        }

        public static void SetPersistenceMethod<T>(Action<T> saveMethod)
        {
            persistenceService.SetPersistenceMethod(saveMethod);
        }

        public static void SetPropertyNamerFor<T>(IPropertyNamer propertyNamer)
        {
            propertyNamers.Add(typeof(T), propertyNamer);
        }

        public static IPropertyNamer GetPropertyNamerFor<T>()
        {
            if (!propertyNamers.ContainsKey(typeof(T)))
            {
                return defaultPropertyNamer;
            }

            return propertyNamers[typeof (T)];
        }

        public static void DisablePropertyNamingFor<T, TFunc>(Expression<Func<T, TFunc>> func)
        {
            HasDisabledAutoNameProperties = true;
            disabledAutoNameProperties.Add(GetProperty(func));
        }

        public static bool ShouldIgnoreProperty(PropertyInfo info)
        {
            if (disabledAutoNameProperties.Contains(info))
                return true;

            return false;
        }

        private static PropertyInfo GetProperty<MODEL, T>(Expression<Func<MODEL, T>> expression)
        {
            MemberExpression memberExpression = getMemberExpression(expression);

            return (PropertyInfo)memberExpression.Member;
        }

        private static MemberExpression getMemberExpression<MODEL, T>(Expression<Func<MODEL, T>> expression)
        {
            return getMemberExpression(expression, true);
        }

        private static MemberExpression getMemberExpression<MODEL, T>(Expression<Func<MODEL, T>> expression, bool enforceCheck)
        {
            MemberExpression memberExpression = null;
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = expression.Body as MemberExpression;
            }

            return memberExpression;
        }
    }
}