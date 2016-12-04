using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzWare.NBuilder.Extensions
{
    internal static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string self)
        {
#if NET35
            return string.IsNullOrEmpty(self?.Trim());
#else
            return string.IsNullOrWhiteSpace(self);
#endif

        }
    }
}
