using FizzWare.NBuilder.Generators;
using System.Text.RegularExpressions;
using Xunit;

namespace FizzWare.NBuilder.Tests.Unit
{
    public class GetRandomUsaTests
    {
        [Fact]
        public void GetRandomUsa_GeneratesCorrectAddressLine1()
        {
            // Arrange
            var expectedRegex = new Regex("^\\d{2,4} ([a-z]+|[a-z]+ ?[a-z]+) [a-z]+$", RegexOptions.IgnoreCase);
            for (int i = 0; i < 100; i++)
            {
                // Act
                var result = GetRandom.Usa.AddressLine1();

                // Assert
                Assert.Matches(expectedRegex, result);
            }
        }

        [Fact]
        public void GetRandomUsa_GeneratesCorrectAddressLine2()
        {
            // Arrange
            var expectedRegex = new Regex("^[a-z]+ \\d{2,4}$", RegexOptions.IgnoreCase);
            for (int i = 0; i < 100; i++)
            {
                // Act
                var result = GetRandom.Usa.AddressLine2();

                // Assert
                Assert.Matches(expectedRegex, result);
            }
        }

        [Fact]
        public void GetRandomUsa_GeneratesCorrectCity()
        {
            // Arrange
            var expectedRegex = new Regex("^([a-z]+|[a-z]+ .[a-z]*)$", RegexOptions.IgnoreCase);
            for (int i = 0; i < 100; i++)
            {
                // Act
                var result = GetRandom.Usa.City();

                // Assert
                Assert.Matches(expectedRegex, result);
            }
        }

        [Fact]
        public void GetRandomUsa_GeneratesCorrectState()
        {
            // Arrange
            var expectedRegex = new Regex("^[A-Z]{2}$");
            for (int i = 0; i < 100; i++)
            {
                // Act
                var result = GetRandom.Usa.State();

                // Assert
                Assert.Matches(expectedRegex, result);
            }
        }

        [Fact]
        public void GetRandomUsa_GeneratesCorrectZipCodeShort()
        {
            // Arrange
            var expectedRegex = new Regex("^\\d{5}$", RegexOptions.IgnoreCase);
            for (int i = 0; i < 100; i++)
            {
                // Act
                var result = GetRandom.Usa.ZipCodeShort();

                // Assert
                Assert.Matches(expectedRegex, result);
            }
        }

        [Fact]
        public void GetRandomUsa_GeneratesCorrectZipCodeLong()
        {
            // Arrange
            var expectedRegex = new Regex("^\\d{5}-\\d{4}$", RegexOptions.IgnoreCase);
            for (int i = 0; i < 100; i++)
            {
                // Act
                var result = GetRandom.Usa.ZipCodeLong();

                // Assert
                Assert.Matches(expectedRegex, result);
            }
        }
    }
}
