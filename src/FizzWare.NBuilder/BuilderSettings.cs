using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FizzWare.NBuilder.Extensions;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder
{
    public class BuilderSettings
    {

        private IPersistenceService persistenceService;
        public bool AutoNameProperties;
        private Dictionary<Type, IPropertyNamer> propertyNamers;
        private IPropertyNamer defaultPropertyNamer;

        private List<PropertyInfo> disabledAutoNameProperties;

        internal  bool HasDisabledAutoNameProperties;

        public BuilderSettings()
        {
            ResetToDefaults();
        }

        public  void ResetToDefaults()
        {
            SetDefaultPropertyNamer(new SequentialPropertyNamer(new ReflectionUtil(),this));
            persistenceService = new PersistenceService();
            AutoNameProperties = true;
            propertyNamers = new Dictionary<Type, IPropertyNamer>();
            HasDisabledAutoNameProperties = false;
            disabledAutoNameProperties = new List<PropertyInfo>();
        }

        public  void SetDefaultPropertyNamer(IPropertyNamer propertyNamer)
        {
            defaultPropertyNamer = propertyNamer;
        }

        public  void SetPersistenceService(IPersistenceService service)
        {
            persistenceService = service;
        }

        public  IPersistenceService GetPersistenceService()
        {
            return persistenceService;
        }

        public  void SetCreatePersistenceMethod<T>(Action<T> saveMethod)
        {
            persistenceService.SetPersistenceCreateMethod(saveMethod);
        }

        public  void SetUpdatePersistenceMethod<T>(Action<T> saveMethod)
        {
            persistenceService.SetPersistenceUpdateMethod(saveMethod);
        }

        public  void SetPropertyNamerFor<T>(IPropertyNamer propertyNamer)
        {
            propertyNamers.Add(typeof(T), propertyNamer);
        }

        public  IPropertyNamer GetPropertyNamerFor<T>()
        {
            if (!propertyNamers.ContainsKey(typeof(T)))
            {
                return defaultPropertyNamer;
            }

            return propertyNamers[typeof (T)];
        }

        public  void DisablePropertyNamingFor<T, TFunc>(Expression<Func<T, TFunc>> func)
        {
            HasDisabledAutoNameProperties = true;
            disabledAutoNameProperties.Add(GetProperty(func));
        }

        public  bool ShouldIgnoreProperty(PropertyInfo info)
        {
            if (disabledAutoNameProperties.Any(x => {
                var typeInfo = x.DeclaringType.GetTypeInfo(); 
                return (typeInfo.IsInterface ? typeInfo.IsAssignableFrom(info.DeclaringType) : x.DeclaringType == info.DeclaringType) &&
                x.Name == info.Name;
                }))
            {
                return true;
            }

            return false;
        }

        private  PropertyInfo GetProperty<TModel, T>(Expression<Func<TModel, T>> expression)
        {
            MemberExpression memberExpression = GetMemberExpression(expression);

            return (PropertyInfo)memberExpression.Member;
        }

        private  MemberExpression GetMemberExpression<TModel, T>(Expression<Func<TModel, T>> expression)
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