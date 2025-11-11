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

        //method to add include to property

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpression.Add(includeExpression);
        }
    }
}
