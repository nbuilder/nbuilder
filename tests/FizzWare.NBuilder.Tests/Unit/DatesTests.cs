using System;
using FizzWare.NBuilder.Dates;

using Shouldly;
using Xunit;


namespace FizzWare.NBuilder.Tests.Unit.Dates
{
    // ReSharper disable AccessToStaticMemberViaDerivedType
    public class DatesTests
    {
        [Fact]
        public void TheYear_x_uses_that_year_to_make_dates()
        {
            var months = The.Year(1999);
            var date = months.On.January.The(1);
            date.Year.ShouldBe(1999);
        }

        [Fact]
        public void Can_use_Today_class()
        {
            var time = Today.At(09, 01);
            var expected = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 01, 00);
            time.ShouldBe(expected);
        }

        [Fact]
        public void Can_use_Today_class_with_seconds()
        {
            var time = Today.At(09, 01, 05);
            var expected = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 01, 05);
            time.ShouldBe(expected);
        }

        [Fact]
        public void Can_use_The()
        {
            var year = DateTime.Now.Year;

            for (var i = 1; i < 13; i++)
            {
                for (var j = 1; j < 32; j++)
                {
                    if (i == 2 && j > 28)
                        break;

                    if ( (i == 9 || i == 4 || i == 6 || i == 11) && j > 30)
                        break;

                    var date = new Month(i).The(j);
                    var expected = new DateTime(year, i, j);
                    date.ShouldBe(expected);
                }
            }
        }

        [Fact]
        public void Can_use_The_x_properties()
        {
            DateTime date = new Month(1).The1st;
            date.Day.ShouldBe(1);
            date = new Month(1).The2nd;
            date.Day.ShouldBe(2);
            date = new Month(1).The3rd;
            date.Day.ShouldBe(3);
            date = new Month(1).The4th;
            date.Day.ShouldBe(4);
            date = new Month(1).The5th;
            date.Day.ShouldBe(5);
            date = new Month(1).The6th;
            date.Day.ShouldBe(6);
            date = new Month(1).The7th;
            date.Day.ShouldBe(7);
            date = new Month(1).The8th;
            date.Day.ShouldBe(8);
            date = new Month(1).The9th;
            date.Day.ShouldBe(9);
            date = new Month(1).The10th;
            date.Day.ShouldBe(10);
            date = new Month(1).The11th;
            date.Day.ShouldBe(11);
            date = new Month(1).The12th;
            date.Day.ShouldBe(12);
            date = new Month(1).The13th;
            date.Day.ShouldBe(13);
            date = new Month(1).The14th;
            date.Day.ShouldBe(14);
            date = new Month(1).The15th;
            date.Day.ShouldBe(15);
            date = new Month(1).The16th;
            date.Day.ShouldBe(16);
            date = new Month(1).The17th;
            date.Day.ShouldBe(17);
            date = new Month(1).The18th;
            date.Day.ShouldBe(18);
            date = new Month(1).The19th;
            date.Day.ShouldBe(19);
            date = new Month(1).The20th;
            date.Day.ShouldBe(20);
            date = new Month(1).The21st;
            date.Day.ShouldBe(21);
            date = new Month(1).The22nd;
            date.Day.ShouldBe(22);
            date = new Month(1).The23rd;
            date.Day.ShouldBe(23);
            date = new Month(1).The24th;
            date.Day.ShouldBe(24);
            date = new Month(1).The25th;
            date.Day.ShouldBe(25);
            date = new Month(1).The26th;
            date.Day.ShouldBe(26);
            date = new Month(1).The27th;
            date.Day.ShouldBe(27);
            date = new Month(1).The28th;
            date.Day.ShouldBe(28);
            date = new Month(1).The29th;
            date.Day.ShouldBe(29);
            date = new Month(1).The30th;
            date.Day.ShouldBe(30);
            date = new Month(1).The31st;
            date.Day.ShouldBe(31);
        }

        [Fact]
        public void Can_use_static_months()
        {
            DateTime date = January.The(1);
            date.Month.ShouldBe(1);

            date = February.The(1);
            date.Month.ShouldBe(2);

            date = March.The(1);
            date.Month.ShouldBe(3);

            date = April.The(1);
            date.Month.ShouldBe(4);

            date = May.The(1);
            date.Month.ShouldBe(5);

            date = June.The(1);
            date.Month.ShouldBe(6);

            date = July.The(1);
            date.Month.ShouldBe(7);

            date = August.The(1);
            date.Month.ShouldBe(8);

            date = September.The(1);
            date.Month.ShouldBe(9);

            date = October.The(1);
            date.Month.ShouldBe(10);

            date = November.The(1);
            date.Month.ShouldBe(11);

            date = December.The(1);
            date.Month.ShouldBe(12);
        }

        [Fact]
        public void Can_use_The_x_properties_with_static_months()
        {
            var date = January.The1st;
            date.Day.ShouldBe(1);
            date = January.The2nd;
            date.Day.ShouldBe(2);
            date = January.The3rd;
            date.Day.ShouldBe(3);
            date = January.The4th;
            date.Day.ShouldBe(4);
            date = January.The5th;
            date.Day.ShouldBe(5);
            date = January.The6th;
            date.Day.ShouldBe(6);
            date = January.The7th;
            date.Day.ShouldBe(7);
            date = January.The8th;
            date.Day.ShouldBe(8);
            date = January.The9th;
            date.Day.ShouldBe(9);
            date = January.The10th;
            date.Day.ShouldBe(10);
            date = January.The11th;
            date.Day.ShouldBe(11);

            date = January.The12th;
            date.Day.ShouldBe(12);
            date = January.The13th;
            date.Day.ShouldBe(13);
            date = January.The14th;
            date.Day.ShouldBe(14);
            date = January.The15th;
            date.Day.ShouldBe(15);
            date = January.The16th;
            date.Day.ShouldBe(16);
            date = January.The17th;
            date.Day.ShouldBe(17);
            date = January.The18th;
            date.Day.ShouldBe(18);
            date = January.The19th;
            date.Day.ShouldBe(19);
            date = January.The20th;
            date.Day.ShouldBe(20);
            date = January.The21st;
            date.Day.ShouldBe(21);
            date = January.The22nd;
            date.Day.ShouldBe(22);
            date = January.The23rd;
            date.Day.ShouldBe(23);
            date = January.The24th;
            date.Day.ShouldBe(24);
            date = January.The25th;
            date.Day.ShouldBe(25);
            date = January.The26th;
            date.Day.ShouldBe(26);
            date = January.The27th;
            date.Day.ShouldBe(27);
            date = January.The28th;
            date.Day.ShouldBe(28);
            date = January.The29th;
            date.Day.ShouldBe(29);
            date = January.The30th;
            date.Day.ShouldBe(30);
            date = January.The31st;
            date.Day.ShouldBe(31);
        }

        [Fact]
        public void Can_use_The_time()
        {
            var time = The.Time(09, 31);
            time.ShouldBe(new TimeSpan(0, 09, 31, 0));
        }

        [Fact]
        public void Can_use_The_time_with_seconds()
        {
            var time = The.Time(14, 15, 16);
            time.ShouldBe(new TimeSpan(0, 14, 15, 16));
        }

        [Fact]
        public void Can_use_At_time()
        {
            var time = At.Time(09, 31);
            var expected = new TimeSpan(0, 09, 31, 0);
            time.ShouldBe(expected);
        }

        [Fact]
        public void Can_use_At_time_with_seconds()
        {
            var time = At.Time(14, 15, 16);
            time.ShouldBe(new TimeSpan(0, 14, 15, 16));
        }

        [Fact]
        public void Can_use_all_the_months()
        {
            var date = new Month(1).The(1);
            date.Month.ShouldBe(1);
            date = new Month(2).The(1);
            date.Month.ShouldBe(2);
            date = new Month(3).The(1);
            date.Month.ShouldBe(3);
            date = new Month(4).The(1);
            date.Month.ShouldBe(4);
            date = new Month(5).The(1);
            date.Month.ShouldBe(5);
            date = new Month(6).The(1);
            date.Month.ShouldBe(6);
            date = new Month(7).The(1);
            date.Month.ShouldBe(7);
            date = new Month(8).The(1);
            date.Month.ShouldBe(8);
            date = new Month(9).The(1);
            date.Month.ShouldBe(9);
            date = new Month(10).The(1);
            date.Month.ShouldBe(10);
            date = new Month(11).The(1);
            date.Month.ShouldBe(11);
            date = new Month(12).The(1);
            date.Month.ShouldBe(12);
        }

        [Fact]
        public void Can_use_months_class()
        {
            var date = new Months(2009).January.The(1);
            date.Month.ShouldBe(1);

            date = new Months(2009).February.The(1);
            date.Month.ShouldBe(2);

            date = new Months(2009).March.The(1);
            date.Month.ShouldBe(3);

            date = new Months(2009).April.The(1);
            date.Month.ShouldBe(4);

            date = new Months(2009).May.The(1);
            date.Month.ShouldBe(5);

            date = new Months(2009).June.The(1);
            date.Month.ShouldBe(6);

            date = new Months(2009).July.The(1);
            date.Month.ShouldBe(7);

            date = new Months(2009).August.The(1);
            date.Month.ShouldBe(8);

            date = new Months(2009).September.The(1);
            date.Month.ShouldBe(9);

            date = new Months(2009).October.The(1);
            date.Month.ShouldBe(10);

            date = new Months(2009).November.The(1);
            date.Month.ShouldBe(11);

            date = new Months(2009).December.The(1);
            date.Month.ShouldBe(12);
        }

        [Fact]
        public void Can_use_On_class()
        {
            var date = On.January.The(1);
            date.Month.ShouldBe(1);

            date = On.February.The(1);
            date.Month.ShouldBe(2);

            date = On.March.The(1);
            date.Month.ShouldBe(3);

            date = On.April.The(1);
            date.Month.ShouldBe(4);

            date = On.May.The(1);
            date.Month.ShouldBe(5);

            date = On.June.The(1);
            date.Month.ShouldBe(6);

            date = On.July.The(1);
            date.Month.ShouldBe(7);

            date = On.August.The(1);
            date.Month.ShouldBe(8);

            date = On.September.The(1);
            date.Month.ShouldBe(9);

            date = On.October.The(1);
            date.Month.ShouldBe(10);

            date = On.November.The(1);
            date.Month.ShouldBe(11);

            date = On.December.The(1);
            date.Month.ShouldBe(12);
        }

        [Fact]
        public void Can_use_At_extension_method()
        {
            DateTime date = DateTime.Now;
            var dateWithTime = date.At(09, 31);
            var expected = new TimeSpan(0, 09, 31, 0);
            dateWithTime.TimeOfDay.ShouldBe(expected);
        }

        [Fact]
        public void Can_use_At_extension_method_with_seconds()
        {
            DateTime date = DateTime.Now;
            var dateWithTime = date.At(23, 20, 15);
            var expected = new TimeSpan(0, 23, 20, 15);
            dateWithTime.TimeOfDay.ShouldBe(expected);
        }

        [Fact]
        public void DoesNotAlterStaticProperties() 
        {
            var minDateTime = DateTime.MinValue;
            var maxDateTime = DateTime.MaxValue;

            var generatedDateTime = Builder<DateTime>.CreateNew().Build();

            DateTime.MinValue.ShouldBe(minDateTime);
            DateTime.MaxValue.ShouldBe(maxDateTime);
        }
    }
    // ReSharper restore AccessToStaticMemberViaDerivedType
}