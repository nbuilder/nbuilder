using System;

namespace NBuilderCore.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsDefaultValue(this object o)
        {
            if (o is ValueType)
            {
                var @default = Activator.CreateInstance(o.GetType());
                return o.Equals(@default);
            }
            return o == null;
        }
    }
}
