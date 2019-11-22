using FizzWare.NBuilder.Generators;
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
    }
}
