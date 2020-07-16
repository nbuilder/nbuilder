using System;
using System.Collections.Generic;
using System.Text;

namespace FizzWare.NBuilder.Generators
{
    public static class Url
    {

        /// <summary>
        /// Generate a random website URL preceded with www.
        /// </summary>
        public static string RandomWithWWW()
        {
            return string.Format("www.{0}.com", GetRandom.String(10, false));
        }

        /// <summary>
        /// Generate a random website URL with a subdomain, but not beginning with www.
        /// </summary>
        public static string Random()
        {
            return string.Format("{0}.{1}.com", GetRandom.String(5, false), GetRandom.String(10, false));
        }
    }
}
