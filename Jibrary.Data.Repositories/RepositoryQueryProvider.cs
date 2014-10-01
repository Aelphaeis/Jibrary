using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Jibrary.Data.Repositories
{
    public class RepositoryQueryProvider : QueryProviderBase
    {
        public override object Execute(Expression expression)
        {
            if (expression.CanReduce)
                expression.Reduce();

            var LambdaExpr = Expression.Lambda<Func<Object>>(expression);
            Func<Object> compiledExpr = LambdaExpr.Compile();
            return compiledExpr();
        }
    }
}
