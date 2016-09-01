using System;

namespace NBuilderCore
{
    public class TypeCreationException : BuilderException
    {
        public TypeCreationException(string message)
            : base (message)
        {
        }

        public TypeCreationException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}