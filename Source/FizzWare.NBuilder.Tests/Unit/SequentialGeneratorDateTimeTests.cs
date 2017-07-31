using System;
using NUnit.Framework;
using Shouldly;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace FizzWare.NBuilder.Tests.Unit
{
    
    public class SequentialGeneratorDateTimeTests
    {
        private SequentialGenerator<DateTime> generator;
        private DateTime startingValue;

        public SequentialGeneratorDateTimeTests()
        {
            generator = new SequentialGenerator<DateTime>();
            startingValue = new DateTime(9, 9, 9, 9, 9, 9, 9);
            generator.StartingWith(startingValue);
        }

        [Fact]
        public void Generate_DefaultSetUp_IncrementsFromMinDateTimeValue()
        {
            generator = new SequentialGenerator<DateTime>();
            generator.Generate().ShouldBe(DateTime.MinValue);
        }

        [Fact]
        public void Generate_DefaultIncrement_IncrementsByDay()
        {
            DateTime oneDay = startingValue.AddDays(1);
            DateTime twoDays = startingValue.AddDays(2);

            generator.Generate();            
            
            generator.Generate().ShouldBe(oneDay);
            generator.Generate().ShouldBe(twoDays);
        }

        [Fact]
        public void Generate_TicksIncrement_AllowsIncrementingByTicks()
        {
            DateTime oneTick = startingValue.AddTicks(1);
            DateTime twoTicks = startingValue.AddTicks(2);

            generator.IncrementDateBy = IncrementDate.Tick;
            generator.Generate();

            generator.Generate().Ticks.ShouldBe(oneTick.Ticks);
            generator.Generate().ShouldBe(twoTicks);        
        }

        [Fact]
        public void Generate_MillisecondsIncrement_IncrementsByMilliseconds()
        {
            DateTime oneMillisecond = startingValue.AddMilliseconds(1);
            DateTime twoMilliseconds = startingValue.AddMilliseconds(2);

            generator.IncrementDateBy = IncrementDate.Millisecond;
            generator.Generate();

            generator.Generate().ShouldBe(oneMillisecond);
            generator.Generate().ShouldBe(twoMilliseconds);
        }

        [Fact]
        public void Generate_SecondsIncrement_IncrementsBySeconds()
        {
            DateTime oneSecond = startingValue.AddSeconds(1);
            DateTime twoSeconds = startingValue.AddSeconds(2);

            generator.IncrementDateBy = IncrementDate.Second;
            generator.Generate();

            generator.Generate().ShouldBe(oneSecond);
            generator.Generate().ShouldBe(twoSeconds);
        }

        [Fact]
        public void Generate_MinutesIncrement_IncrementsByMinutes()
        {
            DateTime oneMinute = startingValue.AddMinutes(1);
            DateTime twoMinutes = startingValue.AddMinutes(2);

            generator.IncrementDateBy = IncrementDate.Minute;
            generator.Generate();

            generator.Generate().ShouldBe(oneMinute);
            generator.Generate().ShouldBe(twoMinutes);
        }

        [Fact]
        public void Generate_HoursIncrement_IncrementsByHours()
        {
            DateTime oneHour = startingValue.AddHours(1);
            DateTime twoHours = startingValue.AddHours(2);

            generator.IncrementDateBy = IncrementDate.Hour;
            generator.Generate();

            generator.Generate().ShouldBe(oneHour);
            generator.Generate().ShouldBe(twoHours);
        }

        [Fact]
        public void Generate_DaysIncrement_IncrementsByDays()
        {
            DateTime oneDay = startingValue.AddDays(1);
            DateTime twoDays = startingValue.AddDays(2);

            generator.IncrementDateBy = IncrementDate.Day;
            generator.Generate();

            generator.Generate().ShouldBe(oneDay);
            generator.Generate().ShouldBe(twoDays);
        }

        [Fact]
        public void Generate_MonthsIncrement_IncrementsByMonths()
        {
            DateTime oneMonth = startingValue.AddMonths(1);
            DateTime twoMonths = startingValue.AddMonths(2);

            generator.IncrementDateBy = IncrementDate.Month;
            generator.Generate();

            generator.Generate().ShouldBe(oneMonth);
            generator.Generate().ShouldBe(twoMonths);
        }

        [Fact]
        public void Generate_YearsIncrement_IncrementsByYears()
        {
            DateTime oneYear = startingValue.AddYears(1);
            DateTime twoYears = startingValue.AddYears(2);

            generator.IncrementDateBy = IncrementDate.Year;
            generator.Generate();

            generator.Generate().ShouldBe(oneYear);
            generator.Generate().ShouldBe(twoYears);
        }

        [Fact]
        public void Generate_TicksDecrement_AllowsDecrementingByTicks()
        {
            DateTime oneTick = startingValue.AddTicks(-1);
            DateTime twoTicks = startingValue.AddTicks(-2);

            generator.IncrementDateBy = IncrementDate.Tick;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            generator.Generate().Ticks.ShouldBe(oneTick.Ticks);
            generator.Generate().ShouldBe(twoTicks);
        }

        [Fact]
        public void Generate_MillisecondsDecrement_DecrementsByMilliseconds()
        {
            DateTime oneMillisecond = startingValue.AddMilliseconds(-1);
            DateTime twoMilliseconds = startingValue.AddMilliseconds(-2);

            generator.IncrementDateBy = IncrementDate.Millisecond;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            generator.Generate().ShouldBe(oneMillisecond);
            generator.Generate().ShouldBe(twoMilliseconds);
        }

        [Fact]
        public void Generate_SecondsDecrement_DecrementsBySeconds()
        {
            DateTime oneSecond = startingValue.AddSeconds(-1);
            DateTime twoSeconds = startingValue.AddSeconds(-2);

            generator.IncrementDateBy = IncrementDate.Second;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            generator.Generate().ShouldBe(oneSecond);
            generator.Generate().ShouldBe(twoSeconds);
        }

        [Fact]
        public void Generate_MinutesDecrement_DecrementsByMinutes()
        {
            DateTime oneMinute = startingValue.AddMinutes(-1);
            DateTime twoMinutes = startingValue.AddMinutes(-2);

            generator.IncrementDateBy = IncrementDate.Minute;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            generator.Generate().ShouldBe(oneMinute);
            generator.Generate().ShouldBe(twoMinutes);
        }

        [Fact]
        public void Generate_HoursDecrement_DecrementsByHours()
        {
            DateTime oneHour = startingValue.AddHours(-1);
            DateTime twoHours = startingValue.AddHours(-2);

            generator.IncrementDateBy = IncrementDate.Hour;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            generator.Generate().ShouldBe(oneHour);
            generator.Generate().ShouldBe(twoHours);
        }

        [Fact]
        public void Generate_DaysDecrement_DecrementsByDays()
        {
            DateTime oneDay = startingValue.AddDays(-1);
            DateTime twoDays = startingValue.AddDays(-2);

            generator.IncrementDateBy = IncrementDate.Day;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            generator.Generate().ShouldBe(oneDay);
            generator.Generate().ShouldBe(twoDays);
        }

        [Fact]
        public void Generate_MonthsDecrement_DecrementsByMonths()
        {
            DateTime oneMonth = startingValue.AddMonths(-1);
            DateTime twoMonths = startingValue.AddMonths(-2);

            generator.IncrementDateBy = IncrementDate.Month;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            generator.Generate().ShouldBe(oneMonth);
            generator.Generate().ShouldBe(twoMonths);
        }

        [Fact]
        public void Generate_YearsDecrement_DecrementsByYears()
        {
            DateTime oneYear = startingValue.AddYears(-1);
            DateTime twoYears = startingValue.AddYears(-2);

            generator.IncrementDateBy = IncrementDate.Year;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            generator.Generate().ShouldBe(oneYear);
            generator.Generate().ShouldBe(twoYears);
        }

        [Fact]
        public void Generate_MultiValueIncrement_AllowsDatesToBeIncrementedByValuesGreaterThanOne()
        {
            const double increment = 2;

            DateTime expectedIncrementedDate = startingValue.AddMilliseconds(increment);

            generator.IncrementDateBy = IncrementDate.Millisecond;
            generator.IncrementDateValueBy = increment;
            generator.Generate();

            generator.Generate().ShouldBe(expectedIncrementedDate);
        }

        [Fact]
        public void Generate_MultiValueDecrement_AllowsDatesToBeDecrementedByValuesGreaterThanOne()
        {
            const double increment = 2;

            DateTime expectedIncrementedDate = startingValue.AddMilliseconds(-increment);

            generator.IncrementDateBy = IncrementDate.Millisecond;
            generator.IncrementDateValueBy = increment;
            generator.Direction = GeneratorDirection.Descending;
            generator.Generate();

            generator.Generate().ShouldBe(expectedIncrementedDate);

        }

        // TODO FIX
        #if !SILVERLIGHT
        [Fact]
        public void Generate_IncrementDaysMoreThanMaximumAllowedValue_ThrowsException()
        {
            generator.StartingWith(DateTime.MaxValue);
            generator.IncrementDateBy = IncrementDate.Day;
            generator.Generate();
            Should.Throw<ArgumentOutOfRangeException>(() => generator.Generate());
        }
        
        [Fact]
        public void Generate_IncrementTicksMoreThanMaximumAllowedValue_ThrowsException()
        {
            generator.StartingWith(DateTime.MaxValue);
            generator.IncrementDateBy = IncrementDate.Tick;
            generator.Generate();
            Should.Throw<ArgumentOutOfRangeException>(() => generator.Generate());        
        }
      
        [Fact]
        public void Generate_IncrementMonthsMoreThanMaximumAllowedValue_ThrowsException()
        {
            generator.StartingWith(DateTime.MaxValue);
            generator.IncrementDateBy = IncrementDate.Month;
            generator.Generate();
            Should.Throw<ArgumentOutOfRangeException>(() => generator.Generate());
        }

        [Fact]
        public void Generate_IncrementYearsMoreThanMaximumAllowedValue_ThrowsException()
        {
            generator.StartingWith(DateTime.MaxValue);
            generator.IncrementDateBy = IncrementDate.Year;
            generator.Generate();
            Should.Throw<ArgumentOutOfRangeException>(() => generator.Generate());
        }

        #endif
    }
}
