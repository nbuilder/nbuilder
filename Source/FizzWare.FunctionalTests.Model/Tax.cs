using Castle.ActiveRecord;

namespace FizzWare.NBuilder.FunctionalTests.Model
{
    [ActiveRecord]
    public class TaxType
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Property]
        public string Name { get; set; }

        [Property]
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