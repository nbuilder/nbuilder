#pragma warning disable CS0618 // Type or member is obsolete
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

        [Fact]
        public void UseNullAsDefaultValueForNullableType_WithNonNullableType_ThrowsArgumentException()
        {
            var exception = Should.Throw<ArgumentException>(() => 
                BuilderSetup.UseNullAsDefaultValueForNullableType(typeof(int)));

            exception.Message.ShouldContain("Type 'System.Int32' is not a nullable value type");
            exception.Message.ShouldContain("Only nullable value types like 'int?' or 'Guid?' are allowed");
        }

        [Fact]
        public void UseNullAsDefaultValueForNullableType_WithReferenceType_ThrowsArgumentException()
        {
            var exception = Should.Throw<ArgumentException>(() => 
                BuilderSetup.UseNullAsDefaultValueForNullableType(typeof(string)));

            exception.Message.ShouldContain("Type 'System.String' is not a nullable value type");
            exception.Message.ShouldContain("Only nullable value types like 'int?' or 'Guid?' are allowed");
        }

        [Fact]
        public void UseNullAsDefaultValueForNullableType_WithNullableType_DoesNotThrow()
        {
            Should.NotThrow(() => BuilderSetup.UseNullAsDefaultValueForNullableType(typeof(int?)));
        }

        [Fact]
        public void UseNullAsDefaultValueForNullableType_WithMultipleNullableTypes_DoesNotThrow()
        {
            Should.NotThrow(() => 
                BuilderSetup.UseNullAsDefaultValueForNullableType(typeof(int?), typeof(Guid?), typeof(DateTime?)));
        }

        [Fact]
        public void UseNullAsDefaultValueForNullableType_WithMixedTypes_ThrowsArgumentException()
        {
            var exception = Should.Throw<ArgumentException>(() => 
                BuilderSetup.UseNullAsDefaultValueForNullableType(typeof(int?), typeof(int)));

            exception.Message.ShouldContain("Type 'System.Int32' is not a nullable value type");
        }

        // BuilderSettings validation tests (instance-based API)
        [Fact]
        public void BuilderSettings_UseNullAsDefaultValueForNullableType_WithNonNullableType_ThrowsArgumentException()
        {
            var settings = new BuilderSettings();
            var exception = Should.Throw<ArgumentException>(() => 
                settings.UseNullAsDefaultValueForNullableType(typeof(int)));

            exception.Message.ShouldContain("Type 'System.Int32' is not a nullable value type");
            exception.Message.ShouldContain("Only nullable value types like 'int?' or 'Guid?' are allowed");
        }

        [Fact]
        public void BuilderSettings_UseNullAsDefaultValueForNullableType_WithReferenceType_ThrowsArgumentException()
        {
            var settings = new BuilderSettings();
            var exception = Should.Throw<ArgumentException>(() => 
                settings.UseNullAsDefaultValueForNullableType(typeof(string)));

            exception.Message.ShouldContain("Type 'System.String' is not a nullable value type");
            exception.Message.ShouldContain("Only nullable value types like 'int?' or 'Guid?' are allowed");
        }

        [Fact]
        public void BuilderSettings_UseNullAsDefaultValueForNullableType_WithNullableType_DoesNotThrow()
        {
            var settings = new BuilderSettings();
            Should.NotThrow(() => settings.UseNullAsDefaultValueForNullableType(typeof(int?)));
        }

        [Fact]
        public void BuilderSettings_UseNullAsDefaultValueForNullableType_WithNull_ThrowsArgumentNullException()
        {
            var settings = new BuilderSettings();
            Should.Throw<ArgumentNullException>(() => 
                settings.UseNullAsDefaultValueForNullableType(null));
        }

        [Fact]
        public void BuilderSettings_UseNullAsDefaultValueForNullableType_WithMultipleValidTypes_DoesNotThrow()
        {
            var settings = new BuilderSettings();
            Should.NotThrow(() => 
            {
                settings.UseNullAsDefaultValueForNullableType(typeof(int?));
                settings.UseNullAsDefaultValueForNullableType(typeof(Guid?));
                settings.UseNullAsDefaultValueForNullableType(typeof(DateTime?));
            });
        }
    }
}
#pragma warning restore CS0618 // Type or member is obsolete