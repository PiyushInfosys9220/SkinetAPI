using Core.Interfaces;
using System.Linq.Expressions;


namespace Core.Specification
{
    public class BaseSpecfication<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
    {

        protected BaseSpecfication() : this(null) { }
        public Expression<Func<T, bool>>? Criteria { get; } = criteria;

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }
    }
}
