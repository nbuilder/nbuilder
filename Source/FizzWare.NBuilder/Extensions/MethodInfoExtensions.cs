using System;
using System.Diagnostics;
using System.Reflection;

namespace FizzWare.NBuilder.Extensions
{
    public static class MethodInfoExtensions
    {
        public static MethodInfo GetInfo(this Delegate @delegate)
        {
#if NETCORE
            return @delegate.GetMethodInfo();
#else
            return @delegate.Method;
#endif
        }
    }
}
