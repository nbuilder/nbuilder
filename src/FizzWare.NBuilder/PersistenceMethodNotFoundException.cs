using System;

namespace FizzWare.NBuilder
{
    public class PersistenceMethodNotFoundException : Exception
    {
        public PersistenceMethodNotFoundException(string message)
            : base (message)
        {
        }
    }
}