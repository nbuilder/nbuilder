using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using Rhino.Mocks;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class RandomDeclarationTests
    {
        private RandomDeclaration<MyClass> declaration;
        private IListBuilderImpl<MyClass> listBuilderImpl;
        private IObjectBuilder<MyClass> objectBuilder;
        private IUniqueRandomGenerator uniqueRandomGenerator;
        private const int amount = 5;
        private const int listSize = 10;
        private const int start = 0;
        private const int end = listSize - 1;

        [SetUp]
        public void SetUp()
        {
            listBuilderImpl = MockRepository.GenerateStub<IListBuilderImpl<MyClass>>();
            objectBuilder = MockRepository.GenerateStub<IObjectBuilder<MyClass>>();
            uniqueRandomGenerator = MockRepository.GenerateMock<IUniqueRandomGenerator>();

            declaration = new RandomDeclaration<MyClass>(listBuilderImpl, objectBuilder, uniqueRandomGenerator, amount, start, end);
        }

        [Test]
        public void Construct_ConstructsEachItem()
        {
            // Act
            declaration.Construct();

            // Assert
            objectBuilder.AssertWasCalled(x => x.Construct(Arg<int>.Is.Anything), opt => opt.Repeat.Times(amount));
        }

        [Test]
        public void AddToMaster_AddsEachItemToTheList()
        {
            var masterList = new MyClass[listSize];

            objectBuilder.Stub(x => x.Construct(Arg<int>.Is.Anything)).Return(new MyClass()).Repeat.Times(amount);

            uniqueRandomGenerator.Stub(x => x.Next(start, end)).Return(0).Repeat.Once();
            uniqueRandomGenerator.Stub(x => x.Next(start, end)).Return(2).Repeat.Once();
            uniqueRandomGenerator.Stub(x => x.Next(start, end)).Return(4).Repeat.Once();
            
            declaration.Construct();

            // Act
            declaration.AddToMaster(masterList);

            // Assert
            Assert.That(masterList[0], Is.Not.Null);
            Assert.That(masterList[2], Is.Not.Null);
            Assert.That(masterList[4], Is.Not.Null);
        }
    }
}