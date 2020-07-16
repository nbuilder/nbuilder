using System;
using System.Collections.Generic;
using System.Text;

namespace FizzWare.NBuilder.Generators
{
    /// <summary>
    /// This class can randomly generate data specifically for the United States.
    /// </summary>
    public static class Usa
    {
        /// <summary>
        /// Generate a random phone number in the format ###-###-####.
        /// </summary>
        public static string PhoneNumber()
        {
            return string.Format("{0:000}-{1:000}-{2:0000}",
                GetRandom.Int(200, 999), GetRandom.Int(200, 999), GetRandom.Int(0, 9999));
        }

        /// <summary>
        /// Generate a random SSN in the format of ###-##-####.
        /// </summary>
        public static string SocialSecurityNumber()
        {
            return string.Format("{0}-{1}-{2}", GetRandom.NumericString(3), GetRandom.NumericString(2), GetRandom.NumericString(4));
        }
    }
}
