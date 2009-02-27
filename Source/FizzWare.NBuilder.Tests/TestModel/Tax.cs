


namespace FizzWare.NBuilder.Tests.TestModel
{
    public class TaxType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Percentage { get; set; }

        public TaxType()
        {
            
        }

        public TaxType(string name, decimal percentage)
        {
            Name = name;
            Percentage = percentage;
        }
    }
}