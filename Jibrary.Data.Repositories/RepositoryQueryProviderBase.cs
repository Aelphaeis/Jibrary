using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
namespace Jibrary.Data.Repositories
{
    public abstract class RepositoryQueryProviderBase : IQueryProvider 
    {

        public virtual IQueryable CreateQuery<T>()
        {
            return new RepositoryQuery<T>(this);
        }
        public virtual IQueryable<T> CreateQuery<T>(Expression expression)
        {
            return new RepositoryQuery<T>(this, expression);
        }

        public virtual IQueryable CreateQuery(Expression expression) 
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            try {
                return (IQueryable)Activator.CreateInstance(typeof(RepositoryQuery<>).MakeGenericType(elementType), new object[] { this, expression });
            }
            catch (TargetInvocationException tie) {
                throw tie.InnerException;
            }
        }

        public virtual T Execute<T>(Expression expression)
        {
            var LambdaExp = Expression.Lambda<Func<T>>(expression);
            var compiledExp = LambdaExp.Compile();
            return compiledExp.Invoke();
        }

        public virtual object Execute(Expression expression)
        {
            var elementType = TypeSystem.GetElementType(expression.Type);
            var curr = MethodInfo.GetCurrentMethod();
            var exe = GetType().GetMethods()
                .First(p => p.Name == curr.Name && p.IsGenericMethod)
                .MakeGenericMethod(expression.Type);
            return exe.Invoke(this, new Object[] { expression });
        }
    }
}
