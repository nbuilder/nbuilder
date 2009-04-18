using FizzWare.NBuilder.FunctionalTests.Model;

namespace FizzWare.NBuilder.FunctionalTests.Extensibility
{
    static class LocationGenerator
    {
        private static readonly SequentialGenerator<char> charGenerator;
        private static readonly SequentialGenerator<int> shelfGenerator;
        private static readonly SequentialGenerator<int> locGenerator;

        static LocationGenerator()
        {
            charGenerator = new SequentialGenerator<char>();
            shelfGenerator = new SequentialGenerator<int>();
            locGenerator = new SequentialGenerator<int> {Increment = 1000};

            charGenerator.StartingWith('A');
            shelfGenerator.StartingWith(1);
            locGenerator.StartingWith(7500);
        }

        public static WarehouseLocation Generate()
        {
            var aisle = charGenerator.Generate();
            var shelf = shelfGenerator.Generate();
            var loc = locGenerator.Generate();

            var location = new WarehouseLocation(aisle, shelf, loc);

            return location;
        }
    }
}