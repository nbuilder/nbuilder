using System.Collections;
using System.Linq.Expressions;

namespace FizzWare.NBuilder.Implementation
{
    public class MultiFunction
    {
        private readonly Expression expression;
        private readonly object list;

        public MultiFunction(Expression expression, object list)
        {
            this.expression = expression;
            this.list = list;
        }

        public void Call<T>(T obj)
        {
            IEnumerable enumerable = list as IEnumerable;

            foreach (var item in enumerable)
            {
                ((LambdaExpression) expression).Compile().DynamicInvoke(obj, item);
            }
        }
    }
}