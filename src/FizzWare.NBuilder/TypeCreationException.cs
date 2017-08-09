using System;

namespace FizzWare.NBuilder
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