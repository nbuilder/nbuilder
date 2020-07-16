using System;
using System.Collections.Generic;
using System.Text;

namespace FizzWare.NBuilder.Generators
{
    public static class Email
    {
        public static string Random()
        {
            return string.Format("{0}@{1}.com", GetRandom.String(8), GetRandom.String(7));
        }
    }
}
