using System.ComponentModel.DataAnnotations;

namespace FizzWare.NBuilder.FunctionalTests.Model
{
    
    public class TaxType
    {
        [Key]
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