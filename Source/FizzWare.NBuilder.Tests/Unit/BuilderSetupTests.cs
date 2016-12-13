using System;
using FizzWare.NBuilder.Tests.TestClasses;
using NSubstitute;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class BuilderSetupTests
    {
        private IPersistenceService persistenceService;
        private IMyClassRepository repository;
        BuilderSettings builderSettings;

        [SetUp]
        public void SetUp()
        {
            persistenceService = Substitute.For<IPersistenceService>();
            repository = Substitute.For<IMyClassRepository>();
            builderSettings = new BuilderSettings();
            builderSettings.SetPersistenceService(this.persistenceService);
        }

        [Test]
        public void ShouldBeAbleToRegisterThePersistenceService()
        {
            Assert.That(builderSettings.GetPersistenceService(), Is.EqualTo(this.persistenceService));
        }

        [Test]
        public void ShouldBeAbleToSetCreatePersistenceMethod()
        {
            Action<MyClass> func = x => repository.Save(x);

            builderSettings.SetCreatePersistenceMethod<MyClass>(func);

            persistenceService.Received().SetPersistenceCreateMethod(func);
        }

        [Test]
        public void ShouldBeAbleToSetUpdatePersistenceMethod()
        {
            Action<MyClass> func = x => repository.Save(x);

            builderSettings.SetUpdatePersistenceMethod<MyClass>(func);

            persistenceService.Received().SetPersistenceUpdateMethod(func);
        }
    }
}