using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class BuilderSetupTests
    {
        private MockRepository mocks;
        private IPersistenceService persistenceService;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            persistenceService = mocks.DynamicMock<IPersistenceService>();

            BuilderSetup.RegisterPersistenceService(this.persistenceService);
        }

        [Test]
        public void ShouldBeAbleToRegisterThePersistenceService()
        {
            Assert.That(BuilderSetup.GetPersistenceService(), Is.EqualTo(this.persistenceService));
        }

        [Test]
        public void ShouldBeAbleToSetPersistenceMethod()
        {
            const IMyClassRepository repository = null;
            Action<MyClass> func = x => repository.Save(x);

            using (mocks.Record())
            {
                persistenceService.Expect(x => x.SetPersistenceMethod(func));
            }

            BuilderSetup.SetPersistenceMethod<MyClass>(func);
        }
    }
}