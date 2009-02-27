using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzWare.NBuilder.Tests.TestModel
{
    public class ShoppingBasket
    {
        public int Id { get; set; }

        public IList<BasketItem> Items { get; set; }
        
        public ShoppingBasket()
        {
            Items = new List<BasketItem>();
        }

        public void Add(Product product, int quantity)
        {
            var item = new BasketItem(this, product, quantity);
            Items.Add(item);
        }

        public bool Contains(int productId)
        {
            return Items.FirstOrDefault(x => x.Product.Id == productId) != null
                       ? true
                       : false;
        }

        public void Remove(int productId)
        {
            var item = this.Items.FirstOrDefault(i => i.Product.Id == productId);

            if (item == null)
                throw new ArgumentException("Item not found in basket");

            Items.Remove(item);
        }

        public decimal SubTotal 
        { 
            get
            {
                decimal subTotal = 0m;
                foreach (var item in Items)
                {
                    subTotal += item.Product.PriceBeforeTax * item.Quantity;
                }

                return subTotal;
            }
        }

        public decimal TaxTotal 
        { 
            get
            {
                decimal taxTotal = 0m;
                foreach (var item in Items)
                {
                    taxTotal += item.Product.Tax * item.Quantity;
                }

                return taxTotal;
            }
        }

        public decimal Total 
        { 
            get
            {
                return SubTotal + TaxTotal;
            }
        }
    }
}