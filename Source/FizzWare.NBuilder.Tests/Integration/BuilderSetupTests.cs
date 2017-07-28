using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Tests.Integration.Models;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Integration
{
    [TestFixture]
    public class BuilderSetupTests
    {
        [Test]
        public void RegisteringACustomPersistenceService()
        {
            var buildersetup = new BuilderSettings();
            buildersetup.SetPersistenceService(new MockCustomPersistenceService());

           new Builder(buildersetup).CreateNew<Product>().Persist();

            Assert.That(MockCustomPersistenceService.ProductPersisted, Is.True);
        }

    }

    class MockCustomPersistenceService : IPersistenceService
    {
        public static bool ProductPersisted { get; set; }

        public void Create<T>(T obj)
        {
            if (typeof(T) == typeof(Product))
            {
                ProductPersisted = true;
            }
        }

        public void Create<T>(IList<T> obj)
        {
            throw new System.NotImplementedException();
        }

        public void Update<T>(T obj)
        {
            throw new System.NotImplementedException();
        }

        public void Update<T>(IList<T> obj)
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T obj)
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(IList<T> obj)
        {
            throw new System.NotImplementedException();
        }

        public void SetPersistenceCreateMethod<T>(Action<T> saveMethod)
        {
            throw new System.NotImplementedException();
        }

        public void SetPersistenceUpdateMethod<T>(Action<T> saveMethod)
        {
            throw new System.NotImplementedException();
        }
    }
}