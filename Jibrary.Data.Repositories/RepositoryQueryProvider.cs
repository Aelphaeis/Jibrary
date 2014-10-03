using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
namespace Jibrary.Data.Repositories
{
    public class RepositoryQueryProvider : QueryProviderBase
    {
        public override T Execute<T>(Expression expression)
        {
            //This will get us an int
            //var elementType = TypeSystem.GetElementType(expression.Type);
            var LambdaExp = Expression.Lambda<Func<T>>(expression);
            var compiledExp = LambdaExp.Compile();
            return compiledExp.Invoke();
        }

        public override object Execute(Expression expression)
        {
            //unware as to whether or not this function works.
            var elementType = TypeSystem.GetElementType(expression.Type);
            var curr =  MethodInfo.GetCurrentMethod();
            var exe = GetType().GetMethods()
                .First(p => p.Name == curr.Name && p.IsGenericMethod)
                .MakeGenericMethod(expression.Type);
            return exe.Invoke(this, new Object[]{ expression} );
        }
    }
}
