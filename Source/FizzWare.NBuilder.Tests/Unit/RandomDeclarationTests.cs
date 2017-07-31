using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.Tests.TestClasses;

using NSubstitute;
using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit
{
    
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

        public RandomDeclarationTests()
        {
            listBuilderImpl = Substitute.For<IListBuilderImpl<MyClass>>();
            objectBuilder = Substitute.For<IObjectBuilder<MyClass>>();
            uniqueRandomGenerator = Substitute.For<IUniqueRandomGenerator>();

            declaration = new RandomDeclaration<MyClass>(listBuilderImpl, objectBuilder, uniqueRandomGenerator, amount, start, end);
        }

        [Fact]
        public void Construct_ConstructsEachItem()
        {
            // Act
            declaration.Construct();

            // Assert
            objectBuilder.Received().Construct(Arg.Any<int>());
        }

        [Fact]
        public void AddToMaster_AddsEachItemToTheList()
        {
            var masterList = new MyClass[listSize];

            objectBuilder.Construct(Arg.Any<int>()).Returns(new MyClass());

            uniqueRandomGenerator.Next(start, end).Returns(0, 2, 4);

            declaration.Construct();

            // Act
            declaration.AddToMaster(masterList);

            // Assert
            masterList[0].ShouldNotBeNull();
            masterList[2].ShouldNotBeNull();
            masterList[4].ShouldNotBeNull();
        }
    }
}