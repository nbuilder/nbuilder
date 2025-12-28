using System;

namespace FizzWare.NBuilder.Generators
{
    /// <summary>
    /// Static class used to generate specific random data.
    /// </summary>
    [Obsolete("GetRandom will be removed in a future release. Please use RandomGenerator.Default instead for better testability and control.")]
    public static class GetRandom
    {
        private static readonly RandomGenerator _generator = RandomGenerator.Default;        
        
        /// <summary>
        /// Generate a random string of specified length that is composed only of the numeric characters 0-9.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        public static string NumericString(int length) => _generator.NumericString(length);

        /// <summary>
        /// Generate a random integer.
        /// </summary>
        public static int Int() => _generator.Int();

        /// <summary>
        /// Generate a random integer within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static int Int(int minValue, int maxValue) => _generator.Int(minValue, maxValue);

        /// <summary>
        /// Generate a random positive integer.
        /// </summary>
        public static int PositiveInt() => _generator.PositiveInt();

        /// <summary>
        /// Generate a random positive integer with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static int PositiveInt(int maxValue) => _generator.PositiveInt(maxValue);

        /// <summary>
        /// Generate a random short.
        /// </summary>
        public static short Short() => _generator.Short();

        /// <summary>
        /// Generate a random short within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static short Short(short minValue, short maxValue) => _generator.Short(minValue, maxValue);

        /// <summary>
        /// Generate a random positive short.
        /// </summary>
        public static short PositiveShort() => Short(0, short.MaxValue);

        /// <summary>
        /// Generate a random positive short with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static short PositiveShort(short maxValue) => _generator.PositiveShort(maxValue);

        /// <summary>
        /// Generate a random long.
        /// </summary>
        public static long Long() => _generator.Long();

        /// <summary>
        /// Generate a random long within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static long Long(long minValue, long maxValue) => _generator.Long(minValue, maxValue);

        /// <summary>
        /// Generate a random positive long.
        /// </summary>
        public static long PositiveLong() => _generator.PositiveLong();

        /// <summary>
        /// Generate a random positive long with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static long PositiveLong(long maxValue) => _generator.PositiveLong(maxValue);

        /// <summary>
        /// Generate a random uint.
        /// </summary>
        public static uint UInt() => _generator.UInt();

        /// <summary>
        /// Generate a random ulong.
        /// </summary>
        public static ulong ULong() => _generator.ULong();

        /// <summary>
        /// Generate a random ushort.
        /// </summary>
        public static ushort UShort() => _generator.UShort();

        /// <summary>
        /// Generate a random decimal.
        /// </summary>
        public static decimal Decimal() => _generator.Decimal();

        /// <summary>
        /// Generate a random decimal within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static decimal Decimal(decimal minValue, decimal maxValue) => _generator.Decimal(minValue, maxValue);

        /// <summary>
        /// Generate a random positive decimal.
        /// </summary>
        public static decimal PositiveDecimal() => _generator.PositiveDecimal();

        /// <summary>
        /// Generate a random positive decimal with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static decimal PositiveDecimal(decimal maxValue) => _generator.PositiveDecimal(maxValue);

        /// <summary>
        /// Generate a random float.
        /// </summary>
        public static float Float() => _generator.Float();

        /// <summary>
        /// Generate a random float within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static float Float(float minValue, float maxValue) => _generator.Float(minValue, maxValue);

        /// <summary>
        /// Generate a random positive float.
        /// </summary>
        public static float PositiveFloat() => _generator.PositiveFloat();

        /// <summary>
        /// Generate a random positive float with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static float PositiveFloat(float maxValue) => _generator.PositiveFloat(maxValue);

        /// <summary>
        /// Generate a random double.
        /// </summary>
        public static double Double() => _generator.Double();

        /// <summary>
        /// Generate a random double within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static double Double(double minValue, double maxValue) => _generator.Double(minValue, maxValue);

        /// <summary>
        /// Generate a random positive double.
        /// </summary>
        public static double PositiveDouble() => _generator.PositiveDouble();

        /// <summary>
        /// Generate a random positive double with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        public static double PositiveDouble(double maxValue) => _generator.PositiveDouble(maxValue);

        /// <summary>
        /// Generate a random DateTime.
        /// </summary>
        public static DateTime DateTime() => _generator.DateTime();

        /// <summary>
        /// Generate a random DateTime within a specified range.
        /// </summary>
        /// <param name="minValue">The earliest possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The latest possible value to generate. This value is exclusive.</param>
        /// <param name="kind">The DateTimeKind to generate the DateTime as.</param>
        public static DateTime DateTime(DateTime minValue, DateTime maxValue, DateTimeKind kind = DateTimeKind.Unspecified) => _generator.DateTime(minValue, maxValue, kind);

        /// <summary>
        /// Generate a random DateTime with a specified earliest DateTime value. The latest date will be the maximum value possible for a SQL Server datetime of December 31, 9999.
        /// </summary>
        /// <param name="minValue">The earliest possible value to generate.</param>
        /// <param name="kind">The DateTimeKind to generate the DateTime as.</param>
        public static DateTime DateTimeFrom(DateTime minValue, DateTimeKind kind = DateTimeKind.Unspecified) => _generator.DateTimeFrom(minValue, kind);

