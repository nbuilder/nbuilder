namespace FizzWare.NBuilder.Tests.Integration.Models
{
    public class Invoice
    {
        public decimal Amount { get; private set; }

        public readonly int Id;

        private Invoice() { }

        private Invoice(decimal amount) 
        {
            this.Amount = amount;
        }               
    }
}
