using System;
using FizzWare.NBuilder.Dates;
using FizzWare.NBuilder.Tests.Integration.Models;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Integration
{
    
    // ReSharper disable AccessToStaticMemberViaDerivedType
    public class UsingFluentDates
    {

        [Fact]
        public void Fluent_dates_example()
        {
            var builderSetup = new BuilderSettings();
            var product = new Builder(builderSetup)
                            .CreateNew< Product>()
                            .With(x => x.Created = The.Year(2006).On.May.The10th.At(09, 00))
                            .With(x => x.LastEdited = On.August.The15th.At(15, 43))
                            .Build();

            product.Created.ShouldBe(new DateTime(2006, 5, 10, 09, 00, 00));
            product.LastEdited.ShouldBe(new DateTime(DateTime.Now.Year, 8, 15, 15, 43, 00));
        }

        [Fact]
        public void Using_random_dates()
        {
            var builderSetup = new BuilderSettings();
            var generator = new RandomGenerator();
            
            var products = new Builder(builderSetup)
                            .CreateListOfSize< Product>(100)
                            .All()
                                .With(x => x.Created = generator.Next(July.The(1), November.The(10)))
                            .Build();

            var expectedStart = new DateTime(DateTime.Now.Year, 7, 1, 00, 00, 00);
            var expectedEnd = new DateTime(DateTime.Now.Year, 11, 10, 00, 00, 00);

            foreach (var product in products)
            {
                product.Created.ShouldBeGreaterThanOrEqualTo(expectedStart);
                product.Created.ShouldBeLessThanOrEqualTo(expectedEnd);
            }
        }






        [Fact]
        public void Using_full_syntax()
        {
            var builderSetup = new BuilderSettings();
            var product = new Builder(builderSetup).CreateNew< Product>()
                            .With(x => x.Created = The.Year(2008).On.January.The10th.At(05, 49, 38))
                            .Build();

            product.Created.ShouldBe(new DateTime(2008, 01, 10, 05, 49, 38));
        }

        [Fact]
        public void Not_specifying_the_year()
        {
            // (Defaults to current year)

            var builderSetup = new BuilderSettings();
            var product = new Builder(builderSetup).CreateNew< Product>()
                .With(x => x.Created = On.July.The21st.At(07, 00))
                .Build();

            product.Created.ShouldBe(new DateTime(DateTime.Now.Year, 07, 21, 07, 00, 00));
        }

        [Fact]
        public void Just_the_date()
        {
            var builderSetup = new BuilderSettings();
            var product = new Builder(builderSetup).CreateNew< Product>()
                .With(x => x.Created = On.May.The14th)
                .Build();

            product.Created.ShouldBe(new DateTime(DateTime.Now.Year, 05, 14, 00, 00, 00));
        }

        [Fact]
        public void Static_month_names()
        {
            // You can use the month names on their own without On
            // which one you use is just a matter of preference or one or the other
            // might read better in different contexts.

            var builderSetup = new BuilderSettings();
            var product = new Builder(builderSetup).CreateNew< Product>()
                                .With(x => x.Created = December.The10th.At(09, 00))
                                .Build();

            product.Created.ShouldBe(new DateTime(DateTime.Now.Year, 12, 10, 09, 00, 00));
        }

        [Fact]
        public void Specifying_time_spans()
        {
            // There are two ways of specifying TimeSpans, again which one you use
            // is your choice and which reads better in context.

            var at8_31am = The.Time(08, 31);
            var at9_46am = At.Time(09, 46);

            at8_31am.ShouldBe(new TimeSpan(0, 8, 31, 0));
            at9_46am.ShouldBe(new TimeSpan(0, 9, 46, 0));
        }

        [Fact]
        public void Using_the_and_a_number()
        {
            var builderSetup = new BuilderSettings();
            var product = new Builder(builderSetup).CreateNew< Product>()
                .With(x => x.Created = On.August.The(21).At(16, 38, 46))
                .Build();

            product.Created.ShouldBe(new DateTime(DateTime.Now.Year, 08, 21, 16, 38, 46));
        }

        [Fact]
        public void Using_Today()
        {
            var date = Today.At(09, 00);
            date.ShouldBe(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 00, 00));
        }
    }
    // ReSharper restore AccessToStaticMemberViaDerivedType
}