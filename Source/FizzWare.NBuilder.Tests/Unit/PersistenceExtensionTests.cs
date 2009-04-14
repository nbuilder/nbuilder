using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class PersistenceExtensionTests
    {
        private MockRepository mocks;
        private IMyClassRepository repository;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();

            repository = mocks.DynamicMock<IMyClassRepository>();
        }

        [Test]
        public void ShouldBeAbleToUseBuilderSetup()
        {
            using (mocks.Record())
            {
                
            }

            BuilderSetup.SetPersistenceMethod<MyClass>(x => repository.Save(x));
            Builder<MyClass>.CreateNew().Persist();
        }
    }
}