using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Jibrary.Data.Repositories
{
    public class RepositoryExpressionVisitor : ExpressionVisitor
    {
        protected override Expression VisitConstant(ConstantExpression node)
        {
            //if(node.Type == typeof(RepositoryQuery<>))
                //todo work here

            return base.VisitConstant(node);
        }
    }
}
