using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace FizzWare.NBuilder
{
    public static class ReflectionHelper
    {
#if DNCORE

        public static TypeInfo GetTypeInfo(Type type)
        {
            return type.GetTypeInfo();
        }
        public static MethodInfo GetMethodInfo(Delegate del)
        {
            return del.GetMethodInfo();
        }
#else
        public static Type GetTypeInfo(Type type)
        {
            return type;
        }
        public static  MulticastDelegate GetMethodInfo(MulticastDelegate del)
        {
            return del;
        }

#endif
    }
}
