using ECommerceDomain.Contarcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService.Specification
{
    public class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : ECommerceDomain.Entities.BaseEntity<TKey>
    {
        public BaseSpecification(Expression<Func<TEntity, bool>> criteriaExp)
        {
            Criteria = criteriaExp;
        }
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];

        public Expression<Func<TEntity, bool>> Criteria { get; }

        public Expression<Func<TEntity, object>> OrderBy { get;private  set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get;  private set; }

        //method to add include to property

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpression.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
    }
}
