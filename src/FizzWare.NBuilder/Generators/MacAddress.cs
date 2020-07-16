using System;
using System.Collections.Generic;
using System.Text;

namespace FizzWare.NBuilder.Generators
{
    public static class MacAddress
    {
        public static string Random(string separator = "-")
        {
            return $"{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{separator}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{separator}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{separator}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{separator}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}{separator}{GetRandom.PositiveShort(16):X}{GetRandom.PositiveShort(16):X}";
        }
    }
}