        /// <summary>
        /// Generate a random DateTime with a specified latest DateTime value. The earliest date will be the minimum value possible for a SQL Server datetime of January 1, 1753.
        /// </summary>
        /// <param name="maxValue">The latest possible value to generate.</param>
        /// <param name="kind">The DateTimeKind to generate the DateTime as.</param>
        public static DateTime DateTimeThrough(DateTime maxValue, DateTimeKind kind = DateTimeKind.Unspecified) => _generator.DateTimeThrough(maxValue, kind);

        /// <summary>
        /// Generate a random bool value.
        /// </summary>
        public static bool Boolean() => _generator.Boolean();

        /// <summary>
        /// Generate a random byte.
        /// </summary>
        public static byte Byte() => _generator.Byte();

        /// <summary>
        /// Generate a random sbyte.
        /// </summary>
        public static sbyte SByte() => _generator.SByte();

        /// <summary>
        /// Generate a random char.
        /// </summary>
        public static char Char() => _generator.Char();

        /// <summary>
        /// Generate a random Guid.
        /// </summary>
        public static Guid Guid() => _generator.Guid();

        /// <summary>
        /// Generate a random first name from a pre-defined list of names.
        /// </summary>
        public static string FirstName() => _generator.FirstName();

        /// <summary>
        /// Generate a random last name from a pre-defined list of names.
        /// </summary>
        public static string LastName() => _generator.LastName();

        /// <summary>
        /// Generate a random phrase using words from Lorem Ipsum as a string that is at most the specified length.
        /// </summary>
        /// <param name="length">The maximum length the phrase should be.</param>
        public static string Phrase(int length) => _generator.Phrase(length);

        /// <summary>
        /// Generate a random string of specified length. Each letter will randomly be uppercase or lowercase.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        public static string String(int length) => _generator.String(length, null, null);

        /// <summary>
        /// Generate a random string of specified length. Every letter will be uppercase.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        public static string UpperCaseString(int length) => _generator.String(length, true, null);

        /// <summary>
        /// Generate a random string of specified length. Every letter will be lowercase.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        public static string LowerCaseString(int length) => _generator.String(length, false, null);

        /// <summary>
        /// Generate a random string of specified length. Can choose if the string should be entirely uppercase or lowercase. Can also choose any characters that should not be included.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <param name="upperCase">Whether or not the string should be uppercase. Leave null to have each character randomly determined as uppercase or lowercase.</param>
        /// <param name="characterToExclude">Any characters that should not be included in the randomly generated string.</param>
        public static string String(int length, bool? upperCase, params char[] characterToExclude) => _generator.String(length, upperCase, characterToExclude);

        /// <summary>
        /// Generate a random character.
        /// </summary>
        /// <param name="upperCase">Whether or not the character should be uppercase. If specified as null, case will be randomly determined.</param>
        public static char Letter(bool? upperCase) => _generator.Letter(upperCase);

        /// <summary>
        /// This class can randomly generate data specifically for the United Kingdom.
        /// </summary>
        public static class UK
        {
            private static readonly RandomGenerator.UK _ukGenerator = RandomGenerator.UK.Default;

            /// <summary>
            /// Generate a random phone number in the format #### ### ###
            /// </summary>
            public static string PhoneNumber() => _ukGenerator.PhoneNumber();

            /// <summary>
            /// Generate a random county in the United Kingdom.
            /// </summary>
            public static string County() => _ukGenerator.County();
        }

        /// <summary>
        /// This class can randomly generate data specifically for the United States.
        /// </summary>
        public static class Usa
        {
            private static readonly RandomGenerator.USA _usaGenerator = RandomGenerator.USA.Default;

            /// <summary>
            /// Generate a random phone number in the format ###-###-####.
            /// </summary>
            public static string PhoneNumber() => _usaGenerator.PhoneNumber();

            /// <summary>
            /// Generate a random SSN in the format of ###-##-####.
            /// </summary>
            public static string SocialSecurityNumber() => _usaGenerator.SocialSecurityNumber();

            public static string State() => _usaGenerator.State();
        }

        /// <summary>
        /// Generate a random e-mail address on a .com domain.
        /// </summary>
        public static string Email() => _generator.Email();

        /// <summary>
        /// Generate a random website URL preceded with www.
        /// </summary>
        public static string WwwUrl() => _generator.WwwUrl();

        /// <summary>
        /// Generate a random website URL with a subdomain, but not beginning with www.
        /// </summary>
        public static string Url() => _generator.Url();

        /// <summary>
        /// Generate a random IPv4 Address.
        /// </summary>
        public static string IpAddress() => _generator.IpAddress();

        /// <summary>
        /// Generate a random IPv6 Address.
        /// </summary>
        public static string IpAddressV6() => _generator.IpAddressV6();

        /// <summary>
        /// Generate a random MAC address.
        /// </summary>
        /// <param name="separator">Optional to override the default separator from - used in IEEE 802</param>
        public static string MacAddress(string separator = "-") => _generator.MacAddress(separator);

        /// <summary>
        /// Get a random option from a specified enum type.
        /// </summary>
        /// <typeparam name="T">An enum type</typeparam>
        public static T Enumeration<T>() where T : struct => _generator.Enumeration<T>();

        /// <summary>
        /// Get a random option from a specified enum type.
        /// </summary>
        /// <param name="type">The enum type you wish to get a random value of.</param>
        public static Enum Enumeration(Type type) => _generator.Enumeration(type);
    }
}