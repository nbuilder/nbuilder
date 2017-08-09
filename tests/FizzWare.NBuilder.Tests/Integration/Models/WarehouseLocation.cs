namespace FizzWare.NBuilder.Tests.Integration.Models
{
    public struct WarehouseLocation
    {
        public char Aisle;
        public int Shelf;
        public int Location;

        public WarehouseLocation(char aisle, int shelf, int location)
        {
            Aisle = aisle;
            Shelf = shelf;
            Location = location;
        }
    }
}