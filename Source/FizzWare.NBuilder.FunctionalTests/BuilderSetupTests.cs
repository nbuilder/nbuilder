using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.FunctionalTests.Model;
using NUnit.Framework;

namespace FizzWare.NBuilder.FunctionalTests
{
    [TestFixture]
    public class BuilderSetupTests
    {
        [Test]
        public void RegisteringACustomPersistenceService()
        {
            BuilderSetup.SetPersistenceService(new MockCustomPersistenceService());

            Builder<Product>.CreateNew().Persist();

            Assert.That(MockCustomPersistenceService.ProductPersisted, Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            BuilderSetup.ResetToDefaults();
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