using System;
using System.Collections.Generic;
using FizzWare.NBuilder.Tests.Integration;
using FizzWare.NBuilder.Tests.Integration.Models;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit
{
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

    public class BuilderSetupTests
    {
        private IPersistenceService persistenceService;
        private IMyClassRepository repository;
        BuilderSettings builderSettings;

        public BuilderSetupTests()
        {
            persistenceService = Substitute.For<IPersistenceService>();
            repository = Substitute.For<IMyClassRepository>();
            builderSettings = new BuilderSettings();
            builderSettings.SetPersistenceService(this.persistenceService);
        }
        [Fact]
        public void RegisteringACustomPersistenceService()
        {
            var buildersetup = new BuilderSettings();
            buildersetup.SetPersistenceService(new MockCustomPersistenceService());

            new Builder(buildersetup).CreateNew<Product>().Persist();

            MockCustomPersistenceService.ProductPersisted.ShouldBeTrue();
        }

        [Fact]
        public void ShouldBeAbleToRegisterThePersistenceService()
        {
            builderSettings.GetPersistenceService().ShouldBe(this.persistenceService);
        }

        [Fact]
        public void ShouldBeAbleToSetCreatePersistenceMethod()
        {
            Action<MyClass> func = x => repository.Save(x);

            builderSettings.SetCreatePersistenceMethod<MyClass>(func);

            persistenceService.Received().SetPersistenceCreateMethod(func);
        }

        [Fact]
        public void ShouldBeAbleToSetUpdatePersistenceMethod()
        {
            Action<MyClass> func = x => repository.Save(x);

            builderSettings.SetUpdatePersistenceMethod<MyClass>(func);

            persistenceService.Received().SetPersistenceUpdateMethod(func);
        }
    }
}