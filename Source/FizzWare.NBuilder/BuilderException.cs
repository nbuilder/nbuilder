using System;

namespace FizzWare.NBuilder
{
    public class BuilderException : Exception
    {
        public BuilderException(string message)
            : base (message)
        {
        }

        public BuilderException(string message, Exception innerException)
            : base (message, innerException)
        {
        }
    }
}