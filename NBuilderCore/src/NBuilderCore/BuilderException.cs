using System;

namespace NBuilderCore
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