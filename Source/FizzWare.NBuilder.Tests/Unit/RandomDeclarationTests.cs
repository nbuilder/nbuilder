using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;
using NUnit.Framework;
using NSubstitute;

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
            listBuilderImpl = Substitute.For<IListBuilderImpl<MyClass>>();
            objectBuilder = Substitute.For<IObjectBuilder<MyClass>>();
            uniqueRandomGenerator = Substitute.For<IUniqueRandomGenerator>();

            declaration = new RandomDeclaration<MyClass>(listBuilderImpl, objectBuilder, uniqueRandomGenerator, amount, start, end);
        }

        [Test]
        public void Construct_ConstructsEachItem()
        {
            // Act
            declaration.Construct();

            // Assert
            objectBuilder.Received().Construct(Arg.Any<int>());
        }

        [Test]
        public void AddToMaster_AddsEachItemToTheList()
        {
            var masterList = new MyClass[listSize];

            objectBuilder.Construct(Arg.Any<int>()).Returns(new MyClass());

            uniqueRandomGenerator.Next(start, end).Returns(0, 2, 4);

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