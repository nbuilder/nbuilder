using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzWare.NBuilder.FunctionalTests.Model
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
