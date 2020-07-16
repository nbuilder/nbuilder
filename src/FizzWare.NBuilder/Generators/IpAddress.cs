using System;
using System.Collections.Generic;
using System.Text;

namespace FizzWare.NBuilder.Generators
{
    public static class IpAddress
    {
        /// <summary>
        /// Generate a random IPv4 Address.
        /// </summary>
        public static string Random()
        {
            return $"{GetRandom.PositiveInt(255)}.{GetRandom.PositiveInt(255)}.{GetRandom.PositiveInt(255)}.{GetRandom.PositiveInt(255)}";
        }

        /// <summary>
        /// Generate a random IPv6 Address.
        /// </summary>
        public static string RandomV6()
        {
            return $"{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}:{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}:{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}:{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}:{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}:{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}:{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}:{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}";
        }
    }
}
