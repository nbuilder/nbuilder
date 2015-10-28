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
        BuilderSetup builderSetup;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            persistenceService = mocks.DynamicMock<IPersistenceService>();
            repository = mocks.DynamicMock<IMyClassRepository>();
            builderSetup = new BuilderSetup();
            builderSetup.SetPersistenceService(this.persistenceService);
        }

        [Test]
        public void ShouldBeAbleToRegisterThePersistenceService()
        {
            Assert.That(builderSetup.GetPersistenceService(), Is.EqualTo(this.persistenceService));
        }

        [Test]
        public void ShouldBeAbleToSetCreatePersistenceMethod()
        {
            Action<MyClass> func = x => repository.Save(x);

            using (mocks.Record())
            {
                persistenceService.Expect(x => x.SetPersistenceCreateMethod(func));
            }

            builderSetup.SetCreatePersistenceMethod<MyClass>(func);
        }

        [Test]
        public void ShouldBeAbleToSetUpdatePersistenceMethod()
        {
            Action<MyClass> func = x => repository.Save(x);

            using (mocks.Record())
            {
                persistenceService.Expect(x => x.SetPersistenceUpdateMethod(func));
            }

            builderSetup.SetUpdatePersistenceMethod<MyClass>(func);
        }
    }
}