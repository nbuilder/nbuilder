using System;


namespace FizzWare.NBuilder.Tests.TestModel
{
    public class BasketItem
    {
        // Is there a way round adding these two??

        int Id { get; set; } // Have to add this field or activerecord complains

        //[Property]
        //int ShoppingBasketId { get; set; } // Have to add this field or activerecord complains
        
        public ShoppingBasket Basket { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        internal BasketItem()
        {
        }

        internal BasketItem(ShoppingBasket basket)
        {
            Basket = basket;
        }

        internal BasketItem(ShoppingBasket basket, Product product, int quantity)
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
    }
}