using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePersistence
{
    public class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ECommerceDomain.Contarcts.ISpecification<TEntity, TKey> specification) where TEntity : ECommerceDomain.Entities.BaseEntity<TKey>
        {
            var query = inputQuery;
            if (specification is not null)
            {
                if (specification.IncludeExpression is not null && specification.IncludeExpression.Any())
                {
                    // Apply includes from specification
                    //foreach (var includeExpression in specification.IncludeExpression)
                    //{
                    //    query = query.Include(includeExpression);
                    //}
                    query = specification.IncludeExpression.Aggregate
                    (query, (current, include) => current.Include(include));
                }
            }
            return query;
        }
    }
}
