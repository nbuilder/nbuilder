namespace FizzWare.NBuilder.Tests.Integration.Models
{
    
    public class BasketItem
    {
        public ShoppingBasket Basket { get; }

        public Product Product { get; }

        public int Quantity { get; }

        private BasketItem(ShoppingBasket basket)
        {
            Basket = basket;
        }

        public BasketItem(ShoppingBasket basket, Product product, int quantity)
            : this(basket)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal PriceBeforeTax => Product.PriceBeforeTax * Quantity;

        public decimal PriceAfterTax => Product.PriceAfterTax * Quantity;

        public decimal Tax => Product.Tax * Quantity;

        public string DiscountCode { get; set; }
    }
}