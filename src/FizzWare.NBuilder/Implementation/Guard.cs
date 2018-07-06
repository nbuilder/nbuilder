using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzWare.NBuilder.Implementation
{
    public static class Guard
    {
        public static void Against<T>(bool condition, T exception) where T : Exception
        {
            if (condition)
            {
                throw exception;
            }
        }

        public static void Against(bool condition, string errorMessage)
        {
            if (condition)
            {
                throw new ArgumentException(errorMessage);
            }
        }
    }
}