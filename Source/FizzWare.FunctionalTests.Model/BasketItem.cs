using Castle.ActiveRecord;
using System;

namespace FizzWare.NBuilder.FunctionalTests.Model
{
    [ActiveRecord]
    public class BasketItem
    {
        [PrimaryKey]
        int Id { get; set; }

        [BelongsTo(Type = typeof(ShoppingBasket), Column = "ShoppingBasketId")]
        public ShoppingBasket Basket { get; set; }

        [BelongsTo(Type = typeof(Product), Column = "ProductId")]
        public Product Product { get; set; }

        [Property]
        public int Quantity { get; set; }

        [Obsolete("For NHibernate")]
        private BasketItem()
        {
        }

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