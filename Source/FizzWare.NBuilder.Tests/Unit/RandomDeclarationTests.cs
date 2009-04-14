using System;
using System.Linq;
using System.Text;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class RandomDeclarationTests
    {
        private MockRepository mocks;
        private RandomDeclaration<MyClass> declaration;
        private IListBuilderImpl<MyClass> listBuilderImpl;
        private IObjectBuilder<MyClass> objectBuilder;
        private IUniqueRandomGenerator<int> uniqueRandomGenerator;
        private const int start = 0;
        private const int end = 9;
        private const int amount = 5;
        private const int listSize = 10;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            listBuilderImpl = mocks.DynamicMock<IListBuilderImpl<MyClass>>();
            objectBuilder = mocks.DynamicMock<IObjectBuilder<MyClass>>();
            uniqueRandomGenerator = mocks.DynamicMock<IUniqueRandomGenerator<int>>();

            declaration = new RandomDeclaration<MyClass>(listBuilderImpl, objectBuilder, uniqueRandomGenerator, amount, start, end);
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void ShouldBeAbleToConstruct()
        {
            using (mocks.Record())
                objectBuilder.Expect(x => x.Construct()).Return(new MyClass()).Repeat.Times(amount);

            using (mocks.Playback())
                declaration.Construct();
        }

        [Test]
        public void ShouldBeAbleToAddToMaster()
        {
            var masterList = new MyClass[listSize];

            using (mocks.Record())
            {
                objectBuilder.Expect(x => x.Construct()).Return(new MyClass()).Repeat.Times(amount);

                uniqueRandomGenerator.Expect(x => x.Generate(start, end)).Return(0);
                uniqueRandomGenerator.Expect(x => x.Generate(start, end)).Return(2);
                uniqueRandomGenerator.Expect(x => x.Generate(start, end)).Return(4);
                uniqueRandomGenerator.Expect(x => x.Generate(start, end)).Return(6);
                uniqueRandomGenerator.Expect(x => x.Generate(start, end)).Return(8);
            }

            using (mocks.Ordered())
            {
                declaration.Construct();
                declaration.AddToMaster(masterList);
            }

            Assert.That(masterList[0], Is.Not.Null);
            Assert.That(masterList[2], Is.Not.Null);
            Assert.That(masterList[4], Is.Not.Null);
            Assert.That(masterList[6], Is.Not.Null);
            Assert.That(masterList[8], Is.Not.Null);
        }
    }
}