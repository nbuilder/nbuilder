using System;
using System.Collections;

namespace NBuilderCore.Implementation
{
    public class MultiFunction
    {
        private readonly MulticastDelegate del;
        private readonly object list;

        public MultiFunction(MulticastDelegate del, object list)
        {
            this.del = del;
            this.list = list;
        }

        public void Call<T>(T obj)
        {
            IEnumerable enumerable = list as IEnumerable;

            foreach (var item in enumerable)
            {
                del.DynamicInvoke(obj, item);
            }
        }
    }
}