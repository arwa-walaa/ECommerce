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
                //where criteria
                if (specification.Criteria is not null)
                {
                    query = query.Where(specification.Criteria);
                }
              

                //include
                if (specification.IncludeExpression is not null && specification.IncludeExpression.Any())
                {
                  
                    query = specification.IncludeExpression.Aggregate
                    (query, (current, include) => current.Include(include));
                }
                //order by
                if (specification.OrderBy is not null)
                {
                    query = query.OrderBy(specification.OrderBy);
                }
                if (specification.OrderByDescending is not null)
                {
                    query = query.OrderByDescending(specification.OrderByDescending);
                }
            }
            return query;
        }

    }
}
