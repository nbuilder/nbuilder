using System;

namespace NBuilderCore
{
    public class PersistenceMethodNotFoundException : Exception
    {
        public PersistenceMethodNotFoundException(string message)
            : base (message)
        {
        }
    }
}