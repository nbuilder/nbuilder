using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
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

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            persistenceService = mocks.DynamicMock<IPersistenceService>();
            repository = mocks.DynamicMock<IMyClassRepository>();

            BuilderSetup.SetPersistenceService(this.persistenceService);
        }

        [Test]
        public void ShouldBeAbleToRegisterThePersistenceService()
        {
            Assert.That(BuilderSetup.GetPersistenceService(), Is.EqualTo(this.persistenceService));
        }

        [Test]
        public void ShouldBeAbleToSetCreatePersistenceMethod()
        {
            Action<MyClass> func = x => repository.Save(x);

            using (mocks.Record())
            {
                persistenceService.Expect(x => x.SetPersistenceCreateMethod(func));
            }

            BuilderSetup.SetCreatePersistenceMethod<MyClass>(func);
        }

        [Test]
        public void ShouldBeAbleToSetUpdatePersistenceMethod()
        {
            Action<MyClass> func = x => repository.Save(x);

            using (mocks.Record())
            {
                persistenceService.Expect(x => x.SetPersistenceUpdateMethod(func));
            }

            BuilderSetup.SetUpdatePersistenceMethod<MyClass>(func);
        }
    }
}