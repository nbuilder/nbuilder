using System;
using NUnit.Framework;

namespace FizzWare.NBuilder.Tests.Unit
{
    [TestFixture]
    public class SequentialGeneratorDateTimeTests
    {
        private SequentialGenerator<DateTime> generator;
        private DateTime startingValue;

        [SetUp]
        public void SetUp()
        {
            generator = new SequentialGenerator<DateTime>();
            startingValue = new DateTime(9, 9, 9, 9, 9, 9, 9);
            generator.StartingWith(startingValue);
        }

        [Test]
        public void Generate_DefaultSetUp_IncrementsFromMinDateTimeValue()
        {
            generator = new SequentialGenerator<DateTime>();
            Assert.That(generator.Generate(), Is.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void Generate_DefaultIncrement_IncrementsByDay()
        {
            DateTime oneDay = startingValue.AddDays(1);
            DateTime twoDays = startingValue.AddDays(2);

            generator.Generate();            
            
            Assert.That(generator.Generate(), Is.EqualTo(oneDay));
            Assert.That(generator.Generate(), Is.EqualTo(twoDays));
        }

        [Test]
        public void Generate_TicksIncrement_AllowsIncrementingByTicks()
        {
            DateTime oneTick = startingValue.AddTicks(1);
            DateTime twoTicks = startingValue.AddTicks(2);

            generator.IncrementDateBy = IncrementDate.Tick;
            generator.Generate();

            Assert.That(generator.Generate().Ticks, Is.EqualTo(oneTick.Ticks));
            Assert.That(generator.Generate(), Is.EqualTo(twoTicks));        
        }

        [Test]
        public void Generate_MillisecondsIncrement_IncrementsByMilliseconds()
        {
            DateTime oneMillisecond = startingValue.AddMilliseconds(1);
            DateTime twoMilliseconds = startingValue.AddMilliseconds(2);

            generator.IncrementDateBy = IncrementDate.Millisecond;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(oneMillisecond));
            Assert.That(generator.Generate(), Is.EqualTo(twoMilliseconds));
        }

        [Test]
        public void Generate_SecondsIncrement_IncrementsBySeconds()
        {
            DateTime oneSecond = startingValue.AddSeconds(1);
            DateTime twoSeconds = startingValue.AddSeconds(2);

            generator.IncrementDateBy = IncrementDate.Second;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(oneSecond));
            Assert.That(generator.Generate(), Is.EqualTo(twoSeconds));
        }

        [Test]
        public void Generate_MinutesIncrement_IncrementsByMinutes()
        {
            DateTime oneMinute = startingValue.AddMinutes(1);
            DateTime twoMinutes = startingValue.AddMinutes(2);

            generator.IncrementDateBy = IncrementDate.Minute;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(oneMinute));
            Assert.That(generator.Generate(), Is.EqualTo(twoMinutes));
        }

