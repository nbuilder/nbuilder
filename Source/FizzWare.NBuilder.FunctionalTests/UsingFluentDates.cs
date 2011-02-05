using System;
using FizzWare.NBuilder.Dates;
using FizzWare.NBuilder.FunctionalTests.Model;
using NUnit.Framework;

namespace FizzWare.NBuilder.FunctionalTests
{
    [TestFixture]
    // ReSharper disable AccessToStaticMemberViaDerivedType
    public class UsingFluentDates
    {

        [Test]
        public void Fluent_dates_example()
        {
            var product = Builder<Product>
                            .CreateNew()
                            .With(x => x.Created = The.Year(2006).On.May.The10th.At(09, 00))
                            .With(x => x.LastEdited = On.August.The15th.At(15, 43))
                            .Build();

            Assert.That(product.Created, Is.EqualTo(new DateTime(2006, 5, 10, 09, 00, 00)));
            Assert.That(product.LastEdited, Is.EqualTo(new DateTime(DateTime.Now.Year, 8, 15, 15, 43, 00)));
        }

        [Test]
        public void Using_random_dates()
        {
            var generator = new RandomGenerator();
            
            var products = Builder<Product>
                            .CreateListOfSize(100)
                            .All()
                                .With(x => x.Created = generator.Next(July.The(1), November.The(10)))
                            .Build();

            var expectedStart = new DateTime(DateTime.Now.Year, 7, 1, 00, 00, 00);
            var expectedEnd = new DateTime(DateTime.Now.Year, 11, 10, 00, 00, 00);

            foreach (var product in products)
            {
                Assert.That(product.Created, Is.AtLeast(expectedStart));
                Assert.That(product.Created, Is.AtMost(expectedEnd));
            }
        }






        [Test]
        public void Using_full_syntax()
        {
            var product = Builder<Product>.CreateNew()
                            .With(x => x.Created = The.Year(2008).On.January.The10th.At(05, 49, 38))
                            .Build();

            Assert.That(product.Created, Is.EqualTo(new DateTime(2008, 01, 10, 05, 49, 38)));
        }

        [Test]
        public void Not_specifying_the_year()
        {
            // (Defaults to current year)

            var product = Builder<Product>.CreateNew()
                .With(x => x.Created = On.July.The21st.At(07, 00))
                .Build();

            Assert.That(product.Created, Is.EqualTo(new DateTime(DateTime.Now.Year, 07, 21, 07, 00, 00)));
        }

        [Test]
        public void Just_the_date()
        {
            var product = Builder<Product>.CreateNew()
                .With(x => x.Created = On.May.The14th)
                .Build();

            Assert.That(product.Created, Is.EqualTo(new DateTime(DateTime.Now.Year, 05, 14, 00, 00, 00)));
        }

        [Test]
        public void Static_month_names()
        {
            // You can use the month names on their own without On
            // which one you use is just a matter of preference or one or the other
            // might read better in different contexts.

            var product = Builder<Product>.CreateNew()
                                .With(x => x.Created = December.The10th.At(09, 00))
                                .Build();

            Assert.That(product.Created, Is.EqualTo(new DateTime(DateTime.Now.Year, 12, 10, 09, 00, 00)));
        }

        [Test]
        public void Specifying_time_spans()
        {
            // There are two ways of specifying TimeSpans, again which one you use
            // is your choice and which reads better in context.

            var at8_31am = The.Time(08, 31);
            var at9_46am = At.Time(09, 46);

            Assert.That(at8_31am, Is.EqualTo(new TimeSpan(0, 8, 31, 0)));
            Assert.That(at9_46am, Is.EqualTo(new TimeSpan(0, 9, 46, 0)));
        }

        [Test]
        public void Using_the_and_a_number()
        {
            var product = Builder<Product>.CreateNew()
                .With(x => x.Created = On.August.The(21).At(16, 38, 46))
                .Build();

            Assert.That(product.Created, Is.EqualTo(new DateTime(DateTime.Now.Year, 08, 21, 16, 38, 46)));
        }

        [Test]
        public void Using_Today()
        {
            var date = Today.At(09, 00);
            Assert.That(date, Is.EqualTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00)));
        }
    }
    // ReSharper restore AccessToStaticMemberViaDerivedType
}