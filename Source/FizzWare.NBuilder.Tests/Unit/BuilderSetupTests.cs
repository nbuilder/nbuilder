using System;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class BuilderSetupTests
    {
        private MockRepository mocks;
        private IPersistenceService persistenceService;
        private IMyClassRepository repository;
        BuilderSettings builderSettings;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            persistenceService = mocks.DynamicMock<IPersistenceService>();
            repository = mocks.DynamicMock<IMyClassRepository>();
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

            using (mocks.Record())
            {
                persistenceService.Expect(x => x.SetPersistenceCreateMethod(func));
            }

            builderSettings.SetCreatePersistenceMethod<MyClass>(func);
        }

        [Test]
        public void ShouldBeAbleToSetUpdatePersistenceMethod()
        {
            Action<MyClass> func = x => repository.Save(x);

            using (mocks.Record())
            {
                persistenceService.Expect(x => x.SetPersistenceUpdateMethod(func));
            }

            builderSettings.SetUpdatePersistenceMethod<MyClass>(func);
        }
    }
}