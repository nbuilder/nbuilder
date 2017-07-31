using System;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit
{
    
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