using System;
using FizzWare.NBuilder.Dates;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FizzWare.NBuilder.Tests.Unit.Dates
{
    // ReSharper disable AccessToStaticMemberViaDerivedType
    [TestFixture]
    public class DatesTests
    {
        [Test]
        public void TheYear_x_uses_that_year_to_make_dates()
        {
            var months = The.Year(1999);
            var date = months.On.January.The(1);
            Assert.That(date.Year, Is.EqualTo(1999));
        }

        [Test]
        public void Can_use_Today_class()
        {
            var time = Today.At(09, 01);
            Assert.That(time, Is.EqualTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 01, 00)));
        }

        [Test]
        public void Can_use_Today_class_with_seconds()
        {
            var time = Today.At(09, 01, 05);
            Assert.That(time, Is.EqualTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 09, 01, 05)));
        }

        [Test]
        public void Can_use_The()
        {
            var year = DateTime.Now.Year;

            for (int i = 1; i < 13; i++)
            {
                for (int j = 1; j < 32; j++)
                {
                    if (i == 2 && j > 28)
                        break;

                    if ( (i == 9 || i == 4 || i == 6 || i == 11) && j > 30)
                        break;

                    var date = new Month(i).The(j);
                    Assert.That(date, Is.EqualTo(new DateTime(year, i, j)));
                }
            }
        }

        [Test]
        public void Can_use_The_x_properties()
        {
            DateTime date = new Month(1).The1st;
            Assert.That(date.Day, Is.EqualTo(1));
            date = new Month(1).The2nd;
            Assert.That(date.Day, Is.EqualTo(2));
            date = new Month(1).The3rd;
            Assert.That(date.Day, Is.EqualTo(3));
            date = new Month(1).The4th;
            Assert.That(date.Day, Is.EqualTo(4));
            date = new Month(1).The5th;
            Assert.That(date.Day, Is.EqualTo(5));
            date = new Month(1).The6th;
            Assert.That(date.Day, Is.EqualTo(6));
            date = new Month(1).The7th;
            Assert.That(date.Day, Is.EqualTo(7));
            date = new Month(1).The8th;
            Assert.That(date.Day, Is.EqualTo(8));
            date = new Month(1).The9th;
            Assert.That(date.Day, Is.EqualTo(9));
            date = new Month(1).The10th;
            Assert.That(date.Day, Is.EqualTo(10));
            date = new Month(1).The11th;
            Assert.That(date.Day, Is.EqualTo(11));
            date = new Month(1).The12th;
            Assert.That(date.Day, Is.EqualTo(12));
            date = new Month(1).The13th;
            Assert.That(date.Day, Is.EqualTo(13));
            date = new Month(1).The14th;
            Assert.That(date.Day, Is.EqualTo(14));
            date = new Month(1).The15th;
            Assert.That(date.Day, Is.EqualTo(15));
            date = new Month(1).The16th;
            Assert.That(date.Day, Is.EqualTo(16));
            date = new Month(1).The17th;
            Assert.That(date.Day, Is.EqualTo(17));
            date = new Month(1).The18th;
            Assert.That(date.Day, Is.EqualTo(18));
            date = new Month(1).The19th;
            Assert.That(date.Day, Is.EqualTo(19));
            date = new Month(1).The20th;
            Assert.That(date.Day, Is.EqualTo(20));
            date = new Month(1).The21st;
            Assert.That(date.Day, Is.EqualTo(21));
            date = new Month(1).The22nd;
            Assert.That(date.Day, Is.EqualTo(22));
            date = new Month(1).The23rd;
            Assert.That(date.Day, Is.EqualTo(23));
            date = new Month(1).The24th;
            Assert.That(date.Day, Is.EqualTo(24));
            date = new Month(1).The25th;
            Assert.That(date.Day, Is.EqualTo(25));
            date = new Month(1).The26th;
            Assert.That(date.Day, Is.EqualTo(26));
            date = new Month(1).The27th;
            Assert.That(date.Day, Is.EqualTo(27));
            date = new Month(1).The28th;
            Assert.That(date.Day, Is.EqualTo(28));
            date = new Month(1).The29th;
            Assert.That(date.Day, Is.EqualTo(29));
            date = new Month(1).The30th;
            Assert.That(date.Day, Is.EqualTo(30));
            date = new Month(1).The31st;
            Assert.That(date.Day, Is.EqualTo(31));
        }

        [Test]
        public void Can_use_static_months()
        {
            DateTime date = January.The(1);
            Assert.That(date.Month, Is.EqualTo(1));

            date = February.The(1);
            Assert.That(date.Month, Is.EqualTo(2));

            date = March.The(1);
            Assert.That(date.Month, Is.EqualTo(3));

            date = April.The(1);
            Assert.That(date.Month, Is.EqualTo(4));

            date = May.The(1);
            Assert.That(date.Month, Is.EqualTo(5));

            date = June.The(1);
            Assert.That(date.Month, Is.EqualTo(6));

            date = July.The(1);
            Assert.That(date.Month, Is.EqualTo(7));

            date = August.The(1);
            Assert.That(date.Month, Is.EqualTo(8));

            date = September.The(1);
            Assert.That(date.Month, Is.EqualTo(9));

            date = October.The(1);
            Assert.That(date.Month, Is.EqualTo(10));

            date = November.The(1);
            Assert.That(date.Month, Is.EqualTo(11));

            date = December.The(1);
            Assert.That(date.Month, Is.EqualTo(12));
        }

        [Test]
        public void Can_use_The_x_properties_with_static_months()
        {
            DateTime date;

            date = January.The1st;
            Assert.That(date.Day, Is.EqualTo(1));
            date = January.The2nd;
            Assert.That(date.Day, Is.EqualTo(2));
            date = January.The3rd;
            Assert.That(date.Day, Is.EqualTo(3));
            date = January.The4th;
            Assert.That(date.Day, Is.EqualTo(4));
            date = January.The5th;
            Assert.That(date.Day, Is.EqualTo(5));
            date = January.The6th;
            Assert.That(date.Day, Is.EqualTo(6));
            date = January.The7th;
            Assert.That(date.Day, Is.EqualTo(7));
            date = January.The8th;
            Assert.That(date.Day, Is.EqualTo(8));
            date = January.The9th;
            Assert.That(date.Day, Is.EqualTo(9));
            date = January.The10th;
            Assert.That(date.Day, Is.EqualTo(10));
            date = January.The11th;
            Assert.That(date.Day, Is.EqualTo(11));

            date = January.The12th;
            Assert.That(date.Day, Is.EqualTo(12));
            date = January.The13th;
            Assert.That(date.Day, Is.EqualTo(13));
            date = January.The14th;
            Assert.That(date.Day, Is.EqualTo(14));
            date = January.The15th;
            Assert.That(date.Day, Is.EqualTo(15));
            date = January.The16th;
            Assert.That(date.Day, Is.EqualTo(16));
            date = January.The17th;
            Assert.That(date.Day, Is.EqualTo(17));
            date = January.The18th;
            Assert.That(date.Day, Is.EqualTo(18));
            date = January.The19th;
            Assert.That(date.Day, Is.EqualTo(19));
            date = January.The20th;
            Assert.That(date.Day, Is.EqualTo(20));
            date = January.The21st;
            Assert.That(date.Day, Is.EqualTo(21));
            date = January.The22nd;
            Assert.That(date.Day, Is.EqualTo(22));
            date = January.The23rd;
            Assert.That(date.Day, Is.EqualTo(23));
            date = January.The24th;
            Assert.That(date.Day, Is.EqualTo(24));
            date = January.The25th;
            Assert.That(date.Day, Is.EqualTo(25));
            date = January.The26th;
            Assert.That(date.Day, Is.EqualTo(26));
            date = January.The27th;
            Assert.That(date.Day, Is.EqualTo(27));
            date = January.The28th;
            Assert.That(date.Day, Is.EqualTo(28));
            date = January.The29th;
            Assert.That(date.Day, Is.EqualTo(29));
            date = January.The30th;
            Assert.That(date.Day, Is.EqualTo(30));
            date = January.The31st;
            Assert.That(date.Day, Is.EqualTo(31));
        }

        [Test]
        public void Can_use_The_time()
        {
            var time = The.Time(09, 31);
            Assert.That(time, Is.EqualTo(new TimeSpan(0, 09, 31, 0)));
        }

        [Test]
        public void Can_use_The_time_with_seconds()
        {
            var time = The.Time(14, 15, 16);
            Assert.That(time, Is.EqualTo(new TimeSpan(0, 14, 15, 16)));
        }

        [Test]
        public void Can_use_At_time()
        {
            var time = At.Time(09, 31);
            Assert.That(time, Is.EqualTo(new TimeSpan(0, 09, 31, 0)));
        }

        [Test]
        public void Can_use_At_time_with_seconds()
        {
            var time = At.Time(14, 15, 16);
            Assert.That(time, Is.EqualTo(new TimeSpan(0, 14, 15, 16)));
        }

        [Test]
        public void Can_use_all_the_months()
        {
            DateTime date;

            date = new Month(1).The(1);
            Assert.That(date.Month, Is.EqualTo(1));
            date = new Month(2).The(1);
            Assert.That(date.Month, Is.EqualTo(2));
            date = new Month(3).The(1);
            Assert.That(date.Month, Is.EqualTo(3));
            date = new Month(4).The(1);
            Assert.That(date.Month, Is.EqualTo(4));
            date = new Month(5).The(1);
            Assert.That(date.Month, Is.EqualTo(5));
            date = new Month(6).The(1);
            Assert.That(date.Month, Is.EqualTo(6));
            date = new Month(7).The(1);
            Assert.That(date.Month, Is.EqualTo(7));
            date = new Month(8).The(1);
            Assert.That(date.Month, Is.EqualTo(8));
            date = new Month(9).The(1);
            Assert.That(date.Month, Is.EqualTo(9));
            date = new Month(10).The(1);
            Assert.That(date.Month, Is.EqualTo(10));
            date = new Month(11).The(1);
            Assert.That(date.Month, Is.EqualTo(11));
            date = new Month(12).The(1);
            Assert.That(date.Month, Is.EqualTo(12));
        }

        [Test]
        public void Can_use_months_class()
        {
            DateTime date;

            date = new Months(2009).January.The(1);
            Assert.That(date.Month, Is.EqualTo(1));

            date = new Months(2009).February.The(1);
            Assert.That(date.Month, Is.EqualTo(2));

            date = new Months(2009).March.The(1);
            Assert.That(date.Month, Is.EqualTo(3));

            date = new Months(2009).April.The(1);
            Assert.That(date.Month, Is.EqualTo(4));

            date = new Months(2009).May.The(1);
            Assert.That(date.Month, Is.EqualTo(5));

            date = new Months(2009).June.The(1);
            Assert.That(date.Month, Is.EqualTo(6));

            date = new Months(2009).July.The(1);
            Assert.That(date.Month, Is.EqualTo(7));

            date = new Months(2009).August.The(1);
            Assert.That(date.Month, Is.EqualTo(8));

            date = new Months(2009).September.The(1);
            Assert.That(date.Month, Is.EqualTo(9));

            date = new Months(2009).October.The(1);
            Assert.That(date.Month, Is.EqualTo(10));

            date = new Months(2009).November.The(1);
            Assert.That(date.Month, Is.EqualTo(11));

            date = new Months(2009).December.The(1);
            Assert.That(date.Month, Is.EqualTo(12));
        }

        [Test]
        public void Can_use_On_class()
        {
            DateTime date;

            date = On.January.The(1);
            Assert.That(date.Month, Is.EqualTo(1));

            date = On.February.The(1);
            Assert.That(date.Month, Is.EqualTo(2));

            date = On.March.The(1);
            Assert.That(date.Month, Is.EqualTo(3));

            date = On.April.The(1);
            Assert.That(date.Month, Is.EqualTo(4));

            date = On.May.The(1);
            Assert.That(date.Month, Is.EqualTo(5));

            date = On.June.The(1);
            Assert.That(date.Month, Is.EqualTo(6));

            date = On.July.The(1);
            Assert.That(date.Month, Is.EqualTo(7));

            date = On.August.The(1);
            Assert.That(date.Month, Is.EqualTo(8));

            date = On.September.The(1);
            Assert.That(date.Month, Is.EqualTo(9));

            date = On.October.The(1);
            Assert.That(date.Month, Is.EqualTo(10));

            date = On.November.The(1);
            Assert.That(date.Month, Is.EqualTo(11));

            date = On.December.The(1);
            Assert.That(date.Month, Is.EqualTo(12));
        }

        [Test]
        public void Can_use_At_extension_method()
        {
            DateTime date = DateTime.Now;
            var dateWithTime = date.At(09, 31);
            Assert.That(dateWithTime.TimeOfDay, Is.EqualTo(new TimeSpan(0, 09, 31, 0)));
        }

        [Test]
        public void Can_use_At_extension_method_with_seconds()
        {
            DateTime date = DateTime.Now;
            var dateWithTime = date.At(23, 20, 15);
            Assert.That(dateWithTime.TimeOfDay, Is.EqualTo(new TimeSpan(0, 23, 20, 15)));
        }
    }
    // ReSharper restore AccessToStaticMemberViaDerivedType
}