using System;

namespace FizzWare.NBuilder
{
    public abstract class Constraint : IConstraint
    {
        // This allows the code to read better
        public Constraint Elements => this;

        public abstract int GetEnd();
    }
}