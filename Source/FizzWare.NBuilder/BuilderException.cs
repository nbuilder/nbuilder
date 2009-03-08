using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzWare.NBuilder
{
    public class BuilderException : Exception
    {
        public BuilderException(string message)
            : base (message)
        {
        }
    }
}