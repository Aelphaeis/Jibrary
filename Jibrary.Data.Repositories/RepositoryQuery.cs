using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Linq.Expressions;
namespace Jibrary.Data.Repositories
{ 
    public class RepositoryQuery<T> :  IOrderedQueryable<T>, IQueryable<T>, IEnumerable<T>, IOrderedQueryable, IQueryable, IEnumerable
    {
        public virtual Expression Expression { get; private set; }
        public virtual IQueryProvider Provider { get; private set; }
        Type IQueryable.ElementType{ get { return typeof(T); } }

        public RepositoryQuery(IQueryProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            Provider = provider;
            Expression = Expression.Constant(this);
        }

        public RepositoryQuery(IQueryProvider provider, Expression expression) 
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

          
            if (expression == null)
                throw new ArgumentNullException("expression");

            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
                throw new ArgumentOutOfRangeException("expression");
            Provider = provider;
            Expression = expression;
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)Provider.Execute(Expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Provider.Execute(Expression)).GetEnumerator();
        }
    }
}
