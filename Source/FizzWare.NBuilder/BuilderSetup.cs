using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder
{
    public sealed class BuilderSetup
    {
        private static IPersistenceService persistenceService = new PersistenceService();

        public static void RegisterPersistenceService(IPersistenceService service)
        {
            persistenceService = service;
        }

        public static IPersistenceService GetPersistenceService()
        {
            return persistenceService;
        }

        // TODO:
        //public static IPropertyNamer<T> GetPropertyNamerFor<T>()
        //{
            // CreateSetupClasses();
        //}

        public static void SetPersistenceMethod<T>(Action<T> saveMethod)
        {
            persistenceService.SetPersistenceMethod(saveMethod);
        }
    }
}