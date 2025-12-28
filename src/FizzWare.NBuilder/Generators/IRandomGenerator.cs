using System;
using System.Reflection.Emit;

namespace FizzWare.NBuilder
{
    public interface IRandomGenerator
    {
        ushort Next(ushort min, ushort max);
        uint Next(uint min, uint max);
        ulong Next(ulong min, ulong max);

        short Next(short min, short max);
        int Next(int min, int max);
        long Next(long min, long max);

        float Next(float min, float max);
        double Next(double min, double max);
        decimal Next(decimal min, decimal max);

        char Next(char min, char max);
        byte Next(byte min, byte max);
        sbyte Next(sbyte min, sbyte max);

        DateTime Next(DateTime min, DateTime max, DateTimeKind kind = DateTimeKind.Unspecified);

        bool Next();

        /// <summary>
        /// Generate a random string of specified length that is composed only of the numeric characters 0-9.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        string NumericString(int length);

        /// <summary>
        /// Generate a random integer.
        /// </summary>
        int Int();

        /// <summary>
        /// Generate a random integer within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        int Int(int minValue, int maxValue);

        /// <summary>
        /// Generate a random positive integer.
        /// </summary>
        int PositiveInt();

        /// <summary>
        /// Generate a random positive integer with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        int PositiveInt(int maxValue);

        /// <summary>
        /// Generate a random short.
        /// </summary>
        short Short();

        /// <summary>
        /// Generate a random short within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        short Short(short minValue, short maxValue);

        /// <summary>
        /// Generate a random positive short.
        /// </summary>
        short PositiveShort();

        /// <summary>
        /// Generate a random positive short with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        short PositiveShort(short maxValue);

        /// <summary>
        /// Generate a random long.
        /// </summary>
        long Long();

        /// <summary>
        /// Generate a random long within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        long Long(long minValue, long maxValue);

        /// <summary>
        /// Generate a random positive long.
        /// </summary>
        long PositiveLong();

        /// <summary>
        /// Generate a random positive long with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        long PositiveLong(long maxValue);

        /// <summary>
        /// Generate a random uint.
        /// </summary>
        uint UInt();

        /// <summary>
        /// Generate a random ulong.
        /// </summary>
        ulong ULong();

        /// <summary>
        /// Generate a random ushort.
        /// </summary>
        ushort UShort();

        /// <summary>
        /// Generate a random decimal.
        /// </summary>
        decimal Decimal();

        /// <summary>
        /// Generate a random decimal within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        decimal Decimal(decimal minValue, decimal maxValue);

        /// <summary>
        /// Generate a random positive decimal.
        /// </summary>
        decimal PositiveDecimal();

        /// <summary>
        /// Generate a random positive decimal with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        decimal PositiveDecimal(decimal maxValue);

        /// <summary>
        /// Generate a random float.
        /// </summary>
        float Float();

        /// <summary>
        /// Generate a random float within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        float Float(float minValue, float maxValue);

        /// <summary>
        /// Generate a random positive float.
        /// </summary>
        float PositiveFloat();

        /// <summary>
        /// Generate a random positive float with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        float PositiveFloat(float maxValue);

        /// <summary>
        /// Generate a random double.
        /// </summary>
        double Double();

        /// <summary>
        /// Generate a random double within a specified range.
        /// </summary>
        /// <param name="minValue">The minimum possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        double Double(double minValue, double maxValue);

        /// <summary>
        /// Generate a random positive double.
        /// </summary>
        double PositiveDouble();

        /// <summary>
        /// Generate a random positive double with a specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum possible value to generate. This value is exclusive.</param>
        double PositiveDouble(double maxValue);

        /// <summary>
        /// Generate a random DateTime.
        /// </summary>
        DateTime DateTime();

        /// <summary>
        /// Generate a random DateTime within a specified range.
        /// </summary>
        /// <param name="minValue">The earliest possible value to generate. This value is inclusive.</param>
        /// <param name="maxValue">The latest possible value to generate. This value is exclusive.</param>
        /// <param name="kind">The DateTimeKind to generate the DateTime as.</param>
        DateTime DateTime(DateTime minValue, DateTime maxValue, DateTimeKind kind = DateTimeKind.Unspecified);

        /// <summary>
        /// Generate a random DateTime with a specified earliest DateTime value. The latest date will be the maximum value possible for a SQL Server datetime of December 31, 9999.
        /// </summary>
        /// <param name="minValue">The earliest possible value to generate.</param>
        /// <param name="kind">The DateTimeKind to generate the DateTime as.</param>
        DateTime DateTimeFrom(DateTime minValue, DateTimeKind kind = DateTimeKind.Unspecified);

        /// <summary>
        /// Generate a random DateTime with a specified latest DateTime value. The earliest date will be the minimum value possible for a SQL Server datetime of January 1, 1753.
        /// </summary>
        /// <param name="maxValue">The latest possible value to generate.</param>
        /// <param name="kind">The DateTimeKind to generate the DateTime as.</param>
        DateTime DateTimeThrough(DateTime maxValue, DateTimeKind kind = DateTimeKind.Unspecified);

        /// <summary>
        /// Generate a random bool value.
        /// </summary>
        bool Boolean();

        /// <summary>
        /// Generate a random byte.
        /// </summary>
        byte Byte();

        /// <summary>
        /// Generate a random sbyte.
        /// </summary>
        sbyte SByte();

        /// <summary>
        /// Generate a random char.
        /// </summary>
        char Char();

        /// <summary>
        /// Generate a random Guid.
        /// </summary>
        Guid Guid();

        T Enumeration<T>() where T : struct;
        Enum Enumeration(Type type);

        /// <summary>
        /// Generate a random first name from a pre-defined list of names.
        /// </summary>
        string FirstName();

        /// <summary>
        /// Generate a random last name from a pre-defined list of names.
        /// </summary>
        string LastName();

        string Phrase(int length);

        string String(int length);

        /// <summary>
        /// Generate a random string of specified length. Can choose if the string should be entirely uppercase or lowercase. Can also choose any characters that should not be included.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <param name="upperCase">Whether or not the string should be uppercase. Leave null to have each character randomly determined as uppercase or lowercase.</param>
        /// <param name="characterToExclude">Any characters that should not be included in the randomly generated string.</param>

        string String(int length, bool? upperCase, params char[] characterToExclude);

        /// <summary>
        /// Generate a random character.
        /// </summary>
        /// <param name="upperCase">Whether or not the character should be uppercase. If specified as null, case will be randomly determined.</param>
        char Letter(bool? upperCase);

        /// <summary>
        /// Generate a random string of specified length. Every letter will be uppercase.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        string UpperCaseString(int length);

        /// <summary>
        /// Generate a random string of specified length. Every letter will be lowercase.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        string LowerCaseString(int length);

        /// <summary>
        /// Generate a random e-mail address on a .com domain.
        /// </summary>
        string Email();

        /// <summary>
        /// Generate a random website URL preceded with www.
        /// </summary>
        string WwwUrl();

        /// <summary>
        /// Generate a random website URL with a subdomain, but not beginning with www.
        /// </summary>
        string Url();

        /// <summary>
        /// Generate a random IPv4 Address.
        /// </summary>
        string IpAddress();

        /// <summary>
        /// Generate a random IPv6 Address.
        /// </summary>
        string IpAddressV6();

        /// <summary>
        /// Generate a random MAC address.
        /// </summary>
        /// <param name="separator">Optional to override the default separator from - used in IEEE 802</param>
        string MacAddress(string separator = "-");
    }
}