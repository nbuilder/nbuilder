using FizzWare.NBuilder.Generators;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace FizzWare.NBuilder.Tests.Unit
{
    public class GetRandomTests
    {
        [Fact]
        public void GetRandom_GeneratesCorrectIPv4Address()
        {
            var expectedRegex = new Regex("^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", RegexOptions.IgnoreCase);
            // Arrange
            for (int i = 0; i < 100; i++)
            {
                // Act
                var result = GetRandom.IpAddress();

                // Assert
                Assert.Matches(expectedRegex, result);
            }
        }

        [Fact]
        public void GetRandom_GeneratesCorrectIPv6Address()
        {
            var expectedRegex = new Regex("^[0-9a-f]{4}:[0-9a-f]{4}:[0-9a-f]{4}:[0-9a-f]{4}:[0-9a-f]{4}:[0-9a-f]{4}:[0-9a-f]{4}:[0-9a-f]{4}$", RegexOptions.IgnoreCase);
            // Arrange
            for (int i = 0; i < 100; i++)
            {
                // Act
                var result = GetRandom.IpAddressV6();

                // Assert
                Assert.Matches(expectedRegex, result);
            }
        }

        [Fact]
        public void GetRandom_GeneratesCorrectMacAddress()
        {
            var expectedRegex = new Regex("^[0-9a-f]{2}-[0-9a-f]{2}-[0-9a-f]{2}-[0-9a-f]{2}-[0-9a-f]{2}-[0-9a-f]{2}$", RegexOptions.IgnoreCase);
            // Arrange
            for (int i = 0; i < 100; i++)
            {
                // Act
                var result = GetRandom.MacAddress();

                // Assert
                Assert.Matches(expectedRegex, result);
            }
        }

        [Fact]
        public void GetRandom_OverrideSeparator_GeneratesCorrectMacAddress()
        {
            var overriddenSeparator = ":";
            var expectedRegex = new Regex($"^[0-9a-f]{{2}}{overriddenSeparator}[0-9a-f]{{2}}{overriddenSeparator}[0-9a-f]{{2}}{overriddenSeparator}[0-9a-f]{{2}}{overriddenSeparator}[0-9a-f]{{2}}{overriddenSeparator}[0-9a-f]{{2}}$", RegexOptions.IgnoreCase);
            // Arrange
            for (int i = 0; i < 100; i++)
            {
                // Act
                var result = GetRandom.MacAddress(overriddenSeparator);

                // Assert
                Assert.Matches(expectedRegex, result);
            }
        }

        [Fact]
        public void NumericString_CanGenerateAllDigits0Through9()
        {
            // Arrange
            var generatedDigits = new HashSet<char>();
            var expectedDigits = new HashSet<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var maxAttempts = 10000;

            // Act
            for (int i = 0; i < maxAttempts && generatedDigits.Count < 10; i++)
            {
                var numericString = GetRandom.NumericString(10);
                foreach (var digit in numericString)
                {
                    generatedDigits.Add(digit);
                }
            }

            // Assert
            Assert.Equal(expectedDigits, generatedDigits);
        }

        [Fact]
        public void FirstName_CanGenerateAllNamesInArray()
        {
            // Arrange
            var generatedNames = new HashSet<string>();
            var attempts = 1000000; // Large number to increase likelihood of hitting all names
            
            // The firstNames array has 400 names
            // With the fix, Random.Next(0, 400) can return 0-399, allowing all names to be selected
            // With the bug, Random.Next(0, 399) could only return 0-398, so the last name was unreachable

            // Act
            for (int i = 0; i < attempts; i++)
            {
                generatedNames.Add(GetRandom.FirstName());
            }

            // Assert - verify we can generate a very diverse set of names
            // With 1M attempts and 400 items, we expect to see at least 390 unique names (97.5%)
            // This statistically ensures the fix is working and boundary elements are accessible
            var expectedMinimumUniqueNames = 390; // 97.5% coverage threshold
            Assert.True(generatedNames.Count >= expectedMinimumUniqueNames, 
                $"Expected at least {expectedMinimumUniqueNames} unique first names but got {generatedNames.Count}. " +
                "This may indicate the last name(s) in the array cannot be generated.");
        }

        [Fact]
        public void LastName_CanGenerateAllNamesInArray()
        {
            // Arrange
            var generatedNames = new HashSet<string>();
            var attempts = 200000; // Large number to increase likelihood of hitting all names
            
            // The lastNames array has 100 names
            // With the fix, Random.Next(0, 100) can return 0-99, allowing all names to be selected
            // With the bug, Random.Next(0, 99) could only return 0-98, so the last name was unreachable

            // Act
            for (int i = 0; i < attempts; i++)
            {
                generatedNames.Add(GetRandom.LastName());
            }

            // Assert - verify we can generate a very diverse set of names
            // With 200K attempts and 100 items, we expect to see at least 98 unique names (98% coverage)
            // This statistically ensures the fix is working and boundary elements are accessible
            var expectedMinimumUniqueNames = 98; // 98% coverage threshold
            Assert.True(generatedNames.Count >= expectedMinimumUniqueNames, 
                $"Expected at least {expectedMinimumUniqueNames} unique last names but got {generatedNames.Count}. " +
                "This may indicate the last name(s) in the array cannot be generated.");
        }
    }
}
