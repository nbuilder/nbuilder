using System;
using System.ComponentModel.DataAnnotations;

namespace FizzWare.NBuilder.FunctionalTests.Model
{
    
    public class BasketItem
    {
        [Key]
        int Id { get; set; }

        public virtual ShoppingBasket Basket { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public BasketItem(ShoppingBasket basket)
        {
            Basket = basket;
        }

        public BasketItem(ShoppingBasket basket, Product product, int quantity)
            : this(basket)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal PriceBeforeTax
        {
            get
            {
                return Product.PriceBeforeTax * Quantity;
            }
        }

        public decimal PriceAfterTax
        {
            get
            {
                return Product.PriceAfterTax * Quantity;
            }
        }

        public decimal Tax
        {
            get
            {
                return Product.Tax * Quantity;
            }
        }

        public string DiscountCode { get; set; }
    }
}