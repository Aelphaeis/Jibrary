using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
namespace Jibrary.Data.Repositories
{
    public abstract class QueryProviderBase : IQueryProvider 
    {
        protected QueryProviderBase() { }

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
            return (T)Execute(expression);
        }

        public abstract object Execute(Expression expression);
    }
}