        [Test]
        public void Generate_HoursIncrement_IncrementsByHours()
        {
            DateTime oneHour = startingValue.AddHours(1);
            DateTime twoHours = startingValue.AddHours(2);

            generator.IncrementDateBy = IncrementDate.Hour;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(oneHour));
            Assert.That(generator.Generate(), Is.EqualTo(twoHours));
        }

        [Test]
        public void Generate_DaysIncrement_IncrementsByDays()
        {
            DateTime oneDay = startingValue.AddDays(1);
            DateTime twoDays = startingValue.AddDays(2);

            generator.IncrementDateBy = IncrementDate.Day;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(oneDay));
            Assert.That(generator.Generate(), Is.EqualTo(twoDays));
        }

        [Test]
        public void Generate_MonthsIncrement_IncrementsByMonths()
        {
            DateTime oneMonth = startingValue.AddMonths(1);
            DateTime twoMonths = startingValue.AddMonths(2);

            generator.IncrementDateBy = IncrementDate.Month;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(oneMonth));
            Assert.That(generator.Generate(), Is.EqualTo(twoMonths));
        }

        [Test]
        public void Generate_YearsIncrement_IncrementsByYears()
        {
            DateTime oneYear = startingValue.AddYears(1);
            DateTime twoYears = startingValue.AddYears(2);

            generator.IncrementDateBy = IncrementDate.Year;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(oneYear));
            Assert.That(generator.Generate(), Is.EqualTo(twoYears));
        }

        [Test]
        public void Generate_TicksDecrement_AllowsDecrementingByTicks()
        {
            DateTime oneTick = startingValue.AddTicks(-1);
            DateTime twoTicks = startingValue.AddTicks(-2);

            generator.IncrementDateBy = IncrementDate.Tick;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            Assert.That(generator.Generate().Ticks, Is.EqualTo(oneTick.Ticks));
            Assert.That(generator.Generate(), Is.EqualTo(twoTicks));
        }

        [Test]
        public void Generate_MillisecondsDecrement_DecrementsByMilliseconds()
        {
            DateTime oneMillisecond = startingValue.AddMilliseconds(-1);
            DateTime twoMilliseconds = startingValue.AddMilliseconds(-2);

            generator.IncrementDateBy = IncrementDate.Millisecond;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(oneMillisecond));
            Assert.That(generator.Generate(), Is.EqualTo(twoMilliseconds));
        }

        [Test]
        public void Generate_SecondsDecrement_DecrementsBySeconds()
        {
            DateTime oneSecond = startingValue.AddSeconds(-1);
            DateTime twoSeconds = startingValue.AddSeconds(-2);

            generator.IncrementDateBy = IncrementDate.Second;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(oneSecond));
            Assert.That(generator.Generate(), Is.EqualTo(twoSeconds));
        }

        [Test]
        public void Generate_MinutesDecrement_DecrementsByMinutes()
        {
            DateTime oneMinute = startingValue.AddMinutes(-1);
            DateTime twoMinutes = startingValue.AddMinutes(-2);

            generator.IncrementDateBy = IncrementDate.Minute;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(oneMinute));
            Assert.That(generator.Generate(), Is.EqualTo(twoMinutes));
        }

        [Test]
        public void Generate_HoursDecrement_DecrementsByHours()
        {
            DateTime oneHour = startingValue.AddHours(-1);
            DateTime twoHours = startingValue.AddHours(-2);

            generator.IncrementDateBy = IncrementDate.Hour;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(oneHour));
            Assert.That(generator.Generate(), Is.EqualTo(twoHours));
        }

        [Test]
        public void Generate_DaysDecrement_DecrementsByDays()
        {
            DateTime oneDay = startingValue.AddDays(-1);
            DateTime twoDays = startingValue.AddDays(-2);

            generator.IncrementDateBy = IncrementDate.Day;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(oneDay));
            Assert.That(generator.Generate(), Is.EqualTo(twoDays));
        }

        [Test]
        public void Generate_MonthsDecrement_DecrementsByMonths()
        {
            DateTime oneMonth = startingValue.AddMonths(-1);
            DateTime twoMonths = startingValue.AddMonths(-2);

            generator.IncrementDateBy = IncrementDate.Month;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(oneMonth));
            Assert.That(generator.Generate(), Is.EqualTo(twoMonths));
        }

        [Test]
        public void Generate_YearsDecrement_DecrementsByYears()
        {
            DateTime oneYear = startingValue.AddYears(-1);
            DateTime twoYears = startingValue.AddYears(-2);

            generator.IncrementDateBy = IncrementDate.Year;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(oneYear));
            Assert.That(generator.Generate(), Is.EqualTo(twoYears));
        }

        [Test]
        public void Generate_MultiValueIncrement_AllowsDatesToBeIncrementedByValuesGreaterThanOne()
        {
            const double increment = 2;

            DateTime expectedIncrementedDate = startingValue.AddMilliseconds(increment);

            generator.IncrementDateBy = IncrementDate.Millisecond;
            generator.IncrementDateValueBy = increment;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(expectedIncrementedDate));
        }

        [Test]
        public void Generate_MultiValueDecrement_AllowsDatesToBeDecrementedByValuesGreaterThanOne()
        {
            const double increment = 2;

            DateTime expectedIncrementedDate = startingValue.AddMilliseconds(-increment);

            generator.IncrementDateBy = IncrementDate.Millisecond;
            generator.IncrementDateValueBy = increment;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            Assert.That(generator.Generate(), Is.EqualTo(expectedIncrementedDate));

        }

        // TODO FIX
        #if !SILVERLIGHT
        [Test]
        public void Generate_IncrementDaysMoreThanMaximumAllowedValue_ThrowsException()
        {
            generator.StartingWith(DateTime.MaxValue);
            generator.IncrementDateBy = IncrementDate.Day;
            generator.Generate();
            Assert.Throws<ArgumentOutOfRangeException>(() => generator.Generate());
        }
        
        [Test]
        public void Generate_IncrementTicksMoreThanMaximumAllowedValue_ThrowsException()
        {
            generator.StartingWith(DateTime.MaxValue);
            generator.IncrementDateBy = IncrementDate.Tick;
            generator.Generate();
            Assert.Throws<ArgumentOutOfRangeException>(() => generator.Generate());        
        }
      
        [Test]
        public void Generate_IncrementMonthsMoreThanMaximumAllowedValue_ThrowsException()
        {
            generator.StartingWith(DateTime.MaxValue);
            generator.IncrementDateBy = IncrementDate.Month;
            generator.Generate();
            Assert.Throws<ArgumentOutOfRangeException>(() => generator.Generate());
        }

        [Test]
        public void Generate_IncrementYearsMoreThanMaximumAllowedValue_ThrowsException()
        {
            generator.StartingWith(DateTime.MaxValue);
            generator.IncrementDateBy = IncrementDate.Year;
            generator.Generate();
            Assert.Throws<ArgumentOutOfRangeException>(() => generator.Generate());
        }

        #endif
    }
}
