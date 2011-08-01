using System;
using FizzWare.NBuilder.Dates;

namespace FizzWare.NBuilder.FunctionalTests.WebsiteExamples
{
    public class Product
    {
        public Product(string title)
        {
            Title = title;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
    }

    // I want to use this file as a sanity check to make sure the syntax of the examples on the webiste is valid
    // Not actually tests, I just want to make sure they compile.
    public class WebsiteExamples
    {
        public void HomePage_1()
        {
            var products = Builder<Product>.CreateListOfSize(10)
                               .TheFirst(2)
                                   .With(x => x.Title = "special title")
                               .Build();
        }

        public void HomePage_2()
        {
            var products = Builder<Product>.CreateListOfSize(10)
                               .All()
                                   .WithConstructor(() => new Product("my title"))
                               .Random(5)
                                   .WithConstructor(() => new Product("my other title"))
                                   .And(x => x.Created = August.The17th.At(09, 00))
                               .Build();
        }

        public void HomePage_3()
        {
            var generator = new UniqueRandomGenerator();

            var products = Builder<Product>.CreateListOfSize(10)
                                           .TheFirst(2)
                                               .With(x => x.Title = "special title 1")
                                               .And(x => x.Description = "special description 1")
                                           .TheNext(3)
                                               .With(x => x.Title = "special title 2")
                                           .TheNext(5)
                                               .With(x => x.Title = "special title 3")
                                               .And(x => x.Price = generator.Next(0m, 10m))
                                           .Build();
        }
    }
}