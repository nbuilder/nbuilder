using System;

namespace FizzWare.NBuilder
{
    public abstract class PickerConstraint
    {
        protected Random random = new Random((int)DateTime.Now.Ticks);

        // This allows the code to read better
        public PickerConstraint Elements
        {
            get
            {
                return this;
            }
        }

        public abstract int GetStart(int max);

        public abstract int GetEnd(int max);
    }
}